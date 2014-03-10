using System;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using RCPA.Gui.Image;
using RCPA.Proteomics.Raw;
using ZedGraph;
using RCPA.Proteomics.Spectrum;
using System.Collections.Generic;
using RCPA.Gui.Command;

namespace RCPA.Proteomics.Quantification.SILAC
{
  public partial class SilacQuantificationSummaryItemForm : Form, IQuantificationPeptideForm
  {
    private readonly IFileFormat<SilacQuantificationSummaryItem> fileFormat =
      new SilacQuantificationSummaryItemXmlFormat();

    private bool bShowSummary;

    private ItemCheckedEventHandler checkedEvent;
    private IAnnotation parentObj;
    private bool rawFileExist;

    private SilacQuantificationSummaryItem summary;
    private string summaryFilename = "";

    private IQuantificationSummaryOption option = new SilacQuantificationSummaryOption();

    public SilacQuantificationSummaryItemForm()
    {
      InitializeComponent();
    }

    public SilacQuantificationSummaryItemForm(string summaryFilename, IAnnotation parentObj)
      : this()
    {
      SetSummaryFilename(summaryFilename, parentObj);
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
        (item.Tag as SilacPeakListPair).IsSelected = item.Selected;
      }

      var e = new UpdateQuantificationItemEventArgs(option, summary);

      OnUpdateItem(e);
    }

    public void SetSummaryFilename(string summaryFilename, IAnnotation parentObj)
    {
      if (this.summaryFilename.Equals(summaryFilename))
      {
        return;
      }

      this.parentObj = parentObj;

      this.summary = this.fileFormat.ReadFromFile(summaryFilename);

      this.summaryFilename = summaryFilename;
      Text = summaryFilename;

      ShowSummary();

      this.btnSave.Enabled = false;
    }

    private void ShowSummary()
    {
      this.bShowSummary = true;
      try
      {
        this.txtPeptideSequence.Text = this.summary.PeptideSequence;
        this.textBox1.Text = this.summary.Charge.ToString();
        this.txtSampleComposition.Text = this.summary.SampleAtomComposition;
        this.txtReferenceComposition.Text = this.summary.ReferenceAtomComposition;
        this.txtRegressionCorrelation.Text = MyConvert.Format("{0:0.0000}", this.summary.RegressionCorrelation);
        this.txtRatio.Text = MyConvert.Format("{0:0.0000}", this.summary.Ratio);

        this.rawFileExist = File.Exists(this.summary.RawFilename);

        InitExperimentalScans();
      }
      finally
      {
        this.bShowSummary = false;
      }
    }

    private void UpdateSummary()
    {
      this.bShowSummary = true;
      try
      {
        this.txtRegressionCorrelation.Text = MyConvert.Format("{0:0.0000}", this.summary.RegressionCorrelation);
        this.txtRatio.Text = MyConvert.Format("{0:0.0000}", this.summary.Ratio);

        UpdateRatio();
      }
      finally
      {
        this.bShowSummary = false;
      }
    }

    private void SilacQuantificationResultForm_Load(object sender, EventArgs e)
    {
      UpdateItem += new ZedGraphExperimental_ScanPPMCorrelation(zgcExperimentalScans, CreateGraphics()).Update;
      UpdateItem += new ZedGraphIndividualScan(zgcExperimentalIndividualScan).Update;
      UpdateItem += new ZedGraphTheoreticalProfiles(zgcTheoreticalIsotopic).Update;
      UpdateItem += new ZedGraphSilacRegression(zgcSilacRegression, "Regression", "Reference Intensity", "Sample Intensity").Update;
    }

