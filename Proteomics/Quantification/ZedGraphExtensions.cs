using System.Collections.Generic;
using ZedGraph;

namespace RCPA.Proteomics.Quantification
{
  public static class ZedGraphExtensions
  {
    public static LinearRegressionRatioResult GetRegressionResult(this PointPairList pplTotal)
    {
      var refIntensities = new List<double>();
      var sampleIntensities = new List<double>();
      pplTotal.ForEach(pp =>
      {
        refIntensities.Add(pp.X);
        sampleIntensities.Add(pp.Y);
      });

      if (pplTotal.Count == 0)
      {
        return new LinearRegressionRatioResult(0, 0);
      }

      var intensities = new double[2][];
      intensities[0] = refIntensities.ToArray();
      intensities[1] = sampleIntensities.ToArray();

      return LinearRegressionRatioCalculator.CalculateRatio(intensities);
    }
  }
}
