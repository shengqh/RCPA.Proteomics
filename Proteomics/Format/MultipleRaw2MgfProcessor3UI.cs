using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Linq;
using System.Windows.Forms;
using RCPA.Gui;
using RCPA.Gui.FileArgument;
using RCPA.Proteomics.Processor;
using RCPA;
using System.IO;
using RCPA.Proteomics.IO;
using RCPA.Proteomics;
using RCPA.Gui.Command;
using RCPA.Proteomics.Mascot;
using RCPA.Proteomics.Spectrum;
using RCPA.Proteomics.Raw;
using RCPA.Proteomics.Quantification.IsobaricLabelling;

namespace RCPA.Proteomics.Format
{
  public partial class MultipleRaw2MgfProcessor3UI : AbstractFileProcessorUI
  {
    public static readonly string Title = "TurboRawToMGF - " + RawFileFactory.SupportedRawFormatString + " To Mascot Generic Format Converter";

    public static readonly string Version = "2.0.6";

    private RcpaComboBox<ITitleFormat> titleFormat;
    private RcpaDoubleField minMassRange;
    private RcpaDoubleField maxMassRange;
    private RcpaDoubleField minIonIntensity;
    private RcpaIntegerField minIonCount;
    private RcpaDoubleField minTotalIonIntensity;
    private RcpaDoubleField precursorShift;
    private RcpaDoubleField productIonShift;
    private RcpaIntegerField topX;
    private RcpaDoubleField productIonPPM;
    private RcpaTextField neutralLoss;
    private RcpaTextField specialIons;
    private RcpaDoubleField removeIonWindow;

    private RcpaDoubleField precursorPPM;

    private RcpaDoubleField maxShiftPPM;
    private RcpaDoubleField retentionTimeWindow;

    private RcpaComboBox<ChargeClass> defaultCharge;
    private RcpaComboBox<IsobaricType> isobaricTypes;
    private RcpaComboBox<IIsobaricLabellingProtease> proteases;

    private RcpaFileField txtOffsetFile;

