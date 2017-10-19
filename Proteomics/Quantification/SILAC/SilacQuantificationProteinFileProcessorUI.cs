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
using RCPA.Proteomics.Summary;
using RCPA.Proteomics.Mascot;
using RCPA.Proteomics.Sequest;
using RCPA.Proteomics.Raw;

namespace RCPA.Proteomics.Quantification.SILAC
{
  public partial class SilacQuantificationProteinFileProcessorUI : AbstractFileProcessorUI
  {
    public static readonly string title = "Silac Quantification Calculator";
    public static readonly string version = "1.0.9";

    private RcpaDirectoryField rawDir;
    private RcpaDoubleField precursorPPMTolerance;
    private RcpaFileField silacFile;
    private RcpaComboBox<IRawFormat> rawFormats;
    private RcpaTextField ignoreModifications;
    private RcpaDoubleField minCorrelation;
    private RcpaIntegerField _profileLength;

    public SilacQuantificationProteinFileProcessorUI()
    {
      InitializeComponent();

      base.SetFileArgument("IdentificationResult", new OpenFileArgument("Identification Result", new string[] { "txt", "noredundant" }));

      this.Text = Constants.GetSQHTitle(title, version);
      rawDir = new RcpaDirectoryField(btnRawDirectory, txtRawDirectory, "RawDir", "Raw File", false);
      rawFormats = new RcpaComboBox<IRawFormat>(comboBox1, "RawFormat", new IRawFormat[] { new ThermoRawFormat() }, 0);
      precursorPPMTolerance = new RcpaDoubleField(txtPrecursorTolerance, "PrecursorPPMTolerance", "precursor PPM tolerance", 50, true);
      silacFile = new RcpaFileField(btnSilacFile, txtSilacFile, "SilacFile", new OpenFileArgument("SILAC Configuration", "ini"), true);
      ignoreModifications = new RcpaTextField(textBox1, "IgnoreModifications", "Ignore Modifications (such like @ for Heavy labelling of Leu)", "", false);
      minCorrelation = new RcpaDoubleField(txtMinPepRegCorrelation, "MinCorrelation", "Minimum Peptide Regression Correlation", 0.8, true);
      _profileLength = new RcpaIntegerField(txtProfileLength, "ProfileLength", "Profile length used in calculation", 3, true);

      this.AddComponent(rawDir);
      this.AddComponent(rawFormats);
      this.AddComponent(precursorPPMTolerance);
      this.AddComponent(silacFile);
      this.AddComponent(ignoreModifications);
      this.AddComponent(minCorrelation);
      this.AddComponent(_profileLength);
    }

    protected override void OnAfterLoadOption(EventArgs e)
    {
      base.OnAfterLoadOption(e);
      if (this.silacFile.FullName == "")
      {
        this.silacFile.FullName = new FileInfo(FileUtils.AppPath() + "\\Forward_Lys_isotope.ini").FullName;
      }
    }
    protected override IFileProcessor GetFileProcessor()
    {
      double ppmTolerance = precursorPPMTolerance.Value;

      string rawDirectory;
      if (rawDir.FullName == "")
      {
        rawDirectory = new FileInfo(GetOriginFile()).DirectoryName;
      }
      else
      {
        rawDirectory = rawDir.FullName;
      }

      return new SilacQuantificationProteinFileProcessor(
        new SilacQuantificationOption()
        {
          RawFormat = rawFormats.SelectedItem,
          RawDir = rawDirectory,
          SilacParamFile = silacFile.FullName,
          PPMTolerance = ppmTolerance,
          IgnoreModifications = ignoreModifications.Text,
          ProfileLength = _profileLength.Value,
          KeepPeptideWithMostScan = rbKeepPeptideWithMostScan.Checked
        })
      {
        MinPeptideRegressionCorrelation = minCorrelation.Value
      };
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
        return SilacQuantificationProteinFileProcessorUI.title;
      }

      public string GetVersion()
      {
        return SilacQuantificationProteinFileProcessorUI.version;
      }

      public void Run()
      {
        new SilacQuantificationProteinFileProcessorUI().MyShow();
      }

      #endregion

      #region IToolSecondLevelCommand Members

      public string GetSecondLevelCommandItem()
      {
        return "SILAC";
      }

      #endregion
    }
  }
}

