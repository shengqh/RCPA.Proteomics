using RCPA.Proteomics.Summary;
using System;
using System.Collections.Generic;
using System.Xml.Linq;

namespace RCPA.Proteomics.Percolator
{
  public class PercolatorInputXmlPsmReader : IFileReader<List<IIdentifiedSpectrum>>
  {
    public List<IIdentifiedSpectrum> ReadFromFile(string fileName)
    {
      var result = new List<IIdentifiedSpectrum>();
      XElement root = XElement.Load(fileName);
      var features = root.FindElement("featureDescriptions");
      var descriptions = features.FindElements("featureDescription");

      var missIndex = FindIndex(fileName, descriptions, "# Missed Cleavages");

      var scans = root.FindElements("fragSpectrumScan");
      foreach (var scan in scans)
      {
        var scanNumber = int.Parse(scan.FindAttribute("scanNumber").Value);
        var psms = scan.FindElements("peptideSpectrumMatch");
        foreach (var psm in psms)
        {
          IIdentifiedSpectrum spec = new IdentifiedSpectrum();
          spec.Query.QueryId = scanNumber;
          spec.Id = psm.FindAttribute("id").Value.StringAfter("decoy_");
          spec.FromDecoy = psm.FindAttribute("isDecoy").Value.Equals("true");
          spec.TheoreticalMH = double.Parse(psm.FindAttribute("calculatedMassToCharge").Value);
          spec.ExperimentalMH = double.Parse(psm.FindAttribute("experimentalMassToCharge").Value);
          spec.Query.Charge = int.Parse(psm.FindAttribute("chargeState").Value);
          var pep = new IdentifiedPeptide(spec);
          pep.Sequence = psm.FindElement("peptide").FindElement("peptideSequence").Value;
          pep.AddProtein(psm.FindElement("occurence").FindAttribute("proteinId").Value);

          var featureEles = psm.FindElement("features").FindElements("feature");
          //The first one is the score.
          spec.Score = double.Parse(featureEles[0].Value);
          spec.NumMissedCleavages = int.Parse(featureEles[missIndex].Value);
          result.Add(spec);
        }
      }

      return result;
    }

    private static int FindIndex(string fileName, List<XElement> descriptions, string featureDescription)
    {
      var expMHindex = descriptions.FindIndex(m => m.Attribute("description").Value.Equals(featureDescription));
      if (expMHindex == -1)
      {
        throw new Exception(string.Format("Cannot find feature '{0}' in {1}", featureDescription, fileName));
      }
      return expMHindex;
    }
  }
}
