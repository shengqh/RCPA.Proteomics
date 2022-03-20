using MathNet.Numerics.Statistics;
using System;
using System.Collections.Generic;
using System.Linq;

namespace RCPA.Proteomics.Summary
{
  public class ProteinFDR
  {
    public double Mean { get; set; }
    public double StandardDeviation { get; set; }
  }

  public class ProteinFDREstimator : AbstractThreadProcessor
  {
    private ProteinFDREstimatorOptions options;
    public ProteinFDREstimator(ProteinFDREstimatorOptions options)
    {
      this.options = options;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="ir">protein group information, all protein has to be marked as FromDecoy or not</param>
    /// <param name="spectra">binned spectra list, order by descending score in each bin, to marked as FromDecoy or not</param>
    /// <param name="iteration"></param>
    /// <returns></returns>
    public ProteinFDR Estimate(IIdentifiedResult ir, List<List<IIdentifiedSpectrum>> spectra, int iteration = 1000, int binSize = 100, Random rand = null)
    {
      var targetGroups = (from g in ir
                          where g.All(m => !m.FromDecoy)
                          select g).ToList();

      spectra.ForEach(m => m.ForEach(l => l.AssignedDecoy = false));

      var fdr = (ir.Count - targetGroups.Count) * 1.0 / targetGroups.Count;

      List<List<IIdentifiedSpectrum>> assignedDecoyCandidates = new List<List<IIdentifiedSpectrum>>();
      foreach (var splist in spectra)
      {
        for (int i = 0; i < splist.Count; i++)
        {
          if (splist[i].FromDecoy)
          {
            var lst = new List<IIdentifiedSpectrum>();
            for (int j = i + 1; j < splist.Count && lst.Count < binSize; j++)
            {
              if (splist[j].FromDecoy)
              {
                continue;
              }

              lst.Add(splist[j]);
            }

            for (int j = i - 1; j >= 0 && lst.Count < binSize; j--)
            {
              if (splist[j].FromDecoy)
              {
                continue;
              }

              lst.Add(splist[j]);
            }

            assignedDecoyCandidates.Add(lst);
          }
        }
      }

      if (rand == null)
      {
        rand = new Random();
      }

      List<double> estimatedFdrs = new List<double>();
      List<IIdentifiedSpectrum> unassigned;
      for (int i = 0; i < iteration; i++)
      {
        assignedDecoyCandidates.ForEach(m => m.ForEach(l => l.AssignedDecoy = false));

        foreach (var decoyCand in assignedDecoyCandidates)
        {
          if (decoyCand.Any(l => l.AssignedDecoy))
          {
            unassigned = decoyCand.Where(l => !l.AssignedDecoy).ToList();
          }
          else
          {
            unassigned = decoyCand;
          }
          var currand = rand.Next(unassigned.Count - 1);
          unassigned[currand].AssignedDecoy = true;
        }

        var assignedDecoy = (from g in targetGroups
                             let decoyPepCount = g.First().Peptides.Where(m => m.Spectrum.AssignedDecoy || m.Spectrum.FromDecoy).Count()
                             let targetPepCount = g.First().Peptides.Count
                             where decoyPepCount >= targetPepCount
                             select g).Count();

        var estimatedFdr = assignedDecoy * 1.0 / targetGroups.Count;
        estimatedFdrs.Add(estimatedFdr);
      }

      var meanSd = Statistics.MeanStandardDeviation(estimatedFdrs);
      return new ProteinFDR()
      {
        Mean = meanSd.Item1,
        StandardDeviation = meanSd.Item2
      };
    }

    public override IEnumerable<string> Process()
    {
      throw new NotImplementedException();
    }
  }
}
