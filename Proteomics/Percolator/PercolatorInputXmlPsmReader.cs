using RCPA.Proteomics.Summary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;

namespace RCPA.Proteomics.Percolator
{
  public class PercolatorInputXmlPsmReader : IFileReader<List<IIdentifiedSpectrum>>
  {
    public List<IIdentifiedSpectrum> ReadFromFile(string fileName)
    {
      var result = new List<IIdentifiedSpectrum>();
      XElement root = XElement.Load(fileName);
      var scans = root.FindElements("fragSpectrumScan");
      foreach (var scan in scans)
      {
        var scanNumber = int.Parse(scan.FindAttribute("scanNumber").Value);
        var psms = scan.FindElements("peptideSpectrumMatch");
        foreach (var psm in psms)
        {
          IIdentifiedSpectrum spec = new IdentifiedSpectrum();
          spec.Query.QueryId = scanNumber;
          spec.Id = int.Parse(psm.FindAttribute("id").Value.StringAfter("decoy_"));
          spec.FromDecoy = psm.FindAttribute("isDecoy").Value.Equals("true");
          spec.TheoreticalMH = double.Parse(psm.FindAttribute("calculatedMassToCharge").Value);
          spec.ExperimentalMH = double.Parse(psm.FindAttribute("experimentalMassToCharge").Value);
          spec.Query.Charge = int.Parse(psm.FindAttribute("chargeState").Value);
          var pep = new IdentifiedPeptide(spec);
          pep.Sequence = psm.FindElement("peptide").FindElement("peptideSequence").Value;
          pep.AddProtein(psm.FindElement("occurence").FindAttribute("proteinId").Value);
          result.Add(spec);
        }
      }

      return result;
    }
  }
}
