using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using RCPA.Proteomics.Mascot;
using RCPA.Proteomics.Utils;
using System.Text.RegularExpressions;
using System.IO;

namespace RCPA.Proteomics.Quantification.ITraq
{
  public class IsobaricLabelingEfficiencyCalculator : AbstractThreadFileProcessor
  {
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
                   where seq.Contains('K')
                   select seq).Count();

      var kmod = (from seq in seqs
                  where seq.Contains('K') && IsFullModifiedK(seq)
                  select seq).Count();

      var result = fileName + ".labelingEfficiency";
      using (StreamWriter sw = new StreamWriter(result))
      {
        sw.WriteLine("Total PSMs\t" + peptides.Count.ToString());
        sw.WriteLine("N-terminal modified PSMs\t" + nmod.ToString());
        sw.WriteLine("K-contained PSMs\t" + kseqs.ToString());
        sw.WriteLine("K-full-modified PSMs\t" + kmod.ToString());
      }

      return new string[] { result };
    }

    public bool IsFullModifiedK(string sequence)
    {
      for (int i = 0; i < sequence.Length; i++)
      {
        if (sequence[i] == 'K')
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
