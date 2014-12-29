using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using RCPA.Gui;
using RCPA;
using RCPA.Gui.FileArgument;
using RCPA.Gui.Command;
using System.IO;
using RCPA.Proteomics.Quantification;

namespace RCPA.Proteomics.Quantification.O18
{
  public partial class O18QuantificationFileProcessorUI : AbstractFileProcessorUI
  {
    public static string title = "O18 Quantification Calculator";
    public static string version = "1.1.7";

    private RcpaDirectoryField rawDir;
    private RcpaDoubleField purityOfWater;
    private RcpaDoubleField precursorPPMTolerance;
    private RcpaCheckBox postDigestionLabelling;
    private RcpaCheckBox limitScanRange;
    private RcpaDoubleField scanStart;
    private RcpaDoubleField scanEnd;
    private RcpaComboBox<string> rawExtensions;

    public O18QuantificationFileProcessorUI()
    {
      InitializeComponent();

      base.SetFileArgument("MascotTextResult", new OpenFileArgument("Identified Result", new string[] { "txt", "noredundant" }));

      this.Text = Constants.GetSQHTitle(title, version);
      this.rawDir = new RcpaDirectoryField(btnRawDirectory, txtRawDirectory, "RawDir", "Raw File", false);
      this.purityOfWater = new RcpaDoubleField(txtPurityOfO18Water, "PurityOfO18Water", "purity of O18 water", 0.95, true);
      this.precursorPPMTolerance = new RcpaDoubleField(txtPrecursorTolerance, "PrecursorPPMTolerance", "precursor PPM tolerance", 50, true);
      this.postDigestionLabelling = new RcpaCheckBox(cbPostDigestionLabelling, "PostDigestionLabelling", false);
      this.limitScanRange = new RcpaCheckBox(cbScanRange, "LimitScanRange", false);
      this.scanStart = new RcpaDoubleField(txtScanStart, "ScanStart", "Limit scan start percentage", 20, false);
      this.scanEnd = new RcpaDoubleField(txtScanEnd, "ScanEnd", "Limit scan end percentage", 80, false);
      this.rawExtensions = new RcpaComboBox<string>(cbRawFormat, "RawFormat", new string[] { ".raw", ".mzXML", ".mzData", ".mzData.xml" }, 0);

      this.scanStart.MinValue = 0.0;
      this.scanStart.MaxValue = 100.0;
      this.scanEnd.MinValue = 0.0;
      this.scanEnd.MaxValue = 100.0;

      this.AddComponent(rawDir);
      this.AddComponent(purityOfWater);
      this.AddComponent(precursorPPMTolerance);
      this.AddComponent(postDigestionLabelling);
      this.AddComponent(limitScanRange);
      this.AddComponent(scanStart);
      this.AddComponent(scanEnd);
      AddComponent(this.rawExtensions);
    }

    protected override void DoBeforeValidate()
    {
      this.scanStart.Required = this.limitScanRange.Checked;
      this.scanEnd.Required = this.limitScanRange.Checked;
    }

    protected override string GetOriginFile()
    {
      return base.GetOriginFile() + ".O18Params";
    }

    protected override IFileProcessor GetFileProcessor()
    {
      O18QuantificationFileProcessorOptions options = new O18QuantificationFileProcessorOptions();
      options.PPMTolerance = precursorPPMTolerance.Value;
      options.PurityOfO18Water = purityOfWater.Value;
      options.ProteinFile = base.GetOriginFile();
      if (rawDir.FullName == "")
      {
        options.RawDirectory = new FileInfo(options.ProteinFile).DirectoryName;
      }
      else
      {
        options.RawDirectory = rawDir.FullName;
      }
      options.RawExtension = rawExtensions.SelectedItem;

      options.SoftwareVersion = this.Text;
      options.IsPostDigestionLabelling = postDigestionLabelling.Checked;
      options.IsScanLimited = limitScanRange.Checked;
      if (options.IsScanLimited)
      {
        options.ScanPercentageStart = scanStart.Value;
        options.ScanPercentageEnd = scanEnd.Value;
      }
      options.Save(this.GetOriginFile());

      return new O18QuantificationFileProcessor();
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
        new O18QuantificationFileProcessorUI().MyShow();
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

