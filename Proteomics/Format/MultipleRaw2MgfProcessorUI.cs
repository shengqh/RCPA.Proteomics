using RCPA.Gui;
using RCPA.Gui.Command;
using RCPA.Gui.FileArgument;
using RCPA.Proteomics.Mascot;
using RCPA.Proteomics.Processor;
using RCPA.Proteomics.Quantification.IsobaricLabelling;
using RCPA.Proteomics.Raw;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace RCPA.Proteomics.Format
{
  public partial class MultipleRaw2MgfProcessorUI : AbstractFileProcessorUI
  {
    public static readonly string Title = "TurboRawToMGF - " + RawFileFactory.SupportedRawFormatString + " To Mascot Generic Format Converter";

    public static readonly string Version = "2.0.8";

    private RcpaComboBox<ITitleFormat> titleFormat;
    private RcpaDoubleField minMassRange;
    private RcpaDoubleField maxMassRange;
    private RcpaDoubleField minIonIntensity;
    private RcpaIntegerField minIonCount;
    private RcpaDoubleField minTotalIonIntensity;
    private RcpaIntegerField topX;
    private RcpaDoubleField productIonPPM;
    private RcpaTextField neutralLoss;
    private RcpaTextField specialIons;
    private RcpaDoubleField removeIonWindow;

    private RcpaDoubleField precursorPPM;

    private RcpaDoubleField retentionTimeWindow;

    private RcpaComboBox<ChargeClass> defaultCharge;
    private RcpaComboBox<IsobaricType> isobaricTypes;
    private RcpaComboBox<IIsobaricLabellingProtease> proteases;

    private readonly OpenFileArgument openParamFile = new OpenFileArgument("Parameter File", "param");
    private readonly SaveFileArgument saveParamFile = new SaveFileArgument("Parameter File", "param");

    public MultipleRaw2MgfProcessorUI()
    {
      InitializeComponent();

      this.Text = Constants.GetSQHTitle(Title, Version);

      this.SetDirectoryArgument("TargetDir", "Target MGF");

      var options = new MultipleRaw2MgfOptions();

      this.titleFormat = new RcpaComboBox<ITitleFormat>(cbTitleFormat, "TitleFormat", MascotTitleFactory.Titles, 0);
      this.minMassRange = new RcpaDoubleField(txtMWRangeFrom, "MWRangeFrom", "Min Mass", options.PrecursorMassRange.From, true);
      this.maxMassRange = new RcpaDoubleField(txtMWRangeTo, "MWRangeTo", "Max Mass", options.PrecursorMassRange.To, true);
      this.minIonIntensity = new RcpaDoubleField(txtMinIonIntensity, "MinIonIntensity", "Min Ion Intensity", options.MinimumIonIntensity, true);
      this.minIonCount = new RcpaIntegerField(txtMinIonCount, "MinIonCount", "Min Ion Count", options.MinimumIonCount, true);
      this.minTotalIonIntensity = new RcpaDoubleField(txtMinIonIntensityThreshold, "MinTotalIonIntensity", "Min Total Ion Intensity", options.MinimumTotalIonIntensity, true);

      this.defaultCharge = new RcpaComboBox<ChargeClass>(cbDefaultCharge, "DefaultCharge",
        new ChargeClass[] {
          new ChargeClass(new int[]{}),
          new ChargeClass(new int[]{2,3})},
          1);

      this.rawFiles.FileArgument = new OpenFileArgument("Raw", RawFileFactory.GetSupportedRawFormats());

      //high resolution MS/MS
      productIonPPM = new RcpaDoubleField(txtDeisotopic, "DeisotopicPPM", "Deisotopic Product Ion Tolerance (ppm)", options.ProductIonPPM, false);
      AddComponent(productIonPPM);
      cbDeisotopic.Checked = options.Deisotopic;
      cbDeconvolution.Checked = options.ChargeDeconvolution;

      cbKeepTopX.Checked = options.KeepTopX;
      topX = new RcpaIntegerField(txtTopX, "TopX", "Top X Peaks in 100 dalton window", options.TopX, false);
      topX.PreCondition = cbKeepTopX;
      AddComponent(topX);

      cbGroupByMode.Checked = options.GroupByMode;
      cbGroupByMsLevel.Checked = options.GroupByMsLevel;
      cbParallelMode.Checked = options.ParallelMode;

      removeIonWindow = new RcpaDoubleField(txtRemoveMassWindow, "removeMassWindow", "Remove Mass Window", options.RemoveIonWindow, false);
      removeIonWindow.PreCondition = cbRemoveIons;
      AddComponent(removeIonWindow);


      isobaricTypes = new RcpaComboBox<IsobaricType>(cbxIsobaricTypes, "IsobaricType", IsobaricTypeFactory.IsobaricTypes, 0);
      isobaricTypes.PreCondition = cbRemoveIsobaricIons;
      AddComponent(isobaricTypes);

      proteases = new RcpaComboBox<IIsobaricLabellingProtease>(cbProteases, "Protease", IsobaricLabellingProteaseFactory.Proteases, 0);
      proteases.PreCondition = cbRemoveIsobaricIons;
      AddComponent(proteases);

      this.AddComponent(titleFormat);
      this.AddComponent(minMassRange);
      this.AddComponent(maxMassRange);
      this.AddComponent(minIonIntensity);
      this.AddComponent(minIonCount);
      this.AddComponent(minTotalIonIntensity);
      this.AddComponent(defaultCharge);

      cbRemoveSpecialIons.PreCondition = cbRemoveIons;
      specialIons = new RcpaTextField(txtSpecialIons, "RemoveIonMzRange", "Remove special mz range, for example, 113.5-117.5,145.5.0-155.5 for iTRAQ plex 4", options.SpecialIons, false);
      specialIons.PreCondition = cbRemoveSpecialIons;
      AddComponent(specialIons);

      cbRemoveIsobaricIons.PreCondition = cbRemoveIons;

      cbRemoveIsobaricIonsInLowRange.PreCondition = cbRemoveIsobaricIons;
      cbRemoveIsobaricIonsInHighRange.PreCondition = cbRemoveIsobaricIons;

      retentionTimeWindow = new RcpaDoubleField(txtRetentionTimeWindow, "RetentionTimeWindow", "Retention time window for smoothing offset", 0.5, false);
      AddComponent(retentionTimeWindow);

      cbRemovePrecursorLargeIons.PreCondition = cbRemovePrecursor;

      precursorPPM = new RcpaDoubleField(txtPrecursorPPM, "PrecursorPPM", "Precursor PPM", 50, false);
      precursorPPM.PreCondition = cbRemovePrecursor;
      AddComponent(precursorPPM);

      neutralLoss = new RcpaTextField(txtNeutralLoss, "NeutralLoss", "Neutral loss atom composition", "NH3,H2O,", false);
      neutralLoss.PreCondition = cbRemovePrecursor;
      AddComponent(neutralLoss);

      InsertButton(0, btnSave);
      InsertButton(0, btnLoad);
    }

    protected override void DoBeforeValidate()
    {
      if (cbRemovePrecursor.Checked)
      {
        var options = GetPrecursorOptions();

        try
        {
          options.ParseOffsets();
        }
        catch (Exception)
        {
          throw new Exception("Wrong format of neutral loss atom composition, should like (NH3,H2O, or -17,-18)");
        }
      }

      if ((cbDeisotopic.EnabledAndChecked || cbDeconvolution.EnabledAndChecked) && productIonPPM.Value == 0.0)
      {
        throw new Exception("Product ion tolerance cannot be zero.");
      }

      if (cbKeepTopX.Checked && topX.Value <= 0)
      {
        throw new Exception("Top X count cannot be zero.");
      }

      if (cbRemoveSpecialIons.EnabledAndChecked)
      {
        ParseRemoveMassRange();
      }
    }

    private PeakListRemovePrecursorProcessorOptions GetPrecursorOptions()
    {
      var options = new PeakListRemovePrecursorProcessorOptions();
      options.RemovePrecursor = cbRemovePrecursor.Checked;

      if (options.RemovePrecursor)
      {
        options.NeutralLoss = txtNeutralLoss.Text;
        options.RemoveChargeMinus1Precursor = cbRemovePrecursorMinus1ChargeIon.Checked;
        options.RemoveIonLargerThanPrecursor = cbRemovePrecursorLargeIons.Checked;
        options.RemoveIsotopicIons = cbRemovePrecursorIsotopicIons.Checked;

        try
        {
          options.PPMTolerance = double.Parse(txtPrecursorPPM.Text);
        }
        catch (Exception)
        {
          throw new Exception("Input precursor PPM first!");
        }
      }

      return options;
    }

    private List<Pair<double, double>> ParseRemoveMassRange()
    {
      var mzWindow = removeIonWindow.Value;

      List<Pair<double, double>> result = new List<Pair<double, double>>();

      var parts = txtSpecialIons.Text.Split(new char[] { ',', ';' }, StringSplitOptions.RemoveEmptyEntries);

      bool bWrongFormat = parts.Length == 0;

      foreach (var part in parts)
      {
        var values = part.Split(new char[] { '-' }, StringSplitOptions.RemoveEmptyEntries);
        if (values.Length == 1)
        {
          try
          {
            var tagmass = MyConvert.ToDouble(values[0]);
            result.Add(new Pair<double, double>(tagmass - mzWindow, tagmass + mzWindow));
          }
          catch (Exception)
          {
            bWrongFormat = true;
            break;
          }
        }
        else if (values.Length == 2)
        {
          try
          {
            var first = MyConvert.ToDouble(values[0]);
            var second = MyConvert.ToDouble(values[1]);
            result.Add(new Pair<double, double>(first, second));
          }
          catch (Exception)
          {
            bWrongFormat = true;
            break;
          }
        }
        else
        {
          bWrongFormat = true;
          break;
        }
      }

      if (bWrongFormat)
      {
        throw new Exception("Wrong format of remove mass range, should like 113.5-117.5, 145.1076, 291.2141");
      }

      return result;
    }

    private List<string> parameters = new List<string>();
    protected override IFileProcessor GetFileProcessor()
    {
      var options = FormToOptions();
      return new MultipleRaw2MgfProcessor(options);
    }

    public class Command : IToolCommand
    {
      #region IToolCommand Members

      public string GetClassification()
      {
        return MenuCommandType.Format;
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
        new MultipleRaw2MgfProcessorUI().MyShow();
      }

      #endregion
    }

    private void cbxIsobaricTypes_SelectedIndexChanged(object sender, EventArgs e)
    {
      if (isobaricTypes != null && removeIonWindow != null)
      {
        try
        {
          txtIsobaricIons.Text = FormToOptions().GetIsobaricProcessor().ToString().Replace('\n', ';');
        }
        catch (Exception)
        {
        }
      }
    }

    private MultipleRaw2MgfOptions FormToOptions()
    {
      var result = new MultipleRaw2MgfOptions();

      result.ConverterName = Title;
      result.ConverterVersion = Version;

      result.TargetDirectory = GetOriginFile();
      result.RawFiles = rawFiles.FileNames;
      result.MascotTitleName = titleFormat.SelectedItem.FormatName;
      result.PrecursorMassRange = new MassRange(minMassRange.Value, maxMassRange.Value);
      result.MinimumIonIntensity = minIonIntensity.Value;
      result.MinimumIonCount = minIonCount.Value;
      result.MinimumTotalIonIntensity = minTotalIonIntensity.Value;
      result.DefaultCharges = defaultCharge.SelectedItem;

      result.ProductIonPPM = productIonPPM.Value;
      result.Deisotopic = cbDeisotopic.Checked;
      result.ChargeDeconvolution = cbDeconvolution.Checked;

      result.KeepTopX = cbKeepTopX.Checked;
      result.TopX = topX.Value;
      result.GroupByMode = cbGroupByMode.Checked;
      result.GroupByMsLevel = cbGroupByMsLevel.Checked;
      result.ParallelMode = cbParallelMode.Checked;
      result.ExtractRawMS3 = cbExtractRawMS3.Checked;
      result.Overwrite = cbOverwrite.Checked;
      result.OutputMzXmlFormat = cbOutputMzXmlFormat.Checked;

      result.RemoveIons = cbRemoveIons.Checked;
      result.RemoveIonWindow = removeIonWindow.Value;
      result.RemoveSpecialIons = cbRemoveSpecialIons.Checked;
      result.SpecialIons = txtSpecialIons.Text;
      result.RemoveIsobaricIons = cbRemoveIsobaricIons.Checked;
      result.IsobaricType = isobaricTypes.SelectedItem;
      result.ProteaseName = proteases.SelectedItem.ToString();
      result.RemoveIsobaricIonsInLowRange = cbRemoveIsobaricIonsInLowRange.Checked;
      result.RemoveIsobaricIonsInHighRange = cbRemoveIsobaricIonsInHighRange.Checked;
      result.RemoveIsobaricIonsReporters = rbRemoveReporters.Checked;

      result.PrecursorOptions = GetPrecursorOptions();

      return result;
    }

    private void OptionsToForm(MultipleRaw2MgfOptions options)
    {
      originalFile.FullName = options.TargetDirectory;
      rawFiles.FileNames = options.RawFiles;
      titleFormat.SelectedItem = titleFormat.Items.Where(l => l.FormatName.Equals(options.MascotTitleName)).First();
      minMassRange.Value = options.PrecursorMassRange.From;
      maxMassRange.Value = options.PrecursorMassRange.To;
      minIonIntensity.Value = options.MinimumIonIntensity;
      minIonCount.Value = options.MinimumIonCount;
      minTotalIonIntensity.Value = options.MinimumTotalIonIntensity;
      defaultCharge.SelectedItem = defaultCharge.Items.Where(l => l.ToString().Equals(options.DefaultCharges.ToString())).First();

      productIonPPM.Value = options.ProductIonPPM;
      cbDeisotopic.Checked = options.Deisotopic;
      cbDeconvolution.Checked = options.ChargeDeconvolution;

      cbKeepTopX.Checked = options.KeepTopX;
      topX.Value = options.TopX;
      cbGroupByMode.Checked = options.GroupByMode;
      cbGroupByMsLevel.Checked = options.GroupByMsLevel;
      cbParallelMode.Checked = options.ParallelMode;
      cbExtractRawMS3.Checked = options.ExtractRawMS3;
      cbOverwrite.Checked = options.Overwrite;
      cbOutputMzXmlFormat.Checked = options.OutputMzXmlFormat;

      cbRemoveIons.Checked = options.RemoveIons;
      removeIonWindow.Value = options.RemoveIonWindow;
      cbRemoveSpecialIons.Checked = options.RemoveSpecialIons;
      txtSpecialIons.Text = options.SpecialIons;
      cbRemoveIsobaricIons.Checked = options.RemoveIsobaricIons;
      isobaricTypes.SelectedItem = options.IsobaricType;
      proteases.SelectedItem = proteases.Items.Where(l => l.ToString().Equals(options.ProteaseName)).First();
      rbRemoveReporters.Checked = options.RemoveIsobaricIonsReporters;
      cbRemoveIsobaricIonsInLowRange.Checked = options.RemoveIsobaricIonsInLowRange;
      cbRemoveIsobaricIonsInHighRange.Checked = options.RemoveIsobaricIonsInHighRange;

      cbRemovePrecursor.Checked = options.PrecursorOptions.RemovePrecursor;
      txtPrecursorPPM.Text = options.PrecursorOptions.PPMTolerance.ToString();
      cbRemoveNeutralLoss.Checked = options.PrecursorOptions.RemoveNeutralLoss;
      txtNeutralLoss.Text = options.PrecursorOptions.NeutralLoss;
      cbRemovePrecursorIsotopicIons.Checked = options.PrecursorOptions.RemoveIsotopicIons;
      cbRemovePrecursorMinus1ChargeIon.Checked = options.PrecursorOptions.RemoveChargeMinus1Precursor;
      cbRemovePrecursorLargeIons.Checked = options.PrecursorOptions.RemoveIonLargerThanPrecursor;
    }

    private void CheckMzXmlEnabled(Control control)
    {
      foreach (Control ctl in control.Controls)
      {
        if ("1".Equals(ctl.Tag))
        {
          ctl.Enabled = !cbOutputMzXmlFormat.Checked;
        }
        CheckMzXmlEnabled(ctl);
      }
    }

    private void rbMzXml_CheckedChanged(object sender, EventArgs e)
    {
      CheckMzXmlEnabled(this);
    }

    private void btnLoad_Click(object sender, EventArgs e)
    {
      var dlg = this.openParamFile.GetFileDialog();
      if (dlg.ShowDialog() == DialogResult.OK)
      {
        var options = new MultipleRaw2MgfOptions();
        try
        {
          options.LoadFromFile(dlg.FileName);
          OptionsToForm(options);
        }
        catch (Exception ex)
        {
          MessageBox.Show(this, "Failed to load parameters from " + dlg.FileName + "\nError:" + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
      }
    }

    private void btnSave_Click(object sender, EventArgs e)
    {
      try
      {
        ValidateComponents();
      }
      catch (Exception ex)
      {
        MessageBox.Show(this, "Validation failed : " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        return;
      }

      var dlg = saveParamFile.GetFileDialog();
      if (dlg.ShowDialog() == DialogResult.OK)
      {
        var options = FormToOptions();
        options.SaveToFile(dlg.FileName);
      }
    }
  }
}