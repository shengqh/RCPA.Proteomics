using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using RCPA.Proteomics.Mascot;
using RCPA.Proteomics.Spectrum;
using RCPA.Proteomics.Quantification.IsobaricLabelling;
using RCPA.Proteomics.Processor;
using System.IO;
using RCPA.Proteomics.Format.Offset;
using RCPA.Proteomics.Statistic;
using RCPA.Proteomics.Raw;
using RCPA.Utils;

namespace RCPA.Proteomics.Format
{
  public enum MassCalibrationType { mctNone, mctFixed, mctOffsetFile, mctAuto };

  public class MassRange
  {
    public MassRange(double from, double to)
    {
      this.From = from;
      this.To = to;
    }

    public double From { get; set; }

    public double To { get; set; }
  }

  public class MultipleRaw2MgfOptions : AbstractRawConverterOptions
  {
    public MultipleRaw2MgfOptions()
    {
      this.ConverterName = MultipleRaw2MgfProcessorUI.Title;
      this.ConverterVersion = MultipleRaw2MgfProcessorUI.Version;

      this.MascotTitleName = "SEQUEST";
      this.PrecursorMassRange = new MassRange(400, 5000);
      this.MinimumIonIntensity = 1;
      this.MinimumIonCount = 15;
      this.MinimumTotalIonCount = 100;
      this.DefaultCharges = new RCPA.Proteomics.ChargeClass(new[] { 2, 3 });

      //CID
      this.ProductIonPPM = 20;
      this.ChargeDeconvolution = false;
      this.Deisotopic = false;

      this.TopX = 0;

      this.RemoveMassRange = false;
      this.CalibratePrecursor = false;
      this.CalibrateProductIon = false;

      this.Shift10Dalton = false;
    }

    public MascotGenericFormatWriter<Peak> GetMGFWriter()
    {
      var result = new MascotGenericFormatWriter<Peak>()
      {
        TitleFormat = MascotTitleFactory.FindTitleOrDefault(MascotTitleName)
      };

      result.Comments.Clear();
      result.Comments.Add("Converter=" + ConverterName);
      result.Comments.Add("DefaultCharges=" + DefaultCharges.ToString());
      result.Comments.Add("ProductIonPPM=" + ProductIonPPM.ToString());

      result.DefaultCharges = this.DefaultCharges.DefaultCharges;

      return result;
    }

    public bool Shift10Dalton { get; set; }

    public string ConverterName { get; set; }

    public string ConverterVersion { get; set; }

    public string MascotTitleName { get; set; }

    public string[] RawFiles { get; set; }

    public MassRange PrecursorMassRange { get; set; }

    public double MinimumIonIntensity { get; set; }

    public int MinimumIonCount { get; set; }

    public double MinimumTotalIonCount { get; set; }

    public ChargeClass DefaultCharges { get; set; }

    public bool KeepTopX
    {
      get
      {
        return TopX > 0;
      }
    }

    public int TopX { get; set; }

    public double ProductIonPPM { get; set; }

    public bool Deisotopic { get; set; }

    public bool ChargeDeconvolution { get; set; }

    public bool RemoveMassRange { get; set; }

    public double RemoveIonWindow { get; set; }

    public bool RemoveSpecialIons { get; set; }

    public string SpecialIons { get; set; }

    public bool RemoveIsobaricIons { get; set; }

    public IsobaricType IsobaricType { get; set; }

    public string ProteaseName { get; set; }

    public bool RemoveIsobaricIonsReporters { get; set; }

    public bool RemoveIsobaricIonsInLowRange { get; set; }

    public bool RemoveIsobaricIonsInHighRange { get; set; }

    public MassCalibrationType CalibrationType { get; set; }

    public bool CalibratePrecursor { get; set; }

    public bool CalibrateProductIon { get; set; }

    public double ShiftPrecursorPPM { get; set; }

    public double ShiftProductIonPPM { get; set; }

    public bool OutputMzXmlFormat { get; set; }

    private string _offsetFile;

    private Dictionary<string, PrecursorOffsetEntry> _expOffsets = null;
    public string OffsetFile
    {
      get
      {
        return _offsetFile;
      }
      set
      {
        if (_offsetFile != value)
        {
          _offsetFile = value;
        }

        if (File.Exists(_offsetFile))
        {
          _expOffsets = PrecursorOffsetEntry.ReadFromFile(_offsetFile).ToDictionary(m => m.Experimental);
        }
      }
    }

    public double[] SilicoPolymers { get; set; }

    public double MaxShiftPPM { get; set; }

    public double RetentionTimeWindow { get; set; }

    public bool RemovePrecursorAndNeutralLoss { get; set; }

    public string NeutralLossAtomComposition { get; set; }

    public bool RemoveIonsLargerThanPrecursor { get; set; }

