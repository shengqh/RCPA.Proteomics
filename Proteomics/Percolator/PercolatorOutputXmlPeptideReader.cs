using RCPA.Proteomics.Summary;
using System.Collections.Generic;
using System.Xml.Linq;

namespace RCPA.Proteomics.Percolator
{
  public class PercolatorOutputXmlPeptideReader : IFileReader<List<IIdentifiedSpectrum>>
  {
    public List<IIdentifiedSpectrum> ReadFromFile(string fileName)
    {
      var result = new List<IIdentifiedSpectrum>();
      XElement root = XElement.Load(fileName);
      var peptides = root.FindElement("peptides").FindElements("peptide");
      foreach (var peptide in peptides)
      {
        IIdentifiedSpectrum spec = new IdentifiedSpectrum();
        var pep = new IdentifiedPeptide(spec);

        pep.Sequence = peptide.FindAttribute("peptide_id").Value;
        spec.FromDecoy = peptide.FindAttribute("decoy").Value.Equals("true");
        spec.SpScore = double.Parse(peptide.FindElement("svm_score").Value);
        spec.QValue = double.Parse(peptide.FindElement("q_value").Value);
        spec.Score = double.Parse(peptide.FindElement("pep").Value);
        spec.TheoreticalMass = double.Parse(peptide.FindElement("calc_mass").Value);
        pep.AddProtein(peptide.FindElement("protein_id").Value);
        spec.Probability = double.Parse(peptide.FindElement("p_value").Value);
        result.Add(spec);
      }

      return result;
    }
  }
}
