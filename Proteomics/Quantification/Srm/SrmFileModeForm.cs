using DigitalRune.Windows.Docking;
using RCPA.Utils;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using ZedGraph;

namespace RCPA.Proteomics.Quantification.Srm
{
  public partial class SrmFileModeForm : DockableForm
  {
    public SrmCompoundModeForm CompoundForm { get; set; }

    private List<SrmPairedResult> pairedResults = null;

    private SrmPairedResult CurrentResult
    {
      get
      {
        if (tvMRM.SelectedNode == null)
        {
          return null;
        }
        else
        {
          return GetResult(tvMRM.SelectedNode);
        }
      }
    }

    public event UpdateMRMPairedProductIonEventHandler ProductIonUpdate;
    public event UpdateMRMPairedPeptideItemEventHandler PeptideItemUpdate;

    public SrmViewOption ViewOption { get; set; }

    public SrmFileModeForm()
    {
      InitializeComponent();

      PeptideItemUpdate += UpdatePeptide;
      PeptideItemUpdate += new ZedGraphMRMPeptideItemScans(zgcPeptide, this.CreateGraphics()).Update;
      ProductIonUpdate += new ZedGraphMRMProductIonScansAndRegression(zgcTransaction, this.CreateGraphics()).Update;

      ViewOption = new SrmViewOption();
    }

    public void UpdatePeptide(object sender, UpdateMRMPairedPeptideItemEventArgs e)
    {
      var s = e.Item;
      gvProductPair.DataSource = e.Item.ProductIonPairs;
    }

    protected void OnUpdateProductIon(UpdateMRMPairedProductIonEventArgs e)
    {
      if (ProductIonUpdate != null)
      {
        ProductIonUpdate(this, e);
      }
    }

    protected void OnUpdatePeptideItem(UpdateMRMPairedPeptideItemEventArgs e)
    {
      if (PeptideItemUpdate != null)
      {
        PeptideItemUpdate(this, e);
      }
    }

    public void PairedResultChanged(object sender, PairedResultChangedEventArgs e)
    {
      this.pairedResults = e.PairedResult;

      InitializeFileList();
    }

    public void ViewChanged()
    {
      InitializeFileList();
    }

    private bool displaying = false;

    private void InitializeFileList()
    {
      try
      {
        tvMRM.BeginUpdate();
        displaying = true;
        try
        {
          tvMRM.Nodes.Clear();

          pairedResults.Sort((m1, m2) => m1.FileName.CompareTo(m2.FileName));

          foreach (var mrmPairs in pairedResults)
          {
            var fileNode = tvMRM.Nodes.Add(mrmPairs.PureFileName);
            fileNode.Tag = mrmPairs;

            mrmPairs.Sort((m1, m2) => m1.ToString().CompareTo(m2.ToString()));
            foreach (var mrm in mrmPairs)
            {
              if (!ViewOption.ViewDecoy && mrm.IsDecoy)
              {
                continue;
              }

              TreeNode node = fileNode.Nodes.Add("");

              AssignItem(mrm, node);
            }
          }
        }
        finally
        {
          displaying = false;
          tvMRM.EndUpdate();
        }

        if (tvMRM.Nodes.Count > 1)
        {
          tvMRM.SelectedNode = FindFirstCheckedNode();
        }
      }
      catch (Exception ex)
      {
        Console.WriteLine(ex.StackTrace);
        MessageBox.Show(ex.Message);
      }
    }

    private TreeNode FindFirstCheckedNode()
    {
      foreach (TreeNode m in tvMRM.Nodes)
      {
        foreach (TreeNode n in m.Nodes)
        {
          if (n.Checked)
          {
            return n;
          }
        }
      }

      return tvMRM.Nodes[0];
    }

    private SrmPairedResult GetResult(TreeNode node)
    {
      if (node == null)
      {
        return null;
      }

      while (node.Parent != null)
      {
        node = node.Parent;
      }

      return node.Tag as SrmPairedResult;
    }

