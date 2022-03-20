using RCPA.Proteomics.Mascot;
using RCPA.Proteomics.Summary;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace RCPA.Proteomics.MaxQuant
{
  public class MaxQuantEvidenceToPeptideProcessor : AbstractThreadProcessor
  {
    private MaxQuantEvidenceToPeptideProcessorOptions options;

    public MaxQuantEvidenceToPeptideProcessor(MaxQuantEvidenceToPeptideProcessorOptions options)
    {
      this.options = options;
    }

    private Regex modPattern = new Regex(@"(\(.+?\))");
    private string modstring = "*@#$%^&~";
    private Dictionary<string, char> moddic = new Dictionary<string, char>();

    public override IEnumerable<string> Process()
    {
      var evidences = new MascotPeptideTextFormat().ReadFromFile(options.InputFile);

      //Remove the PSM without mapped to proteins, usually it is from decoy database.
      evidences.RemoveAll(m => string.IsNullOrWhiteSpace(m.Annotations["Proteins"] as string));

      if (options.RemoveContanimant)
      {
        evidences.RemoveAll(m => (m.Annotations["Proteins"] as string).Contains("CON_"));
      }

      foreach (var spectrum in evidences)
      {
        ParseMaxQuantEvidencePeptide(spectrum);
      }

      new MascotPeptideTextFormat("\tFileScan\tSequence\tObs\tMH+\tDiff(MH+)\tDiffPPM\tCharge\tRank\tScore\tExpectValue\tReference\tMissCleavage\tModification\tMatchCount\tNumProteaseTermini").WriteToFile(options.OutputFile, evidences);

      return new string[] { options.OutputFile };
    }

    public void ParseMaxQuantEvidencePeptide(IIdentifiedSpectrum spectrum)
    {
      var proteins = (spectrum.Annotations["Proteins"] as string).Split(';');
      foreach (var protein in proteins)
      {
        spectrum.Peptide.AddProtein(protein);
      }

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
      spectrum.Annotations.Clear();
    }
  }
}
