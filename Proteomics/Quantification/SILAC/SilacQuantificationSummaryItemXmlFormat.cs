using RCPA.Proteomics.Isotopic;
using RCPA.Proteomics.Spectrum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;

namespace RCPA.Proteomics.Quantification.SILAC
{
  public class SilacQuantificationSummaryItemXmlFormat : IFileFormat<SilacQuantificationSummaryItem>
  {
    #region IFileFormat<SilacQuantificationSummaryItem> Members

    public SilacQuantificationSummaryItem ReadFromFile(string filename)
    {
      XElement root = XElement.Load(filename);

      var result = new SilacQuantificationSummaryItem(root.GetChildValue("SampleIsLight", true));

      result.RawFilename = root.GetChildValue("RawFilename", "");
      result.SoftwareVersion = root.GetChildValue("SoftwareVersion", "");
      result.PeptideSequence = root.GetChildValue("PeptideSequence");
      result.Charge = root.GetChildValue("Charge", 2);
      result.SampleAtomComposition = root.GetChildValue("SampleAtomComposition");
      result.ReferenceAtomComposition = root.GetChildValue("ReferenceAtomComposition");
      result.Ratio = root.GetChildValue("Ratio", 0.0);
      result.RegressionCorrelation = root.GetChildValue("Correlation", 0.0);
      result.SampleAbundance = root.GetChildValue("SampleAbundance", 0.0);
      result.ReferenceAbundance = root.GetChildValue("ReferenceAbundance", 0.0);

      result.SampleProfile = new List<Peak>();
      ReadProfile(root, "SampleProfile", result.SampleProfile);

      result.ReferenceProfile = new List<Peak>();
      ReadProfile(root, "ReferenceProfile", result.ReferenceProfile);

      var observed = root.Element("ObservedEnvelopes");
      result.ObservedEnvelopes = new SilacEnvelopes();
      foreach (var pkl in observed.Elements())
      {
        var peakList = new SilacPeakListPair();
        int scan = Convert.ToInt32(pkl.Attribute("Scan").Value);
        double retentiontime = MyConvert.ToDouble(pkl.Attribute("Retentiontime").Value);
        peakList.Enabled = Convert.ToBoolean(pkl.Attribute("Enabled").Value);
        peakList.IsIdentified = Convert.ToBoolean(pkl.Attribute("Identified").Value);

        var extended = pkl.Attribute("ExtendedIdentification");
        if (extended != null)
        {
          peakList.IsExtendedIdentification = Convert.ToBoolean(pkl.Attribute("ExtendedIdentification").Value);
        }
        else
        {
          peakList.IsExtendedIdentification = false;
        }

        peakList.LightIntensity = MyConvert.ToDouble(pkl.Attribute("LightIntensity").Value);
        peakList.HeavyIntensity = MyConvert.ToDouble(pkl.Attribute("HeavyIntensity").Value);

        peakList.Light = new PeakList<Peak>();
        peakList.Light.ScanTimes.Add(new ScanTime(scan, retentiontime));

        peakList.Heavy = new PeakList<Peak>();
        peakList.Heavy.ScanTimes.Add(new ScanTime(scan, retentiontime));

        ReadPeakList(pkl, "LightPeakList", peakList.Light);
        ReadPeakList(pkl, "HeavyPeakList", peakList.Heavy);

        result.ObservedEnvelopes.Add(peakList);
      }

      if (result.LightProfile[0].Mz == 0.0)
      {
        EmassProfileBuilder builder = new EmassProfileBuilder();

        double minPercentage = 0.0001;

        AtomComposition acSample = new AtomComposition(result.SampleAtomComposition);
        result.SampleProfile = builder.GetProfile(acSample, result.Charge, minPercentage);

        AtomComposition acRef = new AtomComposition(result.ReferenceAtomComposition);
        result.ReferenceProfile = builder.GetProfile(acRef, result.Charge, minPercentage);
      }

      result.CalculateCorrelation();

      return result;
    }

    public void WriteToFile(string filename, SilacQuantificationSummaryItem t)
    {
      XElement root = new XElement("SilacQuantificationInformation",
        new XElement("RawFilename", t.RawFilename),
        new XElement("SoftwareVersion", t.SoftwareVersion),
        new XElement("PeptideSequence", t.PeptideSequence),
        new XElement("Charge", t.Charge),
        new XElement("SampleAtomComposition", t.SampleAtomComposition),
        new XElement("ReferenceAtomComposition", t.ReferenceAtomComposition),
        new XElement("SampleIsLight", t.SampleIsLight),
        new XElement("Ratio", MyConvert.Format("{0:0.0000}", t.Ratio)),
        new XElement("Correlation", MyConvert.Format("{0:0.0000}", t.RegressionCorrelation)),
        new XElement("SampleAbundance", MyConvert.Format("{0:0.0}", t.SampleAbundance)),
        new XElement("ReferenceAbundance", MyConvert.Format("{0:0.0}", t.ReferenceAbundance)),
        GetProfileElement("SampleProfile", t.SampleProfile),
        GetProfileElement("ReferenceProfile", t.ReferenceProfile),
        new XElement("ObservedEnvelopes",
          from pkl in t.ObservedEnvelopes
          select new XElement("Envelope",
            new XAttribute("Scan", pkl.Light.ScanTimes[0].Scan),
            new XAttribute("Retentiontime", MyConvert.Format("{0:0.00##}", pkl.Light.ScanTimes[0].RetentionTime)),
            new XAttribute("Enabled", pkl.Enabled),
            new XAttribute("Identified", pkl.IsIdentified),
            new XAttribute("ExtendedIdentification", pkl.IsExtendedIdentification),
            new XAttribute("LightIntensity", MyConvert.Format("{0:0.0}", pkl.LightIntensity)),
            new XAttribute("HeavyIntensity", MyConvert.Format("{0:0.0}", pkl.HeavyIntensity)),
            GetPeakListElement("LightPeakList", pkl.Light),
            GetPeakListElement("HeavyPeakList", pkl.Heavy))));
      root.Save(filename);
    }

    #endregion

    private void ReadPeakList(XElement pkl, string elementName, PeakList<Peak> peakList)
    {
      var pklNode = pkl.Element(elementName);
      foreach (var peak in pklNode.Elements())
      {
        double mz = MyConvert.ToDouble(peak.Attribute("Mz").Value);
        double intensity = MyConvert.ToDouble(peak.Attribute("Intensity").Value);
        var p = new Peak(mz, intensity);
        peakList.Add(p);
      }
    }

    private void ReadProfile(XElement root, string name, List<Peak> profile)
    {
      XElement profileEle = root.Element(name);
      foreach (var percentage in profileEle.Elements("Percentage"))
      {
        double mz = 0.0;
        if (percentage.Attribute("mz") != null)
        {
          mz = MyConvert.ToDouble(percentage.Attribute("mz").Value);
        }

        profile.Add(new Peak(mz, MyConvert.ToDouble(percentage.Value)));
      }
    }

    private XElement GetPeakListElement(string elementName, PeakList<Peak> pkl)
    {
      return new XElement(elementName,
                  from peak in pkl
                  select new XElement("Peak",
                    new XAttribute("Mz", MyConvert.Format("{0:0.0000}", peak.Mz)),
                    new XAttribute("Intensity", MyConvert.Format("{0:0.0}", peak.Intensity))));
    }

    private XElement GetProfileElement(string elementName, List<Peak> profile)
    {
      return new XElement(elementName,
         from p in profile
         select new XElement("Percentage", p.Intensity, new XAttribute("mz", MyConvert.Format("{0:0.00000}", p.Mz))));
    }
  }
}