using System;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using RCPA.Proteomics.Raw;
using ZedGraph;
using RCPA.Proteomics.Spectrum;
using RCPA.Tools.Quantification;
using RCPA.Tools.Quantification.O18;
using RCPA.Gui.Command;

namespace RCPA.Proteomics.Quantification.O18
{
  public partial class O18QuantificationSummaryItemForm : Form, IQuantificationPeptideForm
  {
    private bool bShowSummary;
    private ISampleAbundanceCalculator calc;
    private IFileFormat<O18QuantificationSummaryItem> fileFormat;
    private string o18SummaryFilename = "";

    private IAnnotation parentObj;
    private bool rawFileExist;

    private IQuantificationSummaryOption options;

    private O18QuantificationSummaryItem summary;

    public O18QuantificationSummaryItemForm()
    {
      InitializeComponent();
    }

    public O18QuantificationSummaryItemForm(string o18SummaryFilename, IAnnotation parentObj)
      : this()
    {
      SetSummaryFilename(o18SummaryFilename, parentObj);
    }

    public UpdateEvent UpdateParent { get; set; }

    public event UpdateQuantificationItemEvent UpdateItem;

    protected void OnUpdateItem(UpdateQuantificationItemEventArgs e)
    {
      if (UpdateItem != null)
      {
        UpdateItem(this, e);
      }
    }

    private void DoUpdateItem()
    {
      foreach (ListViewItem item in lvExperimentalScans.Items)
      {
        (item.Tag as QuanEnvelope).IsSelected = item.Selected;
      }

      var e = new UpdateQuantificationItemEventArgs(options, summary);

      OnUpdateItem(e);
    }

    public void SetSummaryFilename(string o18SummaryFilename, IAnnotation parentObj)
    {
      if (this.o18SummaryFilename.Equals(o18SummaryFilename))
      {
        return;
      }

      this.parentObj = parentObj;

      if (this.summary != null)
      {
        ClearAllData();
      }

      this.fileFormat = new O18QuantificationSummaryItemXmlFormat();
      this.summary = this.fileFormat.ReadFromFile(o18SummaryFilename);

      this.o18SummaryFilename = o18SummaryFilename;
      Text = o18SummaryFilename;

      this.calc = this.summary.GetCalculator();
      this.options = new O18QuantificationSummaryViewerOptions(o18SummaryFilename);

      ShowSummary(false);

      this.btnSave.Enabled = false;
    }

    private void ClearAllData()
    {
      ClearData(this.zgcTheoreticalIsotopic, true);
      ClearData(this.zgcExperimentalScans, true);
      ClearData(this.zgcExperimentalIndividualScan, true);
      ClearData(this.zgcRatio, true);
      ClearData(this.zgcRegression, true);
    }

    private void ShowSummary(bool bUpdate)
    {
      this.bShowSummary = true;
      try
      {
        if (!bUpdate)
        {
          this.txtPeptideSequence.Text = this.summary.PeptideSequence;
          this.txtCompositionFormula.Text = this.summary.PeptideAtomComposition;
          this.txtPurityOfO18Water.Text = this.summary.PurityOfO18Water.ToString();
          this.txtPostDigestionLabeling.Text = this.summary.IsPostDigestionLabelling.ToString();

          this.rawFileExist = File.Exists(this.summary.RawFilename);

          UpdateTheoreticalIsotopicGraph();
          ShowExperimentalScans();
        }

        this.txtRatio.Text = this.summary.SampleAbundance.Ratio.ToString();
        this.txtRegressionCorrelation.Text = this.summary.SpeciesAbundance.RegressionCorrelation.ToString();
        this.txtLabellingEfficiency.Text = this.summary.SampleAbundance.LabellingEfficiency.ToString();

        ShowRatio();
        ShowRegression();
      }
      finally
      {
        this.bShowSummary = false;
      }
    }

    private void O18PurityQuantificationResultForm_Load(object sender, EventArgs e)
    {
      UpdateItem += new ZedGraphO18Experimental_ScanPPM(zgcExperimentalScans, CreateGraphics()).Update;
      UpdateItem += new ZedGraphO18IndividualScan(zgcExperimentalIndividualScan).Update;

      InitGraph(this.zgcTheoreticalIsotopic, "Theoretical Isotopic", "Isotopic Position", "Abundance Percentage", true,
                0.0);
      InitGraph(this.zgcRatio, "O16/O18 Ratio", "", "Intensity", false, 1.0);
      InitGraph(this.zgcRegression, "Observed vs Regression", "m/z", "Intensity", true, 0.0);
    }

