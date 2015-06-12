using System;
using System.Drawing;
using System.Windows.Forms;
using System.Linq;
using RCPA.Gui;
using RCPA.Gui.Command;
using RCPA.Gui.FileArgument;
using RCPA.Gui.Image;
using RCPA.Proteomics;
using RCPA.Proteomics.Raw;
using ZedGraph;
using RCPA.Proteomics.Spectrum;
using System.Text;
using System.Collections.Generic;
using RCPA.Utils;
using System.IO;
using DigitalRune.Windows.Docking;
using System.Drawing.Imaging;

namespace RCPA.Proteomics.Quantification.Srm
{
  public partial class SrmValidatorUI : ComponentUI
  {
    public class LinearResult
    {
      public double Ratio { get; set; }
      public double Correl { get; set; }
      public override string ToString()
      {
        return MyConvert.Format("[{0:0.0000}; {1:0.0000}]", Ratio, Correl);
      }
    }

    private static readonly string title = "SRM Validator";

    private static readonly string version = "1.2.4";

    /// <summary>
    /// 当前读入的文件
    /// </summary>
    private List<SrmPairedResult> pairedResults = null;

    /// <summary>
    /// 由文件整合得到的总结果
    /// </summary>
    private SrmFileItemList files = null;

    /// <summary>
    /// 结果中所有可展示的compound
    /// </summary>
    private List<CompoundItem> visibleCompounds = null;

    /// <summary>
    /// 当前compound
    /// </summary>
    private CompoundItem currentCompound = null;

    /// <summary>
    /// 当前compound mode下的compound
    /// </summary>
    private SrmPairedPeptideItem compoundPeptide = null;

    /// <summary>
    /// 文件模式下，当前compound在当前文件中peptide
    /// </summary>
    private SrmPairedPeptideItem filePeptide = null;

    /// <summary>
    /// compound模式下，当前compound在所有文件中的product ion pairs
    /// </summary>
    private List<FileItems> currentItems = null;

    /// <summary>
    /// 动态产生的Graph
    /// </summary>
    private List<ZedGraphSrmPeptideItemScanRegression> compoundScanGraphs = new List<ZedGraphSrmPeptideItemScanRegression>();

    private SrmViewOption viewOption = new SrmViewOption();

    private static readonly int FirstProductIonColumn = 2;

    public event UpdateMRMPairedProductIonEventHandler FileModeProductIonUpdate;

    public event UpdateMRMPairedPeptideItemEventHandler FileModePeptideItemUpdate;

    public event UpdateMRMPairedPeptideItemEventHandler CompoundModePeptideItemUpdate;

    [RcpaOption("ViewDecoy", RcpaOptionType.Boolean)]
    protected bool ViewDecoy
    {
      get
      {
        return viewOption.ViewDecoy;
      }
      set
      {
        viewOption.ViewDecoy = value;
        InitializeCompoundList();
      }
    }

    [RcpaOption("ViewValidOnly", RcpaOptionType.Boolean)]
    protected bool ViewValidOnly
    {
      get
      {
        return viewOption.ViewValidOnly;
      }
      set
      {
        viewOption.ViewValidOnly = value;
        InitializeCompoundList();
      }
    }

    [RcpaOption("ViewGreenLine", RcpaOptionType.Boolean)]
    protected bool ViewGreenLine
    {
      get
      {
        return viewOption.ViewGreenLine;
      }
      set
      {
        viewOption.ViewGreenLine = value;
        DoDisplayChanged();
      }
    }

    [RcpaOption("ViewCurrentHighlight", RcpaOptionType.Boolean)]
    protected bool ViewCurrentHighlight
    {
      get
      {
        return viewOption.ViewCurrentHighlight;
      }
      set
      {
        viewOption.ViewCurrentHighlight = value;
        DoDisplayChanged();
      }
    }

    [RcpaOption("ViewInvalidCompoundAsNA", RcpaOptionType.Boolean)]
    protected bool ViewInvalidCompoundAsNA
    {
      get
      {
        return viewOption.ViewInvalidCompoundAsNA;
      }
      set
      {
        viewOption.ViewInvalidCompoundAsNA = value;
        gvFiles.Refresh();
      }
    }

    private bool _ratioByArea = true;

    [RcpaOption("RatioByArea", RcpaOptionType.Boolean)]
    protected bool RatioByArea
    {
      get
      {
        return _ratioByArea;
      }
      set
      {
        _ratioByArea = value;
        RecalculateRatio();
        gvProductPair.Refresh();
        gvFiles.Refresh();
      }
    }

    private void RecalculateRatio()
    {
      if (null != pairedResults)
      {
        pairedResults.ForEach(m =>
        {
          m.Options.RatioByArea = RatioByArea;
          m.CalculateRatio();
        });
      }
    }

