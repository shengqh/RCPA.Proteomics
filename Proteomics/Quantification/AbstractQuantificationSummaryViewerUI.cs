using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using RCPA.Gui;
using RCPA.Gui.Command;
using RCPA.Gui.FileArgument;
using RCPA.Gui.Image;
using RCPA.Proteomics.Mascot;
using RCPA.Proteomics.Summary;
using RCPA.Utils;
using ZedGraph;
using RCPA.Proteomics.PropertyConverter;
using RCPA.Proteomics.Quantification.SILAC;

namespace RCPA.Proteomics.Quantification
{
  public partial class AbstractQuantificationSummaryViewerUI : AbstractUI
  {
    private static readonly Color OUTLIER_COLOR = Color.SkyBlue;

    protected IIdentifiedResultTextFormat format = new MascotResultTextFormat();

    protected List<int> peptideColumnIndecies = new List<int>();

    protected List<string> peptideColumns = new List<string>();

    protected List<int> proteinColumnIndecies = new List<int>();

    protected List<string> proteinColumns = new List<string>();

    protected RcpaFileField summaryFile;

    protected bool blItemCheckBlocking;

    protected bool bUpdatingPeptide;

    protected bool bUpdatingProtein;

    protected IIdentifiedResult mr;

    private IEnumerable<IIdentifiedSpectrum> validSpectra;

    protected Dictionary<IIdentifiedSpectrum, List<IIdentifiedProteinGroup>> pepProMap;

    protected string summaryFilename;

    protected IQuantificationSummaryOption option;

    protected IProteinRatioCalculator calc;

    protected IQuantificationPeptideForm peptideForm;

    protected SaveFileArgument exportFile;

    public event UpdateQuantificationItemEvent UpdatePeptide;

    public event UpdateQuantificationItemEvent UpdateProtein;

    public event UpdateQuantificationItemEvent UpdateResult;

    private ListViewColumnField lvcProteins;

    private ListViewColumnField lvcPeptides;

    private bool bFirstLoad;

    [RcpaOption("ScanHeaders", RcpaOptionType.StringList)]
    public List<string> ExportScanHeaders { get; set; }

    [RcpaOption("ScoreDecimal", RcpaOptionType.Int32)]
    public int ScoreDecimal { get; set; }

    [RcpaOption("DiffDecimal", RcpaOptionType.Int32)]
    public int DiffDecimal { get; set; }

    public AbstractQuantificationSummaryViewerUI()
    {
      InitializeComponent();

      this.mr = null;
      this.pepProMap = null;
      this.ScoreDecimal = 0;
      this.DiffDecimal = 3;

      InsertButton(1, btnSave);
      InsertButton(2, btnExport);
      InsertButton(2, btnView);

      this.btnSave.Enabled = false;
      this.btnExport.Enabled = false;

      lvcProteins = new ListViewColumnField(lvProteins, "LvProteins");
      AddComponent(lvcProteins);

      lvcPeptides = new ListViewColumnField(lvPeptides, "LvPeptides");
      AddComponent(lvcPeptides);
      this.ExportScanHeaders = new List<string>();

      bFirstLoad = true;
    }

    protected override void OnAfterLoadOption(EventArgs e)
    {
      base.OnAfterLoadOption(e);

      ResetSpectrumFactory();
    }

    protected void InitializeSummaryFile()
    {
      this.summaryFile = new RcpaFileField(this.btnSummaryFile, this.txtSummaryFile, "SummaryFile", GetSummaryOpenFileArgument(), true);
      AddComponent(this.summaryFile);
    }

    protected override void DoRealGo()
    {
      this.summaryFilename = this.summaryFile.FullName;

      this.mr = this.format.ReadFromFile(this.summaryFilename);

      if (this.format.ProteinFormat.Headers.Contains("Reference\t"))
      {
        this.format.ProteinFormat.Headers = this.format.ProteinFormat.Headers.Replace("Reference\t", "Name\tDescription\t");
      }

      if (!this.format.PeptideFormat.Headers.Contains("Obs\t"))
      {
        this.format.PeptideFormat.Headers = this.format.PeptideFormat.Headers.Replace("MH+\t", "Obs\tMH+\t");
      }

      ProcessIdentifiedResult(mr);

      RefreshAll();
    }

