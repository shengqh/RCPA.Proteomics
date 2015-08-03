using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using RCPA.Proteomics.Mascot;
using RCPA.Proteomics.Summary;
using RCPA.Proteomics.MaxQuant;
using System.IO;
using RCPA.Seq;
using RCPA.Proteomics.Modification;

namespace RCPA.Proteomics.MaxQuant
{
  public class MaxQuant2MascotPeptideProcessor2 : AbstractThreadProcessor
  {
    private MaxQuant2MascotPeptideProcessorOption options;

    public MaxQuant2MascotPeptideProcessor2(MaxQuant2MascotPeptideProcessorOption options)
    {
      this.options = options;
    }

    public override IEnumerable<string> Process()
    {
      var spectra = new MaxQuantPeptideTextReader().ReadFromFile(options.SiteFile);
      spectra.RemoveAll(m => m.DeltaScore < options.MinDeltaScore || m.Probability < options.MinProbability);
      spectra = (from g in spectra.GroupBy(m => m.Query.FileScan.ShortFileName)
                 select g.OrderBy(l => l.Score).Last()).ToList();

      if (options.IsSILAC)
      {
        var spmap = spectra.ToDictionary(m => m.Query.FileScan.ShortFileName);

        var existModificationChar = (from sp in spectra
                                     from c in sp.Sequence
                                     where !char.IsLetter(c)
                                     select c).Distinct().Count();

        Dictionary<char, char> labelChars = new Dictionary<char, char>();
        foreach (var c in options.SILACAminoacids)
        {
          labelChars[c] = ModificationConsts.MODIFICATION_CHAR[++existModificationChar];
        }

        using (var sr = new StreamReader(options.MSMSFile))
        {
          var headers = sr.ReadLine().Split('\t');
          var silacIndex = Array.IndexOf(headers, "SILAC State");
          var rawIndex = Array.IndexOf(headers, "Raw File");
          var scanIndex = Array.IndexOf(headers, "Scan Number");
          string line;
          while ((line = sr.ReadLine()) != null)
          {
            if (string.IsNullOrWhiteSpace(line))
            {
              break;
            }

            var parts = line.Split('\t');
            if (parts[silacIndex].Equals("Light"))
            {
              continue;
            }

            var raw = parts[rawIndex];
            var scan = int.Parse(parts[scanIndex]);
            var sf = new SequestFilename(raw, scan, scan, 0, "");
            var name = sf.ShortFileName;

            IIdentifiedSpectrum sp;
            if (spmap.TryGetValue(name, out sp))
            {
              foreach (var pep in sp.Peptides)
              {
                var seq = pep.Sequence;
                StringBuilder sb = new StringBuilder();
                for (int i = seq.Length - 1; i >= 0; i--)
                {
                  char heavyChar;
                  if (labelChars.TryGetValue(seq[i], out heavyChar))
                  {
                    sb.Append(heavyChar);
                  }
                  sb.Append(seq[i]);
                }
                pep.Sequence = SequenceUtils.GetReversedSequence(sb.ToString());
              }
            }
          }
        }
      }



      string resultFilename = options.SiteFile + ".peptides";
      new MascotPeptideTextFormat("\t\"File, Scan(s)\"\tSequence\tCharge\tScore\tDeltaScore\tExpectValue\tPValue\tModification").WriteToFile(resultFilename, spectra);

      return new[] { resultFilename };
    }
  }
}