    public DisplayType CurrentDisplayType
    {
      get
      {
        return viewOption.ViewType;
      }
      set
      {
        viewOption.ViewType = value;
        DoDisplayChanged();
      }
    }

    public SrmValidatorUI()
    {
      InitializeComponent();

      this.Text = Constants.GetSQHTitle(title, version);

      this.viewOption = new SrmViewOption();

      FileModePeptideItemUpdate += new ZedGraphMRMPeptideItemScans(zgcFileModePeptide, this.CreateGraphics()).Update;
      FileModePeptideItemUpdate += new ZedGraphSrmPeptideItem(zgcAllInOne).Update;
      FileModeProductIonUpdate += new ZedGraphMRMProductIonScansAndRegression(zgcFileModeTransaction, this.CreateGraphics()).Update;

      CompoundModePeptideItemUpdate += new ZedGraphMRMPeptideItemScans(zgcCompoundModePeptide, this.CreateGraphics()).Update;
      CompoundModePeptideItemUpdate += new ZedGraphMRMPeptideItemRetentionTimes(zgcRetentionTime).Update;

      btnFileMode.PerformClick();
    }

    private void DoDisplayChanged()
    {
      btnFullSize.Checked = CurrentDisplayType == DisplayType.FullSize;
      btnFullHeight.Checked = CurrentDisplayType == DisplayType.FullHeight;
      btnPerfectSize.Checked = CurrentDisplayType == DisplayType.PerfectSize;

      if (null != files)
      {
        DoUpdateCompoundGraph();
        DoUpdateFileModePeptide();
        DoUpdateFileModeProductIon();
      }
    }

    public class Command : IToolSecondLevelCommand
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
        new SrmValidatorUI().MyShow();
      }

      #endregion

      #region IToolSecondLevelCommand Members

      public string GetSecondLevelCommandItem()
      {
        return "SRMBuilder";
      }

