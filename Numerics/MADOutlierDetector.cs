using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MathNet.Numerics.Statistics;

namespace RCPA.Numerics
{
  /// <summary>
  /// Modified z-score calculation.
  /// xm=median{xi}, sample median
  /// MAD = median{|xi-xm|}, median of absolute deviation about the median
  /// z-score=0.6745 * (xi-xm) / MAD, modified z-score
  /// outlier = z-score > 3.5
  /// </summary>
  public class MADOutlierDetector : IOutlierDetector
  {
    #region IOutlierDetector Members

    public List<int> Detect(IEnumerable<double> values)
    {
      List<int> result = new List<int>();
      if (values.Count() < 4)
      {
        return result;
      }

      var median = Statistics.Median(values);
      var distances = (from s in values
                       let v = Math.Abs(s - median)
                       orderby v
                       select v).ToArray();
      var mad = Statistics.Median(distances);

      var tmp = 0.6745 / mad;
      var zs = (from v in values
                let z = Math.Abs(tmp * (v - median))
                select z).ToArray();

      for (int i = 0; i < zs.Length; i++)
      {
        if (zs[i] >= 3.5)
        {
          result.Add(i);
        }
      }

      return result;
    }

    #endregion
  }
}
