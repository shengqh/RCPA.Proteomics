using RCPA.Format;
using RCPA.Gui;
using RCPA.Gui.Command;
using RCPA.Gui.FileArgument;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace RCPA.Proteomics.Quantification.Srm
{
  public partial class SrmDistillerUI : AbstractFileProcessorUI
  {
    private static readonly string title = "SRM Distiller";

    private static readonly string version = "1.2.4";

    private RcpaListViewMultipleFileDirectoryField dataDirs;

    private RcpaDoubleField rtToleranceInSecond;

    private RcpaDoubleField signalToNoise;

    private RcpaTextField precursorMassDistance;

    private RcpaCheckBox smooth;

    private RcpaCheckBox refinePeakPicking;

    private RcpaCheckBox deductBaseLine;

    private RcpaDoubleField mzTolerance;

    private RcpaDoubleField validateSignalToNoise;

    private RcpaDoubleField validateCorrel;

    private RcpaIntegerField minValidTransactionPair;

    private RcpaRadioButton clusterByPredefine;

    private RcpaRadioButton clusterByRealData;

    private RcpaFileField predefinedFile;

    private RcpaRadioButton peakPickingByBaseline;

    private RcpaRadioButton peakPickingByHighestPeak;

    private RcpaDoubleField baseLinePercentage;

    private RcpaDoubleField highestPeakPercentage;

    private RcpaDoubleField precursorDistance;

    private RcpaComboBox<IFileReader2<List<SrmTransition>>> formats;

    private RcpaIntegerField minEnabledScan;

    private RcpaCheckBox hasDecoy;

    private RcpaTextField decoyPattern;

    private RcpaCheckBox ratioByArea;

    public SrmDistillerUI()
    {
      InitializeComponent();

      base.SetDirectoryArgument("TargetDirectory", "Target");

      this.dataDirs = new RcpaListViewMultipleFileDirectoryField(
        this.btnAddFiles,
        this.btnAddDirectory,
        this.btnAddSubdirectory,
        this.btnRemove,
        this.btnLoad,
        this.btnSave,
        this.lvDirectories,
        "Files",
        new OpenFileArgument("mzXml / mzData / Thermo Raw", new string[] { "mzXml", "mzData", "raw" }),
        "Agilent .d directory",
        true,
        true);
      AddComponent(this.dataDirs);

      this.rtToleranceInSecond = new RcpaDoubleField(txtRetentionTimeTolerance, "RTToleranceSecond", "Retention Time Tolerance in Second", 1.0, true);
      AddComponent(this.rtToleranceInSecond);

      this.signalToNoise = new RcpaDoubleField(txtSignalToNoise, "SignalToNoise", "Signal to Noise", 3.0, true);
      AddComponent(this.signalToNoise);

      this.precursorMassDistance = new RcpaTextField(txtPrecursorTolerance, "PrecursorMassDistance", "Distance between light and heavy precursor mass", "7,8,10", true);
      AddComponent(this.precursorMassDistance);

      this.smooth = new RcpaCheckBox(cbSmooth, "Smooth", true);
      AddComponent(this.smooth);

      this.refinePeakPicking = new RcpaCheckBox(cbRefinePeakPicking, "RefinePeakPicking", true);
      AddComponent(this.refinePeakPicking);

      this.deductBaseLine = new RcpaCheckBox(cbBaseLineExtraction, "DeductBaseLine", false);
      AddComponent(this.deductBaseLine);

      this.mzTolerance = new RcpaDoubleField(txtMzTolerance, "MzTolerance", "Product ion m/z tolerance", 0.1, true);
      AddComponent(this.mzTolerance);

      this.validateSignalToNoise = new RcpaDoubleField(txtValidateSignalToNoise, "ValidateSignalToNoise", "Validation signal to noise", 2, true);
      AddComponent(this.validateSignalToNoise);

      this.validateCorrel = new RcpaDoubleField(txtValidateCorrel, "ValidateCorrel", "Validation regression correlation", 0.5, true);
      AddComponent(this.validateCorrel);

      this.minValidTransactionPair = new RcpaIntegerField(txtMinValidTransactionPair, "minValidTransactionPair", "Minimum valid transaction pair count", 2, true);
      AddComponent(this.minValidTransactionPair);

      this.clusterByPredefine = new RcpaRadioButton(rbClusterByPredefine, "ClusterByPredefined", true);
      AddComponent(this.clusterByPredefine);

      this.clusterByRealData = new RcpaRadioButton(rbClusterByRealData, "ClusterByRealData", false);
      AddComponent(this.clusterByRealData);

      this.predefinedFile = new RcpaFileField(btnPredefined, txtPredefinedFile, "PredefinedFile", new OpenFileArgument("Predefined SRM Transaction File", ".txt"), false);
      AddComponent(this.predefinedFile);

      this.peakPickingByBaseline = new RcpaRadioButton(rbBaseline, "PeakPickingByBaseline", true);
      AddComponent(this.peakPickingByBaseline);

      this.peakPickingByHighestPeak = new RcpaRadioButton(rbHighestPeak, "PeakPickingByHighestPeak", false);
      AddComponent(this.peakPickingByHighestPeak);

      this.baseLinePercentage = new RcpaDoubleField(txtBaselinePercentage, "BaselinePercentage", "Baseline percentage", 5, true);
      AddComponent(this.baseLinePercentage);

      this.highestPeakPercentage = new RcpaDoubleField(txtHighestPeakPercentage, "HighestPeakPercentage", "Highest peak percentage", 5, false);
      AddComponent(this.highestPeakPercentage);

      this.precursorDistance = new RcpaDoubleField(txtMaxPrecursorDistance, "precursorDistance", "Maximum Precursor Distance", 20, true);
      AddComponent(this.precursorDistance);

      this.formats = new RcpaComboBox<IFileReader2<List<SrmTransition>>>(cbFormat, "FileFormat", SrmFormatFactory.GetReaders().ToArray(), 0);
      AddComponent(this.formats);

      this.minEnabledScan = new RcpaIntegerField(txtMinEnabledScan, "MinValidScan", "Minimum valid scan count", 5, true);
      AddComponent(this.minEnabledScan);

      this.hasDecoy = new RcpaCheckBox(cbDecoyPattern, "HasDecoy", false);
      AddComponent(this.hasDecoy);

      this.ratioByArea = new RcpaCheckBox(cbRatioByArea, "ratioByArea", true);
      AddComponent(this.ratioByArea);

      this.decoyPattern = new RcpaTextField(txtDecoyPattern, "DecoyPattern", "Decoy pattern", "DECOY", false);
      this.decoyPattern.ValidateFunc = m =>
      {
        try
        {
          new Regex(m);
          return true;
        }
        catch (Exception)
        {
          return false;
        }
      };
      AddComponent(this.decoyPattern);

      this.Text = Constants.GetSQHTitle(title, version);
    }

    protected double[] ParseGaps()
    {
      var parts = precursorMassDistance.Text.Split(new char[] { ',', ';' });
      return (from p in parts
              let m = MyConvert.ToDouble(p.Trim())
              select m).ToArray();
    }

    protected override void DoBeforeValidate()
    {
      base.DoBeforeValidate();

      this.rtToleranceInSecond.Required = clusterByRealData.Checked;
      this.precursorMassDistance.Required = clusterByRealData.Checked;
      this.predefinedFile.Required = clusterByPredefine.Checked;

      this.baseLinePercentage.Required = this.rbBaseline.Checked;
      this.signalToNoise.Required = this.rbBaseline.Checked;
      this.highestPeakPercentage.Required = this.rbHighestPeak.Checked;

      this.decoyPattern.Required = this.cbDecoyPattern.Checked;

      if (clusterByRealData.Checked)
      {
        try
        {
          ParseGaps();
        }
        catch (Exception)
        {
          throw new Exception("Wrong format of allowed gaps (should like 4,5,8,10)");
        }
      }
      else
      {
        formats.SelectedItem.ReadFromFile(predefinedFile.FullName);
      }
    }

    protected override string GetOriginFile()
    {
      return base.GetOriginFile() + "\\srmdistiller.options";
    }

    protected override IFileProcessor GetFileProcessor()
    {
      SrmOptions options = new SrmOptions()
      {
        DistillerSoftware = this.Text,
        AskForSmooth = smooth.Checked,
        DeductBaseLine = deductBaseLine.Checked,
        RangeSelectionByNoise = peakPickingByBaseline.Checked,
        ValidationMinSignalToNoise = validateSignalToNoise.Value,
        ValidationMinRegressionCorrelation = validateCorrel.Value,
        MzTolerance = mzTolerance.Value,
        MinValidTransitionPair = minValidTransactionPair.Value,
        //MinRetentionWindowInMinute = 0,
        MinEnabledScan = minEnabledScan.Value,
        HasDecoy = hasDecoy.Checked,
        DecoyPattern = decoyPattern.Text,
        RatioByArea = cbRatioByArea.Checked,
        RefineData = cbRefinePeakPicking.Checked
      };

      if (peakPickingByBaseline.Checked)
      {
        options.MinPeakPickingSignalToNoise = signalToNoise.Value;
        options.LowestPercentageForNoise = baseLinePercentage.Value / 100;
      }
      else
      {
        options.PercentageOfHighestPeak = highestPeakPercentage.Value / 100;
      }

      if (clusterByPredefine.Checked)
      {
        options.DefinitionFile = predefinedFile.FullName;
        options.DefinitionFileFormat = formats.SelectedItem.GetName();
      }
      else
      {
        options.PrecursorMassDistance = ParseGaps();
        options.RetentionTimeToleranceInSecond = rtToleranceInSecond.Value;
      }

      options.RawFiles = dataDirs.SelectPathMap;
      options.ToXml(true).Save(this.GetOriginFile());

      return new SrmDistiller();
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
        new SrmDistillerUI().MyShow();
      }

      #endregion

      #region IToolSecondLevelCommand Members

      public string GetSecondLevelCommandItem()
      {
        return "SRMBuilder";
      }

      #endregion
    }

    private void rbClusterByRealData_Click(object sender, EventArgs e)
    {
      rbClusterByPredefine.Checked = false;
    }

    private void rbClusterByPredefine_Click(object sender, EventArgs e)
    {
      rbClusterByRealData.Checked = false;
    }

    private void rbBaseline_Click(object sender, EventArgs e)
    {
      rbHighestPeak.Checked = false;
    }

    private void rbHighestPeak_Click(object sender, EventArgs e)
    {
      rbBaseline.Checked = false;
    }

    private void btnAddTag_Click(object sender, EventArgs e)
    {
      if (lvDirectories.SelectedItems.Count == 0)
      {
        return;
      }

      var form = new InputTextForm(null, null, "Input group name", "Input group name", "", false);

      if (form.ShowDialog() == System.Windows.Forms.DialogResult.OK)
      {
        foreach (ListViewItem item in lvDirectories.SelectedItems)
        {
          if (item.SubItems.Count == 1)
          {
            item.SubItems.Add(form.Value);
          }
          else
          {
            item.SubItems[1].Text = form.Value;
          }
        }
      }
    }

    private void btnEdit_Click(object sender, EventArgs e)
    {
      SrmTransitionDefinitionForm form = new SrmTransitionDefinitionForm();

      if (File.Exists(formats.SelectedItem.GetFormatFile()))
      {
        form.ReadFormatFile(formats.SelectedItem.GetFormatFile());
      }

      form.ShowDialog();
    }

    private void btnNew_Click(object sender, EventArgs e)
    {
      SrmTransitionDefinitionForm form = new SrmTransitionDefinitionForm();

      if (File.Exists(txtPredefinedFile.Text))
      {
        form.NewFromData(txtPredefinedFile.Text);
      }

      form.ShowDialog();
    }

    private void btnNewSample_Click(object sender, EventArgs e)
    {
      TextFileDefinition def = new TextFileDefinition();
      def.ReadFromFile(formats.SelectedItem.GetFormatFile());

      string ext = "csv";
      if (def.Delimiter != ',')
      {
        ext = "txt";
      }

      dlgSave.Filter = "Sample File|*." + ext + "|All Files|*.*";
      if (dlgSave.ShowDialog() == System.Windows.Forms.DialogResult.OK)
      {
        def.WriteSampleFile(dlgSave.FileName);
        MessageBox.Show(this, "File saved to  : " + dlgSave.FileName);
      }
    }
  }
}
