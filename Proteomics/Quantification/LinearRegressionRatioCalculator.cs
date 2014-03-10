using System.Collections.Generic;
using System.Linq;
using RCPA.Utils;

namespace RCPA.Proteomics.Quantification
{
  public static class LinearRegressionRatioCalculator
  {
    public static LinearRegressionRatioResult CalculateRatio(double[][] intensities)
    {
      double maxA = intensities[0].Max();
      double maxB = intensities[1].Max();

      if (maxA == 0.0)
      {
        return new LinearRegressionRatioResult(20, 0.0);
      }

      if (maxB == 0.0)
      {
        return new LinearRegressionRatioResult(0.05, 0.0);
      }

      if (intensities[0].Length == 1) // only one point
      {
        return new LinearRegressionRatioResult(intensities[1][0] / intensities[0][0], 0.0);
      }

      var aList = new List<double>(intensities[0]);
      aList.Add(0);
      var a = new double[1][];
      a[0] = aList.ToArray();

      var bList = new List<double>(intensities[1]);
      bList.Add(0);
      double[] b = bList.ToArray();

      var x = new double[1];

      double rnorm = 0.0;
      NonNegativeLeastSquaresCalc.NNLS(a, b.Length, 1, b, x, out rnorm, null, null, null);

      var bPredicted = new double[aList.Count];
      for (int i = 0; i < aList.Count; i++)
      {
        bPredicted[i] = x[0] * aList[i];
      }

      var result = new LinearRegressionRatioResult();
      result.Ratio = x[0];
      result.RSquare = StatisticsUtils.RSquare(bList.ToArray(), bPredicted);
      result.PointCount = bList.Count;
      result.FCalculatedValue = StatisticsUtils.FCalculatedValueForLinearRegression(bList.ToArray(), bPredicted);

      if (double.IsInfinity(result.FCalculatedValue) || double.IsNaN(result.FCalculatedValue) || result.FCalculatedValue < 0)
      {
        result.FCalculatedValue = 0;
      }

      result.FProbability = StatisticsUtils.FProbabilityForLinearRegression(bList.Count, result.FCalculatedValue);

      return result;
    }

    public static LinearRegressionRatioResult CalculateRatioDistance(double[][] intensities)
    {
      int len = intensities[0].Length;

      double maxA = intensities[0].Max();
      double maxB = intensities[1].Max();

      if (maxA == 0.0)
      {
        return new LinearRegressionRatioResult(20, 0.0);
      }

      if (maxB == 0.0)
      {
        return new LinearRegressionRatioResult(0.05, 0.0);
      }

      if (len == 1) // only one point
      {
        return new LinearRegressionRatioResult(intensities[1][0] / intensities[0][0], 0.0);
      }

      var a = new double[2][];
      a[0] = new double[len];
      a[1] = new double[len];
      for (int i = 0; i < len; i++)
      {
        a[0][i] = intensities[0][i];
        a[1][i] = 1;
      }

      double[] b = new List<double>(intensities[1]).ToArray();

      var x = new double[2];

      double rnorm = 0.0;
      NonNegativeLeastSquaresCalc.NNLS(a, b.Length, 2, b, x, out rnorm, null, null, null);

      var bPredicted = new double[len];
      for (int i = 0; i < len; i++)
      {
        bPredicted[i] = x[0] * intensities[0][i] + x[1];
      }

      var result = new LinearRegressionRatioResult();
      result.Ratio = x[0];
      result.Distance = x[1];
      result.RSquare = StatisticsUtils.RSquare(intensities[1], bPredicted);
      result.PointCount = b.Length;
      result.FCalculatedValue = StatisticsUtils.FCalculatedValueForLinearRegression(intensities[1], bPredicted);

      if (double.IsInfinity(result.FCalculatedValue) || double.IsNaN(result.FCalculatedValue) || result.FCalculatedValue < 0)
      {
        result.FCalculatedValue = 0;
      }

      result.FProbability = StatisticsUtils.FProbabilityForLinearRegression(len, result.FCalculatedValue);

      return result;
    }
  }
}