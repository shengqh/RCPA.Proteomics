using DigitalRune.Windows.Docking;
using RCPA.Utils;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Threading;
using System.Windows.Forms;
using ZedGraph;

namespace RCPA.Proteomics.Quantification.Srm
{
  public partial class SrmCompoundModeForm : DockableForm, ISrmForm
  {
    private SrmFileItemList allFiles;

    private List<SrmPairedResult> allResults;

    private List<FileItems> currentItems = new List<FileItems>();

    private SrmPairedPeptideItem currentCompound;

    private CompoundItem compoundInList;

    private DisplayType currentDisplayType = DisplayType.PerfectSize;

    private ZedGraphMRMPeptideItemScans peptideGraph;

    private ZedGraphMRMPeptideItemRetentionTimes rtGraph;

    private List<ZedGraphMRMPeptideItemScans> scanGraphs = new List<ZedGraphMRMPeptideItemScans>();

    private static readonly int FirstProductIonColumn = 2;

    public SrmViewOption ViewOption { get; set; }

    public SrmCompoundModeForm()
    {
      InitializeComponent();

      this.peptideGraph = new ZedGraphMRMPeptideItemScans(zgcPeptide, this.CreateGraphics());

      this.rtGraph = new ZedGraphMRMPeptideItemRetentionTimes(zgcRetentionTime);

      this.ViewOption = new SrmViewOption();
    }

    public void InitializeGraphics()
    {
      var filenames = (from pr in allFiles.Items
                       orderby pr.PairedResult.PureFileName
                       select pr.PairedResult.PureFileName).Distinct().ToList();

      while (tcGraph.TabCount > 1)
      {
        tcGraph.TabPages.RemoveAt(tcGraph.TabCount - 1);
        scanGraphs.RemoveAt(scanGraphs.Count - 1);
      }

      while (tcGraph.TabCount < filenames.Count + 1)
      {
        tcGraph.TabPages.Add(filenames[tcGraph.TabCount - 1]);
        var page = tcGraph.TabPages[tcGraph.TabCount - 1];
        var zgc = new ZedGraphControl();
        zgc.Parent = page;
        page.Controls.Add(zgc);
        zgc.Dock = DockStyle.Fill;
        var scan = new ZedGraphMRMPeptideItemScans(zgc, this.CreateGraphics());
        scanGraphs.Add(scan);
      }

      for (int i = 0; i < filenames.Count; i++)
      {
        scanGraphs[i].FileName = filenames[i];
      }
    }

    private void InitializeFileColumns()
    {
      var filenames = (from pr in this.allResults
                       orderby pr.PureFileName
                       select pr.PureFileName).ToList();

      gvFiles.Columns.Clear();
      gvFiles.Columns.Add(new DataGridViewCheckBoxColumn()
      {
        Name = "Enabled",
        HeaderText = "Enabled",
        Width = 60
      });

      gvFiles.Columns.Add(new DataGridViewTextBoxColumn()
      {
        Name = "ProductIon",
        HeaderText = "Product Ion",
        Width = 150
      });

      filenames.ForEach(m => gvFiles.Columns.Add(new DataGridViewTextBoxColumn()
      {
        Name = m,
        HeaderText = m,
        Width = 150
      }));
    }

    public void ViewChanged()
    {
      InitializeCompoundList();
    }

    private void InitializeCompoundList()
    {
      lvPeptides.BeginUpdate();
      try
      {
        lvPeptides.Items.Clear();

        foreach (var comp in allFiles.Compounds)
        {
          if (!ViewOption.ViewDecoy && comp.IsDecoy)
          {
            continue;
          }

          lvPeptides.Items.Add(comp);

          var totalCount = (from v in comp.TransitionItems
                            from vl in v.FileItems
                            select vl).Count();

          var goodCount = (from v in comp.TransitionItems
                           from vl in v.FileItems
                           where vl.PairedProductIon.Enabled
                           select vl).Count();

          lvPeptides.SetItemChecked(lvPeptides.Items.Count - 1, goodCount >= totalCount / 2);
        }

        if (lvPeptides.Items.Count > 0)
        {
          lvPeptides.SetSelected(0, true);
        }
      }
      catch (Exception ex)
      {
        Console.WriteLine(ex.StackTrace);
        MessageBox.Show(ex.Message);
      }
      finally
      {
        lvPeptides.EndUpdate();
      }
    }

