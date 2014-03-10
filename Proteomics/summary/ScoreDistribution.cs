using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RCPA.Proteomics.Summary
{
  public class ScoreDistribution : Dictionary<OptimalResultCondition, List<OptimalResult>>
  {
    public double CalculateSubsetFdr(ScoreDistribution subset, IFalseDiscoveryRateCalculator calc)
    {
      double targetCount = 0;
      double decoyCount = 0;

      foreach (OptimalResultCondition cond in subset.Keys)
      {
        var subsetOrs = subset[cond];
        var totalOrs = this[cond];

        var counts =
          from s in subsetOrs
          join t in totalOrs on s.Score equals t.Score
          select new { TargetCount = (int)s.PeptideCountFromTargetDB, DecoyCount = (double)s.PeptideCountFromTargetDB * t.PeptideCountFromDecoyDB / t.PeptideCountFromTargetDB };

        targetCount +=
          (from c in counts
           select c.TargetCount).Sum();

        decoyCount +=
          (from c in counts
           select c.DecoyCount).Sum();
      }

      return calc.Calculate((int)decoyCount, (int)targetCount);
    }

    public double CalculateFdr(IFalseDiscoveryRateCalculator calc)
    {
      double targetCount = 0;
      double decoyCount = 0;

        targetCount +=
          (from ors in this.Values
           from or in ors
           select (int)or.PeptideCountFromTargetDB).Sum();

        decoyCount +=
          (from ors in this.Values
           from or in ors
           select (int)or.PeptideCountFromDecoyDB).Sum();
      

      return calc.Calculate((int)decoyCount, (int)targetCount);
    }

  }
}
