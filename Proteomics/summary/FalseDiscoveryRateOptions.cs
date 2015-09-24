using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using RCPA.Proteomics.Summary.Uniform;
using RCPA.Proteomics.Mascot;

namespace RCPA.Proteomics.Summary
{
  public class FalseDiscoveryRateOptions : IXml
  {
    public bool FilterByFdr { get; set; }

    public FalseDiscoveryRateLevel FdrLevel { get; set; }

    public FalseDiscoveryRateType FdrType { get; set; }

    public int FdrPeptideCount { get; set; }

    public bool FilterOneHitWonder { get; set; }

    public int MinOneHitWonderPeptideCount { get; set; }

    public double MaxPeptideFdr { get; set; }

    public double FdrValue { get; set; }

    public ITargetDecoyConflictType TargetDecoyConflictType { get; set; }

    public bool ByDecoySpectra { get; set; }

    public int MinDecoyScan { get; set; }

    public double MinTargetDecoySpectraRatio { get; set; }

    public FalseDiscoveryRateOptions()
    {
      FilterByFdr = true;

      FdrLevel = FalseDiscoveryRateLevel.Protein;

      FdrType = FalseDiscoveryRateType.Target;

      FdrPeptideCount = 0;

      MaxPeptideFdr = 0.01;

      FdrValue = 0.01;

      TargetDecoyConflictType = ResolveTargetDecoyConflictTypeFactory.Decoy;

      FilterOneHitWonder = true;

      MinOneHitWonderPeptideCount = 2;

      ByDecoySpectra = false;

      MinDecoyScan = MascotGenericFormatShiftPrecursorProcessorOptions.DEFAULT_ShiftScan;

      MinTargetDecoySpectraRatio = 2.0;
    }

    public CalculateQValueFunc GetQValueFunction()
    {
      if (FdrLevel == FalseDiscoveryRateLevel.UniquePeptide)
      {
        return IdentifiedSpectrumUtils.CalculateUniqueQValue;
      }
      else
      {
        return IdentifiedSpectrumUtils.CalculateQValue;
      }
    }

    public IFalseDiscoveryRateCalculator GetFalseDiscoveryRateCalculator()
    {
      if (this.FdrType == FalseDiscoveryRateType.Target)
      {
        return new TargetFalseDiscoveryRateCalculator();
      }
      else
      {
        return new TotalFalseDiscoveryRateCalculator();
      }
    }

    public IIdentifiedSpectrumBuilder GetSpectrumBuilder()
    {
      if (!FilterByFdr)
      {
        return new UniformSpectrumFixedCriteriaBuilder();
      }

      if (FdrLevel == FalseDiscoveryRateLevel.Protein)
      {
        return new UniformSpectrumProteinFdrBuilder();
      }
      else if (FdrLevel == FalseDiscoveryRateLevel.SimpleProtein)
      {
        return new UniformSpectrumSimpleProteinFdrBuilder();
      }

      return new UniformSpectrumPeptideFdrBuilder();
    }

    public void Save(XElement parentNode)
    {
      parentNode.Add(new XElement("FalseDiscoveryRate",
        new XElement("Filtered", FilterByFdr.ToString()),
        new XElement("Level", FdrLevel.ToString()),
        new XElement("MaxPeptideFdr", MaxPeptideFdr.ToString()),
        new XElement("FdrPeptideCount", FdrPeptideCount.ToString()),
        new XElement("Type", FdrType.ToString()),
        new XElement("Value", MyConvert.Format(FdrValue)),
        new XElement("TargetDecoyConflictType", TargetDecoyConflictType.Name),
        new XElement("FilterOneHitWonder", FilterOneHitWonder.ToString()),
        new XElement("MinOneHitWonderPeptideCount", MinOneHitWonderPeptideCount.ToString()),
        new XElement("ByDecoySpectra", ByDecoySpectra.ToString()),
        new XElement("MinDecoyScan", MinDecoyScan.ToString()),
        new XElement("MinTargetDecoySpectraRatio", MinTargetDecoySpectraRatio.ToString())));
    }

    public void Load(XElement parentNode)
    {
      XElement xml = parentNode.Element("FalseDiscoveryRate");

      FilterByFdr = Convert.ToBoolean(xml.Element("Filtered").Value);
      FdrLevel = (FalseDiscoveryRateLevel)Enum.Parse(FalseDiscoveryRateLevel.Peptide.GetType(), xml.GetChildValue("Level", FalseDiscoveryRateLevel.Peptide.ToString()));
      MaxPeptideFdr = MyConvert.ToDouble(xml.Element("MaxPeptideFdr").Value);
      FdrPeptideCount = Convert.ToInt32(xml.Element("FdrPeptideCount").Value);
      FdrType = (FalseDiscoveryRateType)Enum.Parse(FalseDiscoveryRateType.Total.GetType(), xml.GetChildValue("Type", FalseDiscoveryRateType.Total.ToString()));
      FdrValue = MyConvert.ToDouble(xml.Element("Value").Value);
      if (xml.Element("TargetDecoyConflictType") != null)
      {
        TargetDecoyConflictType = ResolveTargetDecoyConflictTypeFactory.Find(xml.Element("TargetDecoyConflictType").Value);
      }
      if (xml.Element("FilterOneHitWonder") != null)
      {
        FilterOneHitWonder = bool.Parse(xml.Element("FilterOneHitWonder").Value);
      }
      else
      {
        FilterOneHitWonder = false;
      }

      if (xml.Element("MinOneHitWonderPeptideCount") != null)
      {
        MinOneHitWonderPeptideCount = int.Parse(xml.Element("MinOneHitWonderPeptideCount").Value);
      }
      else
      {
        MinOneHitWonderPeptideCount = 2;
      }
      if (xml.Element("ByDecoySpectra") != null)
      {
        ByDecoySpectra = bool.Parse(xml.Element("ByDecoySpectra").Value);
      }
      else
      {
        ByDecoySpectra = false;
      }
      if (xml.Element("MinDecoyScan") != null)
      {
        MinDecoyScan = int.Parse(xml.Element("MinDecoyScan").Value);
      }
      else
      {
        MinDecoyScan = MascotGenericFormatShiftPrecursorProcessorOptions.DEFAULT_ShiftScan;
      }
      if (xml.Element("MinTargetDecoySpectraRatio") != null)
      {
        MinTargetDecoySpectraRatio = double.Parse(xml.Element("MinTargetDecoySpectraRatio").Value);
      }
      else
      {
        MinTargetDecoySpectraRatio = 2.0;
      }
    }

    public static FalseDiscoveryRateOptions LoadOptions(XElement parentNode)
    {
      var result = new FalseDiscoveryRateOptions();
      result.Load(parentNode);
      return result;
    }
  }
}
