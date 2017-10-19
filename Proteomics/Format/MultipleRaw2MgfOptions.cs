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
using System.Xml.Linq;

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

  public class MultipleRaw2MgfOptions : AbstractRawConverterOptions, IXml
  {
    public MultipleRaw2MgfOptions()
    {
      this.TargetDirectory = string.Empty;

      this.ConverterName = MultipleRaw2MgfProcessorUI.Title;
      this.ConverterVersion = MultipleRaw2MgfProcessorUI.Version;

      this.MascotTitleName = "SEQUEST";
      this.PrecursorMassRange = new MassRange(400, 5000);
      this.MinimumIonIntensity = 1;
      this.MinimumIonCount = 15;
      this.MinimumTotalIonIntensity = 100;
      this.DefaultCharges = new RCPA.Proteomics.ChargeClass(new[] { 2, 3 });

      this.IsobaricType = IsobaricTypeFactory.IsobaricTypes.First();
      this.ProteaseName = "Trypsin";

      //CID
      this.ProductIonPPM = 20;
      this.ChargeDeconvolution = false;
      this.Deisotopic = false;

      this.KeepTopX = true;
      this.TopX = 8;

      this.RemoveIons = false;
      this.RemoveIonWindow = 0.1;
      this.SpecialIons = "113.5-117.5";

      this.PrecursorOptions = new PeakListRemovePrecursorProcessorOptions();
      this.RawFiles = new string[] { };
    }

    public MascotGenericFormatWriter<Peak> GetMGFWriter()
    {
      var result = new MascotGenericFormatWriter<Peak>()
      {
        TitleFormat = MascotTitleFactory.FindTitleOrDefault(MascotTitleName)
      };

      result.Comments.Clear();
      result.Comments.Add("Converter=" + ConverterName);
      result.Comments.Add("ConverterVersion=" + ConverterVersion);
      result.Comments.Add("DefaultCharges=" + DefaultCharges.ToString());
      result.Comments.Add("ProductIonPPM=" + ProductIonPPM.ToString());

      result.DefaultCharges = this.DefaultCharges.DefaultCharges;

      return result;
    }

    public string ConverterName { get; set; }

    public string ConverterVersion { get; set; }

    public string MascotTitleName { get; set; }

    public string[] RawFiles { get; set; }

    public MassRange PrecursorMassRange { get; set; }

    public double MinimumIonIntensity { get; set; }

    public int MinimumIonCount { get; set; }

    public double MinimumTotalIonIntensity { get; set; }

    public ChargeClass DefaultCharges { get; set; }

    public bool KeepTopX { get; set; }

    public int TopX { get; set; }

    public double ProductIonPPM { get; set; }

    public bool Deisotopic { get; set; }

    public bool ChargeDeconvolution { get; set; }

    public bool OutputMzXmlFormat { get; set; }

    public bool MzXmlNestedScan { get; set; }

    public bool RemoveIons { get; set; }

    public double RemoveIonWindow { get; set; }

    public bool RemoveSpecialIons { get; set; }

    public string SpecialIons { get; set; }

    public bool RemoveIsobaricIons { get; set; }

    public IsobaricType IsobaricType { get; set; }

    public string ProteaseName { get; set; }

    public bool RemoveIsobaricIonsReporters { get; set; }

    public bool RemoveIsobaricIonsInLowRange { get; set; }

    public bool RemoveIsobaricIonsInHighRange { get; set; }

    public bool ParallelMode { get; set; }

    public PeakListRemovePrecursorProcessorOptions PrecursorOptions { get; set; }

    public CompositeProcessor<PeakList<Peak>> GetProcessor(string fileName, IProgressCallback callBack)
    {
      CompositeProcessor<PeakList<Peak>> result = new CompositeProcessor<PeakList<Peak>>();

      if (!this.OutputMzXmlFormat)
      {
        AddGeneralProcessor(result);
      }

      if (RemoveIons)
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

      if (PrecursorOptions.RemovePrecursor)
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

      result.Add(new PeakListMinTotalIonIntensityProcessor<Peak>(MinimumTotalIonIntensity));
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

    public void Save(XElement parentNode)
    {
      parentNode.Add(
        new XElement("TargetDirectory", TargetDirectory),
        new XElement("RawFiles", from rf in RawFiles
                                 select new XElement("File", rf)),
        new XElement("MascotTitleName", MascotTitleName),
        new XElement("PrecursorMassRange", new XAttribute("From", PrecursorMassRange.From), new XAttribute("To", PrecursorMassRange.To)),
        new XElement("MinimumIonIntensity", MinimumIonIntensity),
        new XElement("MinimumIonCount", MinimumIonCount),
        new XElement("MinimumTotalIonIntensity", MinimumTotalIonIntensity),
        new XElement("DefaultCharges", from charge in DefaultCharges.DefaultCharges
                                       select new XElement("Charge", charge)),

        new XElement("ProductIonPPM", ProductIonPPM),
        new XElement("Deisotopic", Deisotopic),
        new XElement("ChargeDeconvolution", ChargeDeconvolution),

        new XElement("KeepTopX", KeepTopX),
        new XElement("TopX", TopX),
        new XElement("GroupByMode", GroupByMode),
        new XElement("GroupByMsLevel", GroupByMsLevel),
        new XElement("ParallelMode", ParallelMode),
        new XElement("ExtractRawMS3", ExtractRawMS3),
        new XElement("Overwrite", Overwrite),
        new XElement("OutputMzXmlFormat", OutputMzXmlFormat),
        new XElement("MzXmlNestedScan", MzXmlNestedScan),

        new XElement("RemoveIons", RemoveIons),
        new XElement("RemoveIonWindow", RemoveIonWindow),
        new XElement("RemoveSpecialIons", RemoveSpecialIons),
        new XElement("SpecialIons", SpecialIons),
        new XElement("RemoveIsobaricIons", RemoveIsobaricIons),
        new XElement("IsobaricType", IsobaricType.Name),
        new XElement("ProteaseName", ProteaseName),
        new XElement("RemoveIsobaricIonsReporters", RemoveIsobaricIonsReporters),
        new XElement("RemoveIsobaricIonsInLowRange", RemoveIsobaricIonsInLowRange),
        new XElement("RemoveIsobaricIonsInHighRange", RemoveIsobaricIonsInHighRange),

        new XElement("Precursor",
          new XElement("RemovePrecursor", PrecursorOptions.RemovePrecursor),
          new XElement("PPMTolerance", PrecursorOptions.PPMTolerance),
          new XElement("RemoveNeutralLoss", PrecursorOptions.RemoveNeutralLoss),
          new XElement("NeutralLoss", PrecursorOptions.NeutralLoss),
          new XElement("RemoveIsotopicIons", PrecursorOptions.RemoveIsotopicIons),
          new XElement("RemoveChargeMinus1Precursor", PrecursorOptions.RemoveChargeMinus1Precursor),
          new XElement("RemoveIonsLargerThanPrecursor", PrecursorOptions.RemoveIonLargerThanPrecursor)));
    }

    public void Load(XElement parentNode)
    {
      TargetDirectory = parentNode.Element("TargetDirectory").Value;
      RawFiles = (from file in parentNode.Element("RawFiles").Elements("File")
                  select file.Value).ToArray();
      MascotTitleName = parentNode.Element("MascotTitleName").Value;
      PrecursorMassRange = new MassRange(double.Parse(parentNode.Element("PrecursorMassRange").Attribute("From").Value), double.Parse(parentNode.Element("PrecursorMassRange").Attribute("To").Value));
      MinimumIonIntensity = double.Parse(parentNode.Element("MinimumIonIntensity").Value);
      MinimumIonCount = int.Parse(parentNode.Element("MinimumIonCount").Value);
      MinimumTotalIonIntensity = int.Parse(parentNode.Element("MinimumTotalIonIntensity").Value);
      DefaultCharges = new ChargeClass((from charge in parentNode.Element("DefaultCharges").Elements("Charge")
                                        select int.Parse(charge.Value)).ToArray());

      ProductIonPPM = double.Parse(parentNode.Element("ProductIonPPM").Value);
      Deisotopic = bool.Parse(parentNode.Element("Deisotopic").Value);
      ChargeDeconvolution = bool.Parse(parentNode.Element("ChargeDeconvolution").Value);

      KeepTopX = bool.Parse(parentNode.Element("KeepTopX").Value);
      TopX = int.Parse(parentNode.Element("TopX").Value);
      GroupByMode = bool.Parse(parentNode.Element("GroupByMode").Value);
      GroupByMsLevel = bool.Parse(parentNode.Element("GroupByMsLevel").Value);
      ParallelMode = bool.Parse(parentNode.Element("ParallelMode").Value);
      ExtractRawMS3 = bool.Parse(parentNode.Element("ExtractRawMS3").Value);
      Overwrite = bool.Parse(parentNode.Element("Overwrite").Value);
      OutputMzXmlFormat = bool.Parse(parentNode.Element("OutputMzXmlFormat").Value);
      MzXmlNestedScan = bool.Parse(parentNode.Element("MzXmlNestedScan").Value);

      RemoveIons = bool.Parse(parentNode.Element("RemoveIons").Value);
      RemoveIonWindow = double.Parse(parentNode.Element("RemoveIonWindow").Value);
      RemoveSpecialIons = bool.Parse(parentNode.Element("RemoveSpecialIons").Value);
      SpecialIons = parentNode.Element("SpecialIons").Value;
      RemoveIsobaricIons = bool.Parse(parentNode.Element("RemoveIsobaricIons").Value);
      IsobaricType = IsobaricTypeFactory.Find(parentNode.Element("IsobaricType").Value);
      ProteaseName = parentNode.Element("ProteaseName").Value;
      RemoveIsobaricIonsReporters = bool.Parse(parentNode.Element("RemoveIsobaricIonsReporters").Value);
      RemoveIsobaricIonsInLowRange = bool.Parse(parentNode.Element("RemoveIsobaricIonsInLowRange").Value);
      RemoveIsobaricIonsInHighRange = bool.Parse(parentNode.Element("RemoveIsobaricIonsInHighRange").Value);

      var precursorNode = parentNode.Element("Precursor");
      PrecursorOptions = new PeakListRemovePrecursorProcessorOptions();
      PrecursorOptions.RemovePrecursor = bool.Parse(precursorNode.Element("RemovePrecursor").Value);
      PrecursorOptions.PPMTolerance = double.Parse(precursorNode.Element("PPMTolerance").Value);
      PrecursorOptions.RemoveNeutralLoss = bool.Parse(precursorNode.Element("RemoveNeutralLoss").Value);
      PrecursorOptions.NeutralLoss = precursorNode.Element("NeutralLoss").Value;
      PrecursorOptions.RemoveChargeMinus1Precursor = bool.Parse(precursorNode.Element("RemoveChargeMinus1Precursor").Value);
      PrecursorOptions.RemoveIsotopicIons = bool.Parse(precursorNode.Element("RemoveIsotopicIons").Value);
      PrecursorOptions.RemoveIonLargerThanPrecursor = bool.Parse(precursorNode.Element("RemoveIonsLargerThanPrecursor").Value);
    }

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