    private static void AssignItem(SrmPairedPeptideItem mrm, TreeNode node)
    {
      node.Nodes.Clear();
      node.Text = mrm.ToString();
      node.Tag = mrm;
      node.Checked = mrm.Enabled;

      mrm.ProductIonPairs.ForEach(m =>
      {
        string ions = MyConvert.Format("{0:0.0000} : {1:0.0000}", m.Light.ProductIon, mrm.IsPaired ? m.Heavy.ProductIon : 0);
        TreeNode subNode = node.Nodes.Add(ions);
        subNode.Tag = m;
        subNode.Checked = m.Enabled;
      });
    }

    private void mnuTree_Opening(object sender, System.ComponentModel.CancelEventArgs e)
    {
      if (tvMRM.SelectedNodes.Count != 2)
      {
        mnuCrosslink.Enabled = false;
      }
      else
      {
        List<TreeNode> selected = new List<TreeNode>();
        foreach (TreeNode node in tvMRM.SelectedNodes)
        {
          if (node.Level == 1 && !((SrmPairedPeptideItem)node.Tag).IsPaired)
          {
            selected.Add(node);
          }
        }

        var mr = (from node in selected
                  select GetResult(node)).Distinct().ToList();

        mnuCrosslink.Enabled = (selected.Count == 2) && (mr.Count == 1);
      }

      mnuSplit.Enabled = tvMRM.SelectedNode.Level == 1;
      mnuSave.Enabled = this.pairedResults.Any(m => m.Modified);
    }

    private void mnuCrosslink_Click(object sender, EventArgs e)
    {
      TreeNode node1 = tvMRM.SelectedNodes[0] as TreeNode;
      TreeNode node2 = tvMRM.SelectedNodes[1] as TreeNode;

      var mr = GetResult(node1);

      SrmPairedPeptideItem item1 = node1.Tag as SrmPairedPeptideItem;
      SrmPairedPeptideItem item2 = node2.Tag as SrmPairedPeptideItem;

      SrmPairedPeptideItem light, heavy;
      if (item1.LightPrecursorMZ < item2.LightPrecursorMZ)
      {
        light = item1;
        heavy = item2;
      }
      else
      {
        light = item2;
        heavy = item1;
      }

      if (light.AddErrorPairedPeptideItem(heavy, mr.Options.AllowedGaps, mr.Options.MzTolerance, mr.Options.RetentionTimeToleranceInSecond))
      {
        mr.Remove(heavy);
        tvMRM.Nodes.Remove(node2);
        AssignItem(light, node1);
        SetCurrentResultModified();
      }
    }

    private void mnuSetScanEnabledOnly_Click(object sender, EventArgs e)
    {
      GraphPane graphScans = mnuSetScanEnabledOnly.Tag as GraphPane;
      double min = graphScans.XAxis.Scale.Min;
      double max = graphScans.XAxis.Scale.Max;

      SrmPairedProductIon ion = graphScans.Tag as SrmPairedProductIon;
      SrmPairedPeptideItem item = FindItemByProductIon(ion);

      if (null == item)
      {
        return;
      }

      item.SetEnabledRetentionTimeRange(min, max);

      CurrentResult.Update(item);

      gvProductPair.Refresh();

      DoUpdateProductIon();

      tvMRM_AfterSelect(null, null);

      SetCurrentResultModified();
    }

    private SrmPairedPeptideItem FindItemByProductIon(SrmPairedProductIon ion)
    {
      return CurrentResult.Find(m => m.ProductIonPairs.Contains(ion));
    }

    private SrmPairedProductIon DoUpdateProductIon()
    {
      var pep = GetPeptide();
      if (pep == null)
      {
        return null;
      }

      CurrencyManager cm = (CurrencyManager)this.BindingContext[pep.ProductIonPairs];
      SrmPairedProductIon item = cm.Current as SrmPairedProductIon;

      pep.ProductIonPairs.ForEach(m => m.IsCurrent = m == item);

      UpdateMRMPairedProductIonEventArgs args = new UpdateMRMPairedProductIonEventArgs(item, new SrmViewOption() { ViewGreenLine = true, ViewType = _currentDisplayType });

      OnUpdateProductIon(args);

      return item;
    }

    private SrmPairedPeptideItem DoUpdatePeptide()
    {
      var result = GetPeptide();

      if (result != null)
      {
        UpdateMRMPairedPeptideItemEventArgs args = new UpdateMRMPairedPeptideItemEventArgs(result, new SrmViewOption() { ViewGreenLine = true, ViewType = _currentDisplayType });
        OnUpdatePeptideItem(args);
      }

      return result;
    }

