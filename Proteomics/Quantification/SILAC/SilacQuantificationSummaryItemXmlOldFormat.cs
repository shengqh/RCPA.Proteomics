using System.Collections.Generic;
using System.Text;
using System.Xml;
using RCPA.Utils;
using RCPA.Proteomics.Spectrum;
using System;

namespace RCPA.Proteomics.Quantification.SILAC
{
  public class SilacQuantificationSummaryItemXmlOldFormat : IFileFormat<SilacQuantificationSummaryItem>
  {
    #region IFileFormat<SilacQuantificationSummaryItem> Members

    public SilacQuantificationSummaryItem ReadFromFile(string filename)
    {
      var doc = new XmlDocument();
      doc.Load(filename);

      XmlNode root = doc.DocumentElement;
      var xmlHelper = new XmlHelper(doc);

      var result = new SilacQuantificationSummaryItem(bool.Parse(xmlHelper.GetValidChild(root, "SampleIsLight").InnerText));

      result.RawFilename = xmlHelper.GetValidChild(root, "RawFilename").InnerText;

      XmlNode n = xmlHelper.GetFirstChild(root, "SoftwareVersion");
      if (n != null)
      {
        result.SoftwareVersion = n.InnerText;
      }

      result.PeptideSequence = xmlHelper.GetValidChild(root, "PeptideSequence").InnerText;
      result.Charge = Convert.ToInt32(xmlHelper.GetValidChild(root, "Charge").InnerText);
      result.SampleAtomComposition = xmlHelper.GetValidChild(root, "SampleAtomComposition").InnerText;
      result.ReferenceAtomComposition = xmlHelper.GetValidChild(root, "ReferenceAtomComposition").InnerText;
      result.Ratio = MyConvert.ToDouble(xmlHelper.GetValidChild(root, "Ratio").InnerText);
      result.RegressionCorrelation = MyConvert.ToDouble(xmlHelper.GetValidChild(root, "Correlation").InnerText);
      result.SampleAbundance = MyConvert.ToDouble(xmlHelper.GetValidChild(root, "SampleAbundance").InnerText);
      result.ReferenceAbundance = MyConvert.ToDouble(xmlHelper.GetValidChild(root, "ReferenceAbundance").InnerText);

      result.SampleProfile = new List<Peak>();
      ReadProfile(xmlHelper, root, "SampleProfile", result.SampleProfile);

      result.ReferenceProfile = new List<Peak>();
      ReadProfile(xmlHelper, root, "ReferenceProfile", result.ReferenceProfile);

      XmlNode observed = xmlHelper.GetValidChild(root, "ObservedEnvelopes");
      result.ObservedEnvelopes = new SilacEnvelopes();
      foreach (XmlNode pkl in observed.ChildNodes)
      {
        var peakList = new SilacPeakListPair();
        int scan = int.Parse(pkl.Attributes["Scan"].InnerText);
        double retentiontime = MyConvert.ToDouble(pkl.Attributes["Retentiontime"].InnerText);
        peakList.Enabled = bool.Parse(pkl.Attributes["Enabled"].InnerText);
        peakList.IsIdentified = bool.Parse(pkl.Attributes["Identified"].InnerText);

        var extended = pkl.Attributes["ExtendedIdentification"];
        if (extended != null)
        {
          peakList.IsExtendedIdentification = bool.Parse(pkl.Attributes["ExtendedIdentification"].InnerText);
        }
        else
        {
          peakList.IsExtendedIdentification = false;
        }

        peakList.LightIntensity = MyConvert.ToDouble(pkl.Attributes["LightIntensity"].InnerText);
        peakList.HeavyIntensity = MyConvert.ToDouble(pkl.Attributes["HeavyIntensity"].InnerText);

        peakList.Light = new PeakList<Peak>();
        peakList.Light.ScanTimes.Add(new ScanTime(scan, retentiontime));

        peakList.Heavy = new PeakList<Peak>();
        peakList.Heavy.ScanTimes.Add(new ScanTime(scan, retentiontime));

        ReadPeakList(xmlHelper, pkl, "LightPeakList", peakList.Light);
        ReadPeakList(xmlHelper, pkl, "HeavyPeakList", peakList.Heavy);

        result.ObservedEnvelopes.Add(peakList);
      }

      result.CalculateCorrelation();

      return result;
    }

