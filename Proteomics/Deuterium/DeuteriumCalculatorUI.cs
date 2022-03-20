using RCPA.Commandline;
using RCPA.Gui;
using RCPA.Gui.Command;
using RCPA.Gui.FileArgument;
using RCPA.Proteomics.Raw;
using RCPA.Proteomics.Summary;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Windows.Forms;

namespace RCPA.Proteomics.Deuterium
{
  public partial class DeuteriumCalculatorUI : AbstractProcessorUI
  {
    public static readonly string title = "Deuterium Calculator";

    public static readonly string version = "1.0.6";

    private RcpaFileField noredundantFile;

    public DeuteriumCalculatorUI()
    {
      InitializeComponent();

      this.noredundantFile = new RcpaFileField(btnNoredundantFile, txtNoredundantFile, "NoredundantFile", new OpenFileArgument("Noredundant/Peptide", new string[] { "noredundant", "peptides" }), true);
      this.AddComponent(this.noredundantFile);

      this.rawFiles.FileArgument = new OpenFileArgument("Raw", RawFileFactory.GetSupportedRawFormats(), true);
      this.rawFiles.FileDescription = "Input raw files (" + RawFileFactory.SupportedRawFormatString + ")";

      this.Text = Constants.GetSQHTitle(title, version);
    }

    protected override ProgressChangedEventHandler GetProgressChanged()
    {
      return new WorkerProgressChangedTextBoxProxy(txtLog, new[] { progressBar }).ProgressChanged;
    }

    protected override void ValidateComponents()
    {
      base.ValidateComponents();

      var cm = pnlClassification.GetClassificationMap();

      foreach (var key in cm.Keys)
      {
        double time;
        if (!double.TryParse(key, out time))
        {
          throw new Exception(string.Format("Time \"{0}\" is not numeric!", key));
        }
      }
    }

    protected override IProcessor GetProcessor()
    {
      var cm = pnlClassification.GetClassificationMap();
      var expTimeMap = new Dictionary<string, double>();
      foreach (var key in cm.Keys)
      {
        var keytime = double.Parse(key);
        foreach (var v in cm[key])
        {
          expTimeMap[v] = keytime;
        }
      }

      var options = new DeuteriumCalculatorOptions()
      {
        InputFile = noredundantFile.FullName,
        OutputFile = noredundantFile.FullName + ".deuterium.tsv",
        MinimumProfileLength = minimumProfileLength.Value,
        MinimumIsotopicPercentage = minimumIsotopicPercentage.Value,
        MinimumProfileCorrelation = minimumProfileCorrelation.Value,
        MaximumProfileDistance = maximumProfileDistance.Value,
        MzTolerancePPM = precursorPPMTolerance.Value,
        Overwrite = cbOverwrite.Checked,
        DrawImage = cbDrawImage.Checked,
        ExcludeIsotopic0 = cbExcludeIsotopic0InFormula.Checked,
        RawFiles = rawFiles.FileNames,
        ThreadCount = threadCount.Value,
        ExperimentalTimeMap = expTimeMap,
        PeptideInAllTimePointOnly = rbPeptideInAllTimePointOnly.Checked
      };

      if (isPeptideFile(noredundantFile.FullName))
      {
        return new PeptideDeuteriumCalculator(options);
      }
      else
      {
        return new ProteinDeuteriumCalculator(options);
      }
    }

    private bool isPeptideFile(string fullName)
    {
      using (var sr = new StreamReader(fullName))
      {
        var line = sr.ReadLine();
        return line.Contains("FileScan");
      }
    }

    public class Command : AbstractCommandLineCommand<DeuteriumCalculatorOptions>, IToolCommand
    {
      public override string Name
      {
        get
        {
          return "deuterium_calc";
        }
      }

      public override string Description
      {
        get
        {
          return "Deuterium calculator";
        }
      }

      public override IProcessor GetProcessor(DeuteriumCalculatorOptions options)
      {
        return new ProteinDeuteriumCalculator(options);
      }

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
        new DeuteriumCalculatorUI().MyShow();
      }

      #endregion
    }

    private void btnLoad_Click(object sender, System.EventArgs e)
    {
      try
      {
        this.noredundantFile.ValidateComponent();
      }
      catch (Exception ex)
      {
        MessageBox.Show(this, ex.Message, "Error");
        return;
      }

      HashSet<string> experimentals = new IdentifiedResultExperimentalReader().ReadFromFile(this.noredundantFile.FullName);

      List<string> sortedExperimentals = new List<string>(experimentals);
      sortedExperimentals.Sort();

      if (sortedExperimentals.Count > 0)
      {
        pnlClassification.InitializeClassificationSet(sortedExperimentals);
      }
    }
  }
}

