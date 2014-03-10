using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.Xml;
using System.Xml.Linq;
using RCPA.Utils;
using RCPA.Proteomics.Spectrum;
using System;
using RCPA.Proteomics.Isotopic;

namespace RCPA.Proteomics.Quantification.O18
{
  public class O18QuantificationSummaryItemXmlFormat : IFileFormat<O18QuantificationSummaryItem>
  {
    #region IFileFormat<O18QuantificationSummaryItem> Members

    public O18QuantificationSummaryItem ReadFromFile(string filename)
    {
      var result = new O18QuantificationSummaryItem();

      var root = XElement.Load(filename);

      result.RawFilename = root.Element("RawFilename").Value;

      var ele = root.Element("SoftwareVersion");
      if (ele != null)
      {
        result.SoftwareVersion = ele.Value;
      }

      result.PeptideSequence = root.Element("PeptideSequence").Value;
      result.AdditionalFormula = root.Element("AdditionalFormula").Value;
      result.PeptideAtomComposition = root.Element("PeptideAtomComposition").Value;
      result.PurityOfO18Water = MyConvert.ToDouble(root.Element("PurityOfO18Water").Value);
      result.IsPostDigestionLabelling = bool.Parse(root.Element("IsPostDigestionLabelling").Value);
      if (root.Element("TheoreticalO16Mz") != null)
      {
        result.TheoreticalO16Mz = MyConvert.ToDouble(root.Element("TheoreticalO16Mz").Value);
        result.Charge = Convert.ToInt32(root.Element("Charge").Value);
      }

      result.PeptideProfile = (from x in root.Element("PeptideProfile").Elements()
                               let mzEle = x.Attribute("Mz")
                               let mz = mzEle == null ? 0.0 : MyConvert.ToDouble(mzEle.Value)
                               let intensity = MyConvert.ToDouble(x.Value)
                               select new Peak(mz, intensity)).ToList();

      if (result.PeptideProfile[0].Mz == 0.0)
      {
        var pkls = IsotopicBuilderFactory.GetBuilder().GetProfile(new AtomComposition(result.PeptideAtomComposition), result.Charge, 10);
        for (int i = 0; i < Math.Min(result.PeptideProfile.Count, pkls.Count); i++)
        {
          result.PeptideProfile[i].Mz = pkls[i].Mz;
          result.PeptideProfile[i].Intensity = pkls[i].Intensity;
        }
      }

      var observed = root.Element("ObservedEnvelopes");
      result.ObservedEnvelopes = new List<O18QuanEnvelope>();
      foreach (var pkl in observed.Elements())
      {
        var peakList = new O18QuanEnvelope();
        int scan = int.Parse(pkl.Attribute("Scan").Value);
        double retentiontime = MyConvert.ToDouble(pkl.Attribute("Retentiontime").Value);
        peakList.Enabled = bool.Parse(pkl.Attribute("Enabled").Value);
        peakList.IsIdentified = bool.Parse(pkl.Attribute("Identified").Value);

        peakList.ScanTimes.Add(new ScanTime(scan, retentiontime));
        foreach (var peak in pkl.Elements())
        {
          double mz = MyConvert.ToDouble(peak.Attribute("Mz").Value);
          double intensity = MyConvert.ToDouble(peak.Attribute("Intensity").Value);
          var p = new Peak(mz, intensity);
          peakList.Add(p);
        }
        result.ObservedEnvelopes.Add(peakList);
      }

      var species = root.Element("SpeciesAbundance");
      result.SpeciesAbundance.O16 = MyConvert.ToDouble(species.Element("O16").Value);
      result.SpeciesAbundance.O181 = MyConvert.ToDouble(species.Element("O181").Value);
      result.SpeciesAbundance.O182 = MyConvert.ToDouble(species.Element("O182").Value);

      var regression = species.Element("Regression");
      result.SpeciesAbundance.RegressionCorrelation = MyConvert.ToDouble(regression.Attribute("Correlation").Value);
      result.SpeciesAbundance.RegressionItems.Clear();
      foreach (var item in regression.Elements())
      {
        var ritem = new SpeciesRegressionItem();
        ritem.Mz = MyConvert.ToDouble(item.Attribute("Mz").Value);
        ritem.ObservedIntensity = MyConvert.ToDouble(item.Attribute("Observed").Value);
        ritem.RegressionIntensity = MyConvert.ToDouble(item.Attribute("Regression").Value);
        result.SpeciesAbundance.RegressionItems.Add(ritem);
      }

      var samples = root.Element("SampleAbundance");

      result.SampleAbundance.LabellingEfficiency = MyConvert.ToDouble(samples.Attribute("LabellingEfficiency").Value);
      result.SampleAbundance.Ratio = MyConvert.ToDouble(samples.Attribute("Ratio").Value);
      result.SampleAbundance.O16 = MyConvert.ToDouble(samples.Element("O16").Value);
      result.SampleAbundance.O18 = MyConvert.ToDouble(samples.Element("O18").Value);

      return result;
    }