    private void tvMRM_AfterSelect(object sender, TreeViewEventArgs e)
    {
      if (displaying)
      {
        return;
      }

      DoUpdatePeptide();

      var node = tvMRM.SelectedNode;
      if (null == node || node.Level != 2)
      {
        return;
      }

      //设置当前product ion
      SrmPairedProductIon item = node.Tag as SrmPairedProductIon;
      SrmPairedPeptideItem pep = node.Parent.Tag as SrmPairedPeptideItem;

      int rowIndex = pep.ProductIonPairs.IndexOf(item);
      CurrencyManager cm = (CurrencyManager)this.BindingContext[pep.ProductIonPairs];
      if (cm.Position != rowIndex)
      {
        cm.Position = rowIndex;
      }
    }

    private void btnExport_Click(object sender, EventArgs e)
    {
      //if (mrmFileName == null)
      //{
      //  return;
      //}

      //dlgExport.InitialDirectory = new FileInfo(mrmFileName).Directory.FullName;
      //if (dlgExport.ShowDialog() == DialogResult.OK)
      //{
      //  var enabledOnly = MessageBox.Show(this, "Export enabled entries only?", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1) == DialogResult.Yes;

      //  new MRMPairedResultTextFormat(enabledOnly).WriteToFile(dlgExport.FileName, mrmPairs);
      //}
    }

    private SrmPairedPeptideItem GetPeptide()
    {
      if (tvMRM.Nodes.Count == 0)
      {
        return null;
      }

      var node = tvMRM.SelectedNode;
      if (node == null)
      {
        return null;
      }

      if (node.Level == 0)
      {
        if (node.Nodes.Count > 0)
          return node.Nodes[0].Tag as SrmPairedPeptideItem;
        else
          return null;
      }

      if (node.Level == 2)
      {
        return node.Parent.Tag as SrmPairedPeptideItem;
      }

      return node.Tag as SrmPairedPeptideItem;
    }

    private void tvMRM_AfterCheck(object sender, TreeViewEventArgs e)
    {
      if (displaying)
      {
        return;
      }

      var node = e.Node;
      if (node.Level == 0)
      {
        return;
      }

      if (node.Level == 1)
      {
        SrmPairedPeptideItem pepItem = node.Tag as SrmPairedPeptideItem;
        pepItem.Enabled = node.Checked;
        return;
      }

      if (node.Level == 2)
      {
        SrmPairedProductIon item = node.Tag as SrmPairedProductIon;
        item.Enabled = node.Checked;

        SrmPairedPeptideItem pepItem = node.Parent.Tag as SrmPairedPeptideItem;
        pepItem.CalculatePeptideRatio();
      }

      tvMRM_AfterSelect(null, null);

      SetCurrentResultModified();
    }

    private void SetCurrentResultModified()
    {
      if (tvMRM.SelectedNode != null)
      {
        var node = tvMRM.SelectedNode;
        var mr = GetResult(node);
        mr.Modified = true;
        mnuSave.Enabled = true;
      }
    }

    private void gvProductPair_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
    {
      if (CurrentResult != null)
      {
        var name = gvProductPair.Columns[e.ColumnIndex].Name;
        if (name.Equals("headerCorrel"))
        {
          double correl = (double)e.Value;
          if (correl < CurrentResult.Options.ValidationMinRegressionCorrelation)
          {
            e.CellStyle.BackColor = Color.Red;
            e.CellStyle.SelectionBackColor = Color.DarkRed;
          }
        }

        if (name.Equals("headerLSN") || name.Equals("headerHSN"))
        {
          double sn = (double)e.Value;
          if (sn < CurrentResult.Options.ValidationMinSignalToNoise)
          {
            e.CellStyle.BackColor = Color.Red;
            e.CellStyle.SelectionBackColor = Color.DarkRed;
          }
        }
      }
    }

    private bool changing = false;

    private void zgcScan_ZoomEvent(ZedGraphControl sender, ZoomState oldState, ZoomState newState)
    {
      if (changing)
      {
        return;
      }

      changing = true;
      try
      {
        if (tvMRM.SelectedNode.Level == 1)
        {
          return;
        }

        foreach (GraphPane pane in zgcPeptide.MasterPane.PaneList)
        {
          newState.ApplyState(pane);
        }
      }
      finally
      {
        changing = false;
      }
    }

