using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MathNet.Numerics.Distributions;
using MathNet.Numerics.Statistics;

namespace RCPA.Numerics
{
  /// <summary>
  /// 又被称为ESD method (extreme studentized deviate)。
  /// 只能用于检测最大偏差的那个值是否outlier。也就是说，只适合找出只有一个outlier的情况。
  /// 不能用于通过循环方法查找所有方法。
  /// 例如：3.2, 3.3, 8.1, 3.2, 2.9, 3.7, 3.1, 3.5, 3.3, 9.2
  /// GrubbsTest将认为没有outlier，但去除8.1后，9.2就被认为是outlier。
  /// 
  /// http://www.graphpad.com/quickcalcs/GrubbsHowTo.cfm
  /// 
  /// You can calculate an approximate P value as follows.
  /// Calculate Z = |mean - value| / SD
  /// Calculate T = sqrt( (N * (N-2) * Z * Z) / ((N-1) * (N-1) - N * Z * Z))
  /// N is the number of values in the sample.
  /// Look up the two-tailed P value for the student t distribution with the calculated value of T and N-2 degrees of freedom. 
  /// Using Excel, the formula is =TDIST(T,DF,2) (the '2' is for a two-tailed P value).
  /// Multiply the P value you obtain in step 2 by N. The result is an approximate P value for the outlier test. 
  /// This P value is the chance of observing one point so far from the others if the data were all sampled from a Gaussian distribution.
  /// If Z is large, this P value will be very accurate. With smaller values of Z, the calculated P value may be too large.
  /// </summary>
  public class GrubbsTestOutlierDetector : IOutlierDetector
  {
    private double alpha;

    public GrubbsTestOutlierDetector(double alpha)
    {
      this.alpha = alpha;
    }

    #region IOutlierDetector Members

    public List<int> Detect(IEnumerable<double> values)
    {
      var ps = GetProbability(values);

      var result = new List<int>();
      for (int i = 0; i < ps.Length; i++)
      {
        if (ps[i] < this.alpha)
        {
          result.Add(i);
        }
      }

      return result;
    }

    public static double[] GetProbability(IEnumerable<double> values)
    {
      var lst = values.ToArray();

      var mean = Statistics.Mean(values);
      var sd = Statistics.StandardDeviation(values);

      var zs = (from l in values
                select Math.Abs(l - mean) / sd).ToArray();

      var ts = (from z in zs
                select Math.Sqrt(lst.Length * (lst.Length - 2) * z * z / ((lst.Length - 1) * (lst.Length - 1) - lst.Length * z * z))).ToList();

      var tdist = new StudentT(0.0,1.0, lst.Length-2);
      var ps = (from t in ts
                select tdist.TwoTailProbability(t) * lst.Length).ToArray();
      return ps;
    }

    #endregion
  }
}
