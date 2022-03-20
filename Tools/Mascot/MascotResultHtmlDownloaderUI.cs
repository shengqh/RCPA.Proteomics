using RCPA.Gui;
using RCPA.Gui.Command;
using RCPA.Gui.FileArgument;
using RCPA.Utils;
using System;

namespace RCPA.Tools.Mascot
{
  public partial class MascotResultHtmlDownloaderUI : AbstractFileProcessorUI
  {
    public static string title = "MascotResult Html Format Downloader";
    public static string version = "1.0.2";

    private static string peptideFormatUrl = "&REPTYPE=peptide&_sigthreshold={0}&REPORT=AUTO&_mudpit=1&_ignoreionsscorebelow={1}&_showsubsets=0&_showpopups=TRUE&_sortunassigned=scoredown&_requireboldred=0";

    private RcpaTextField url;
    private RcpaCheckBox peptideFormat;
    private RcpaDoubleField threshold;
    private RcpaDoubleField minScore;

    private bool fixPeptideDownload;

    public MascotResultHtmlDownloaderUI()
      : this(false)
    { }

    public MascotResultHtmlDownloaderUI(bool fixPeptideDownload)
    {
      InitializeComponent();

      base.SetFileArgument("MascotHtmlResult", new SaveFileArgument("Mascot Html Result", "html"));

      this.Text = Constants.GetSQHTitle(title, version);

      this.url = new RcpaTextField(txtUrl, "URL", "URL", "", true);
      this.peptideFormat = new RcpaCheckBox(cbPeptideFormat, "PeptideFormat", true);
      this.threshold = new RcpaDoubleField(txtThreshold, "Threshold", "Peptide Identification Threshold (0~0.1)", 0.05, fixPeptideDownload);
      this.minScore = new RcpaDoubleField(txtMinScore, "MinScore", "Min Score", 20, false);

      this.AddComponent(url);
      this.AddComponent(peptideFormat);
      this.AddComponent(threshold);
      this.AddComponent(minScore);

      this.fixPeptideDownload = fixPeptideDownload;

      if (fixPeptideDownload)
      {
        cbPeptideFormat.Checked = true;
        cbPeptideFormat.Visible = false;
      }
      else
      {
        this.AfterLoadOption += new EventHandler(cbPeptideFormat_CheckedChanged);
      }
    }

    protected override IFileProcessor GetFileProcessor()
    {
      string strUrl = url.Text;
      if (fixPeptideDownload || peptideFormat.Checked)
      {
        strUrl += MyConvert.Format(peptideFormatUrl, threshold.Value, minScore.Value);
      }

      return new FileDownload(strUrl, true);
    }

    private void cbPeptideFormat_CheckedChanged(object sender, EventArgs e)
    {
      if (threshold != null)
      {
        threshold.Required = cbPeptideFormat.Checked;
      }
    }
  }

  public class MascotResultHtmlDownloaderCommand : IToolCommand
  {
    private bool fixPeptideDownload;
    public MascotResultHtmlDownloaderCommand(bool fixPeptideDownload)
    {
      this.fixPeptideDownload = fixPeptideDownload;
    }
    #region IToolCommand Members

    public string GetClassification()
    {
      return MenuCommandType.Mascot;
    }

    public string GetCaption()
    {
      return MascotResultHtmlDownloaderUI.title;
    }

    public string GetVersion()
    {
      return MascotResultHtmlDownloaderUI.version;
    }

    public void Run()
    {
      new MascotResultHtmlDownloaderUI(fixPeptideDownload).MyShow();
    }

    #endregion
  }

}