    private void gvProductPair_SelectionChanged(object sender, EventArgs e)
    {
      var item = DoUpdateProductIon();

      if (item != null)
      {
        SetTreeSelected(item);
        DoUpdatePeptide();
      }
    }

    private void SetTreeSelected(SrmPairedProductIon item)
    {
      if (tvMRM.SelectedNode.Level < 2)
      {
        return;
      }

      if (tvMRM.SelectedNode.Tag == item)
      {
        return;
      }

      TreeNode root = tvMRM.SelectedNode.Parent;
      for (int i = 0; i < root.Nodes.Count; i++)
      {
        if (root.Nodes[i].Tag == item)
        {
          tvMRM.SelectedNode = root.Nodes[i];
          return;
        }
      }
    }

    private DisplayType _currentDisplayType = DisplayType.PerfectSize;

    public void DisplayTypeChanged(object sender, DisplayTypeChangedEventArgs e)
    {
      _currentDisplayType = e.NewType;

      DoUpdate();
    }

    private void DoUpdate()
    {
      DoUpdatePeptide();
      DoUpdateProductIon();
    }

    private void pmiSplit_Click(object sender, EventArgs e)
    {
      var pep = GetPeptide();
      if (pep != null)
      {
        SrmSplitForm form = new SrmSplitForm();
        form.LeftPeptide = pep;
        if (form.ShowDialog() == System.Windows.Forms.DialogResult.OK)
        {
          SetCurrentResultModified();
        }
      }
    }

    private void zgcPeptide_ContextMenuBuilder(ZedGraphControl sender, ContextMenuStrip menuStrip, Point mousePt, ZedGraphControl.ContextMenuObjectState objState)
    {
      menuStrip.Items.Add("-");
      menuStrip.Items.Add(mnuSetScanEnabledOnly);

      for (int i = 0; i < zgcPeptide.MasterPane.PaneList.Count; i++)
      {
        if (zgcPeptide.MasterPane.PaneList[i].Rect.Contains(mousePt))
        {
          mnuSetScanEnabledOnly.Tag = zgcPeptide.MasterPane.PaneList[i];
        }
      }
    }

    private void zgcTransaction_ContextMenuBuilder(ZedGraphControl sender, ContextMenuStrip menuStrip, Point mousePt, ZedGraphControl.ContextMenuObjectState objState)
    {
      menuStrip.Items.Add("-");
      menuStrip.Items.Add(mnuSetScanEnabledOnly);

      mnuSetScanEnabledOnly.Tag = zgcTransaction.GraphPane;
    }

    private void mnuSave_Click(object sender, EventArgs e)
    {
      foreach (var mr in this.pairedResults)
      {
        if (mr.Modified)
        {
          new SrmPairedResultXmlFormat().WriteToFile(mr.FileName, mr);
          mr.Modified = false;
        }
      }
      mnuSave.Enabled = false;
    }

    private void gvProductPair_CellBeginEdit(object sender, DataGridViewCellCancelEventArgs e)
    {
      e.Cancel = e.ColumnIndex > 0;
    }

    public void ExportStatistic(string fileName)
    {
      using (StreamWriter sw = new StreamWriter(fileName))
      {
        Predicate<SrmPairedPeptideItem> validDecoyAccept = m => m.IsDecoy && m.Enabled;
        Predicate<SrmPairedPeptideItem> invalidTargetAccept = pep => !pep.IsDecoy && !pep.Enabled;
        Predicate<SrmPairedPeptideItem> validTargetAccept = pep => !pep.IsDecoy && pep.Enabled;
        sw.Write("Compound");
        foreach (var pr in pairedResults)
        {
          sw.Write("," + pr.PureFileName);
        }
        sw.WriteLine();

        WriteCount(sw, validDecoyAccept, "Valid decoy");
        WriteCount(sw, invalidTargetAccept, "Invalid target");
        WriteCount(sw, validTargetAccept, "Valid target");
      }
    }

    private void WriteCount(StreamWriter sw, Predicate<SrmPairedPeptideItem> validDecoyAccept, string name)
    {
      sw.Write(name);
      foreach (var pr in pairedResults)
      {
        var count = pr.Count(m => validDecoyAccept(m));
        sw.Write(",{0}", count);
      }
      sw.WriteLine();
    }