    public CompositeProcessor<PeakList<Peak>> GetProcessor(string fileName, IProgressCallback callBack)
    {
      CompositeProcessor<PeakList<Peak>> result = new CompositeProcessor<PeakList<Peak>>();

      if (!this.OutputMzXmlFormat)
      {
        AddGeneralProcessor(result);
      }

      AddCalibrationProcessor(result, fileName, callBack);

      if (RemoveMassRange)
      {
        if (RemoveIsobaricIons)
        {
          result.Add(GetIsobaricProcessor());
        }

        if (RemoveSpecialIons)
        {
          var ranges = ParseRemoveMassRange();
          foreach (var range in ranges)
          {
            result.Add(new PeakListRemoveMassRangeProcessor<Peak>(range.From, range.To));
          }
        }
      }

      if (Deisotopic)
      {
        result.Add(new PeakListDeisotopicByChargeProcessor<Peak>(ProductIonPPM));
      }

      if (PrecursorOptions != null)
      {
        result.Add(new PeakListRemovePrecursorProcessor<Peak>(this.PrecursorOptions));
      }

      if (ChargeDeconvolution)
      {
        result.Add(new PeakListDeconvolutionByChargeProcessor<Peak>(ProductIonPPM));
        if (Deisotopic)
        {
          result.Add(new PeakListDeisotopicByChargeProcessor<Peak>(ProductIonPPM));
        }
      }

      if (KeepTopX)
      {
        result.Add(new PeakListTopXProcessor<Peak>(TopX));
      }

      if (Shift10Dalton)
      {
        result.Add(new PeakListPrecursorShiftDaltonProcessor<Peak>(10.0));
      }

      return result;
    }

    public PeakListRemoveIsobaricIonProcessor<Peak> GetIsobaricProcessor()
    {
      var options = new PeakListRemoveIsobaricIonProcessorOptions();
      options.IsoType = IsobaricType;
      options.MzTolerance = RemoveIonWindow;
      options.Protease = IsobaricLabellingProteaseFactory.GetProtease(ProteaseName);
      options.RemoveLowerRange = RemoveIsobaricIonsInLowRange;
      options.RemoveHighRange = RemoveIsobaricIonsInHighRange;
      options.RemoveReporters = RemoveIsobaricIonsReporters;
      var processor = new PeakListRemoveIsobaricIonProcessor<Peak>(options);
      return processor;
    }

    private void AddGeneralProcessor(CompositeProcessor<PeakList<Peak>> result)
    {
      result.Add(new PeakListMassRangeProcessor<Peak>(PrecursorMassRange.From, PrecursorMassRange.To, new int[] { 2, 3 }));

      result.Add(new PeakListMinIonIntensityProcessor<Peak>(MinimumIonIntensity));

      result.Add(new PeakListMinIonCountProcessor<Peak>(MinimumIonCount));

      result.Add(new PeakListMinTotalIonIntensityProcessor<Peak>(MinimumTotalIonCount));
    }

    private void AddCalibrationProcessor(CompositeProcessor<PeakList<Peak>> result, string fileName, IProgressCallback callBack)
    {
      if (CalibrationType == MassCalibrationType.mctFixed)
      {
        result.Add(new PeakListShiftProcessor<Peak>(new FixedOffset(CalibratePrecursor ? ShiftPrecursorPPM : 0.0, CalibrateProductIon ? ShiftProductIonPPM : 0.0)));
        return;
      }

      if (CalibrationType == MassCalibrationType.mctOffsetFile)
      {
        var exp = RawFileFactory.GetExperimental(fileName);
        if (_expOffsets.ContainsKey(exp))
        {
          var offset = _expOffsets[exp];
          result.Add(new PeakListShiftProcessor<Peak>(new FixedOffset(CalibratePrecursor ? offset.Median : 0.0, CalibrateProductIon ? offset.Median : 0.0)));
        }
        else
        {
          return;
        }
      }

      if (CalibrationType == MassCalibrationType.mctAuto)
      {
        result.Add(new PeakListShiftProcessor<Peak>(new AutoOffset(this.SilicoPolymers, this.MaxShiftPPM, this.RetentionTimeWindow, callBack, fileName, this.CalibratePrecursor, this.CalibrateProductIon)));
        return;
      }
    }

    private List<MassRange> ParseRemoveMassRange()
    {
      var mzWindow = RemoveIonWindow;

      List<MassRange> result = new List<MassRange>();

      var parts = SpecialIons.Split(new char[] { ',', ';' }, StringSplitOptions.RemoveEmptyEntries);

      bool bWrongFormat = parts.Length == 0;

      foreach (var part in parts)
      {
        var values = part.Split(new char[] { '-' }, StringSplitOptions.RemoveEmptyEntries);
        if (values.Length == 1)
        {
          try
          {
            var tagmass = MyConvert.ToDouble(values[0]);
            result.Add(new MassRange(tagmass - mzWindow, tagmass + mzWindow));
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
            result.Add(new MassRange(first, second));
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

    public PeakListRemovePrecursorProcessorOptions PrecursorOptions { get; set; }

    public override string Extension
    {
      get
      {
        if (this.OutputMzXmlFormat)
        {
          return "mzXML";
        }
        else
        {
          return "mgf";
        }
      }
    }
  }
}