    protected virtual void RefreshAll()
    {
      validSpectra = from p in mr.GetSpectra()
                     where option.IsPeptideRatioValid(p)
                     select p;

      this.pepProMap = this.mr.GetPeptideProteinGroupMap();

      this.lvProteins.BeginUpdate();
      this.bUpdatingProtein = true;
      try
      {
        if (bFirstLoad)
        {
          var allColumns = this.format.ProteinFormat.GetHeader().Split('\t').ToList();
          var lvColumns = lvProteins.GetColumnList().ConvertAll(m => m.Text);
          if (lvColumns.Count > 0)
          {
            this.proteinColumns = lvColumns;
          }
          else
          {
            this.proteinColumns = allColumns;
          }

          allColumns = this.format.PeptideFormat.GetHeader().Split('\t').ToList();
          lvColumns = lvPeptides.GetColumnList().ConvertAll(m => m.Text);
          if (lvColumns.Count > 0)
          {
            this.peptideColumns = lvColumns;
          }
          else
          {
            this.peptideColumns = allColumns;
          }

          bFirstLoad = false;
        }

        FillListViewColumns(this.lvProteins, this.format.ProteinFormat.GetHeader(), this.proteinColumns, this.proteinColumnIndecies);

        this.lvProteins.Items.Clear();

        if (this.mr.Count == 0 || this.mr[0].Count == 0 || this.mr[0][0].Peptides.Count == 0)
        {
          return;
        }

        FillListViewColumns(this.lvPeptides, this.format.PeptideFormat.GetHeader(), this.peptideColumns, this.peptideColumnIndecies);

        foreach (IIdentifiedProteinGroup mpg in this.mr)
        {
          bool bEnabled = mpg[0].GetSpectra().Find(m => option.IsPeptideRatioValid(m)) != null;

          if (!bEnabled)
          {
            continue;
          }

          IIdentifiedProtein mp = mpg[0];
          ListViewItem item = this.lvProteins.Items.Add("");

          UpdateProteinEntry(item, mpg, 0);
        }
      }
      finally
      {
        this.lvProteins.EndUpdate();
        this.bUpdatingProtein = false;
      }

      if (lvProteins.Items.Count > 0)
      {
        lvProteins.Items[0].Selected = true;
      }
      else
      {
        DoUpdateResult();
      }

      this.btnSave.Enabled = false;
      this.btnExport.Enabled = true;
      this.btnView.Enabled = true;
    }

    protected virtual void ProcessIdentifiedResult(IIdentifiedResult mr)
    { }

    private void FillListViewColumns(ListView listView, string line, List<string> sortedColumns,
                                     List<int> sortedColumnIndecies)
    {
      sortedColumns.Remove("");
      var columns = new List<string>(line.Split(new[] { '\t' }));
      sortedColumns.RemoveAll(m => !columns.Contains(m));
      sortedColumnIndecies.Clear();
      sortedColumnIndecies.AddRange(sortedColumns.ConvertAll(m => columns.IndexOf(m)));
      sortedColumns.Insert(0, "");

      //record old width
      var dic = new Dictionary<string, int>();
      for (int i = 0; i < listView.Columns.Count; i++)
      {
        dic[listView.Columns[i].Text] = listView.Columns[i].Width;
      }

      for (int i = 0; i < listView.Columns.Count & i < sortedColumns.Count; i++)
      {
        listView.Columns[i].Text = sortedColumns[i];
      }

      for (int i = listView.Columns.Count; i < sortedColumns.Count; i++)
      {
        listView.Columns.Add(sortedColumns[i]);
      }

      for (int i = 0; i < listView.Columns.Count; i++)
      {
        if (dic.ContainsKey(listView.Columns[i].Text))
        {
          listView.Columns[i].Width = dic[listView.Columns[i].Text];
        }
        else
        {
          listView.Columns[i].Width = 100;
        }
      }

      while (listView.Columns.Count > sortedColumns.Count)
      {
        listView.Columns.RemoveAt(listView.Columns.Count - 1);
      }
    }