    public void Export(string targetDir)
    {
      if (pairedResults == null)
      {
        return;
      }

      foreach (var pr in pairedResults)
      {
        var mainFile = new FileInfo(targetDir + "\\" + pr.PureFileName + ".html").FullName;
        var imageDir = FileUtils.ChangeExtension(mainFile, ".images");

        try
        {
          var imageDirName = new DirectoryInfo(imageDir).Name;
          if (!Directory.Exists(imageDir))
          {
            Directory.CreateDirectory(imageDir);
          }

          TreeNode node = null;
          foreach (TreeNode anode in tvMRM.Nodes)
          {
            if (anode.Text.Equals(pr.PureFileName))
            {
              node = anode;
              break;
            }
          }

          using (StreamWriter sw = new StreamWriter(mainFile))
          {
            sw.WriteLine("<html>");
            sw.WriteLine("<head>");
            sw.WriteLine("<title>SRMBuilder Result</title>");
            sw.WriteLine("</head>");
            sw.WriteLine("<body>");

            sw.WriteLine("Options<br>");
            sw.WriteLine("<pre>");
            sw.WriteLine(pr.Options.ToXml().ToString().Replace("<", "&lt;").Replace(">", "&gt;"));
            sw.WriteLine("</pre>");
            sw.WriteLine("<hr>");

            Predicate<SrmPairedPeptideItem> accept = m => m.IsDecoy && m.Enabled;
            var validDecoy = GetCount(node, accept);
            if (validDecoy > 0)
            {
              sw.WriteLine("{0} valid decoy entries (blue highlighted)<br>", validDecoy);
              Output(sw, node, accept, imageDir, imageDirName, Color.Blue);
            }

            accept = pep => !pep.IsDecoy && !pep.Enabled;
            var invalidTarget = GetCount(node, accept);
            if (invalidTarget > 0)
            {
              sw.WriteLine("{0} invalid target entries (red highlighted)<br>", invalidTarget);
              Output(sw, node, accept, imageDir, imageDirName, Color.Red);
            }

            accept = pep => !pep.IsDecoy && pep.Enabled;
            var validTarget = GetCount(node, accept);
            if (validTarget > 0)
            {
              sw.WriteLine("{0} valid target entries<br>", validTarget);
              Output(sw, node, accept, imageDir, imageDirName, Color.Black);
            }

            sw.WriteLine("</body>");
            sw.WriteLine("</html>");
          }
        }
        catch (Exception ex)
        {
          Console.WriteLine(ex.StackTrace);
          MessageBox.Show(MyConvert.Format("Error when saving {0} : {1}", mainFile, ex.Message));
        }
      }
    }

    private void Output(StreamWriter sw, TreeNode node, Predicate<SrmPairedPeptideItem> accept, string imageDir, string imageDirName, Color color)
    {
      foreach (TreeNode subNode in node.Nodes)
      {
        var pep = subNode.Tag as SrmPairedPeptideItem;
        if (accept(pep))
        {
          tvMRM.SelectedNode = subNode;

          WritePeptide(sw, pep, color);

          WriteImage(imageDir, imageDirName, sw, pep);
        }
      }
    }

    private int GetCount(TreeNode node, Predicate<SrmPairedPeptideItem> pred)
    {
      var result = 0;
      foreach (TreeNode subNode in node.Nodes)
      {
        var pep = subNode.Tag as SrmPairedPeptideItem;
        if (pred(pep))
        {
          result++;
        }
      }
      return result;
    }