    public void WriteToFile(string filename, SilacQuantificationSummaryItem t)
    {
      using (var writer = XmlUtils.CreateWriter(filename, Encoding.ASCII))
      {
        writer.WriteStartDocument();
        writer.WriteStartElement("SilacQuantificationInformation");

        writer.WriteElementString("RawFilename", t.RawFilename);
        writer.WriteElementString("SoftwareVersion", t.SoftwareVersion);
        writer.WriteElementString("PeptideSequence", t.PeptideSequence);
        writer.WriteElementString("Charge", t.Charge.ToString());
        writer.WriteElementString("SampleAtomComposition", t.SampleAtomComposition);
        writer.WriteElementString("ReferenceAtomComposition", t.ReferenceAtomComposition);
        writer.WriteElementString("SampleIsLight", t.SampleIsLight.ToString());
        writer.WriteElementString("Ratio", MyConvert.Format("{0:0.0000}", t.Ratio));
        writer.WriteElementString("Correlation", MyConvert.Format("{0:0.0000}", t.RegressionCorrelation));
        writer.WriteElementString("SampleAbundance", MyConvert.Format("{0:0.0}", t.SampleAbundance));
        writer.WriteElementString("ReferenceAbundance", MyConvert.Format("{0:0.0}", t.ReferenceAbundance));

        WriteProfile(writer, "SampleProfile", t.SampleProfile);
        WriteProfile(writer, "ReferenceProfile", t.ReferenceProfile);

        writer.WriteStartElement("ObservedEnvelopes");
        foreach (SilacPeakListPair pkl in t.ObservedEnvelopes)
        {
          writer.WriteStartElement("Envelope");

          writer.WriteAttributeString("Scan", pkl.Light.ScanTimes[0].Scan.ToString());
          writer.WriteAttributeString("Retentiontime", MyConvert.Format("{0:0.00##}", pkl.Light.ScanTimes[0].RetentionTime));
          writer.WriteAttributeString("Enabled", pkl.Enabled.ToString());
          writer.WriteAttributeString("Identified", pkl.IsIdentified.ToString());
          writer.WriteAttributeString("ExtendedIdentification", pkl.IsExtendedIdentification.ToString());
          writer.WriteAttributeString("LightIntensity", MyConvert.Format("{0:0.0}", pkl.LightIntensity));
          writer.WriteAttributeString("HeavyIntensity", MyConvert.Format("{0:0.0}", pkl.HeavyIntensity));

          WritePeakList(writer, "LightPeakList", pkl.Light);
          WritePeakList(writer, "HeavyPeakList", pkl.Heavy);

          writer.WriteEndElement();
        }
        writer.WriteEndElement();

        writer.WriteEndElement();
        writer.WriteEndDocument();
      }
    }

    #endregion

    private void ReadPeakList(XmlHelper xmlHelper, XmlNode pkl, string elementName, PeakList<Peak> peakList)
    {
      XmlNode pklNode = xmlHelper.GetValidChild(pkl, elementName);
      foreach (XmlNode peak in pklNode.ChildNodes)
      {
        double mz = MyConvert.ToDouble(peak.Attributes["Mz"].InnerText);
        double intensity = MyConvert.ToDouble(peak.Attributes["Intensity"].InnerText);
        var p = new Peak(mz, intensity);
        peakList.Add(p);
      }
    }

    private void ReadProfile(XmlHelper xmlHelper, XmlNode root, string name, List<Peak> profile)
    {
      XmlNode sampleProfile = xmlHelper.GetValidChild(root, name);
      foreach (XmlNode percentage in sampleProfile.ChildNodes)
      {
        profile.Add(new Peak(0.0, MyConvert.ToDouble(percentage.InnerText)));
      }
    }

    private static void WritePeakList(XmlTextWriter writer, string elementName, PeakList<Peak> pkl)
    {
      writer.WriteStartElement(elementName);
      foreach (IPeak peak in pkl)
      {
        writer.WriteStartElement("Peak");
        writer.WriteAttributeString("Mz", MyConvert.Format("{0:0.0000}", peak.Mz));
        writer.WriteAttributeString("Intensity", MyConvert.Format("{0:0.0}", peak.Intensity));
        writer.WriteEndElement();
      }
      writer.WriteEndElement();
    }

    private static void WriteProfile(XmlTextWriter writer, string name, List<Peak> profile)
    {
      writer.WriteStartElement(name);
      foreach (var value in profile)
      {
        writer.WriteElementString("Percentage", MyConvert.Format("{0:0.0000}", value.Intensity));
      }
      writer.WriteEndElement();
    }
  }
}