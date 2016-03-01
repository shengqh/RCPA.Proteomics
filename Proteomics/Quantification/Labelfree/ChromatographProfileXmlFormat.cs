using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;

namespace RCPA.Proteomics.Quantification.Labelfree
{
  public class ChromatographProfileXmlFormat : IFileFormat<ChromatographProfile>
  {
    public ChromatographProfile ReadFromFile(string fileName)
    {
      var result = new ChromatographProfile();
      var root = XElement.Load(fileName);

      result.Experimental = root.GetAttributeValue("Experimental", string.Empty);
      result.Sequence = root.GetAttributeValue("Sequence", string.Empty);
      result.Charge = root.GetAttributeValue("Charge", 0);
      result.IdentifiedScan = root.GetAttributeValue("IdentifiedScan", 0);
      result.ObservedMz = root.GetAttributeValue("ObservedMz", 0.0);
      result.TheoreticalMz = root.GetAttributeValue("TheoreticalMz", 0.0);

      List<IsotopicIon> ions = new List<IsotopicIon>();
      foreach (var ionEle in root.Element("IsotopicIons").Elements("Ion"))
      {
        var ion = new IsotopicIon();
        ion.Mz = ionEle.GetAttributeValue("Mz", 0.0);
        ion.Intensity = ionEle.GetAttributeValue("Intensity", 0.0);
        ions.Add(ion);
      }
      result.IsotopicIons = ions.ToArray();

      foreach (var proEle in root.Element("ProfileScans").Elements("ProfileScan"))
      {
        var pscan = new ChromatographProfileScan();
        result.Profiles.Add(pscan);
        pscan.Identified = proEle.GetAttributeValue("Identified", false);
        pscan.RetentionTime = proEle.GetAttributeValue("RetentionTime", 0.0);
        pscan.Scan = proEle.GetAttributeValue("Scan", 0);
        foreach (var scanEle in proEle.Elements("Peak"))
        {
          var peak = new ChromatographProfileScanPeak();
          pscan.Add(peak);
          peak.Mz = scanEle.GetAttributeValue("Mz", 0.0);
          peak.Intensity = scanEle.GetAttributeValue("Intensity", 0.0);
          peak.Charge = scanEle.GetAttributeValue("Charge", 0);
          peak.Noise = scanEle.GetAttributeValue("Noise", 0.0);
        }
      }

      return result;
    }

    public void WriteToFile(string fileName, ChromatographProfile chro)
    {
      var root = new XElement("Chromatograph",
        new XAttribute("Experimental", chro.Experimental),
        new XAttribute("Sequence", chro.Sequence),
        new XAttribute("Charge", chro.Charge),
        new XAttribute("IdentifiedScan", chro.IdentifiedScan),
        new XAttribute("ObservedMz", chro.ObservedMz),
        new XAttribute("TheoreticalMz", chro.TheoreticalMz),
        new XElement("IsotopicIons",
          (from ion in chro.IsotopicIons
           select new XElement("Ion",
             new XAttribute("Mz", ion.Mz),
             new XAttribute("Intensity", ion.Intensity)))),
        new XElement("ProfileScans",
          (from pro in chro.Profiles
           select new XElement("ProfileScan",
            new XAttribute("Identified", pro.Identified),
            new XAttribute("RetentionTime", pro.RetentionTime),
            new XAttribute("Scan", pro.Scan),
            (from peak in pro
             select new XElement("Peak",
              new XAttribute("Isotopic", pro.IndexOf(peak) + 1),
              new XAttribute("Mz", peak.Mz),
              new XAttribute("Intensity", peak.Intensity),
              new XAttribute("Charge", peak.Charge),
              new XAttribute("Noise", peak.Noise)))))));
      root.Save(fileName);
    }
  }
}