    private void InitGraph(ZedGraphControl zgc, string zgcTitle, string xTitle, string yTitle,
                           bool bClusterScaleWidthAuto, double dClusterScaleWidth)
    {
      // Get a reference to the GraphPane instance in the ZedGraphControl
      GraphPane myPane = zgc.GraphPane;

      // Set the titles and axis labels
      myPane.Title.Text = zgcTitle;
      myPane.XAxis.Title.Text = xTitle;
      myPane.YAxis.Title.Text = yTitle;

      // Enable scrollbars if needed
      //zgc.IsShowHScrollBar = true;
      //zgc.IsShowVScrollBar = true;
      zgc.IsAutoScrollRange = true;

      zgc.GraphPane.BarSettings.ClusterScaleWidthAuto = bClusterScaleWidthAuto;
      if (!bClusterScaleWidthAuto)
      {
        zgc.GraphPane.BarSettings.ClusterScaleWidth = dClusterScaleWidth;
      }

      UpdateGraph(zgc);
    }

    private void UpdateGraph(ZedGraphControl zgc)
    {
      // Tell ZedGraph to calculate the axis ranges
      // Note that you MUST call this after enabling IsAutoScrollRange, since AxisChange() sets
      // up the proper scrolling parameters
      zgc.AxisChange();

      // Make sure the Graph gets redrawn
      zgc.Invalidate();
    }

    private void SetDataToBar(ZedGraphControl zgc, PointPairList list, String title, Color color)
    {
      ClearData(zgc, false);

      AddDataToBar(zgc, list, title, color, true);
    }

    private void AddDataToBar(ZedGraphControl zgc, PointPairList list, String title, Color color, bool update)
    {
      GraphPane myPane = zgc.GraphPane;

      BarItem barItem = myPane.AddBar(title, list, color);

      if (update)
      {
        UpdateGraph(zgc);
      }
    }

    private void ClearData(ZedGraphControl zgc, bool update)
    {
      GraphPane myPane = zgc.GraphPane;
      myPane.CurveList.Clear();
      myPane.GraphObjList.Clear();

      if (update)
      {
        zgc.RestoreScale(myPane);
        UpdateGraph(zgc);
      }
    }

    private void UpdateTheoreticalIsotopicGraph()
    {
      var ppl = new PointPairList();
      for (int i = 0; i < this.summary.PeptideProfile.Count; i++)
      {
        ppl.Add(this.summary.PeptideProfile[i].Mz, this.summary.PeptideProfile[i].Intensity);
      }

      ClearData(this.zgcTheoreticalIsotopic, false);
      AddDataToBar(this.zgcTheoreticalIsotopic, ppl, "Theoretical Isotopic Profile", Color.Red, true);
    }

    private void ShowExperimentalScans()
    {
      this.lvExperimentalScans.BeginUpdate();
      try
      {
        for (int i = 0; i < 6; i++)
        {
          this.lvExperimentalScans.Columns[i + 1].Text = MyConvert.Format("{0:0.0000}",
                                                                       this.summary.ObservedEnvelopes[0][i].Mz);
        }

        this.lvExperimentalScans.Items.Clear();
        foreach (var envelope in this.summary.ObservedEnvelopes)
        {
          ListViewItem item = this.lvExperimentalScans.Items.Add(envelope.ScanTimes[0].Scan.ToString());
          for (int i = 0; i < 6; i++)
          {
            item.SubItems.Add(MyConvert.Format("{0:0}", envelope[i].Intensity));
          }
          item.Checked = envelope.Enabled;
          item.BackColor = envelope.IsIdentified ? Color.Red : item.BackColor;
          item.Tag = envelope;
        }
      }
      finally
      {
        this.lvExperimentalScans.EndUpdate();
      }

      this.lvExperimentalScans.Items[0].Selected = true;
    }

    private void lvExperimentalScans_SelectedIndexChanged(object sender, EventArgs e)
    {
      if (this.lvExperimentalScans.SelectedItems.Count == 0)
      {
        return;
      }

      DoUpdateItem();
    }

    private void ShowRatio()
    {
      string[] labels = {"Before Purity Correction", "After Purity Correction"};

      var pplO16 = new[] {this.summary.SpeciesAbundance.O16, this.summary.SampleAbundance.O16};
      var pplO181 = new[] {this.summary.SpeciesAbundance.O181, 0};
      var pplO182 = new[] {this.summary.SpeciesAbundance.O182, this.summary.SampleAbundance.O18};

      ClearData(this.zgcRatio, false);

      this.zgcRatio.GraphPane.AddBar("O16", null, pplO16, Color.Red);
      this.zgcRatio.GraphPane.AddBar("O181", null, pplO181, Color.Green);
      this.zgcRatio.GraphPane.AddBar("O182", null, pplO182, Color.Blue);

      this.zgcRatio.GraphPane.XAxis.Scale.TextLabels = labels;
      this.zgcRatio.GraphPane.XAxis.Type = AxisType.Text;

      UpdateGraph(this.zgcRatio);
    }