    public void WriteToFile(string filename, O18QuantificationSummaryItem t)
    {
      XElement root = new XElement("O18QuantificationInformation",
        new XElement("RawFilename", t.RawFilename),
        new XElement("SoftwareVersion",t.SoftwareVersion),
        new XElement("PeptideSequence", t.PeptideSequence),
        new XElement("AdditionalFormula", t.AdditionalFormula),
        new XElement("PeptideAtomComposition", t.PeptideAtomComposition),
        new XElement("PurityOfO18Water", t.PurityOfO18Water),
        new XElement("IsPostDigestionLabelling", t.IsPostDigestionLabelling),
        new XElement("TheoreticalO16Mz", MyConvert.Format("{0:0.00000}", t.TheoreticalO16Mz)),
        new XElement("Charge", t.Charge),
        new XElement("PeptideProfile",
          from peak in t.PeptideProfile
          select new XElement("Percentage", new XAttribute("Mz", peak.Mz),  MyConvert.Format("{0:0.0000}", peak.Intensity))
        ),
        new XElement("ObservedEnvelopes",
          from pkl in t.ObservedEnvelopes
          select new XElement("Envelope",
            new XAttribute("Scan", pkl.Scan),
            new XAttribute("Retentiontime", MyConvert.Format("{0:0.00##}", pkl.ScanTimes[0].RetentionTime)),
            new XAttribute("Enabled", pkl.Enabled),
            new XAttribute("Identified", pkl.IsIdentified),
            from peak in pkl
            select new XElement("Peak",
              new XAttribute("Mz", MyConvert.Format("{0:0.0000}", peak.Mz)),
              new XAttribute("Intensity", MyConvert.Format("{0:0.0}", peak.Intensity))
            )
          )
        ),
        new XElement("SpeciesAbundance",
          new XElement("O16", MyConvert.Format("{0:0.0}", t.SpeciesAbundance.O16)),
          new XElement("O181", MyConvert.Format("{0:0.0}", t.SpeciesAbundance.O181)),
          new XElement("O182", MyConvert.Format("{0:0.0}", t.SpeciesAbundance.O182)),
          new XElement("Regression",
            new XAttribute("Correlation", MyConvert.Format("{0:0.0000}", t.SpeciesAbundance.RegressionCorrelation)),
            from ri in t.SpeciesAbundance.RegressionItems
            select new XElement("Peak",
              new XAttribute("Mz", MyConvert.Format("{0:0.0000}", ri.Mz)),
              new XAttribute("Observed", MyConvert.Format("{0:0.0}", ri.ObservedIntensity)),
              new XAttribute("Regression", MyConvert.Format("{0:0.0}", ri.RegressionIntensity))
            )
          )
        ),
        new XElement("SampleAbundance",
          new XAttribute("LabellingEfficiency", MyConvert.Format("{0:0.0000}", t.SampleAbundance.LabellingEfficiency)),
          new XAttribute("Ratio", MyConvert.Format("{0:0.00}", t.SampleAbundance.Ratio)),
          new XElement("O16", MyConvert.Format("{0:0.0}", t.SampleAbundance.O16)),
          new XElement("O18", MyConvert.Format("{0:0.0}", t.SampleAbundance.O18))
        )
      );

      root.Save(filename);
    }

    #endregion
  }
}