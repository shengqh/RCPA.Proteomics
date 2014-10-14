using System;

namespace RCPA.Proteomics.Mascot
{
  /// <summary>
  /// http://www.matrixscience.com/pdf/2005WKSHP4.pdf
  /// </summary>
  public static class ExpectValueCalculator
  {
    public static double Calc(double score, int totalMatchCount, double pvalue)
    {
      double identityThreshold = 10 * Math.Log10(totalMatchCount);
      return pvalue * Math.Pow(10, (identityThreshold - score) / 10);
    }

    public static int GetTotalCandidate(double score, double pvalue, double evalue)
    {
      var idt = score + Math.Log10(evalue / 0.05) * 10;
      return (int)Math.Round(Math.Pow(10, idt / 10));
    }
  }
}