    public void PairedResultChanged(object sender, PairedResultChangedEventArgs e)
    {
      this.allResults = e.PairedResult;

      this.allFiles = e.ItemList;

      InitializeGraphics();

      InitializeFileColumns();

      InitializeCompoundList();
    }


    public void DisplayTypeChanged(object sender, DisplayTypeChangedEventArgs e)
    {
      currentDisplayType = e.NewType;

      UpdateGraph();
    }

    private string GetString(TransitionItem transition)
    {
      return MyConvert.Format("{0:0.0000}-{1:0.0000}", transition.LightMz, transition.HeavyMz);
    }

    private void DoCompoundChanged(CompoundItem compound)
    {
      if (compoundInList != compound)
      {
        compoundInList = compound;
        currentItems = GetFileItems(compound); ;
        gvFiles.Rows.Clear();
        gvFiles.RowCount = currentItems.Count;
      }
    }

    private List<FileItems> GetFileItems(CompoundItem compound)
    {
      List<FileItems> tmpItems = new List<FileItems>();

      foreach (var titem in compound.TransitionItems)
      {
        var item = new FileItems();
        item.Precursor = GetString(titem);
        item.Items = new List<SrmFileItem>();

        for (int i = FirstProductIonColumn; i < gvFiles.Columns.Count; i++)
        {
          var fileitem = titem.FileItems.Find(m => m.PairedResult.PureFileName.Equals(gvFiles.Columns[i].Name));
          item.Items.Add(fileitem);
        }

        tmpItems.Add(item);
      }
      return tmpItems;
    }

    private void lvPeptides_SelectedIndexChanged(object sender, EventArgs e)
    {
      if (0 == lvPeptides.SelectedItems.Count)
      {
        return;
      }

      var compound = lvPeptides.SelectedItems[0] as CompoundItem;
      DoCompoundChanged(compound);
    }

    private void gvFiles_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
    {
      if (e.ColumnIndex < FirstProductIonColumn)
      {
        tcGraph.SelectedIndex = 0;
      }
      else
      {
        tcGraph.SelectedIndex = e.ColumnIndex - FirstProductIonColumn + 1;
      }
    }

    private void gvFiles_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
    {
      if (e.RowIndex >= currentItems.Count)
      {
        e.Value = string.Empty;
        return;
      }

      if (e.ColumnIndex < FirstProductIonColumn)
      {
        return;
      }

      var fileitem = currentItems[e.RowIndex].Items[e.ColumnIndex - FirstProductIonColumn];
      if (fileitem == null || fileitem.PairedProductIon.RegressionCorrelation < fileitem.PairedResult.Options.ValidationMinRegressionCorrelation)
      {
        e.CellStyle.BackColor = Color.Red;
        e.CellStyle.SelectionBackColor = Color.DarkRed;
      }
    }

    private void gvFiles_SelectionChanged(object sender, EventArgs e)
    {
      if (gvFiles.SelectedRows.Count == 0)
      {
        return;
      }

      var fis = currentItems[gvFiles.SelectedRows[0].Index].Items;

      currentCompound = new SrmPairedPeptideItem();
      for (int i = 0; i < fis.Count; i++)
      {
        if (fis[i] == null)
        {
          currentCompound.ProductIonPairs.Add(null);
        }
        else
        {
          currentCompound.ProductIonPairs.Add(fis[i].PairedProductIon);
        }
      }

      UpdateGraph();
    }

