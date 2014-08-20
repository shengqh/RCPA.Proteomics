using System;
using System.Windows.Forms;
using RCPA.Gui;
using RCPA.Gui.Command;
using RCPA.Gui.FileArgument;
using RCPA.Proteomics.Raw;
using RCPA.Proteomics.Spectrum;
using ZedGraph;

namespace RCPA.Tools.Raw
{
  public partial class RawFileViewerUI : AbstractFileProcessorUI
  {
    private const string Title = "Raw File Viewer";
    private const string Version = "1.0.2";
    private readonly RcpaIntegerField _scan;
    private readonly RcpaIntegerField _msLevel;

    private int _firstScan;
    private int _lastScan;
    private IRawFile _reader;

    public RawFileViewerUI()
    {
      InitializeComponent();

      base.SetFileArgument("RawFile", new OpenFileArgument("Raw", new[] {"raw", "mzData.xml", "mzData", "mzXML"}));

      _scan = new RcpaIntegerField(txtScan, "Scan", "Scan", 0, false);
      _msLevel = new RcpaIntegerField(txtMslevel, "MSLevel", "MS Level (0 ~ any level)", 0, false);

      zgcScan.InitGraph("Scan", "M/Z", "Intensity", true, 0.0);
      zgcScan.GraphPane.XAxis.Scale.Min = 0;
      zgcScan.GraphPane.XAxis.Scale.Max = 2000;

      Text = Constants.GetSQHTitle(Title, Version);
    }

    private int Scan
    {
      get { return _scan.Value; }
      set { _scan.Value = value; }
    }

    protected override void DoRealGo()
    {
      if (_reader != null)
      {
        _reader.Close();
      }

      _reader = RawFileFactory.GetRawFileReader(GetOriginFile());

      _firstScan = _reader.GetFirstSpectrumNumber();
      _lastScan = _reader.GetLastSpectrumNumber();

      txtScan.Text = _firstScan.ToString();
      DisplayScan(0);
    }

    private void DisplayScan(int increment)
    {
      if (_reader == null)
      {
        return;
      }

      try
      {
        _scan.ValidateComponent();

        try
        {
          zgcScan.ClearData(false);

          Scan += increment;

          var mslevel = _msLevel.Value;
          if (mslevel != 0)
          {
            var curlevel = _reader.GetMsLevel(Scan);
            while (curlevel != mslevel && Scan >= _firstScan && Scan <= _lastScan)
            {
              if (increment == 0)
              {
                increment = 1;
              }

              Scan += increment;
              curlevel = _reader.GetMsLevel(Scan);
            }
          }
          else
          {
            if (Scan < _firstScan)
            {
              Scan = _firstScan;
            }

            if (Scan > _lastScan)
            {
              Scan = _lastScan;
            }
          }
          if (!_reader.IsScanValid(Scan))
          {
            throw new Exception(MyConvert.Format("Scan {0} is not valid.", Scan));
          }

          if (_reader is RawFileImpl)
          {
            txtFilter.Text = (_reader as RawFileImpl).GetFilterForScanNum(Scan);
          }

          var pkl = _reader.GetPeakList(_scan.Value);
          bsPeak.DataSource = pkl;

          var pplLight = new PointPairList();
          pkl.ForEach(p => pplLight.Add(p.Mz, p.Intensity, p.Charge));

          zgcScan.GraphPane.AddIndividualLine(MyConvert.Format("Scan {0}; MS {1}", _scan.Value, _reader.GetMsLevel(_scan.Value)), pplLight);
        }
        finally
        {
          zgcScan.UpdateGraph();
        }
      }
      catch (Exception ex)
      {
        MessageBox.Show(ex.Message);
      }
    }

    private void txtScan_KeyUp(object sender, KeyEventArgs e)
    {
      if (e.KeyCode == Keys.Return)
      {
        DisplayScan(0);
      }
    }

    private void btnNext_Click(object sender, EventArgs e)
    {
      DisplayScan(1);
    }

    private void btnLast_Click(object sender, EventArgs e)
    {
      Scan = _lastScan;

      DisplayScan(0);
    }

    private void btnPrev_Click(object sender, EventArgs e)
    {
      DisplayScan(-1);
    }

    private void btnFirst_Click(object sender, EventArgs e)
    {
      Scan = _firstScan;

      DisplayScan(0);
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

    private void RawFileViewerUI_FormClosing(object sender, FormClosingEventArgs e)
    {
      if (_reader != null)
      {
        _reader.Close();
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
        return Title;
      }

      public string GetVersion()
      {
        return Version;
      }

      public void Run()
      {
        new RawFileViewerUI().MyShow();
      }

      #endregion
    }
  }
}