    private void UpdateProteinEntry(ListViewItem item, IIdentifiedProteinGroup mpg, int index)
    {
      IIdentifiedProtein mp = mpg[index];
      var parts = new List<string>(this.format.ProteinFormat.GetString(mp).Split(new[] { '\t' }));

      if (item.SubItems.Count != this.proteinColumnIndecies.Count)
      {
        item.SubItems.Clear();
        item.Text = MyConvert.Format("${0}-{1}", mpg.Index, index + 1);
        for (int i = 0; i < proteinColumnIndecies.Count; i++)
        {
          item.SubItems.Add(parts[proteinColumnIndecies[i]]);
        }
      }
      else
      {
        for (int i = 0; i < proteinColumnIndecies.Count; i++)
        {
          item.SubItems[i].Text = parts[proteinColumnIndecies[i]];
        }
      }
      item.Checked = option.IsProteinRatioValid(mpg[0]) && !option.IsProteinOutlier(mpg[0]);

      item.Tag = mpg;

      UpdateProteinColor(mpg[index], item);
    }

    private void UpdatePeptideHit(IIdentifiedSpectrum mph, ListViewItem item)
    {
      item.SubItems.Clear();
      var parts = new List<string>(this.format.PeptideFormat.GetString(mph).Split(new[] { '\t' }));

      for (int i = 0; i < peptideColumnIndecies.Count; i++)
      {
        item.SubItems.Add(parts[peptideColumnIndecies[i]]);
      }
      item.Checked = option.IsPeptideRatioValid(mph) && !option.IsPeptideOutlier(mph);
      item.Tag = mph;

      UpdatePeptideColor(mph, item);
    }

    private void UpdateAllColor()
    {
      foreach (ListViewItem item in lvProteins.Items)
      {
        UpdateProteinColor((item.Tag as IIdentifiedProteinGroup)[0], item);
      }

      foreach (ListViewItem item in lvPeptides.Items)
      {
        UpdatePeptideColor(item.Tag as IIdentifiedSpectrum, item);
      }
    }

    private void UpdateProteinColor(IIdentifiedProtein protein, ListViewItem item)
    {
      if (option.IsProteinOutlier(protein))
      {
        item.BackColor = OUTLIER_COLOR;
      }
      else
      {
        item.BackColor = item.ListView.BackColor;
      }
    }

    private void UpdatePeptideColor(IIdentifiedSpectrum mph, ListViewItem item)
    {
      if (option.IsPeptideOutlier(mph))
      {
        item.BackColor = OUTLIER_COLOR;
      }
      else
      {
        item.BackColor = item.ListView.BackColor;
      }
    }

    protected void lvPeptides_MouseDoubleClick(object sender, MouseEventArgs e)
    {
      if (this.lvPeptides.SelectedItems.Count == 0)
      {
        return;
      }

      ShowPeptideInfo(this.lvPeptides.SelectedItems[0].Tag as IIdentifiedSpectrum);
    }

