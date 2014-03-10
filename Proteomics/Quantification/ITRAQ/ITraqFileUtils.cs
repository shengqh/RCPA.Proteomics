//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using MathNet.Numerics.Statistics;
//using RCPA.Proteomics.Spectrum;
//using MathNet.Numerics.Distributions;

//namespace RCPA.Proteomics.Quantification.ITraq
//{
//  public class ITraqFileUtils
//  {
//    public static ITraqResult GetITraqResult(List<PeakList<Peak>> pkls, double mean, double peakTolerance, int minPeakCount)
//    {
//      ITraqResult result = new ITraqResult();
//      double mean114 = 114 + mean;
//      double mean115 = 115 + mean;
//      double mean116 = 116 + mean;
//      double mean117 = 117 + mean;
//      foreach (PeakList<Peak> pkl in pkls)
//      {
//        Peak peak114 = pkl.FindPeak(mean114, peakTolerance).FindMaxIntensityPeak();
//        Peak peak115 = pkl.FindPeak(mean115, peakTolerance).FindMaxIntensityPeak();
//        Peak peak116 = pkl.FindPeak(mean116, peakTolerance).FindMaxIntensityPeak();
//        Peak peak117 = pkl.FindPeak(mean117, peakTolerance).FindMaxIntensityPeak();

//        int nullCount = 0;

//        CheckPeakNull(ref peak114, ref nullCount);
//        CheckPeakNull(ref peak115, ref nullCount);
//        CheckPeakNull(ref peak116, ref nullCount);
//        CheckPeakNull(ref peak117, ref nullCount);

//        if (nullCount > 4 - minPeakCount)
//        {
//          continue;
//        }

//        ITraqItem item = new ITraqItem { Scan = pkl.ScanTimes[0], I114 = peak114.Intensity, I115 = peak115.Intensity, I116 = peak116.Intensity, I117 = peak117.Intensity, ScanMode = pkl.ScanMode };

//        result.Add(item);
//      }
//      return result;
//    }

//    private static void CheckPeakNull(ref Peak peak, ref int nullCount)
//    {
//      if (peak == null)
//      {
//        nullCount++;
//        peak = new Peak(0, ITraqConsts.NULL_INTENSITY);
//      }
//    }

//    public static MeanStandardDeviation GetDistances(List<PeakList<Peak>> pkls)
//    {
//      List<double> distances = new List<double>();

//      foreach (PeakList<Peak> pkl in pkls)
//      {
//        AddToDistance(distances, pkl, 114.0);
//        AddToDistance(distances, pkl, 115.0);
//        AddToDistance(distances, pkl, 116.0);
//        AddToDistance(distances, pkl, 117.0);
//      }

//      return new MeanStandardDeviation(distances);
//    }

//    private static void AddToDistance(List<double> distances, PeakList<Peak> pkl, double theoreticalMz)
//    {
//      Peak peak = pkl.FindPeak(theoreticalMz, 0.45).FindMaxIntensityPeak();
//      if (peak != null)
//      {
//        distances.Add(peak.Mz - theoreticalMz);
//      }
//    }
//  }
//}
