using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using RCPA.Proteomics.Mascot;
using System.Text.RegularExpressions;
using RCPA.Proteomics.Summary;
using System.IO;

namespace RCPA.Proteomics.MaxQuant
{
  public class MaxQuantEvidenceToPeptideProcessor : AbstractThreadFileProcessor
  {
    public override IEnumerable<string> Process(string fileName)
    {
      var evidences = new MascotPeptideTextFormat().ReadFromFile(fileName);

      var modPattern = new Regex(@"(\(.+?\))");
      var modstring = "*@#$%^&~";
      var moddic = new Dictionary<string, char>();
      foreach (var spectrum in evidences)
      {
        if (spectrum.Modifications == null || spectrum.Modifications.Equals("Unmodified"))
        {
          spectrum.Modifications = string.Empty;
        }

        if (spectrum.Annotations.ContainsKey("PEP"))
        {
          spectrum.ExpectValue = double.Parse(spectrum.Annotations["PEP"] as string);
        }

        spectrum.Query.FileScan.Experimental = spectrum.Annotations["Raw file"] as string;
        spectrum.Query.FileScan.FirstScan = int.Parse(spectrum.Annotations["MS/MS Scan Number"] as string);
        spectrum.Query.FileScan.LastScan = spectrum.Query.FileScan.FirstScan;
        spectrum.TheoreticalMass = double.Parse(spectrum.Annotations["Mass"] as string);

        if (spectrum.Annotations.ContainsKey("MS/MS m/z"))
        {
          spectrum.ObservedMz = double.Parse(spectrum.Annotations["MS/MS m/z"] as string);
        }
        spectrum.Rank = 1;

        var newseq = spectrum.Sequence;
        var m = modPattern.Match(newseq);
        while (m.Success)
        {
          var mod = m.Groups[1].Value;
          if (!moddic.ContainsKey(mod))
          {
            moddic[mod] = modstring[moddic.Count];
          }
          var modc = moddic[mod];
          newseq = newseq.Replace(mod, modc.ToString());
          m = m.NextMatch();
        }

        if (newseq.StartsWith("_"))
        {
          newseq = "-." + newseq.Substring(1);
        }
        if (newseq.EndsWith("_"))
        {
          newseq = newseq.Substring(0, newseq.Length - 1) + ".-";
        }

        spectrum.Peptide.Sequence = newseq;
        var proteins = (spectrum.Annotations["Proteins"] as string).Split(';');
        foreach (var protein in proteins)
        {
          spectrum.Peptide.AddProtein(protein);
        }
        spectrum.Annotations.Clear();
      }

      var result = Path.ChangeExtension(fileName, "peptides");
      new MascotPeptideTextFormat("\tFileScan\tSequence\tObs\tMH+\tDiff(MH+)\tDiffPPM\tCharge\tRank\tScore\tExpectValue\tReference\tMissCleavage\tModification\tMatchCount\tNumProteaseTermini").WriteToFile(result, evidences);

      return new string[] { result };
    }
  }
}
