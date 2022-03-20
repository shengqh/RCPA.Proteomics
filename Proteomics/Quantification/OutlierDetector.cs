using MathNet.Numerics.Distributions;
using MathNet.Numerics.Statistics;
using System;
using System.Collections.Generic;
using System.Linq;

namespace RCPA.Proteomics.Quantification
{
  public static class OutlierDetector
  {
    public static int Detect(IEnumerable<double> data, double evalue)
    {
      var values = new List<double>(data);
      if (values.Count < 3)
      {
        return -1;
      }

      var allmean = values.Average();
      var alldistances = (from v in values
                          select Math.Abs(v - allmean)).ToList();

      var maxdistance = alldistances.Max();
      var maxdistanceindex = alldistances.IndexOf(maxdistance);

      var maxvalue = values[maxdistanceindex];
      values.Remove(maxvalue);

      var mean = Statistics.Mean(values);
      var sd = Statistics.StandardDeviation(values);
      var nb = new Normal(mean, sd);
      var range = nb.GetProbRange(1 - evalue);

      if (maxvalue < range.First || maxvalue > range.Second)
      {
        //data.ToList().ForEach(m => Console.Write("{0:0.0000},", m));
        //Console.WriteLine(" - {0:0.0000} - {1:E}", maxvalue, prob);
        return maxdistanceindex;
      }

      return -1;
    }
  }
}
