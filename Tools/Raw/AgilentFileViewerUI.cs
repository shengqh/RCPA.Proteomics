using System;
using System.Drawing;
using System.Windows.Forms;
using RCPA.Gui;
using RCPA.Gui.Command;
using RCPA.Gui.FileArgument;
using RCPA.Gui.Image;
using RCPA.Proteomics;
using RCPA.Proteomics.Raw;
using ZedGraph;
using RCPA.Proteomics.Spectrum;
using Agilent.MassSpectrometry.DataAnalysis;
using System.Text;

namespace RCPA.Tools.Raw
{
  public partial class AgilentFileViewerUI : AbstractFileProcessorUI
  {
    private static readonly string title = "Agilent File Viewer";
    private static readonly string version = "1.0.0";

    public AgilentFileViewerUI()
    {
      InitializeComponent();

      base.SetDirectoryArgument("RawDirectory", "Agilent Raw Directory");

      this.scan = new RcpaIntegerField(txtScan, "Scan", "Scan", 0, false);
      AddComponent(this.scan);

      this.minPeakIntensity = new RcpaDoubleField(txtMinPeakIntensity, "minPeakIntensity", "Minmum Peak Intensity", 1, true);
      AddComponent(this.minPeakIntensity);

      this.topPeak = new RcpaIntegerField(txtTopPeaks, "TopPeaks", "Top peak count", 100, true);
      AddComponent(this.topPeak);

      ZedGraphicExtension.InitGraph(this.zgcScan, "Scan", "M/Z", "Intensity", true, 0.0);
      this.zgcScan.GraphPane.XAxis.Scale.Min = 0;
      this.zgcScan.GraphPane.XAxis.Scale.Max = 2000;

      this.Text = Constants.GetSQHTitle(title, version);
    }

    IMsdrDataReader reader = new MassSpecDataReader();
    double[] retentionTimes;
    private int firstScan = 0;
    private int lastScan = 0;

    private RcpaIntegerField scan;
    private RcpaIntegerField topPeak;
    private RcpaDoubleField minPeakIntensity;

    protected override void DoRealGo()
    {
      reader.CloseDataFile();

      reader.OpenDataFile(GetOriginFile());

      firstScan = 1;

      lastScan = (int)reader.MSScanFileInformation.TotalScansPresent;

      retentionTimes = reader.GetRetentionTimes();

      txtScan.Text = firstScan.ToString();

      DisplayScan();
    }

    private void DisplayScan()
    {
      if (this.reader == null)
      {
        return;
      }

      try
      {
        this.scan.ValidateComponent();
        this.minPeakIntensity.ValidateComponent();

        double rt = retentionTimes[this.scan.Value - 1];

        PeakList<Peak> pkl = reader.GetPeakList(rt, this.minPeakIntensity.Value, topPeak.Value);

        try
        {
          ZedGraphicExtension.ClearData(this.zgcScan, false);

          var pplLight = new PointPairList();
          pkl.ForEach(p => pplLight.Add(p.Mz, p.Intensity));

          ZedGraphicExtension.AddIndividualLine(this.zgcScan, MyConvert.Format("Scan {0}", this.scan.Value), pplLight, Color.Blue, false);

          if (reader.GetMsLevel(rt) == 2)
          {
            this.zgcScan.GraphPane.Title.Text = MyConvert.Format("Precursor MZ = {0:0.0000}", pkl.PrecursorMZ);
          }
          else
          {
            this.zgcScan.GraphPane.Title.Text = string.Empty;
          }
        }
        finally
        {
          ZedGraphicExtension.UpdateGraph(this.zgcScan);
        }

        StringBuilder sb = new StringBuilder();
        pkl.ForEach(m => sb.AppendLine(MyConvert.Format("{0:0.0000}\t{1:0.00}", m.Mz, m.Intensity)));
        txtPeaks.Text = sb.ToString();
      }
      catch (Exception ex)
      {
        MessageBox.Show(ex.Message);
      }
    }

    public class Command : IToolCommand
    {
      #region IToolCommand Members

      public string GetClassification()
      {
        return MenuCommandType.Misc;
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
        new AgilentFileViewerUI().MyShow();
      }

      #endregion
    }

    private void txtScan_KeyUp(object sender, KeyEventArgs e)
    {
      if (e.KeyCode == Keys.Return)
      {
        DisplayScan();
      }
    }

    private int Scan
    {
      get
      {
        return scan.Value;
      }
      set
      {
        scan.Value = value;
      }
    }

    private void btnNext_Click(object sender, EventArgs e)
    {
      Scan = Scan + 1;

      if (Scan >= lastScan)
      {
        Scan = lastScan;
      }

      DisplayScan();
    }

    private void btnLast_Click(object sender, EventArgs e)
    {
      Scan = lastScan;

      DisplayScan();
    }

    private void btnPrev_Click(object sender, EventArgs e)
    {
      Scan = Scan - 1;

      if (Scan < firstScan)
      {
        Scan = firstScan;
      }

      DisplayScan();
    }

    private void btnFirst_Click(object sender, EventArgs e)
    {
      Scan = firstScan;

      DisplayScan();
    }

    private void RawFileViewerUI_KeyDown(object sender, KeyEventArgs e)
    {
      if (e.KeyCode == Keys.Right)
      {
        btnNext.PerformClick();
      }
      else if (e.KeyCode == Keys.Left)
      {
        btnPrev.PerformClick();
      }
      else if (e.KeyCode == Keys.Home)
      {
        btnFirst.PerformClick();
      }
      else if (e.KeyCode == Keys.End)
      {
        btnLast.PerformClick();
      }
    }
  }
}