    private void UpdateGraph()
    {
      if (currentCompound == null)
      {
        return;
      }

      UpdateMRMPairedPeptideItemEventArgs args = new UpdateMRMPairedPeptideItemEventArgs(currentCompound, new SrmViewOption() { ViewGreenLine = true, ViewType = currentDisplayType });

      peptideGraph.Update(this, args);

      rtGraph.Update(this, args);

      scanGraphs.ForEach(m => m.Update(this, args));
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

    private void mnuSetScanEnabledOnly_Click(object sender, EventArgs e)
    {
      GraphPane graphScans = mnuSetScanEnabledOnly.Tag as GraphPane;
      double min = graphScans.XAxis.Scale.Min;
      double max = graphScans.XAxis.Scale.Max;

      SrmPairedProductIon ion = graphScans.Tag as SrmPairedProductIon;

      var item = allFiles.Items.Find(m => m.PairedProductIon == ion);

      item.PairedPeptide.SetEnabledRetentionTimeRange(min, max);

      item.PairedResult.Update(item.PairedPeptide);

      item.PairedResult.Modified = true;

      gvFiles.Refresh();
      UpdateGraph();
    }

    private void gvFiles_CellValueNeeded(object sender, DataGridViewCellValueEventArgs e)
    {
      if (e.RowIndex >= currentItems.Count)
      {
        e.Value = string.Empty;
        return;
      }

      if (e.ColumnIndex == 0)
      {
        e.Value = currentItems[e.RowIndex].Enabled;
      }
      else if (e.ColumnIndex == 1)
      {
        e.Value = currentItems[e.RowIndex].Precursor;
      }
      else
      {
        var fileitem = currentItems[e.RowIndex].Items[e.ColumnIndex - FirstProductIonColumn];
        if (fileitem == null)
        {
          e.Value = string.Empty;
        }
        else
        {
          var text = MyConvert.Format("[{0:0.0000} , {1:0.0000}]", fileitem.PairedProductIon.Ratio, fileitem.PairedProductIon.RegressionCorrelation);
          e.Value = text;
        }
      }
    }

    public void ExportCompound(string filename, bool removeDecoy, bool validOnly)
    {
      using (StreamWriter sw = new StreamWriter(filename))
      {
        sw.Write("ObjectName,CompoundFormula,LightMz,HeavyMz,Enabled");
        var filenames = (from pr in allFiles.Items
                         orderby pr.PairedResult.PureFileName
                         select pr.PairedResult.PureFileName).Distinct().ToList();
        filenames.ForEach(m => sw.Write(",{0}_Ratio,{0}_SD,{0}_ValidTransCount", m));
        sw.WriteLine();

        for (int i = 0; i < lvPeptides.Items.Count; i++)
        {
          if (validOnly && !lvPeptides.GetItemChecked(i))
          {
            continue;
          }

          CompoundItem item = lvPeptides.Items[i] as CompoundItem;
          if (removeDecoy && item.IsDecoy)
          {
            continue;
          }

          sw.Write("{0},{1},{2:0.0000},{3:0.0000},{4}", item.ObjectName, item.PrecursurFormula, item.LightMz, item.HeavyMz, lvPeptides.GetItemChecked(i));
          foreach (var file in filenames)
          {
            var pep = (from t in item.TransitionItems
                       from m in t.FileItems
                       where m.PairedResult.PureFileName.Equals(file)
                       select m.PairedPeptide).FirstOrDefault();
            if (pep == null || pep.Ratio == -1)
            {
              sw.Write(",#N/A,0,0");
            }
            else
            {
              var transCount = pep.ProductIonPairs.Count(m => m.Enabled);
              if (double.IsNaN(pep.SD) || double.IsInfinity(pep.SD))
              {
                sw.Write(",{0:0.0000},0,{1}", pep.Ratio, transCount);
              }
              else
              {
                sw.Write(",{0:0.0000},{1:0.0000},{2}", pep.Ratio, pep.SD, transCount);
              }
            }
          }
          sw.WriteLine();
        }
      }
    }

    public void ExportTransition(string filename, bool removeDecoy, bool validOnly)
    {
      using (StreamWriter sw = new StreamWriter(filename))
      {
        sw.Write("ObjectName,CompoundFormula,LightPrecursorMz,LightProductIon,HeavyPrecursorMz,HeavyProductIon,Enabled");
        var filenames = (from pr in allFiles.Items
                         orderby pr.PairedResult.PureFileName
                         select pr.PairedResult.PureFileName).Distinct().ToList();
        filenames.ForEach(m => sw.Write(",{0}_Ratio,{0}_R2,{0}_LightArea,{0}_HeavyArea,{0}_ValidScanCount", m));
        sw.WriteLine();

        for (int i = 0; i < lvPeptides.Items.Count; i++)
        {
          if (validOnly && !lvPeptides.GetItemChecked(i))
          {
            continue;
          }

          CompoundItem item = lvPeptides.Items[i] as CompoundItem;
          if (removeDecoy && item.IsDecoy)
          {
            continue;
          }

          var fileitems = GetFileItems(item);

          foreach (var fitem in fileitems)
          {
            if (validOnly && !fitem.Enabled)
            {
              continue;
            }
            var product = (from f in fitem.Items
                           where f != null
                           select f).First().PairedProductIon;

            sw.Write("{0},{1},{2:0.0000},{3:0.0000},{4:0.0000},{5:0.0000},{6}", item.ObjectName, item.PrecursurFormula, item.LightMz, product.LightProductIon, item.HeavyMz, product.HeavyProductIon, fitem.Enabled);
            foreach (var file in filenames)
            {
              var pep = (from t in fitem.Items
                         where t.PairedResult.PureFileName.Equals(file)
                         select t.PairedProductIon).FirstOrDefault();
              if (pep == null || pep.Ratio == -1)
              {
                sw.Write(",#N/A,0,#N/A,#N/A,0");
              }
              else
              {
                var scanCount = pep.EnabledScanCount;
                if (double.IsNaN(pep.RegressionCorrelation) || double.IsInfinity(pep.RegressionCorrelation))
                {
                  sw.Write(",{0:0.0000},0,{1:0.0},{2:0.0},{3}", pep.Ratio, pep.LightArea, pep.HeavyArea, scanCount);
                }
                else
                {
                  sw.Write(",{0:0.0000},{1:0.0000},{2:0.0},{3:0.0},{4}", pep.Ratio, pep.RegressionCorrelation, pep.LightArea, pep.HeavyArea, scanCount);
                }
              }
            }
            sw.WriteLine();
          }
        }
      }
    }

    public void Focus(object p)
    {
      if (p is SrmPairedPeptideItem)
      {
        var pep = p as SrmPairedPeptideItem;

        var fileitem = FindComponent(pep);

        lvPeptides.SelectedItem = fileitem;

        this.Show();
      }
      else if (p is SrmPairedProductIon)
      {
        var pro = p as SrmPairedProductIon;

        var pep = (from f in allFiles.Items
                   where f.PairedProductIon == pro
                   select f.PairedPeptide).FirstOrDefault();

        var fileitem = FindComponent(pep);
        DoCompoundChanged(fileitem);

        lvPeptides.SelectedItem = fileitem;
        var proindex = pep.ProductIonPairs.IndexOf(pro);

        CurrencyManager cm = (CurrencyManager)this.BindingContext[fileitem.TransitionItems];
        while (cm.Position != proindex)
        {
          cm.Position = proindex;
          Thread.Sleep(100);
        }

        this.Show();
      }
    }

    private CompoundItem FindComponent(SrmPairedPeptideItem pep)
    {
      var fileitem = (from f in allFiles.Compounds
                      where f.ObjectName.Equals(pep.ProductIonPairs[0].ObjectName) && f.PrecursurFormula.Equals(pep.ProductIonPairs[0].PrecursorFormula)
                      select f).FirstOrDefault();
      return fileitem;
    }

    private void gvFiles_CellBeginEdit(object sender, DataGridViewCellCancelEventArgs e)
    {
      if (e.ColumnIndex > 0)
      {
        e.Cancel = true;
      }
    }

    #region ISrmForm Members

    public object GetCurrentFocusedObject()
    {
      if (null == lvPeptides.SelectedItem || null == currentCompound || 0 == currentCompound.ProductIonPairs.Count)
      {
        return null;
      }

      if (tcGraph.SelectedIndex == 0)
      {
        int index = 0;
        if (gvFiles.SelectedRows.Count > 0)
        {
          index = gvFiles.SelectedRows[0].Index;
        }

        var fis = currentItems[index].Items;
        return fis.First().PairedProductIon;
      }
      else
      {

      }
      throw new NotImplementedException();
    }

    public void SetCurrentFocusedObject(object obj)
    {
      throw new NotImplementedException();
    }

    #endregion
  }
}