    public MultipleRaw2MgfProcessor3UI()
    {
      InitializeComponent();

      this.Text = Constants.GetSQHTitle(Title, Version);

      this.SetDirectoryArgument("TargetDir", "Target MGF");

      this.titleFormat = new RcpaComboBox<ITitleFormat>(cbTitleFormat, "TitleFormat", MascotTitleFactory.Titles, 0);
      this.minMassRange = new RcpaDoubleField(txtMWRangeFrom, "MWRangeFrom", "Min Mass", 400, true);
      this.maxMassRange = new RcpaDoubleField(txtMWRangeTo, "MWRangeTo", "Max Mass", 5000, true);
      this.minIonIntensity = new RcpaDoubleField(txtMinIonIntensity, "MinIonIntensity", "Min Ion Intensity", 1.0, true);
      this.minIonCount = new RcpaIntegerField(txtMinIonCount, "MinIonCount", "Min Ion Count", 15, true);
      this.minTotalIonIntensity = new RcpaDoubleField(txtMinIonIntensityThreshold, "MinTotalIonIntensity", "Min Total Ion Intensity", 100.0, true);

      this.defaultCharge = new RcpaComboBox<ChargeClass>(cbDefaultCharge, "DefaultCharge",
        new ChargeClass[] { 
          new ChargeClass(new int[]{}),
          new ChargeClass(new int[]{2,3})},
          1);

      this.rawFiles.FileArgument = new OpenFileArgument("Raw", RawFileFactory.GetSupportedRawFormats());

      neutralLoss = new RcpaTextField(txtNeutralLossComposition, "NeutralLoss", "Neutral loss atom composition", "NH3,H2O,", false);
      neutralLoss.PreCondition = cbRemovePrecursor;
      AddComponent(neutralLoss);

      precursorShift = new RcpaDoubleField(txtPredursorShift, "PrecursorShiftValue", "Precursor Shift (ppm)", 0, false);
      AddComponent(precursorShift);

      productIonShift = new RcpaDoubleField(txtPreductIonShift, "ProductIonShiftValue", "Product Ion Shift (ppm)", 0, false);
      AddComponent(productIonShift);

      topX = new RcpaIntegerField(txtTopX, "TopX", "Top X Peaks in 100 dalton window", 8, false);
      topX.PreCondition = cbKeepTopX;
      AddComponent(topX);

      removeIonWindow = new RcpaDoubleField(txtRemoveIonWindow, "removeMassWindow", "Remove Mass Window", 0.1, false);
      removeIonWindow.PreCondition = cbRemoveMassRange;
      AddComponent(removeIonWindow);

      productIonPPM = new RcpaDoubleField(txtDeisotopic, "DeisotopicPPM", "Deisotopic Product Ion Tolerance (ppm)", 20, false);
      AddComponent(productIonPPM);

      specialIons = new RcpaTextField(txtSpecialIons, "RemoveIonMzRange", "Remove special mz range, for example, 113.5-117.5,145.5.0-155.5 for iTRAQ plex 4", "113.5-117.5", false);
      specialIons.PreCondition = cbRemoveSpecialIons;
      AddComponent(specialIons);

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

      cbRemoveSpecialIons.PreCondition = cbRemoveMassRange;
      cbRemoveIsobaricIons.PreCondition = cbRemoveMassRange;
      cbRemovePrecursorLargeIons.PreCondition = cbRemoveMassRange;
      cbRemovePrecursor.PreCondition = cbRemoveMassRange;

      txtOffsetFile = new RcpaFileField(btnShiftFile, txtShiftFile, "OffsetFile", new OpenFileArgument("Precursor Offset", "offset"), false);
      AddComponent(txtOffsetFile);

      cbRemoveIsobaricIonsInLowRange.PreCondition = cbRemoveIsobaricIons;
      cbRemoveIsobaricIonsInHighRange.PreCondition = cbRemoveIsobaricIons;

      maxShiftPPM = new RcpaDoubleField(txtMaxShiftPPM, "MaxShiftPPM", "Maximum offset in ppm", 30, false);
      AddComponent(maxShiftPPM);

      retentionTimeWindow = new RcpaDoubleField(txtRetentionTimeWindow, "RetentionTimeWindow", "Retention time window for smoothing offset", 0.5, false);
      AddComponent(retentionTimeWindow);

      precursorPPM = new RcpaDoubleField(txtPrecursorPPM, "PrecursorPPM", "Precursor PPM", 50, false);
      precursorPPM.PreCondition = cbRemovePrecursor;
      AddComponent(precursorPPM);
    }

