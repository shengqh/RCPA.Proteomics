using System;
using System.Collections.Generic;
using RCPA.Proteomics.Spectrum;

namespace RCPA.Proteomics.Quantification
{
  public class ChromatogramUtils
  {
    public static List<PeakList<T>> FindChromatogram<T>(List<PeakList<T>> fullMsList,
                                                        int identifiedScan, double precursorMz, int charge,
                                                        double mzTolerance) where T : IPeak, new()
    {
      var result = new List<PeakList<T>>();

      int fullScanIndex = FindCorrespondingFullScan(fullMsList, identifiedScan);
      T curPrecursorPeak = fullMsList[fullScanIndex].FindPeak(precursorMz, charge, mzTolerance).FindMaxIntensityPeak();

      if (null == curPrecursorPeak)
      {
        throw new ArgumentException(
          MyConvert.Format("Cannot find peak (mz={0:0.0000}, charge={1}) in scan {2}, mzTolerance={3:0.0000}", precursorMz,
                        charge, fullScanIndex, mzTolerance));
      }

      PeakList<T> envelop = fullMsList[fullScanIndex].FindEnvelope(curPrecursorPeak, mzTolerance, true);
      result.Add(envelop);

      //find prior envelop
      for (int priorIndex = fullScanIndex - 1; priorIndex >= 0; priorIndex--)
      {
        T precursorPeak = fullMsList[priorIndex].FindPeak(precursorMz, charge, mzTolerance).FindMaxIntensityPeak();

        if (null == precursorPeak)
        {
          var emptyEnvelop = new PeakList<T>();
          emptyEnvelop.ScanTimes.AddRange(fullMsList[priorIndex].ScanTimes);
          result.Insert(0, emptyEnvelop);
          break;
        }

        PeakList<T> priorEnvelop = fullMsList[priorIndex].FindEnvelope(precursorPeak, mzTolerance, true);
        result.Insert(0, priorEnvelop);
      }

      //find next envelop
      for (int nextIndex = fullScanIndex + 1; nextIndex < fullMsList.Count; nextIndex++)
      {
        T precursorPeak = fullMsList[nextIndex].FindPeak(precursorMz, charge, mzTolerance).FindMaxIntensityPeak();

        if (null == precursorPeak)
        {
          var emptyEnvelop = new PeakList<T>();
          emptyEnvelop.ScanTimes.AddRange(fullMsList[nextIndex].ScanTimes);
          result.Add(emptyEnvelop);
          break;
        }

        PeakList<T> nextEnvelop = fullMsList[nextIndex].FindEnvelope(precursorPeak, mzTolerance, true);
        result.Add(nextEnvelop);
      }

      return result;
    }

    public static int FindCorrespondingFullScan<T>(List<PeakList<T>> fullMsList, int identifiedScan) where T : IPeak
    {
      for (int i = 0; i < fullMsList.Count; i++)
      {
        if (fullMsList[i].ScanTimes[0].Scan > identifiedScan)
        {
          return i - 1;
        }
      }
      return fullMsList.Count - 1;
    }
  }
}