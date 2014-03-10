using System;

namespace RCPA.Proteomics.Mascot
{
  public sealed class ExpectValueCalculator
  {
    private ExpectValueCalculator()
    {
    }

    public static double Calc(double score, int totalMatchCount, double pvalue)
    {
      double identityscore = 10*Math.Log10(totalMatchCount);
      return pvalue*Math.Pow(10, (identityscore - score)/10);
    }
  }
}