    protected override void DoBeforeValidate()
    {
      txtOffsetFile.Required = rbMassCalibration.Checked && rbMassCalibrationByFile.Checked;

      if (cbRemovePrecursor.Checked)
      {
        var options = GetPrecursorOptions();

        try
        {
          if (options != null)
          {
            options.ParseOffsets();
          }
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

      if (rbMassCalibration.Checked)
      {
        if (MessageBox.Show(this, "Are you sure you want to do the mass calibration?", "Warning", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == System.Windows.Forms.DialogResult.No)
        {
          throw new Exception(string.Empty);
        }
      }
    }

    private PeakListRemovePrecursorProcessorOptions GetPrecursorOptions()
    {
      if (cbRemovePrecursor.Checked || cbRemovePrecursorLargeIons.Checked)
      {
        var options = new PeakListRemovePrecursorProcessorOptions()
        {
          NeutralLoss = txtNeutralLossComposition.Text,
          RemoveChargeMinus1Precursor = cbRemovePrecursorMinus1ChargeIon.Checked,
          RemoveIonLargerThanPrecursor = cbRemovePrecursorLargeIons.Checked,
          RemoveIsotopicIons = cbRemovePrecursorIsotopicIons.Checked,
        };

        if (cbRemovePrecursor.Checked)
        {
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
      else
      {
        return null;
      }
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
      var option = GetConvertOption();

      return new MultipleRaw2MgfProcessor3(option)
      {
        ParallelMode = cbParallel.EnabledAndChecked
      };
    }

    private MascotGenericFormatWriter<Peak> GetMascotGenericFormatWriter()
    {
      MascotGenericFormatWriter<Peak> writer = new MascotGenericFormatWriter<Peak>()
      {
        TitleFormat = titleFormat.SelectedItem
      };

      writer.Comments.Clear();
      writer.Comments.AddRange(parameters);

      writer.DefaultCharges = defaultCharge.SelectedItem.DefaultCharges;

      return writer;
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
        new MultipleRaw2MgfProcessor3UI().MyShow();
      }

      #endregion
    }

    private void cbxIsobaricTypes_SelectedIndexChanged(object sender, EventArgs e)
    {
      if (isobaricTypes != null && removeIonWindow != null)
      {
        try
        {
          txtIsobaricIons.Text = GetConvertOption().GetIsobaricProcessor().ToString().Replace('\n', ';');
        }
        catch (Exception)
        {
        }
      }
    }

    private Raw2MgfOption GetConvertOption()
    {
      var result = new Raw2MgfOption();

      result.PrecursorOptions = GetPrecursorOptions();

      result.ConverterName = Title;
      result.ConverterVersion = Version;
      result.MascotTitleName = titleFormat.SelectedItem.FormatName;
      result.RawFiles = rawFiles.FileNames;
      result.TargetDirectory = GetOriginFile();
      result.GroupByMode = cbByMode.Checked;
      result.GroupByMsLevel = cbByMsLevel.Checked;
      result.PrecursorMassRange = new MassRange(minMassRange.Value, maxMassRange.Value);
      result.MinimumIonIntensity = minIonIntensity.Value;
      result.MinimumIonCount = minIonCount.Value;
      result.MinimumTotalIonCount = minTotalIonIntensity.Value;
      result.DefaultCharges = defaultCharge.SelectedItem;
      result.TopX = cbKeepTopX.Checked ? topX.Value : 0;
      result.ProductIonPPM = productIonPPM.Value;
      result.Deisotopic = cbDeisotopic.Checked;
      result.ChargeDeconvolution = cbDeconvolution.Checked;
      result.RemoveMassRange = cbRemoveMassRange.Checked;
      result.RemoveIonWindow = removeIonWindow.Value;
      result.RemoveSpecialIons = cbRemoveSpecialIons.Checked;
      result.SpecialIons = txtSpecialIons.Text;
      result.RemoveIsobaricIons = cbRemoveIsobaricIons.Checked;
      result.IsobaricType = isobaricTypes.SelectedItem;
      result.ProteaseName = proteases.SelectedItem.ToString();
      result.RemoveIsobaricIonsInLowRange = cbRemoveIsobaricIonsInLowRange.Checked;
      result.RemoveIsobaricIonsInHighRange = cbRemoveIsobaricIonsInHighRange.Checked;
      result.RemoveIsobaricIonsReporters = rbRemoveReporters.Checked;
      if (rbMassCalibrationNone.Checked || (!rbMassCalibration.Checked))
      {
        result.CalibrationType = MassCalibrationType.mctNone;
      }
      else
      {
        result.CalibratePrecursor = cbCalibratePrecursor.Checked;
        result.CalibrateProductIon = cbCalibrateProductIon.Checked;
        if (rbMassCalibrationByFixed.Checked)
        {
          result.CalibrationType = MassCalibrationType.mctFixed;
          result.ShiftPrecursorPPM = precursorShift.Value;
          result.ShiftProductIonPPM = productIonShift.Value;
        }
        else if (rbMassCalibrationByFile.Checked)
        {
          result.CalibrationType = MassCalibrationType.mctOffsetFile;
          result.OffsetFile = txtOffsetFile.FullName;
        }
        else
        {
          result.CalibrationType = MassCalibrationType.mctAuto;
          result.SilicoPolymers = siliconePolymers.GetSelectedPloymers().ToArray();
          result.MaxShiftPPM = maxShiftPPM.Value;
          result.RetentionTimeWindow = retentionTimeWindow.Value;
        }
      }

      result.RemovePrecursorAndNeutralLoss = cbRemovePrecursor.Checked;
      if (result.RemovePrecursorAndNeutralLoss)
      {
        result.NeutralLossAtomComposition = txtNeutralLossComposition.Text;
      }

      result.OutputMzXmlFormat = rbMzXml.Checked;
      return result;
    }

    private void rbMzXml_CheckedChanged(object sender, EventArgs e)
    {
      cbTitleFormat.Enabled = !rbMzXml.Checked;
      cbDefaultCharge.Enabled = !rbMzXml.Checked;
    }
  }
}