    private void ShowRegression()
    {
      this.lvRegression.Items.Clear();
      foreach (SpeciesRegressionItem item in this.summary.SpeciesAbundance.RegressionItems)
      {
        ListViewItem lvItem = this.lvRegression.Items.Add(MyConvert.Format("{0:0.0000}", item.Mz));
        lvItem.SubItems.Add(MyConvert.Format("{0:0}", item.ObservedIntensity));
        lvItem.SubItems.Add(MyConvert.Format("{0:0}", item.RegressionIntensity));
      }

      var pplObserved = new PointPairList();
      var pplRegression = new PointPairList();
      for (int i = 0; i < 6; i++)
      {
        pplObserved.Add(this.summary.SpeciesAbundance.RegressionItems[i].Mz,
                        this.summary.SpeciesAbundance.RegressionItems[i].ObservedIntensity);
        pplRegression.Add(this.summary.SpeciesAbundance.RegressionItems[i].Mz,
                          this.summary.SpeciesAbundance.RegressionItems[i].RegressionIntensity);
      }

      ClearData(this.zgcRegression, false);

      AddDataToBar(this.zgcRegression, pplObserved, "Observed Envelope", Color.Red, false);
      AddDataToBar(this.zgcRegression, pplRegression, "Regression Envelope", Color.Blue, false);

      UpdateGraph(this.zgcRegression);
    }

    private void btnClose_Click(object sender, EventArgs e)
    {
      Close();
    }

    private void lvExperimentalScans_ItemChecked(object sender, ItemCheckedEventArgs e)
    {
      if (this.bShowSummary)
      {
        return;
      }

      if (e.Item == null)
      {
        return;
      }

      int scan = int.Parse(e.Item.Text);
      foreach (var envelope in this.summary.ObservedEnvelopes)
      {
        if (envelope.ScanTimes[0].Scan == scan && envelope.Enabled != e.Item.Checked)
        {
          envelope.Enabled = e.Item.Checked;
          this.summary.CalculateSpeciesAbundanceByLinearRegression();
          ShowSummary(true);
          this.btnSave.Enabled = true;
          break;
        }
      }

      DoUpdateItem();
    }

    private void btnSave_Click(object sender, EventArgs e)
    {
      this.summary.SoftwareVersion = Constants.GetSQHTitle(O18QuantificationFileProcessorUI.title, O18QuantificationFileProcessorUI.version);

      this.fileFormat.WriteToFile(this.o18SummaryFilename, this.summary);

      if (UpdateParent != null)
      {
        this.summary.AssignToAnnotation(this.parentObj, this.o18SummaryFilename);
        UpdateParent(this.parentObj);
      }

      this.btnSave.Enabled = false;
    }

    private void lvExperimentalScans_ItemCheck(object sender, ItemCheckEventArgs e)
    {
      if (this.bShowSummary)
      {
        return;
      }

      if (e.Index == -1)
      {
        return;
      }

      int scan = int.Parse(this.lvExperimentalScans.Items[e.Index].Text);

      int totalEnabled = (from en in this.summary.ObservedEnvelopes
                          where en.ScanTimes[0].Scan == scan
                          select en).Count();

      if (totalEnabled == 0)
      {
        e.NewValue = e.CurrentValue;
      }
    }

    private void mnuSetScanEnabled_Click(object sender, EventArgs e)
    {
      GraphPane graphScans = mnuSetScanEnabled.Tag as GraphPane;
      double min = graphScans.XAxis.Scale.Min;
      double max = graphScans.XAxis.Scale.Max;

      this.bShowSummary = true;
      try
      {
        summary.ObservedEnvelopes.ForEach(m => m.Enabled = m.Scan >= min && m.Scan <= max);
        summary.CalculateSpeciesAbundanceByLinearRegression();

        foreach (ListViewItem item in lvExperimentalScans.Items)
        {
          item.Checked = (item.Tag as O18QuanEnvelope).Enabled;
        }
      }
      finally
      {
        this.bShowSummary = false;
      }

      ShowSummary(true);
      this.btnSave.Enabled = true;

      DoUpdateItem();

      zgcExperimentalScans.MasterPane.PaneList.ForEach(m => zgcExperimentalScans.ZoomOutAll(m));
    }

    private void zgcExperimentalScans_ContextMenuBuilder(ZedGraphControl sender, ContextMenuStrip menuStrip, Point mousePt, ZedGraphControl.ContextMenuObjectState objState)
    {
      menuStrip.Items.Add(mnuSetScanEnabled);

      zgcExperimentalScans.MasterPane.PaneList.ForEach(m =>
      {
        if (m.Rect.Contains(mousePt))
        {
          mnuSetScanEnabled.Tag = m;
        }
      });
    }

    private void O18QuantificationSummaryItemForm_FormClosing(object sender, FormClosingEventArgs e)
    {
      if (btnSave.Enabled)
      {
        var ret = MessageBox.Show(this, "Data changed, do you want to save it?", "Warning", MessageBoxButtons.YesNoCancel);
        if (ret == DialogResult.Cancel)
        {
          e.Cancel = true;
          return;
        }

        if (ret == DialogResult.Yes)
        {
          btnSave.PerformClick();
        }
      }
    }
  }
}