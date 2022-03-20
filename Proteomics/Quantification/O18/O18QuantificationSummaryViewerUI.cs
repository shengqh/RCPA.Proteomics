using RCPA.Gui.Command;
using RCPA.Gui.FileArgument;
using RCPA.Proteomics.PropertyConverter;
using RCPA.Proteomics.Summary;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace RCPA.Proteomics.Quantification.O18
{
  public partial class O18QuantificationSummaryViewerUI : AbstractQuantificationSummaryViewerUI
  {
    public static string title = "O18 Quantification Summary Viewer";

    public static string version = "1.4.7";

    private Dictionary<Pair<IIdentifiedSpectrum, IIdentifiedProteinGroup>, ListViewItem> pepItemMap = new Dictionary<Pair<IIdentifiedSpectrum, IIdentifiedProteinGroup>, ListViewItem>();

    private ZedGraphO18ProteinScan proteinScan;

    public O18QuantificationSummaryViewerUI()
    {
      InitializeComponent();

      this.Text = Constants.GetSQHTitle(title, version);

      this.exportFile = new SaveFileArgument("O18 Quantification Result", "ProO18Quant");

      InitializeSummaryFile();

      var factory = IdentifiedSpectrumPropertyConverterFactory.GetInstance();
      factory.InsertConverter(new IdentifiedSpectrumTheoreticalMinusExperimentalMassConverter<IIdentifiedSpectrum>("{0:0.000}"));
      factory.InsertConverter(new IdentifiedSpectrumScoreConverter<IIdentifiedSpectrum>("{0:0}"));

      this.format.PeptideFormat = new LineFormat<IIdentifiedSpectrum>(factory, "");
    }

    protected override string GetExportConfigFileName()
    {
      return FileUtils.GetConfigDir().FullName + "\\O18ExportConfig.xml";
    }

    protected override OpenFileArgument GetSummaryOpenFileArgument()
    {
      return new OpenFileArgument("O18 Quantification Result", new string[] { ".O18summary" });
    }

    protected override IIdentifiedSpectrumWriter GetScanWriter(string detailDir)
    {
      return new O18ScanTextWriter(detailDir);
    }

    protected override IIdentifiedSpectrumWriter GetScanWriter(string detailDir, string headers)
    {
      return new O18ScanTextWriter(detailDir, headers);
    }

    protected override void ProcessIdentifiedResult(IIdentifiedResult mr)
    {
      this.option = new O18QuantificationSummaryViewerOptions(this.summaryFilename);

      this.calc = option.GetProteinRatioCalculator();


      calc.SummaryFileDirectory = Path.GetDirectoryName(this.summaryFilename);
      calc.DetailDirectory = this.GetDetailDirectoryName();

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
        calc.Calculate(mr, m => true);

        this.format.InitializeByResult(mr);
      }
    }

    protected override string GetDetailDirectoryName()
    {
      FileInfo fi = new FileInfo(summaryFilename);
      return Regex.Replace(fi.Name, "O18.summary$|O18summary$", "details");
    }

    private void O18QuantificationSummaryViewerUI_Load(object sender, EventArgs e)
    {
      UpdatePeptide += new ZedGraphO18Experimental_ScanPPM(zgcExperimentalScans, CreateGraphics()).Update;

      //zgcProtein.AddPanel();
      proteinScan = new ZedGraphO18ProteinScan(zgcProtein, zgcProtein.MasterPane.PaneList.Last(), "Scan Level");
      UpdateProtein += proteinScan.Update;
    }

    protected override QuantificationResultMultipleFileFormat DoGetMultipleFileFormat()
    {
      return new O18QuantificationResultMultipleFileFormat();
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
        return O18QuantificationSummaryViewerUI.title;
      }

      public string GetVersion()
      {
        return O18QuantificationSummaryViewerUI.version;
      }

      public void Run()
      {
        new O18QuantificationSummaryViewerUI().MyShow();
      }

      #endregion

      #region IToolSecondLevelCommand Members

      public string GetSecondLevelCommandItem()
      {
        return "O18";
      }

      #endregion
    }
  }
}

