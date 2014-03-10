using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using RCPA.Proteomics.Summary;
using RCPA.Proteomics.Sequest;
using RCPA.Proteomics.Utils;

namespace RCPA.Tools.Distiller
{
  public class AmbigiousModifiedPeptideRemover : AbstractThreadFileProcessor
  {
    private bool IsAmbigiousSequence(string seq)
    {
      return seq.Contains('p');
    }

    public override IEnumerable<string> Process(string fileName)
    {
      SequestPeptideTextFormat format = new SequestPeptideTextFormat();

      List<IIdentifiedSpectrum> spectra = format.ReadFromFile(fileName);

      List<IIdentifiedSpectrum> positives = spectra.FindAll(m =>
      {
        if (IsAmbigiousSequence(m.Sequence))
        {
          return false;
        }

        if (m.DiffModificationSiteCandidates.Count == 0)
        {
          return true;
        }

        if (1 == m.DiffModificationSiteCandidates.Count)
        {
          string matchedSeq = PeptideUtils.GetMatchedSequence(m.DiffModificationSiteCandidates[0].Sequence);
          return matchedSeq.Equals(m.Sequence);
        }

        return false;
      });

      var ambigious = spectra.Except(positives);

      var positiveSeqs = new HashSet<string>(positives.ConvertAll(m => PeptideUtils.GetMatchedSequence(m.Sequence)));

      var keptAmbigious = new List<IIdentifiedSpectrum>();

      foreach (IIdentifiedSpectrum m in ambigious)
      {
        if (positiveSeqs.Contains(m.Sequence))
        {
          continue;
        }

        string matchedSeq;
        if (IsAmbigiousSequence(m.Sequence))
        {
          matchedSeq = PeptideUtils.GetMatchedSequence(m.DiffModificationSiteCandidates[0].Sequence);
        }
        else
        {
          matchedSeq = PeptideUtils.GetMatchedSequence(m.Sequence);
        }

        if (positiveSeqs.Contains(matchedSeq))
        {
          continue;
        }

        keptAmbigious.Add(m);
      }

      positives.AddRange(keptAmbigious);

      var bin =
        from p in positives
        group p by (p.Sequence + "_" + p.Charge);

      List<IIdentifiedSpectrum> final = new List<IIdentifiedSpectrum>();
      foreach (var b in bin)
      {
        var o = b.OrderByDescending(m => m.Score);
        final.Add(o.First());
      }

      string resultFileName = fileName + ".single";
      format.WriteToFile(resultFileName, final);
      return new string[] { resultFileName };
    }
  }
}