    private static void WritePeptide(StreamWriter sw, SrmPairedPeptideItem pep, Color c)
    {
      String strHtmlColor = System.Drawing.ColorTranslator.ToHtml(c);
      sw.WriteLine("<font color=\"{0}\">", strHtmlColor);
      sw.WriteLine("{0} - {1} : {2:0.0000} - {3:0.0000}<br>", pep.ProductIonPairs[0].ObjectName, pep.ProductIonPairs[0].PrecursorFormula, pep.LightPrecursorMZ, pep.HeavyPrecursorMZ);
      sw.WriteLine("</font>");

      sw.WriteLine("<table border=\"1\">");
      sw.WriteLine("<tr>");
      sw.WriteLine("<th scope=\"col\">Enabled</th>");
      sw.WriteLine("<th scope=\"col\">Light</th>");
      sw.WriteLine("<th scope=\"col\">Heavy</th>");
      sw.WriteLine("<th scope=\"col\">Ratio</th>");
      sw.WriteLine("<th scope=\"col\">Correl</th>");
      sw.WriteLine("<th scope=\"col\">ValidScan</th>");
      sw.WriteLine("</tr>");

      foreach (var ion in pep.ProductIonPairs)
      {
        sw.WriteLine("<tr>");
        if (ion.Enabled)
        {
          sw.WriteLine("<td><input type=\"checkbox\" checked=\"checked\"></td>");
        }
        else
        {
          sw.WriteLine("<td><input type=\"checkbox\"></td>");
        }
        sw.WriteLine("<td>{0:0.0000}</td>", ion.LightProductIon);
        sw.WriteLine("<td>{0:0.0000}</td>", ion.HeavyProductIon);
        sw.WriteLine("<td>{0:0.0000}</td>", ion.Ratio);
        sw.WriteLine("<td>{0:0.0000}</td>", ion.RegressionCorrelation);
        sw.WriteLine("<td>{0}</td>", ion.EnabledScanCount);
        sw.WriteLine("</tr>");
      }
      sw.WriteLine("</table>");
    }

    private void WriteImage(string imageDir, string imageDirName, StreamWriter sw, SrmPairedPeptideItem pep)
    {
      var imageFile = MyConvert.Format("{0}\\{1}.png", imageDir, pep.PrecursorFormula);
      zgcPeptide.GetImage().Save(imageFile, ImageFormat.Png);
      sw.WriteLine("<img src=\"{0}/{1}.png\" width=\"800\" /><br>", imageDirName, pep.PrecursorFormula);
      sw.WriteLine("<hr>");
    }

    public void Export(string filename, bool removeDecoy, bool validOnly)
    {
      //using (StreamWriter sw = new StreamWriter(filename))
      //{
      //  sw.Write("ObjectName,CompoundFormula,LightMz,HeavyMz,Enabled");
      //  var filenames = (from pr in pairedResults
      //                   orderby pr.PureFileName
      //                   select pr.PureFileName).Distinct().ToList();
      //  filenames.ForEach(m => sw.Write(",{0}_Ratio,{0}_SD,{0}_ValidTransCount", m));
      //  sw.WriteLine();

      //  for (int i = 0; i < lvPeptides.Items.Count; i++)
      //  {
      //    if (validOnly && !lvPeptides.GetItemChecked(i))
      //    {
      //      continue;
      //    }

      //    CompoundItem item = lvPeptides.Items[i] as CompoundItem;
      //    if (removeDecoy && item.IsDecoy)
      //    {
      //      continue;
      //    }

      //    sw.Write("{0},{1},{2:0.0000},{3:0.0000},{4}", item.ObjectName, item.PrecursurFormula, item.LightMz, item.HeavyMz, lvPeptides.GetItemChecked(i));
      //    foreach (var file in filenames)
      //    {
      //      var pep = (from t in item.TransitionItems
      //                 from m in t.FileItems
      //                 where m.PairedResult.PureFileName.Equals(file)
      //                 select m.PairedPeptide).FirstOrDefault();
      //      if (pep == null || pep.Ratio == -1)
      //      {
      //        sw.Write(",#N/A,0,0");
      //      }
      //      else
      //      {
      //        var transCount = pep.ProductIonPairs.Count(m => m.Enabled);
      //        if (double.IsNaN(pep.SD) || double.IsInfinity(pep.SD))
      //        {
      //          sw.Write(",{0:0.0000},0,{1}", pep.Ratio, transCount);
      //        }
      //        else
      //        {
      //          sw.Write(",{0:0.0000},{1:0.0000},{2}", pep.Ratio, pep.SD, transCount);
      //        }
      //      }
      //    }
      //    sw.WriteLine();
      //  }
      //}
    }

    private void viewAtCompoundModeToolStripMenuItem_Click(object sender, EventArgs e)
    {
      if (CompoundForm != null && tvMRM.SelectedNode.Tag != null)
      {
        CompoundForm.Focus(tvMRM.SelectedNode.Tag);
      }
    }
  }
}
