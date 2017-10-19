using RCPA.Gui;
using RCPA.Gui.Command;
using RCPA.Gui.FileArgument;
using RCPA.Proteomics.Mascot;
using RCPA.Proteomics.Raw;
using RCPA.Proteomics.Sequest;
using RCPA.Proteomics.Summary;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace RCPA.Proteomics.Quantification.SILAC
{
  public partial class ExtendSilacQuantificationProteinFileProcessorUI : AbstractFileProcessorUI
  {
    public static readonly string title = "Extend Silac Protein Relative Quantification Calculator";
    public static readonly string version = "1.0.0";

    private RcpaDirectoryField rawDir;
    private RcpaDoubleField precursorPPMTolerance;
    private RcpaFileField silacFile;
    private RcpaComboBox<IRawFormat> rawFormats;
    private RcpaComboBox<SearchEngineType> searchEngine;
    private RcpaTextField ignoreModifications;
    private RcpaDoubleField minCorrelation;
    private RcpaIntegerField _profileLength;

    public ExtendSilacQuantificationProteinFileProcessorUI()
    {
      InitializeComponent();

      base.SetFileArgument("IdentificationResult", new OpenFileArgument("Identification Result", new string[] { "txt", "noredundant" }));

      this.Text = Constants.GetSQHTitle(title, version);
      rawDir = new RcpaDirectoryField(btnRawDirectory, txtRawDirectory, "RawDir", "Raw File", false);
      rawFormats = new RcpaComboBox<IRawFormat>(comboBox1, "RawFormat", new IRawFormat[] { new ThermoRawFormat() }, 0);
      precursorPPMTolerance = new RcpaDoubleField(txtPrecursorTolerance, "PrecursorPPMTolerance", "precursor PPM tolerance", 50, true);
      silacFile = new RcpaFileField(btnSilacFile, txtSilacFile, "SilacFile", new OpenFileArgument("SILAC Configuration", "ini"), true);
      searchEngine = new RcpaComboBox<SearchEngineType>(cbSearchEngine, "SearchEngine", new SearchEngineType[] { SearchEngineType.MASCOT, SearchEngineType.SEQUEST }, 0);
      ignoreModifications = new RcpaTextField(textBox1, "IgnoreModifications", "Ignore Modifications (such like @ for Heavy labelling of Leu)", "", false);
      minCorrelation = new RcpaDoubleField(txtMinPepRegCorrelation, "MinCorrelation", "Minimum Peptide Regression Correlation", 0.8, true);
      _profileLength = new RcpaIntegerField(txtProfileLength, "ProfileLength", "Profile length used in calculation", 3, true);

      this.AddComponent(rawDir);
      this.AddComponent(rawFormats);
      this.AddComponent(precursorPPMTolerance);
      this.AddComponent(silacFile);
      this.AddComponent(searchEngine);
      this.AddComponent(ignoreModifications);
      this.AddComponent(minCorrelation);
      this.AddComponent(_profileLength);
      this.AddComponent(datasetClassification);
      this.AddComponent(rawPairClassification);

      this.InsertButton(2, btnLoadParam);
      this.InsertButton(3, btnSaveParam);
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

      IIdentifiedResultTextFormat fileFormat;
      switch (searchEngine.SelectedItem)
      {
        case SearchEngineType.MASCOT:
          fileFormat = new MascotResultTextFormat();
          break;
        case SearchEngineType.SEQUEST:
          fileFormat = new SequestResultTextFormat();
          break;
        default:
          throw new Exception(MyConvert.Format("Unsupported search engine {0}, contact with author.", searchEngine.SelectedItem));
      }

      return new ExtendSilacQuantificationProteinFileProcessor(
        new SilacQuantificationOption()
        {
          RawFormat = rawFormats.SelectedItem,
          RawDir = rawDirectory,
          SilacParamFile = silacFile.FullName,
          PPMTolerance = ppmTolerance,
          IgnoreModifications = ignoreModifications.Text,
          ProfileLength = _profileLength.Value
        },
        fileFormat,
        datasetClassification.GetClassificationSet(), rawPairClassification.GetClassificationSet())
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
        return ExtendSilacQuantificationProteinFileProcessorUI.title;
      }

      public string GetVersion()
      {
        return ExtendSilacQuantificationProteinFileProcessorUI.version;
      }

      public void Run()
      {
        new ExtendSilacQuantificationProteinFileProcessorUI().MyShow();
      }

      #endregion

      #region IToolSecondLevelCommand Members

      public string GetSecondLevelCommandItem()
      {
        return "SILAC";
      }

      #endregion
    }

    private void btnLoad_Click(object sender, EventArgs e)
    {
      try
      {
        this.originalFile.ValidateComponent();
      }
      catch (Exception ex)
      {
        MessageBox.Show(this, ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        return;
      }

      List<string> sortedExperimentals = new IdentifiedResultExperimentalReader().ReadFromFile(GetOriginFile()).ToList();
      sortedExperimentals.Sort();

      if (sortedExperimentals.Count > 0)
      {
        datasetClassification.InitializeClassificationSet(sortedExperimentals, @"(.+)_\d+$");
        rawPairClassification.InitializeClassificationSet(sortedExperimentals, @".+_(\d+)$");
      }
    }

    private OpenFileArgument openParamFile = new OpenFileArgument("Extend SILAC Quantification Param", "SILACParam");
    private SaveFileArgument saveParamFile = new SaveFileArgument("Extend SILAC Quantification Param", "SILACParam");

    private void btnSaveParam_Click(object sender, EventArgs e)
    {
      try
      {
        this.ValidateComponents();
      }
      catch (Exception ex)
      {
        MessageBox.Show(this, ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        return;
      }

      var dlg = saveParamFile.GetFileDialog();
      if (openParamFile.GetFileDialog().FileName != string.Empty && openParamFile.GetFileDialog().FileName != null)
      {
        dlg.FileName = openParamFile.GetFileDialog().FileName;
      }
      if (dlg.ShowDialog() == DialogResult.OK)
      {
        var bk = this.ConfigFileName;
        try
        {
          this.ConfigFileName = dlg.FileName;
          try
          {
            this.SaveOption();
          }
          catch (Exception ex)
          {
            MessageBox.Show(this, ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            return;
          }
        }
        finally
        {
          this.ConfigFileName = bk;
        }
      }
    }

    private void btnLoadParam_Click(object sender, EventArgs e)
    {
      var dlg = openParamFile.GetFileDialog();
      if (saveParamFile.GetFileDialog().FileName != string.Empty && saveParamFile.GetFileDialog().FileName != null)
      {
        dlg.FileName = saveParamFile.GetFileDialog().FileName;
      }

      if (dlg.ShowDialog() == DialogResult.OK)
      {
        var bk = this.ConfigFileName;
        try
        {
          this.ConfigFileName = dlg.FileName;
          try
          {
            this.LoadOption();
          }
          catch (Exception ex)
          {
            MessageBox.Show(this, ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            return;
          }
        }
        finally
        {
          this.ConfigFileName = bk;
        }
      }
    }
  }
}