    protected virtual void ShowPeptideInfo(IIdentifiedSpectrum mph)
    {
      var ratioFile = GetRatioFile(mph);
      if (ratioFile == null)
      {
        return;
      }

      try
      {
        if (this.peptideForm == null || this.peptideForm.IsDisposed)
        {
          this.peptideForm = option.CreateForm();
          this.peptideForm.UpdateParent = UpdateFile;
        }
        this.peptideForm.Show();
        this.peptideForm.SetSummaryFilename(ratioFile, mph);
        this.peptideForm.BringToFront();
      }
      catch (Exception ex)
      {
        MessageBox.Show(this, ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
      }
    }

    private void lvProteins_SelectedIndexChanged(object sender, EventArgs e)
    {
      if (this.bUpdatingProtein)
      {
        return;
      }

      for (int i = 0; i < lvProteins.Items.Count; i++)
      {
        (lvProteins.Items[i].Tag as IIdentifiedProteinGroup).Selected = lvProteins.Items[i].Selected;
      }

      DoUpdateResult();

      this.lvPeptides.BeginUpdate();
      try
      {
        this.bUpdatingPeptide = true;

        this.lvPeptides.Items.Clear();
        if (this.lvProteins.SelectedItems.Count == 0)
        {
          return;
        }

        var mpg = this.lvProteins.SelectedItems[0].Tag as IIdentifiedProteinGroup;

        List<IIdentifiedSpectrum> mphs = mpg[0].GetSpectra();
        foreach (IIdentifiedSpectrum mph in mphs)
        {
          if (option.HasPeptideRatio(mph))
          {
            ListViewItem item = this.lvPeptides.Items.Add("");
            item.Tag = mph;

            UpdatePeptideHit(mph, item);
          }
        }
      }
      finally
      {
        this.bUpdatingPeptide = false;
        this.lvPeptides.EndUpdate();
      }

      DoUpdateProtein();

      lvPeptides.Items[0].Selected = true;
    }

    private void lvPeptides_ItemChecked(object sender, ItemCheckedEventArgs e)
    {
      if (this.bUpdatingPeptide)
      {
        return;
      }

      if (e.Item == null)
      {
        return;
      }

      var spectrum = e.Item.Tag as IIdentifiedSpectrum;
      option.SetPeptideRatioValid(spectrum, e.Item.Checked);

      UpdatePeptideColor(spectrum, e.Item);

      UpdateProteinContainsPeptide(spectrum);

      DoUpdateResult();

      DoUpdateProtein();

      this.btnSave.Enabled = true;
    }

    private void lvPeptides_SelectedIndexChanged(object sender, EventArgs e)
    {
      foreach (var p in validSpectra)
      {
        p.Selected = false;
      }

      for (int i = 0; i < lvPeptides.Items.Count; i++)
      {
        if (lvPeptides.Items[i].Selected)
        {
          var spectrum = lvPeptides.Items[i].Tag as IIdentifiedSpectrum;
          spectrum.Selected = true;
        }
      }

      DoUpdateResult();

      DoUpdateProtein();

      DoUpdatePeptide();
    }

    private void DoUpdatePeptide()
    {
      if (this.mr != null)
      {
        if (tabPeptide == tabControl1.SelectedTab)
        {
          if (lvPeptides.SelectedItems.Count == 0)
          {
            return;
          }

          var mph = lvPeptides.SelectedItems[0].Tag as IIdentifiedSpectrum;
          var ratioFile = GetRatioFile(mph);

          if (ratioFile == null || ratioFile.Equals("-"))
          {
            return;
          }

          var summary = option.ReadRatioFile(ratioFile);
          var e = new UpdateQuantificationItemEventArgs(option, summary);

          OnUpdatePeptide(e);
        }
      }
    }

    protected void OnUpdatePeptide(UpdateQuantificationItemEventArgs e)
    {
      if (UpdatePeptide != null)
      {
        UpdatePeptide(this, e);
      }
    }

    private void DoUpdateProtein()
    {
      if (this.mr != null)
      {
        if (tabProtein == tabControl1.SelectedTab)
        {
          if (lvProteins.SelectedItems.Count == 0)
          {
            return;
          }

          var mpg = lvProteins.SelectedItems[0].Tag as IIdentifiedProteinGroup;

          var e = new UpdateQuantificationItemEventArgs(option, mpg);

          OnUpdateProtein(e);
        }
      }
    }

    protected void OnUpdateProtein(UpdateQuantificationItemEventArgs e)
    {
      if (UpdateProtein != null)
      {
        UpdateProtein(this, e);
      }
    }

    private void DoUpdateResult()
    {
      if (this.mr != null)
      {
        if (tabProteins == tabControl1.SelectedTab || tabPeptides == tabControl1.SelectedTab)
        {
          var e = new UpdateQuantificationItemEventArgs(option, mr);

          OnUpdateResult(e);
        }
      }
    }

    protected void OnUpdateResult(UpdateQuantificationItemEventArgs e)
    {
      if (UpdateResult != null)
      {
        UpdateResult(this, e);
      }
    }

    private void zgcProtein_MouseClick(object sender, MouseEventArgs e)
    {
      ZedGraphicExtension.SetListViewItemVisible(this.lvPeptides, sender, e, true);
    }

    private void zgcProtein_MouseDoubleClick(object sender, MouseEventArgs e)
    {
      var mph = ZedGraphicExtension.GetObject(sender as ZedGraphControl, e) as IIdentifiedSpectrum;

      if (null != mph)
      {
        ShowPeptideInfo(mph);
      }
    }

    private void lvProteins_ItemChecked(object sender, ItemCheckedEventArgs e)
    {
      if (this.bUpdatingProtein)
      {
        return;
      }

      if (e.Item == null)
      {
        return;
      }

      var group = e.Item.Tag as IIdentifiedProteinGroup;
      foreach (IIdentifiedProtein protein in group)
      {
        option.SetProteinRatioValid(protein, e.Item.Checked);
      }

      UpdateProteinColor(group[0], e.Item);

      DoUpdateResult();

      DoUpdateProtein();

      this.btnSave.Enabled = true;
    }

    private void zgcProteins_MouseClick(object sender, MouseEventArgs e)
    {
      ZedGraphicExtension.SetListViewItemVisible(this.lvProteins, sender, e, true);
    }

    private void UpdateFile(IAnnotation annotation)
    {
      if (!(annotation is IIdentifiedSpectrum))
      {
        return;
      }

      var mph = annotation as IIdentifiedSpectrum;

      UpdateProteinContainsPeptide(mph);

      bool bFound = false;
      foreach (ListViewItem item in this.lvPeptides.Items)
      {
        if (item.Tag == mph)
        {
          UpdatePeptideHit(mph, item);
          bFound = true;
        }
      }

      if (bFound)
      {
        DoUpdatePeptide();
      }

      this.btnSave.Enabled = true;
    }

    private void UpdateProteinContainsPeptide(IIdentifiedSpectrum mph)
    {
      List<IIdentifiedProteinGroup> groups = this.pepProMap[mph];

      foreach (IIdentifiedProteinGroup group in groups)
      {
        calc.Calculate(group, m => true);
        UpdateProteinEntries(group);
      }

      DoUpdateResult();

      DoUpdateProtein();
    }

    private void UpdateProteinEntries(IIdentifiedProteinGroup group)
    {
      try
      {
        this.lvProteins.BeginUpdate();

        var items = new List<ListViewItem>();
        foreach (ListViewItem item in this.lvProteins.Items)
        {
          if (item.Tag == group)
          {
            items.Add(item);
          }
        }

        for (int i = 0; i < group.Count && i < items.Count; i++)
        {
          UpdateProteinEntry(items[i], group, i);
        }
      }
      finally
      {
        this.lvProteins.EndUpdate();
      }
    }

    private void lvPeptides_ItemCheck(object sender, ItemCheckEventArgs e)
    {
      if (this.blItemCheckBlocking) //*GM v.2.5.3 IWReq678
      {
        // handling ItemCheck event blicking
        e.NewValue = e.CurrentValue;

        this.blItemCheckBlocking = !this.blItemCheckBlocking;
      }
    }

    private void lvPeptides_MouseDown(object sender, MouseEventArgs e)
    {
      if (e.Clicks >= 2)
      {
        //double click
        this.blItemCheckBlocking = true; //*GM v.2.5.3 IWReq678
      }
    }

    private void btnSave_Click(object sender, EventArgs e)
    {
      this.format.WriteToFile(this.summaryFilename, this.mr);
      this.btnSave.Enabled = false;
    }

    private void btnExport_Click(object sender, EventArgs e)
    {
      FileDialog dialog = exportFile.GetFileDialog();
      if (dialog.ShowDialog(this) == DialogResult.OK)
      {
        IFileWriter<IIdentifiedResult> resultFormat = GetExportFileWriter();

        if (resultFormat == null)
        {
          return;
        }

        try
        {
          resultFormat.WriteToFile(dialog.FileName, mr);
          MessageBox.Show(this, "File " + dialog.FileName + " exported!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
        catch (Exception ex)
        {
          MessageBox.Show(this, "Export " + dialog.FileName + " failed : " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
      }
    }

    private bool IsRelativeDir(string ratioFile)
    {
      return ratioFile.Contains("\\") || ratioFile.Contains("/");
    }

    protected virtual string GetRatioFile(IIdentifiedSpectrum mph)
    {
      string ratioFile;
      if (mph.GetQuantificationItem() != null)
      {
        ratioFile = mph.GetQuantificationItem().Filename;
      }
      else
      {
        ratioFile = (string)mph.Annotations[option.RatioFileKey];
      }

      if (string.IsNullOrEmpty(ratioFile) || ratioFile.Equals("-"))
      {
        return null;
      }

      FileInfo fi = new FileInfo(summaryFilename);
      string result;
      if (IsRelativeDir(ratioFile))
      {
        result = fi.DirectoryName + "/" + ratioFile;
      }
      else
      {
        result = fi.DirectoryName + "/" + GetDetailDirectoryName() + "/" + ratioFile;
      }

      result = new FileInfo(result).FullName;

      if (!File.Exists(result))
      {
        MessageBox.Show(this, "Cannot find file " + result, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        return null;
      }

      return result;
    }

    protected virtual string GetExportConfigFileName()
    {
      throw new NotImplementedException("GetDetailDirectoryName");
    }

    protected virtual string GetDetailDirectoryName()
    {
      throw new NotImplementedException("GetDetailDirectoryName");
    }

    protected virtual OpenFileArgument GetSummaryOpenFileArgument()
    {
      return new OpenFileArgument("NotImplementedException", "NotImplementedException");
    }

    protected virtual IIdentifiedSpectrumWriter GetScanWriter(string detailDir)
    {
      throw new NotImplementedException("GetScanWriter");
    }

    protected virtual IIdentifiedSpectrumWriter GetScanWriter(string detailDir, string headers)
    {
      throw new NotImplementedException("GetScanWriter");
    }

    protected virtual QuantificationResultMultipleFileFormat DoGetMultipleFileFormat()
    {
      return new QuantificationResultMultipleFileFormat();
    }

    protected virtual IFileWriter<IIdentifiedResult> GetExportFileWriter()
    {
      QuantificationExportForm form = new QuantificationExportForm();
      form.ConfigFileName = GetExportConfigFileName();

      IIdentifiedSpectrumWriter writer = GetScanWriter("");
      var allheaders = (from h in writer.Header.Split('\t')
                        where !string.IsNullOrWhiteSpace(h)
                        select h).ToList();

      form.InitializeScan(allheaders, ExportScanHeaders);

      if (form.MyShowDialog(this) != DialogResult.OK)
      {
        return null;
      }

      form.SaveOption();

      if (form.IsExportScan)
      {
        var detailDir = new FileInfo(summaryFilename).DirectoryName + "/" + GetDetailDirectoryName();
        ExportScanHeaders = form.GetCheckedScanHeaders();
        writer = GetScanWriter(detailDir, ExportScanHeaders.Merge("\t"));
      }
      else
      {
        writer = null;
      }

      IIdentifiedResultTextFormat result;
      if (form.IsSingleFile)
      {
        result = GetSingleFileFormat(writer);
      }
      else
      {
        result = GetMultipleFileFormat(writer);
      }

      IIdentifiedProteinGroupWriter groupWriter;

      if (form.IsFilterProteinName)
      {
        string[] parts = form.ProteinNamePattern.Split(';');
        List<string> patterns = new List<string>();
        foreach (string part in parts)
        {
          if (part.Trim().Length > 0)
          {
            patterns.Add(part.Trim());
          }
        }
        groupWriter = new IdentifiedProteinGroupSingleLineWriter(patterns.ToArray());
      }
      else
      {
        groupWriter = new IdentifiedProteinGroupMultipleLineWriter();
      }

      result.GroupWriter = groupWriter;

      result.ValidGroup = (m => m[0].IsEnabled(true) && option.IsProteinRatioValid(m[0]) && !option.IsProteinOutlier(m[0]));

      result.ValidSpectrum = (m => m.IsEnabled(true) && option.IsPeptideRatioValid(m) && !option.IsPeptideOutlier(m));

      return result;
    }

    private IIdentifiedResultTextFormat GetMultipleFileFormat(IIdentifiedSpectrumWriter writer)
    {
      var result = DoGetMultipleFileFormat();
      result.ProteinFormat = format.ProteinFormat.GetLineFormat(proteinColumns.Merge("\t"));
      result.PeptideFormat = format.PeptideFormat.GetLineFormat(peptideColumns.Where(m => !string.IsNullOrWhiteSpace(m)).Merge("\t"));
      result.ScanWriter = writer;
      return result;
    }

    private IIdentifiedResultTextFormat GetSingleFileFormat(IIdentifiedSpectrumWriter writer)
    {
      QuantificationResultSingleFileFormat result = new QuantificationResultSingleFileFormat()
      {
        ScanWriter = writer,
      };
      result.ProteinFormat = format.ProteinFormat.GetLineFormat(proteinColumns.Merge("\t"));
      result.PeptideFormat = format.PeptideFormat.GetLineFormat(peptideColumns.Merge("\t"));
      return result;
    }

    protected virtual void InitializeDisplays()
    {
      zgcProteins.InitMasterPanel(this.CreateGraphics(), 2, "All Proteins");

      UpdateResult += new ZedGraphProteins(zgcProteins, zgcProteins.MasterPane.PaneList[0], "") { OutlierColor = OUTLIER_COLOR }.Update;

      UpdateResult += new ZedGraphProteinsLSPAD(zgcProteins, zgcProteins.MasterPane.PaneList[1], "") { OutlierColor = OUTLIER_COLOR }.Update;

      zgcPeptides.InitMasterPanel(this.CreateGraphics(), 2, "All Peptides");

      UpdateResult += new ZedGraphPeptides(zgcPeptides, zgcPeptides.MasterPane.PaneList[0], "") { OutlierColor = OUTLIER_COLOR }.Update;

      UpdateResult += new ZedGraphPeptidesLSPAD(zgcPeptides, zgcPeptides.MasterPane.PaneList[1], "") { OutlierColor = OUTLIER_COLOR }.Update;

      zgcProtein.InitMasterPanel(this.CreateGraphics(), 2, "Protein");

      UpdateProtein += new ZedGraphProtein(zgcProtein, zgcProtein.MasterPane.PaneList[0], "Peptide Level") { OutlierColor = OUTLIER_COLOR }.Update;
    }

    private void AbstractQuantificationSummaryViewerUI_Load(object sender, EventArgs e)
    {
      InitializeDisplays();
    }

    private void lvProteins_ItemCheck(object sender, ItemCheckEventArgs e)
    {
      if ((Control.ModifierKeys & Keys.Control) == Keys.Control || (Control.ModifierKeys & Keys.Shift) == Keys.Shift)
      {
        e.NewValue = e.CurrentValue;
      }
    }

    private void zgcPeptides_MouseClick(object sender, MouseEventArgs e)
    {
      var obj = zgcPeptides.GetObject(e) as Pair<IIdentifiedProteinGroup, IIdentifiedSpectrum>;

      if (obj == null)
      {
        return;
      }

      lvProteins.SetListViewItemVisible(obj.First, true);
      lvPeptides.SetListViewItemVisible(obj.Second, true);
    }

    private void zgcPeptides_MouseDoubleClick(object sender, MouseEventArgs e)
    {
      var obj = zgcPeptides.GetObject(e) as Pair<IIdentifiedProteinGroup, IIdentifiedSpectrum>;

      if (obj == null)
      {
        return;
      }

      ShowPeptideInfo(obj.Second);
    }

    private void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
    {
      DoUpdateResult();
      DoUpdateProtein();
      DoUpdatePeptide();
    }


    private void btnView_Click(object sender, EventArgs e)
    {
      SelectColumnsForm form = new SelectColumnsForm();
      form.ScoreDecimal = this.ScoreDecimal;
      form.DiffDecimal = this.DiffDecimal;

      var allProteinColumns = this.format.ProteinFormat.GetHeader().Split('\t').ToList();
      var allPeptideColumns = this.format.PeptideFormat.GetHeader().Split('\t').ToList();
      form.InitializeProteins(allProteinColumns, lvProteins.GetColumnList().ConvertAll(m => m.Text).ToList(), null);
      form.InitializePeptides(allPeptideColumns, lvPeptides.GetColumnList().ConvertAll(m => m.Text).ToList(), null);
      form.Text = "Select columns you want to view";

      if (form.ShowDialog() == System.Windows.Forms.DialogResult.OK)
      {
        this.ScoreDecimal = form.ScoreDecimal;
        this.DiffDecimal = form.DiffDecimal;

        ResetSpectrumFactory();

        this.proteinColumns = form.GetCheckedProteinColumns();
        FillListViewColumns(this.lvProteins, this.format.ProteinFormat.GetHeader(), this.proteinColumns, this.proteinColumnIndecies);
        this.peptideColumns = form.GetCheckedPeptideColumns();
        FillListViewColumns(this.lvPeptides, this.format.PeptideFormat.GetHeader(), this.peptideColumns, this.peptideColumnIndecies);

        this.SaveOption();

        DoRealGo();
        //lvProteins_SelectedIndexChanged(null, null);
      }
    }

    private void ResetSpectrumFactory()
    {
      var formatScore = StringUtils.GetDoubleFormat(this.ScoreDecimal);
      var formatDiff = StringUtils.GetDoubleFormat(this.DiffDecimal);

      var factory = IdentifiedSpectrumPropertyConverterFactory.GetInstance();
      factory.InsertConverter(new IdentifiedSpectrumTheoreticalMinusExperimentalMassConverter<IIdentifiedSpectrum>(formatDiff));
      factory.InsertConverter(new IdentifiedSpectrumScoreConverter<IIdentifiedSpectrum>(formatScore));
      this.format.PeptideFormat.Factory = factory;
    }
  }
}