      #endregion
    }

    private void mnuOpenMrms_Click(object sender, EventArgs e)
    {
      if (this.pairedResults != null && this.pairedResults.Any(m => m.Modified))
      {
        var res = MessageBox.Show("Data changed, do you want to save the changes?", "Warning", MessageBoxButtons.YesNoCancel);
        if (res == System.Windows.Forms.DialogResult.Yes)
        {
          mnuSaveChanges.PerformClick();
        }
        else if (res == System.Windows.Forms.DialogResult.Cancel)
        {
          return;
        }
      }

      var selectForm = new MrmSelectFileForm();

      if (selectForm.ShowDialog(this) == System.Windows.Forms.DialogResult.OK)
      {
        var filenames = selectForm.SelectedMrmFiles;

        this.pairedResults = new List<SrmPairedResult>();

        var format = new SrmPairedResultXmlFormat();
        try
        {
          btnClose.Enabled = false;
          mnuClose.Enabled = false;
          btnOpenMrms.Enabled = false;
          mnuOpenMrms.Enabled = false;

          for (int i = 0; i < filenames.Length; i++)
          {
            var filename = filenames[i];

            lblProgress.Text = MyConvert.Format("Reading MRM Scans {0}/{1} - {2}", i + 1, filenames.Length, filename);
            lblProgress.Update();

            var pr = format.ReadFromFile(filename);

            //if (pr.Options.RatioByArea != RatioByArea)
            //{
            //  pr.Options.RatioByArea = RatioByArea;
            //  pr.CalculateRatio();
            //  pr.Modified = true;
            //}

            pr.Options.ValidationSoftware = this.Text;

            this.pairedResults.Add(pr);
          }
          lblProgress.Text = "Finished.";
        }
        finally
        {
          btnClose.Enabled = true;
          mnuClose.Enabled = true;
          btnOpenMrms.Enabled = true;
          mnuOpenMrms.Enabled = true;
        }

        this.files = new SrmFileItemList(this.pairedResults, 0.01);

        InitializeFileModeTabPages();

        InitializeCompundModeFileColumns();

        InitializeCompoundGraphics();

        InitializeCompoundList();

        DoDataChanged(false);
      }
    }

    private void rbFullSize_Click(object sender, EventArgs e)
    {
      CurrentDisplayType = DisplayType.FullSize;
    }

    private void rbPercentage_Click(object sender, EventArgs e)
    {
      CurrentDisplayType = DisplayType.PerfectSize;
    }

    private void rbFullHeight_Click(object sender, EventArgs e)
    {
      CurrentDisplayType = DisplayType.FullHeight;
    }

    private void mnuSaveChanges_Click(object sender, EventArgs e)
    {
      if (pairedResults != null)
      {
        pairedResults.ForEach(m =>
        {
          if (m.Modified)
          {
            new SrmPairedResultXmlFormat().WriteToFile(m.FileName, m);
            m.Modified = false;
          }
        });
      }

      DoDataChanged(false);
    }

    private void mnuClose_Click(object sender, EventArgs e)
    {
      if (IsDataModified())
      {
        if (MessageBox.Show("Data changed, close window will lost changes, continue?", "Warning", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2) == System.Windows.Forms.DialogResult.Cancel)
        {
          return;
        }
      }

      Close();
    }

    private bool IsDataModified()
    {
      return pairedResults != null && pairedResults.Any(m => m.Modified);
    }

    private void mnuExportCompound_Click(object sender, EventArgs e)
    {
      ExportCompoundForm form = new ExportCompoundForm();
      form.InitializeByFileItems(files.Compounds);
      form.MyShowDialog();
    }

    private List<string> GetFileNames()
    {
      if (files == null)
      {
        return new string[] { }.ToList();
      }
      else
      {
        return files.GetFileNames();
      }
    }

    private void mnuExportTransition_Click(object sender, EventArgs e)
    {
      SrmExportForm form = new SrmExportForm();
      form.Text = "Export transition quantification result ...";
      if (form.MyShowDialog() == System.Windows.Forms.DialogResult.OK)
      {
        var validOnly = form.ValidOnly;
        var removeDecoy = form.RemoveDecoy;
        var targetFile = form.TargetFile;

        ExportTransition(targetFile, removeDecoy, validOnly);
      }
    }

    public void ExportTransition(string filename, bool removeDecoy, bool validOnly)
    {
      using (StreamWriter sw = new StreamWriter(filename))
      {
        var filenames = GetFileNames();
        sw.Write("ObjectName,CompoundFormula,LightPrecursorMz,HeavyPrecursorMz,LightProductIon,HeavyProductIon,Enabled,IsDecoy");
        filenames.ForEach(m => sw.Write(",{0}_Ratio,{0}_R2,{0}_Enabled,{0}_ValidScanCount,{0}_LightArea,{0}_HeavyArea,{0}_LightSN,{0}_HeavySN,{0}_Noise", m));
        sw.WriteLine();

        var comps = (from c in files.Compounds
                     where (!removeDecoy || !c.IsDecoy) && (!validOnly || c.Enabled)
                     select c).ToList();

        foreach (var item in comps)
        {
          foreach (var transitem in item.FileItems)
          {
            if (validOnly && !transitem.Enabled)
            {
              continue;
            }
            sw.Write("{0},{1},{2:0.0000},{3:0.0000},{4:0.0000},{5:0.0000},{6},{7}", item.ObjectName, item.PrecursurFormula, item.LightMz, item.HeavyMz, transitem.LightProductIon, transitem.HeavyProductIon, transitem.Enabled, item.IsDecoy);
            foreach (var file in filenames)
            {
              var product = transitem.Items.FirstOrDefault(m => m != null && m.PairedResult.PureFileName.Equals(file));
              if (product == null || product.PairedProductIon == null || product.PairedProductIon.Ratio == -1)
              {
                sw.Write(",#N/A,0,False,0,0,0,0,0,0");
              }
              else
              {
                var pi = product.PairedProductIon;

                double rc;
                if (double.IsNaN(pi.RegressionCorrelation) || double.IsInfinity(pi.RegressionCorrelation))
                {
                  rc = 0.0;
                }
                else
                {
                  rc = pi.RegressionCorrelation;
                }
                sw.Write(",{0:0.0000},{1:0.0000},{2},{3},{4:0.0},{5:0.0},{6:0.0},{7:0.0},{8:0.0}", pi.Ratio, rc, pi.Enabled, pi.EnabledScanCount, pi.LightArea, pi.HeavyArea, pi.LightSignalToNoise, pi.HeavySignalToNoise, pi.Noise);
              }
            }
            sw.WriteLine();
          }
        }
      }
      MessageBox.Show("Transition quantification result has been saved to " + filename);
    }

    private FolderBrowser dlg = new FolderBrowser();
    private void mnuExportHtml_Click(object sender, EventArgs e)
    {
      if (dlg.ShowDialog() == System.Windows.Forms.DialogResult.OK)
      {
        ExportFileModeHtml(dlg.SelectedPath);
      }
    }

    private SaveFileDialog sdlg = new SaveFileDialog();
    private void mnuExportStatistic_Click(object sender, EventArgs e)
    {
      if (sdlg.ShowDialog() == System.Windows.Forms.DialogResult.OK)
      {
        ExportStatistic(sdlg.FileName);
      }
    }

    private void btnFileMode_Click(object sender, EventArgs e)
    {
      var isFileMode = sender == btnFileMode;

      if (isFileMode)
      {
        tcMode.ShowTabPageOnly(tabFileMode);
        tcFiles_SelectedIndexChanged(null, null);
      }
      else
      {
        tcMode.ShowTabPageOnly(tabCompoundMode);
        if (files != null)
        {
          files.Compounds.ForEach(m => m.FileName = string.Empty);
          gvCompounds.Refresh();
        }
      }

      btnFileMode.Checked = isFileMode;
      btnCompoundMode.Checked = !isFileMode;
    }

    private bool IsCompoundMode()
    {
      return tcMode.SelectedTab == tabCompoundMode;
    }

    private void mnuCompoundMode_Click(object sender, EventArgs e)
    {
      btnCompoundMode.PerformClick();
    }

    private void mnuFileMode_Click(object sender, EventArgs e)
    {
      btnFileMode.PerformClick();
    }

    private void mnuView_DropDownOpening(object sender, EventArgs e)
    {
      mnuDecoy.Checked = viewOption.ViewDecoy;
      mnuValidOnly.Checked = viewOption.ViewValidOnly;
      mnuGreenLine.Checked = viewOption.ViewGreenLine;
      mnuHighlightCurrent.Checked = viewOption.ViewCurrentHighlight;

      mnuPerfectSize.Checked = viewOption.ViewType == DisplayType.PerfectSize;
      mnuFullSize.Checked = viewOption.ViewType == DisplayType.FullSize;
      mnuFullHeight.Checked = viewOption.ViewType == DisplayType.FullHeight;

      mnuCompoundMode.Checked = IsCompoundMode();
      mnuFileMode.Checked = !IsCompoundMode();
    }

    private void gvCompounds_SelectionChanged(object sender, EventArgs e)
    {
      if (gvCompounds.SelectedRows.Count == 0)
      {
        return;
      }

      currentCompound = visibleCompounds[gvCompounds.SelectedRows[0].Index];

      DoCompoundChanged();
    }

    private void DoDataChanged(bool value)
    {
      btnSaveChanges.Enabled = value;
      mnuSaveChanges.Enabled = value;
    }

    private void DoCompoundChanged()
    {
      //FileMode下更新
      tcFiles_SelectedIndexChanged(null, null);

      //CompoundMode下更新
      currentItems = currentCompound.FileItems;
      gvFiles.Rows.Clear();
      gvFiles.RowCount = currentItems.Count;
    }

    public void InitializeCompoundGraphics()
    {
      var filenames = GetFileNames();

      for (int i = compoundScanGraphs.Count - 1; i >= 0; i--)
      {
        CompoundModePeptideItemUpdate -= compoundScanGraphs[i].Update;
        compoundScanGraphs.RemoveAt(i);
        tcCompoundGraph.TabPages.RemoveAt(i + 1);
      }

      foreach (var filename in filenames)
      {
        tcCompoundGraph.TabPages.Add(filename);
        var page = tcCompoundGraph.TabPages[tcCompoundGraph.TabCount - 1];
        var zgc = new ZedGraphControl();
        zgc.Parent = page;
        page.Controls.Add(zgc);
        zgc.Dock = DockStyle.Fill;
        var scan = new ZedGraphSrmPeptideItemScanRegression(zgc, this.CreateGraphics(), filename);
        zgc.ContextMenuBuilder += zgcCompoundTransition_ContextMenuBuilder;
        compoundScanGraphs.Add(scan);
        CompoundModePeptideItemUpdate += scan.Update;
      }
    }

    private void InitializeFileModeTabPages()
    {
      var filenames = GetFileNames();

      tcFiles.TabPages.Clear();
      foreach (var filename in filenames)
      {
        tcFiles.TabPages.Add(filename);
      }
    }

    private void InitializeCompundModeFileColumns()
    {
      var filenames = GetFileNames();

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

    private void InitializeCompoundList()
    {
      if (null != files)
      {
        visibleCompounds = files.GetCompounds(viewOption.GetRejectCompoundFilter());
        gvCompounds.DataSource = visibleCompounds;
      }
    }

    private void tcFiles_SelectedIndexChanged(object sender, EventArgs e)
    {
      if (tcFiles.TabCount > 0 && tcFiles.SelectedTab != null)
      {
        var fileName = tcFiles.SelectedTab.Text;

        files.Compounds.ForEach(m => m.FileName = fileName);

        gvCompounds.Refresh();

        filePeptide = currentCompound.GetPeptideItem(fileName);

        gvProductPair.DataSource = filePeptide.ProductIonPairs;
      }
    }

    private void gvProductPair_CellBeginEdit(object sender, DataGridViewCellCancelEventArgs e)
    {
      e.Cancel = e.ColumnIndex > 0;
    }

    private void gvProductPair_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
    {
      if (filePeptide != null)
      {
        if (e.RowIndex >= filePeptide.ProductIonPairs.Count)
        {
          return;
        }

        var currProductIon = filePeptide.ProductIonPairs[e.RowIndex];
        var item = files.FindFileItem(currProductIon);
        if (null != item)
        {
          var pr = item.PairedResult;
          var name = gvProductPair.Columns[e.ColumnIndex].Name;
          if (name.Equals("headerCorrel"))
          {
            double correl = (double)e.Value;
            if (correl < pr.Options.ValidationMinRegressionCorrelation)
            {
              e.CellStyle.BackColor = Color.Red;
              e.CellStyle.SelectionBackColor = Color.DarkRed;
            }
          }

          if (name.Equals("headerLSN") || name.Equals("headerHSN"))
          {
            double sn = (double)e.Value;
            if (sn < pr.Options.ValidationMinSignalToNoise)
            {
              e.CellStyle.BackColor = Color.Red;
              e.CellStyle.SelectionBackColor = Color.DarkRed;
            }
          }
        }
      }
    }

    protected void OnUpdateFileModeProductIon(UpdateMRMPairedProductIonEventArgs e)
    {
      if (FileModeProductIonUpdate != null)
      {
        FileModeProductIonUpdate(this, e);
      }
    }

    protected void OnUpdateFileModePeptideItem(UpdateMRMPairedPeptideItemEventArgs e)
    {
      if (FileModePeptideItemUpdate != null)
      {
        FileModePeptideItemUpdate(this, e);
      }
    }

    private void DoUpdateFileModeProductIon()
    {
      var fileItem = GetFileModeItem();

      fileItem.PairedPeptide.ProductIonPairs.ForEach(m => m.IsCurrent = m == fileItem.PairedProductIon);

      UpdateMRMPairedProductIonEventArgs args = new UpdateMRMPairedProductIonEventArgs(fileItem.PairedProductIon, viewOption);

      OnUpdateFileModeProductIon(args);
    }

    private void DoUpdateFileModePeptide()
    {
      var fileItem = GetFileModeItem();

      UpdateMRMPairedPeptideItemEventArgs args = new UpdateMRMPairedPeptideItemEventArgs(fileItem.PairedPeptide, viewOption);

      OnUpdateFileModePeptideItem(args);
    }

    private SrmFileItem GetFileModeItem()
    {
      CurrencyManager cm = (CurrencyManager)this.BindingContext[filePeptide.ProductIonPairs];

      SrmPairedProductIon item = cm.Current as SrmPairedProductIon;

      var fileItem = files.FindFileItem(item);
      return fileItem;
    }

    private void gvProductPair_SelectionChanged(object sender, EventArgs e)
    {
      DoUpdateFileModeProductIon();
      DoUpdateFileModePeptide();
    }

    private void zgcPeptide_ContextMenuBuilder(ZedGraphControl sender, ContextMenuStrip menuStrip, Point mousePt, ZedGraphControl.ContextMenuObjectState objState)
    {
      for (int i = 0; i < sender.MasterPane.PaneList.Count; i++)
      {
        if (sender.MasterPane.PaneList[i].Rect.Contains(mousePt))
        {
          menuStrip.Items.Add("-");
          menuStrip.Items.Add(mnuSetScanEnabledOnly);
          mnuSetScanEnabledOnly.Tag = sender.MasterPane.PaneList[i];
          return;
        }
      }
    }

    private void zgcCompoundTransition_ContextMenuBuilder(ZedGraphControl sender, ContextMenuStrip menuStrip, Point mousePt, ZedGraphControl.ContextMenuObjectState objState)
    {
      if (sender.MasterPane.PaneList[0].Rect.Contains(mousePt))
      {
        menuStrip.Items.Add("-");
        menuStrip.Items.Add(mnuSetScanEnabledOnly);
        mnuSetScanEnabledOnly.Tag = sender.MasterPane.PaneList[0];
      }
    }

    private void zgcTransaction_ContextMenuBuilder(ZedGraphControl sender, ContextMenuStrip menuStrip, Point mousePt, ZedGraphControl.ContextMenuObjectState objState)
    {
      menuStrip.Items.Add("-");
      menuStrip.Items.Add(mnuSetScanEnabledOnly);

      mnuSetScanEnabledOnly.Tag = zgcFileModeTransaction.GraphPane;
    }

    private void mnuSetScanEnabledOnly_Click(object sender, EventArgs e)
    {
      GraphPane graphScans = mnuSetScanEnabledOnly.Tag as GraphPane;
      double min = graphScans.XAxis.Scale.Min;
      double max = graphScans.XAxis.Scale.Max;

      SrmPairedPeptideItem item;
      SrmFileItem fileitem;
      if (graphScans.Tag is SrmPairedProductIon)
      {
        SrmPairedProductIon ion = graphScans.Tag as SrmPairedProductIon;

        fileitem = files.FindFileItem(ion);
        item = fileitem.PairedPeptide;
      }
      else
      {
        item = graphScans.Tag as SrmPairedPeptideItem;
        fileitem = files.FindFileItem(item.ProductIonPairs.First(m => m != null));
      }

      item.SetEnabledRetentionTimeRange(min, max);

      fileitem.PairedResult.Update(item);
      fileitem.PairedResult.Modified = true;

      DoDataChanged(true);

      gvProductPair.Refresh();
      DoUpdateFileModeProductIon();
      DoUpdateFileModePeptide();

      gvFiles.Refresh();
      DoUpdateCompoundGraph();
    }

    private void gvFiles_CellBeginEdit(object sender, DataGridViewCellCancelEventArgs e)
    {
      if (e.ColumnIndex > 0)
      {
        e.Cancel = true;
      }
    }

    private void gvFiles_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
    {
      if (e.ColumnIndex < FirstProductIonColumn)
      {
        tcCompoundGraph.SelectedIndex = 0;
      }
      else
      {
        tcCompoundGraph.SelectedIndex = e.ColumnIndex - FirstProductIonColumn + 1;
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

    private void DoUpdateCompoundGraph()
    {
      if (compoundPeptide == null)
      {
        return;
      }

      UpdateMRMPairedPeptideItemEventArgs args = new UpdateMRMPairedPeptideItemEventArgs(compoundPeptide, viewOption);

      if (CompoundModePeptideItemUpdate != null)
      {
        CompoundModePeptideItemUpdate(this, args);
      }
    }

    private void gvFiles_SelectionChanged(object sender, EventArgs e)
    {
      if (gvFiles.SelectedRows.Count == 0)
      {
        return;
      }

      var fis = currentItems[gvFiles.SelectedRows[0].Index].Items;

      compoundPeptide = new SrmPairedPeptideItem();
      for (int i = 0; i < fis.Count; i++)
      {
        if (fis[i] == null)
        {
          compoundPeptide.ProductIonPairs.Add(null);
        }
        else
        {
          compoundPeptide.ProductIonPairs.Add(fis[i].PairedProductIon);
        }
      }

      DoUpdateCompoundGraph();
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
        if (fileitem == null || double.IsNaN(fileitem.PairedProductIon.Ratio) || double.IsInfinity(fileitem.PairedProductIon.Ratio))
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

    private void mnuGreenLine_Click(object sender, EventArgs e)
    {
      ViewGreenLine = !ViewGreenLine;
    }

    private void mnuDecoy_Click(object sender, EventArgs e)
    {
      ViewDecoy = !ViewDecoy;
    }

    private void mnuRatioByArea_Click(object sender, EventArgs e)
    {
      RatioByArea = !RatioByArea;
    }

    private void mnuOption_DropDownOpening(object sender, EventArgs e)
    {
      mnuRatioByArea.Checked = RatioByArea;
    }

    public void ExportStatistic(string fileName)
    {
      using (StreamWriter sw = new StreamWriter(fileName))
      {
        Predicate<SrmPairedPeptideItem> validDecoyAccept = m => m.IsDecoy && m.Enabled;
        Predicate<SrmPairedPeptideItem> invalidTargetAccept = pep => !pep.IsDecoy && !pep.Enabled;
        Predicate<SrmPairedPeptideItem> validTargetAccept = pep => !pep.IsDecoy && pep.Enabled;

        //Predicate<SrmPairedPeptideItem> validDecoyAccept = m => m.ProductIonPairs[0].Light.ObjectName.Equals("DECOY") && m.Enabled;
        //Predicate<SrmPairedPeptideItem> invalidTargetAccept = m => !m.ProductIonPairs[0].Light.ObjectName.Equals("DECOY") && !m.Enabled;
        //Predicate<SrmPairedPeptideItem> validTargetAccept = m => !m.ProductIonPairs[0].Light.ObjectName.Equals("DECOY") && m.Enabled;
        sw.Write("Compound");
        foreach (var pr in pairedResults)
        {
          sw.Write("\t" + pr.PureFileName);
        }
        sw.WriteLine();

        if (pairedResults.Any(m => m.Any(n => n.IsDecoy)))
        {
          WriteCount(sw, validDecoyAccept, "Valid decoy");
        }

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
        sw.Write("\t{0}", count);
      }
      sw.WriteLine();
    }

    private CompoundItem FindCompoundByPeptide(SrmPairedPeptideItem item)
    {
      return files.Compounds.FirstOrDefault(m => m.HasPeptideItem(item));
    }

    public void ExportFileModeHtml(string targetDir)
    {
      if (pairedResults == null)
      {
        return;
      }

      btnFileMode.PerformClick();

      var oldViewDecoy = ViewDecoy;
      ViewDecoy = true;
      try
      {
        var result = new List<string>();

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

            foreach (TabPage tab in tcFiles.TabPages)
            {
              if (tab.Text.Equals(pr.PureFileName))
              {
                result.Add(mainFile);

                tcFiles.SelectedTab = tab;

                using (StreamWriter sw = new StreamWriter(mainFile))
                {
                  sw.WriteLine("<html>");
                  sw.WriteLine("<head>");
                  sw.WriteLine("<title>{0} Quantification Result</title>", pr.PureFileName);
                  sw.WriteLine("</head>");
                  sw.WriteLine("<body>");

                  sw.WriteLine("Options<br>");
                  sw.WriteLine("<pre>");
                  sw.WriteLine(pr.Options.ToXml().ToString().Replace("<", "&lt;").Replace(">", "&gt;"));
                  sw.WriteLine("</pre>");
                  sw.WriteLine("<hr>");

                  Predicate<SrmPairedPeptideItem> accept = m => m.IsDecoy && m.Enabled;
                  var validDecoy = GetCount(pr, accept);
                  if (validDecoy > 0)
                  {
                    sw.WriteLine("{0} valid decoy entries (blue highlighted)<br>", validDecoy);
                    Output(sw, pr, accept, imageDir, imageDirName, Color.Blue);
                  }

                  GC.Collect();
                  GC.WaitForFullGCComplete();

                  accept = pep => !pep.IsDecoy && !pep.Enabled;
                  var invalidTarget = GetCount(pr, accept);
                  if (invalidTarget > 0)
                  {
                    sw.WriteLine("{0} invalid target entries (red highlighted)<br>", invalidTarget);
                    Output(sw, pr, accept, imageDir, imageDirName, Color.Red);
                  }

                  GC.Collect();
                  GC.WaitForFullGCComplete();

                  accept = pep => !pep.IsDecoy && pep.Enabled;
                  var validTarget = GetCount(pr, accept);
                  if (validTarget > 0)
                  {
                    sw.WriteLine("{0} valid target entries<br>", validTarget);
                    Output(sw, pr, accept, imageDir, imageDirName, Color.Black);
                  }

                  GC.Collect();
                  GC.WaitForFullGCComplete();

                  sw.WriteLine("</body>");
                  sw.WriteLine("</html>");
                }
              }
            }
          }
          catch (Exception ex)
          {
            Console.WriteLine(ex.StackTrace);
            MessageBox.Show(this, MyConvert.Format("Error when saving {0} : {1}", mainFile, ex.Message), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            return;
          }
        }
      }
      finally
      {
        ViewDecoy = oldViewDecoy;
      }

      MessageBox.Show(this, "Srm result html format saved.", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
    }

    //public void ExportCompoundModeHtml(string targetDir)
    //{
    //  if (pairedResults == null)
    //  {
    //    return;
    //  }

    //  btnCompoundMode.PerformClick();

    //  var oldViewDecoy = ViewDecoy;
    //  ViewDecoy = true;
    //  try
    //  {
    //    var mainFile = new FileInfo(targetDir + "\\compound.html").FullName;
    //    var imageDir = FileUtils.ChangeExtension(mainFile, ".images");

    //    try
    //    {
    //      var imageDirName = new DirectoryInfo(imageDir).Name;
    //      if (!Directory.Exists(imageDir))
    //      {
    //        Directory.CreateDirectory(imageDir);
    //      }

    //      using (StreamWriter sw = new StreamWriter(mainFile))
    //      {
    //        sw.WriteLine("<html>");
    //        sw.WriteLine("<head>");
    //        sw.WriteLine("<title>Compound Quantification Result</title>");
    //        sw.WriteLine("</head>");
    //        sw.WriteLine("<body>");

    //        Predicate<CompoundItem> accept = m => m.IsDecoy && m.Enabled;
    //        var validDecoy = GetCompoundCount(visibleCompounds, accept);
    //        if (validDecoy > 0)
    //        {
    //          sw.WriteLine("{0} valid decoy entries (blue highlighted)<br>", validDecoy);
    //          OutputCompound(sw, visibleCompounds, accept, imageDir, imageDirName, Color.Blue);
    //        }

    //        GC.Collect();
    //        GC.WaitForFullGCComplete();

    //        accept = pep => !pep.IsDecoy && !pep.Enabled;
    //        var invalidTarget = GetCount(pr, accept);
    //        if (invalidTarget > 0)
    //        {
    //          sw.WriteLine("{0} invalid target entries (red highlighted)<br>", invalidTarget);
    //          Output(sw, pr, accept, imageDir, imageDirName, Color.Red);
    //        }

    //        GC.Collect();
    //        GC.WaitForFullGCComplete();

    //        accept = pep => !pep.IsDecoy && pep.Enabled;
    //        var validTarget = GetCount(pr, accept);
    //        if (validTarget > 0)
    //        {
    //          sw.WriteLine("{0} valid target entries<br>", validTarget);
    //          Output(sw, pr, accept, imageDir, imageDirName, Color.Black);
    //        }

    //        GC.Collect();
    //        GC.WaitForFullGCComplete();

    //        sw.WriteLine("</body>");
    //        sw.WriteLine("</html>");
    //      }
    //    }
    //    catch (Exception ex)
    //    {
    //      Console.WriteLine(ex.StackTrace);
    //      MessageBox.Show(this, MyConvert.Format("Error when saving {0} : {1}", mainFile, ex.Message), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
    //      return;
    //    }
    //  }
    //  finally
    //  {
    //    ViewDecoy = oldViewDecoy;
    //  }

    //  MessageBox.Show(this, "Srm result html format saved.", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
    //}

    //private void OutputCompound(StreamWriter sw, List<CompoundItem> list, Predicate<CompoundItem> accept, string imageDir, string imageDirName, Color color)
    //{
    //  CurrencyManager cm = (CurrencyManager)this.BindingContext[visibleCompounds];
    //  var valid = new HashSet<CompoundItem>(from p in list
    //                                                where accept(p)
    //                                                select p);
    //  for (int index = 0; index < visibleCompounds.Count; index++)
    //  {
    //    var comp = visibleCompounds[index];

    //    if (valid.Contains(comp))
    //    {
    //      cm.Position = index;

    //      //gvCompounds.Refresh();
    //      //gvCompounds_SelectionChanged(null, null);

    //      //gvProductPair.Refresh();
    //      //gvProductPair_SelectionChanged(null, null);

    //      //MessageBox.Show(comp.ObjectName + " " + comp.PrecursurFormula);

    //      WritePeptide(sw, pep, color);

    //      WriteImage(imageDir, imageDirName, sw, pep);
    //    }
    //  }
    //}

    //private int GetCompoundCount(List<CompoundItem> list, Predicate<CompoundItem> accept)
    //{
    //  return list.Count(m => accept(m));
    //}

    private void Output(StreamWriter sw, SrmPairedResult node, Predicate<SrmPairedPeptideItem> accept, string imageDir, string imageDirName, Color color)
    {
      CurrencyManager cm = (CurrencyManager)this.BindingContext[visibleCompounds];
      var valid = new HashSet<SrmPairedPeptideItem>(from p in node
                                                    where accept(p)
                                                    select p);
      for (int index = 0; index < visibleCompounds.Count; index++)
      {
        var comp = visibleCompounds[index];
        var pep = comp.GetPeptideItem(node.PureFileName);

        if (valid.Contains(pep))
        {
          cm.Position = index;

          //gvCompounds.Refresh();
          //gvCompounds_SelectionChanged(null, null);

          //gvProductPair.Refresh();
          //gvProductPair_SelectionChanged(null, null);

          //MessageBox.Show(comp.ObjectName + " " + comp.PrecursurFormula);

          WritePeptide(sw, pep, color);

          WriteImage(imageDir, imageDirName, sw, pep);
        }
      }
    }

    private int GetCount(SrmPairedResult node, Predicate<SrmPairedPeptideItem> pred)
    {
      return node.Count(m => pred(m));
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
        if (double.IsNaN(ion.Ratio) || double.IsInfinity(ion.Ratio))
        {
          sw.WriteLine("<td></td>");
          sw.WriteLine("<td></td>");
        }
        else
        {
          sw.WriteLine("<td>{0:0.0000}</td>", ion.Ratio);
          sw.WriteLine("<td>{0:0.0000}</td>", ion.RegressionCorrelation);
        }
        sw.WriteLine("<td>{0}</td>", ion.EnabledScanCount);
        sw.WriteLine("</tr>");
      }
      sw.WriteLine("</table>");
    }

    private void WriteImage(string imageDir, string imageDirName, StreamWriter sw, SrmPairedPeptideItem pep)
    {
      var imageFile = MyConvert.Format("{0}\\{1}.png", imageDir, pep.PrecursorFormula);
      var image = zgcFileModePeptide.GetImage();
      try
      {
        image.Save(imageFile, ImageFormat.Png);
      }
      finally
      {
        image.Dispose();
      }
      sw.WriteLine("<img src=\"{0}/{1}.png\" width=\"800\" /><br>", imageDirName, pep.PrecursorFormula);
      sw.WriteLine("<hr>");
    }

    private void mnuFile_DropDownOpening(object sender, EventArgs e)
    {
      mnuSaveChanges.Enabled = IsDataModified();
      mnuExportResult.Enabled = pairedResults != null;
    }

    private void mnuValidOnly_Click(object sender, EventArgs e)
    {
      ViewValidOnly = !ViewValidOnly;
    }

    private void SrmValidator2UI_FormClosing(object sender, FormClosingEventArgs e)
    {
      SaveOption();
    }

    private void mnuHighlightCurrent_Click(object sender, EventArgs e)
    {
      ViewCurrentHighlight = !ViewCurrentHighlight;
    }
  }
}