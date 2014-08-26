using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using RCPA.Proteomics.Mascot;
using RCPA.Proteomics.Utils;
using System.Text.RegularExpressions;
using System.IO;

namespace RCPA.Proteomics.Quantification.IsobaricLabelling
{
  public class IsobaricLabelingEfficiencyCalculator : AbstractThreadFileProcessor
  {
    private char modificationAminoacid;

    public IsobaricLabelingEfficiencyCalculator(char modificationAminoacid)
    {
      this.modificationAminoacid = modificationAminoacid;
    }

    public override IEnumerable<string> Process(string fileName)
    {
      var peptides = new MascotPeptideTextFormat().ReadFromFile(fileName);

      var seqs = (from p in peptides
                  let pep = p.Peptide
                  select PeptideUtils.GetMatchedSequence(pep.Sequence)).ToList();

      var nmod = (from seq in seqs
                  where !char.IsLetter(seq[0])
                  select seq).Count();

      var kseqs = (from seq in seqs
                   where seq.Contains(modificationAminoacid)
                   select seq).Count();

      var kmod = (from seq in seqs
                  where seq.Contains(modificationAminoacid) && IsFullModifiedK(seq)
                  select seq).Count();

      var result = fileName + ".labelingEfficiency";
      using (StreamWriter sw = new StreamWriter(result))
      {
        sw.WriteLine("Total PSMs\t{0}", peptides.Count);
        sw.WriteLine("N-terminal modified PSMs\t{0}", nmod);
        sw.WriteLine("{0}-contained PSMs\t{1}", modificationAminoacid, kseqs);
        sw.WriteLine("{0}-full-modified PSMs\t{2}", modificationAminoacid, kmod);
      }

      return new string[] { result };
    }

    public bool IsFullModifiedK(string sequence)
    {
      for (int i = 0; i < sequence.Length; i++)
      {
        if (sequence[i] == modificationAminoacid)
        {
          if ((i == sequence.Length - 1) || char.IsLetter(sequence[i + 1]))
          {
            return false;
          }
        }
      }

      return true;
    }
  }
}
