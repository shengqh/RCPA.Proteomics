using System;
using System.Collections.Generic;
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
using RCPA.Proteomics.Quantification;
using RCPA.Proteomics.Summary;
using ZedGraph;
using RCPA.Utils;
using System.Diagnostics;
using RCPA.Proteomics.Quantification.O18;
using RCPA.Proteomics.PropertyConverter;

namespace RCPA.Proteomics.Quantification.SILAC
{
  public partial class SilacQuantificationSummaryViewerUI : AbstractQuantificationSummaryViewerUI
  {
    public static string title = "Silac Quantification Summary Viewer";

    public static string version = "1.0.9";

    private ZedGraphSilacProteinScan proteinScan;

    public SilacQuantificationSummaryViewerUI()
    {
      InitializeComponent();

      Text = Constants.GetSQHTitle(title, version);

      this.calc = new SilacProteinRatioCalculator();

      this.option = new SilacQuantificationSummaryOption();

      this.exportFile = new SaveFileArgument("SILAC Quantification Result", "ProSILACQuant");

      InitializeSummaryFile();

      var factory = IdentifiedSpectrumPropertyConverterFactory.GetInstance();
      factory.InsertConverter(new IdentifiedSpectrumTheoreticalMinusExperimentalMassConverter<IIdentifiedSpectrum>("{0:0.000}"));
      factory.InsertConverter(new IdentifiedSpectrumScoreConverter<IIdentifiedSpectrum>("{0:0}"));

      this.format.PeptideFormat = new LineFormat<IIdentifiedSpectrum>(factory, "");
    }

    protected override void OnAfterLoadOption(EventArgs e)
    {
      base.OnAfterLoadOption(e);
      this.option.MinimumRSquare = minRSquare.Value;
    }

    private void SilacQuantificationSummaryViewerUI_Load(object sender, EventArgs e)
    {
      UpdatePeptide += new ZedGraphExperimental_ScanPPMCorrelation(zgcExperimentalScans, CreateGraphics()).Update;

      proteinScan = new ZedGraphSilacProteinScan(zgcProtein, zgcProtein.MasterPane.PaneList.Last(), "Scan Level");
      UpdateProtein += proteinScan.Update;
    }

    protected override string GetExportConfigFileName()
    {
      return FileUtils.GetConfigDir().FullName + "\\SilacExportConfig.xml";
    }

    protected override IIdentifiedSpectrumWriter GetScanWriter(string detailDir)
    {
      return new SilacScanTextWriter(detailDir);
    }

    protected override IIdentifiedSpectrumWriter GetScanWriter(string detailDir, string headers)
    {
      return new SilacScanTextWriter(detailDir, headers);
    }

    protected override OpenFileArgument GetSummaryOpenFileArgument()
    {
      return new OpenFileArgument("SILAC Quantification Result", new string[] { ".SILACsummary" });
    }

    protected override string GetDetailDirectoryName()
    {
      return new FileInfo(this.summaryFilename).Name + ".details";
    }

    protected override void ProcessIdentifiedResult(IIdentifiedResult mr)
    {
      base.ProcessIdentifiedResult(mr);
      this.proteinScan.SummaryFilename = this.summaryFilename;
      this.proteinScan.DefaultDetailDirectory = this.GetDetailDirectoryName();

      var recalc = true;
      foreach (var mpg in mr)
      {
        if (option.IsProteinRatioValid(mpg[0]))
        {
          recalc = false;
          break;
        }
      }

      if (recalc)
      {
        foreach (var mpg in mr)
        {
          calc.Calculate(mpg, m => true);
        }

        this.format.InitializeByResult(mr);
      }
    }

    protected override QuantificationResultMultipleFileFormat DoGetMultipleFileFormat()
    {
      return new SilacQuantificationResultMultipleFileFormat();
    }

    #region Nested type: Command

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
        new SilacQuantificationSummaryViewerUI().MyShow();
      }

      #endregion

      #region IToolSecondLevelCommand Members

      public string GetSecondLevelCommandItem()
      {
        return "SILAC";
      }

      #endregion
    }

    #endregion

    protected override void RefreshAll()
    {
      this.option.MinimumRSquare = minRSquare.Value;

      foreach (var g in mr)
      {
        var spectra = g[0].GetSpectra();
        bool bChanged = false;
        foreach (var spec in spectra)
        {
          var item = spec.GetQuantificationItem();
          if (item != null)
          {
            var enabled = item.Correlation >= this.option.MinimumRSquare;
            if (item.Enabled != enabled)
            {
              item.Enabled = enabled;
              bChanged = true;
            }
          }
        }
        if (bChanged)
        {
          calc.Calculate(g, m => true);
        }
      }

      base.RefreshAll();
    }

    private void btnUpdate_Click(object sender, EventArgs e)
    {
      if (this.option.MinimumRSquare != minRSquare.Value)
      {
        RefreshAll();
      }
    }
  }
}