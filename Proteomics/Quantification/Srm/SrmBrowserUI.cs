using RCPA.Gui;
using RCPA.Gui.Command;
using RCPA.Gui.FileArgument;
using RCPA.Utils;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using ZedGraph;

namespace RCPA.Proteomics.Quantification.Srm
{
  public partial class SrmBrowserUI : AbstractFileProcessorUI
  {
    private static readonly string title = "MRM Validator";

    private static readonly string version = "1.1.2";

    private SrmPairedResult mrmPairs = new SrmPairedResult();

    private string mrmFileName;

    public event UpdateMRMPairedProductIonEventHandler ProductIonUpdate;
    public event UpdateMRMPairedPeptideItemEventHandler PeptideItemUpdate;

    public SrmBrowserUI()
    {
      InitializeComponent();

      base.SetFileArgument("MRMFile", new OpenFileArgument("MRM Distiller", ".mrm"));

      PeptideItemUpdate += new ZedGraphMRMPeptideItemScans(zgcPeptide, this.CreateGraphics()).Update;

      ProductIonUpdate += new ZedGraphMRMProductIonScansAndRegression(zgcTransaction, this.CreateGraphics()).Update;
      ProductIonUpdate += (sender, e) =>
      {
        var ion = e.Item;
        var lightEnabled = ion.Light == null ? null : (from scan in ion.Light.Intensities
                                                       where scan.Enabled
                                                       select scan).ToList();
        var heavyEnabled = ion.Heavy == null ? null : (from scan in ion.Heavy.Intensities
                                                       where scan.Enabled
                                                       select scan).ToList();
        lightView.DataSource = lightEnabled;
        heavyView.DataSource = heavyEnabled;
      };

      this.AddButton(btnSaveCross);
      this.AddButton(btnExport);

      this.Text = Constants.GetSQHTitle(title, version);
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

    protected override void DoRealGo()
    {
      mrmFileName = GetOriginFile();

      btnGo.Enabled = false;
      try
      {
        lblProgress.Text = "Reading MRM Scans ...";
        mrmPairs = new SrmPairedResultXmlFormat().ReadFromFile(mrmFileName);
        lblProgress.Text = "Finished.";

        DisplayMRMTree();
      }
      finally
      {
        btnGo.Enabled = true;
      }
    }

    private void DisplayMRMTree()
    {
      try
      {
        tvMRM.BeginUpdate();
        try
        {
          tvMRM.Nodes.Clear();

          foreach (var mrm in mrmPairs)
          {
            TreeNode node = tvMRM.Nodes.Add("");

            AssignItem(mrm, node);
          }

          if (tvMRM.Nodes.Count > 0)
          {
            tvMRM.SelectedNode = tvMRM.Nodes[0];
          }
        }
        finally
        {
          tvMRM.EndUpdate();
        }
      }
      catch (Exception ex)
      {
        Console.WriteLine(ex.StackTrace);
        MessageBox.Show(ex.Message);
      }
    }

    private static void AssignItem(SrmPairedPeptideItem mrm, TreeNode node)
    {
      node.Nodes.Clear();
      string txt = MyConvert.Format("{0:0.0000} : {1:0.0000}", mrm.LightPrecursorMZ, mrm.HeavyPrecursorMZ);
      node.Text = txt;
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

    public class Command : IToolCommand
    {
      #region IToolCommand Members

      public string GetClassification()
      {
        return MenuCommandType.Quantification;
      }

      public string GetCaption()
      {
        return title;
      }

      public string GetVersion()
      {
        return version;
      }

      public void Run()
      {
        new SrmBrowserUI().MyShow();
      }

      #endregion
    }

    private void contextMenuStrip1_Opening(object sender, System.ComponentModel.CancelEventArgs e)
    {
      if (tvMRM.SelectedNodes.Count != 2)
      {
        mnuCrosslink.Visible = false;
      }
      else
      {
        List<TreeNode> selected = new List<TreeNode>();
        foreach (TreeNode node in tvMRM.SelectedNodes)
        {
          if (node.Level == 0 && !((SrmPairedPeptideItem)node.Tag).IsPaired)
          {
            selected.Add(node);
          }
        }

        mnuCrosslink.Visible = selected.Count == 2;
      }
    }

    private void mnuCrosslink_Click(object sender, EventArgs e)
    {
      TreeNode node1 = tvMRM.SelectedNodes[0] as TreeNode;
      TreeNode node2 = tvMRM.SelectedNodes[1] as TreeNode;

      SrmPairedPeptideItem item1 = node1.Tag as SrmPairedPeptideItem;
      SrmPairedPeptideItem item2 = node2.Tag as SrmPairedPeptideItem;

      SrmPairedPeptideItem item;
      if (item1.LightPrecursorMZ < item2.LightPrecursorMZ)
      {
        item = item1;
        item.SetHeavyPeptideItem(item2);
        mrmPairs.Remove(item2);
      }
      else
      {
        item = item2;
        item.SetHeavyPeptideItem(item1);
        mrmPairs.Remove(item1);
      }

      tvMRM.Nodes.Remove(node2);
      AssignItem(item, node1);
    }

    private void btnSaveCross_Click(object sender, EventArgs e)
    {
      if (mrmFileName != null)
      {
        new SrmPairedResultXmlFormat().WriteToFile(mrmFileName, mrmPairs);
      }
    }

    private void zgcScan_ContextMenuBuilder(ZedGraphControl sender, ContextMenuStrip menuStrip, Point mousePt, ZedGraphControl.ContextMenuObjectState objState)
    {
      var node = tvMRM.SelectedNode;
      if (null == node)
      {
        return;
      }

      if (node.Tag is SrmPairedPeptideItem)
      {
        menuStrip.Items.Add(mnuSetScanEnabledOnlyToolStripMenuItem);

        for (int i = 0; i < zgcPeptide.MasterPane.PaneList.Count; i++)
        {
          if (zgcPeptide.MasterPane.PaneList[i].Rect.Contains(mousePt))
          {
            mnuSetScanEnabledOnlyToolStripMenuItem.Tag = zgcPeptide.MasterPane.PaneList[i];
          }
        }
      }
      else if (node.Tag is SrmPairedProductIon)
      {
        if (zgcPeptide.GraphPane.Rect.Contains(mousePt))
        {
          menuStrip.Items.Add(mnuSetScanEnabledOnlyToolStripMenuItem);
          mnuSetScanEnabledOnlyToolStripMenuItem.Tag = zgcPeptide.GraphPane;
        }
      }
    }

    private void mnuSetScanEnabledOnlyToolStripMenuItem_Click(object sender, EventArgs e)
    {
      GraphPane graphScans = mnuSetScanEnabledOnlyToolStripMenuItem.Tag as GraphPane;
      double min = graphScans.XAxis.Scale.Min;
      double max = graphScans.XAxis.Scale.Max;

      SrmPairedProductIon ion = graphScans.Tag as SrmPairedProductIon;
      SrmPairedPeptideItem item = FindItemByProductIon(ion);

      if (null == item)
      {
        return;
      }

      item.SetEnabledRetentionTimeRange(min, max);

      mrmPairs.Update(item);

      UpdateProductPairs();

      DoUpdateProductIon();

      tvMRM_AfterSelect(null, null);
    }

    private void UpdateProductPairs()
    {
      gvProductPair.Refresh();
    }

    private SrmPairedPeptideItem FindItemByProductIon(SrmPairedProductIon ion)
    {
      return mrmPairs.Find(m => m.ProductIonPairs.Contains(ion));
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
      UpdateMRMPairedProductIonEventArgs args = new UpdateMRMPairedProductIonEventArgs(item, new SrmViewOption() { ViewGreenLine = true, ViewType = lastType });

      OnUpdateProductIon(args);

      return item;
    }

    private SrmPairedPeptideItem DoUpdatePeptide()
    {
      var result = GetPeptide();

      UpdateMRMPairedPeptideItemEventArgs args = new UpdateMRMPairedPeptideItemEventArgs(result, new SrmViewOption() { ViewGreenLine = true, ViewType = lastType });

      OnUpdatePeptideItem(args);

      return result;
    }

    private void tvMRM_AfterSelect(object sender, TreeViewEventArgs e)
    {
      var node = tvMRM.SelectedNode;
      if (null == node)
      {
        gvProductPair.DataSource = null;
        return;
      }

      SrmPairedPeptideItem pep;

      if (0 == node.Level)
      {
        pep = DoUpdatePeptide();

        if (gvProductPair.DataSource != pep.ProductIonPairs)
        {
          gvProductPair.DataSource = pep.ProductIonPairs;
        }
      }
      else
      {
        SrmPairedProductIon item = node.Tag as SrmPairedProductIon;
        pep = node.Parent.Tag as SrmPairedPeptideItem;

        if (gvProductPair.DataSource != pep.ProductIonPairs)
        {
          DoUpdatePeptide();

          gvProductPair.DataSource = pep.ProductIonPairs;
        }

        int rowIndex = pep.ProductIonPairs.IndexOf(item);
        CurrencyManager cm = (CurrencyManager)this.BindingContext[pep.ProductIonPairs];
        if (cm.Position != rowIndex)
        {
          cm.Position = rowIndex;
        }
      }
    }

    private void btnExport_Click(object sender, EventArgs e)
    {
      if (mrmFileName == null)
      {
        return;
      }

      dlgExport.InitialDirectory = new FileInfo(mrmFileName).Directory.FullName;
      if (dlgExport.ShowDialog() == DialogResult.OK)
      {
        var enabledOnly = MessageBox.Show(this, "Export enabled entries only?", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1) == DialogResult.Yes;

        new SrmPairedResultTextFormat(enabledOnly).WriteToFile(dlgExport.FileName, mrmPairs);
      }
    }

    private SrmPairedPeptideItem GetPeptide()
    {
      var node = tvMRM.SelectedNode;
      if (node == null)
      {
        return null;
      }

      if (node.Level == 1)
      {
        return node.Parent.Tag as SrmPairedPeptideItem;
      }
      else
      {
        return node.Tag as SrmPairedPeptideItem;
      }
    }

    private void tvMRM_AfterCheck(object sender, TreeViewEventArgs e)
    {
      var node = e.Node;
      if (node.Level == 0)
      {
        SrmPairedPeptideItem pepItem = node.Tag as SrmPairedPeptideItem;
        pepItem.Enabled = node.Checked;
        return;
      }

      if (node.Level == 1)
      {
        SrmPairedProductIon item = node.Tag as SrmPairedProductIon;
        item.Enabled = node.Checked;

        SrmPairedPeptideItem pepItem = node.Parent.Tag as SrmPairedPeptideItem;
        pepItem.CalculatePeptideRatio();
      }

      tvMRM_AfterSelect(null, null);
    }

    private void gvProductPair_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
    {
      var name = gvProductPair.Columns[e.ColumnIndex].Name;
      if (name.Equals("headerCorrel"))
      {
        double correl = (double)e.Value;
        if (correl < mrmPairs.Options.ValidationMinRegressionCorrelation)
        {
          e.CellStyle.BackColor = Color.Red;
          e.CellStyle.SelectionBackColor = Color.DarkRed;
        }
      }

      if (name.Equals("headerLSN") || name.Equals("headerHSN"))
      {
        double sn = (double)e.Value;
        if (sn < mrmPairs.Options.ValidationMinSignalToNoise)
        {
          e.CellStyle.BackColor = Color.Red;
          e.CellStyle.SelectionBackColor = Color.DarkRed;
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

    private void lightView_Scroll(object sender, ScrollEventArgs e)
    {
    }

    private void gvProductPair_SelectionChanged(object sender, EventArgs e)
    {
      var item = DoUpdateProductIon();

      if (item != null)
      {
        SetTreeSelected(item);
      }
    }

    private void SetTreeSelected(SrmPairedProductIon item)
    {
      if (tvMRM.SelectedNode.Level == 0)
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

    private DisplayType lastType = DisplayType.PerfectSize;

    private void DoUpdate()
    {
      DoUpdatePeptide();
      DoUpdateProductIon();
    }

    private void rbPercentage_Click(object sender, EventArgs e)
    {
      lastType = DisplayType.PerfectSize;
      DoUpdate();
    }

    private void rbFullSize_Click(object sender, EventArgs e)
    {
      lastType = DisplayType.FullSize;
      DoUpdate();
    }

    private void rbFullHeight_Click(object sender, EventArgs e)
    {
      lastType = DisplayType.FullHeight;
      DoUpdate();
    }

    private void pmiSplit_Click(object sender, EventArgs e)
    {
      var pep = GetPeptide();
      if (pep != null)
      {
        SrmSplitForm form = new SrmSplitForm();
        form.LeftPeptide = pep;
        form.ShowDialog();
      }
    }
  }
}
