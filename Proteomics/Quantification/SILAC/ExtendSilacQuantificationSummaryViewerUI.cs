using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
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
using RCPA.Proteomics.Quantification.SILAC;
using RCPA.Proteomics.Quantification.O18;
using RCPA.Tools.Quantification;

namespace RCPA.Proteomics.Quantification.SILAC
{
  public partial class ExtendSilacQuantificationSummaryViewerUI : AbstractQuantificationSummaryViewerUI
  {
    public static string title = "Extend Silac Quantification Summary Viewer";

    public static string version = "1.0.0";

    public ExtendSilacQuantificationSummaryViewerUI()
    {
      InitializeComponent();

      Text = Constants.GetSQHTitle(title, version);

      this.calc = new SilacProteinRatioCalculator();

      this.option = new SilacQuantificationSummaryOption();

      this.exportFile = new SaveFileArgument("SILAC Quantification Result", "ProSILACQuant");

      InitializeSummaryFile();
    }

    private void SilacQuantificationSummaryViewerUI_Load(object sender, EventArgs e)
    {
      UpdatePeptide += new ZedGraphExperimental_ScanPPMCorrelation(zgcExperimentalScans, CreateGraphics()).Update;
    }

    protected override IIdentifiedSpectrumWriter GetScanWriter(string detailDir)
    {
      return new SilacScanTextWriter(detailDir);
    }

    protected override OpenFileArgument GetSummaryOpenFileArgument()
    {
      return new OpenFileArgument("SILAC Quantification Result", new string[] { ".SILACsummary" });
    }

    protected override string GetDetailDirectoryName()
    {
      return new FileInfo(this.summaryFilename).Name + ".details";
    }

    protected override QuantificationResultMultipleFileFormat DoGetMultipleFileFormat()
    {
      return new SilacQuantificationResultMultipleFileFormat();
    }

    protected override void InitializeDisplays()
    {
      zgcProteins.InitMasterPanel(this.CreateGraphics(), 2, "All Proteins");

      //UpdateResult += new ZedGraphProteins(zgcProteins, zgcProteins.MasterPane.PaneList[0], "") { OutlierColor = OUTLIER_COLOR }.Update;

      //UpdateResult += new ZedGraphProteinsLSPAD(zgcProteins, zgcProteins.MasterPane.PaneList[1], "") { OutlierColor = OUTLIER_COLOR }.Update;

      zgcPeptides.InitMasterPanel(this.CreateGraphics(), 2, "All Peptides");

      //UpdateResult += new ZedGraphPeptides(zgcPeptides, zgcPeptides.MasterPane.PaneList[0], "") { OutlierColor = OUTLIER_COLOR }.Update;

      //UpdateResult += new ZedGraphPeptidesLSPAD(zgcPeptides, zgcPeptides.MasterPane.PaneList[1], "") { OutlierColor = OUTLIER_COLOR }.Update;

      zgcProtein.InitMasterPanel(this.CreateGraphics(), 2, "Protein");

      //UpdateProtein += new ZedGraphProtein(zgcProtein, zgcProtein.MasterPane.PaneList[0], "Peptide Level") { OutlierColor = OUTLIER_COLOR }.Update;
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
        new ExtendSilacQuantificationSummaryViewerUI().MyShow();
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
  }
}