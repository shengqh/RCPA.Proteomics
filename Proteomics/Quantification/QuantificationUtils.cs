using MathNet.Numerics.Distributions;
using MathNet.Numerics.LinearAlgebra.Double;
using MathNet.Numerics.Statistics;
using RCPA.Proteomics.Spectrum;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace RCPA.Proteomics.Quantification
{
  public class QuantificationUtils
  {
    public static Vector GetIntensityMatrix<T>(PeakList<T> pkl, int maxCount) where T : IPeak
    {
      int countLimit = Math.Min(maxCount, pkl.Count);
      var vals = new double[countLimit];
      for (int index = 0; index < countLimit; index++)
      {
        vals[index] = pkl[index].Intensity;
      }

      return new DenseVector(vals);
    }

    public static Vector GetIntensityMatrix<T>(PeakList<T> pkl) where T : IPeak
    {
      return GetIntensityMatrix(pkl, pkl.Count);
    }

    public static Matrix GetProfileMatrix(List<double> profile, int envelopeCount, int maxCount)
    {
      int countLimit = Math.Min(maxCount, envelopeCount);

      var vals = new double[countLimit, 3];
      for (int o16Index = 0; o16Index < countLimit; o16Index++)
      {
        if (o16Index >= profile.Count)
        {
          vals[o16Index, 0] = 0.0;
        }
        else
        {
          vals[o16Index, 0] = profile[o16Index];
        }

        int o181Index = o16Index - 2;
        if (o181Index < 0 || o181Index >= profile.Count)
        {
          vals[o16Index, 1] = 0.0;
        }
        else
        {
          vals[o16Index, 1] = profile[o181Index];
        }

        int o182Index = o16Index - 4;
        if (o182Index < 0 || o182Index >= profile.Count)
        {
          vals[o16Index, 2] = 0.0;
        }
        else
        {
          vals[o16Index, 2] = profile[o182Index];
        }
      }

      return DenseMatrix.OfArray(vals);
    }

    public static double GetTotalIntensity(Matrix intensityMatrix)
    {
      double result = 0;
      for (int i = 0; i < intensityMatrix.RowCount; i++)
      {
        result += intensityMatrix[i, 0];
      }
      return result;
    }


    public static double GetIntensityFromProfile<T>(PeakList<T> samplePeaks, List<double> profile) where T : IPeak
    {
      int end = Math.Min(samplePeaks.Count, profile.Count);
      double intensity = 0.0;
      double percent = 0.0;
      for (int i = 0; i < end; i++)
      {
        if (0 != samplePeaks[i].Intensity)
        {
          intensity += samplePeaks[i].Intensity;
          percent += profile[i];
        }
      }

      if (0 == percent)
      {
        return 0.0;
      }
      else
      {
        return intensity / percent;
      }
    }

    public static void RatioStatistic(StreamWriter sw, string title, List<double> logRatios, int totalCount)
    {
      var filteredLogRatios = (from r in logRatios
                               where !double.IsNaN(r) && !double.IsInfinity(r)
                               select r).ToList();

      if (filteredLogRatios.Count <= 1)
      {
        return;
      }

      var mean = Statistics.Mean(filteredLogRatios);
      var sd = Statistics.StandardDeviation(filteredLogRatios);
      var nd = new Normal(mean, sd);

      var p95 = nd.GetProbRange(0.95);
      var p99 = nd.GetProbRange(0.99);
      var p999 = nd.GetProbRange(0.999);

      sw.WriteLine(MyConvert.Format("{0}\t{1:0.0000}\t{2:0.0000}\t{3:0.00}\t{4:0.00}-{5:0.00}\t{6:0.00}-{7:0.00}\t{8:0.00}-{9:0.00}\t{10}\t{11}\t{12}\t{13}\t{14}",
        title,
        mean,
        sd,
        Math.Exp(mean),
        Math.Exp(p95.First),
        Math.Exp(p95.Second),
        Math.Exp(p99.First),
        Math.Exp(p99.Second),
        Math.Exp(p999.First),
        Math.Exp(p999.Second),
        totalCount,
        filteredLogRatios.Count,
        filteredLogRatios.Count(m => m < p95.First || m > p95.Second),
        filteredLogRatios.Count(m => m < p99.First || m > p99.Second),
        filteredLogRatios.Count(m => m < p999.First || m > p999.Second)
        ));
    }
  }
}