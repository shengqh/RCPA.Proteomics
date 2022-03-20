using RCPA.Proteomics.Quantification;
using System.Linq;
using ZedGraph;

namespace RCPA
{
  public static class ZedGraphExtensions
  {
    public static LinearRegressionRatioResult GetRegression(this PointPairList pplTotal)
    {
      double[][] intensities = new double[2][];

      intensities[0] = (from ppl in pplTotal
                        select ppl.X).ToArray();

      intensities[1] = (from ppl in pplTotal
                        select ppl.Y).ToArray();

      return LinearRegressionRatioCalculator.CalculateRatio(intensities);
    }

    public static PointPairList GetRegressionLine(this PointPairList pplTotal)
    {
      LinearRegressionRatioResult lrrr = GetRegression(pplTotal);

      return pplTotal.GetRegressionLine(lrrr.Ratio);
    }
  }
}