    private void InitExperimentalScans()
    {
      this.lvExperimentalScans.BeginUpdate();
      try
      {
        while (this.lvExperimentalScans.Columns.Count <= 2 * this.summary.ObservedEnvelopes[0].Light.Count)
        {
          this.lvExperimentalScans.Columns.Add("");
        }

        int index = 1;
        this.summary.ObservedEnvelopes[0].Light.ForEach(p => this.lvExperimentalScans.Columns[index++].Text = MyConvert.Format("{0:0.0000}", p.Mz));
        this.summary.ObservedEnvelopes[0].Heavy.ForEach(p => this.lvExperimentalScans.Columns[index++].Text = MyConvert.Format("{0:0.0000}", p.Mz));

        this.lvExperimentalScans.Items.Clear();
        foreach (SilacPeakListPair envelope in this.summary.ObservedEnvelopes)
        {
          ListViewItem item = this.lvExperimentalScans.Items.Add(envelope.Light.ScanTimes[0].Scan.ToString());
          for (int i = 0; i < envelope.Light.Count; i++)
          {
            item.SubItems.Add(MyConvert.Format("{0:0}", envelope.Light[i].Intensity));
          }
          for (int i = 0; i < envelope.Heavy.Count; i++)
          {
            item.SubItems.Add(MyConvert.Format("{0:0}", envelope.Heavy[i].Intensity));
          }
          item.Checked = envelope.Enabled;
          item.BackColor = envelope.IsIdentified ? SilacQuantificationConstants.IDENTIFIED_COLOR : item.BackColor;
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

      UpdateRatio();
    }


    private void UpdateRatio()
    {
      this.txtRegressionCorrelation.Text = MyConvert.Format("{0:0.0000}", this.summary.RegressionCorrelation);
      this.txtRatio.Text = MyConvert.Format("{0:0.0000}", this.summary.Ratio);
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

      var pair = e.Item.Tag as SilacPeakListPair;
      pair.Enabled = e.Item.Checked;
      this.btnSave.Enabled = true;

      this.summary.CalculateRatio();

      UpdateRatio();

      DoUpdateItem();
    }

    private void btnSave_Click(object sender, EventArgs e)
    {
      this.summary.SoftwareVersion = Constants.GetSQHTitle(ExtendSilacQuantificationProteinFileProcessorUI.title, ExtendSilacQuantificationProteinFileProcessorUI.version);
      this.fileFormat.WriteToFile(this.summaryFilename, this.summary);

      if (UpdateParent != null)
      {
        this.summary.AssignToAnnotation(this.parentObj, this.summaryFilename);
        UpdateParent(this.parentObj);
      }

      this.btnSave.Enabled = false;
    }

    private void mnuSelectedChecked_Click(object sender, EventArgs e)
    {
      SetSelectedScanChecked(true);
    }

    private void SetSelectedScanChecked(bool value)
    {
      this.bShowSummary = true;
      try
      {
        foreach (ListViewItem item in this.lvExperimentalScans.Items)
        {
          if (item.Selected)
          {
            item.Checked = value;
            (item.Tag as SilacPeakListPair).Enabled = value;
          }
        }
      }
      finally
      {
        this.bShowSummary = false;
      }

      var e = new ItemCheckedEventArgs(this.lvExperimentalScans.Items[0]);
      lvExperimentalScans_ItemChecked(null, e);
    }

    private void mnuSelectedUnchecked_Click(object sender, EventArgs e)
    {
      SetSelectedScanChecked(false);
    }

    private void lvExperimentalScans_MouseDown(object sender, MouseEventArgs e)
    {
      if (this.checkedEvent == null)
      {
        this.checkedEvent = lvExperimentalScans_ItemChecked;
        this.lvExperimentalScans.ItemChecked += this.checkedEvent;
      }
    }

    private void lvExperimentalScans_ItemCheck(object sender, ItemCheckEventArgs e)
    {
      if ((ModifierKeys & Keys.Shift) == Keys.Shift || (ModifierKeys & Keys.Control) == Keys.Control)
      {
        e.NewValue = e.CurrentValue;
        return;
      }
    }

    private void zgcRegression_MouseClick(object sender, MouseEventArgs e)
    {
      ZedGraphicExtension.SetListViewItemVisible(this.lvExperimentalScans, sender, e, true);
    }

    private void setSelectedScanEnabledOnlyToolStripMenuItem_Click(object sender, EventArgs e)
    {
      GraphPane graphScans = mnuSetScanEnabled.Tag as GraphPane;
      double min = graphScans.XAxis.Scale.Min;
      double max = graphScans.XAxis.Scale.Max;

      this.bShowSummary = true;
      try
      {
        summary.ObservedEnvelopes.ForEach(m => m.Enabled = m.Scan >= min && m.Scan <= max);
        summary.CalculateRatio();

        foreach (ListViewItem item in lvExperimentalScans.Items)
        {
          item.Checked = (item.Tag as SilacPeakListPair).Enabled;
        }
      }
      finally
      {
        this.bShowSummary = false;
      }

      DoUpdateItem();

      UpdateRatio();

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
  }
}