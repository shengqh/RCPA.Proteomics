using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using RCPA.Proteomics.Spectrum;

namespace RCPA.Proteomics.Raw
{
  public static class RawFileHelper
  {
    public static double DefaultIsolationWidth = 0.5;

    public static int FindParentScan(this IRawFile rawFile, int fromScan, int firstScan, int msParentLevel)
    {
      for (int scan = fromScan; scan >= firstScan; scan--)
      {
        if (msParentLevel == rawFile.GetMsLevel(scan))
        {
          return scan;
        }
      }

      return -1;
    }

    public static int FindPreviousFullScan(this IRawFile rawFile, int fromScan, int firstScan)
    {
      return FindParentScan(rawFile, fromScan, firstScan, 1);
    }

    public static int FindNextFullScan(this IRawFile rawFile, int fromScan, int lastScan)
    {
      for (int scan = fromScan; scan <= lastScan; scan++)
      {
        if (1 == rawFile.GetMsLevel(scan))
        {
          return scan;
        }
      }

      return -1;
    }

    public static PeakList<Peak> GetPeakInIsolationWindow(this IRawFile2 RawReader, int scan)
    {
      return GetPeakInIsolationWindow(RawReader, scan, DefaultIsolationWidth);
    }

    public static PeakList<Peak> GetPeakInIsolationWindow(this IRawFile2 RawReader, int scan, double defaultIsolationWidth)
    {
      PeakList<Peak> result = new PeakList<Peak>();

      var precursor = RawReader.GetPrecursorPeakWithMasterScan(scan);
      if(precursor.MasterScan == 0){
        return result;
      }

      if (0 == precursor.IsolationWidth || precursor.IsolationWidth > 5.0)
      {
        precursor.IsolationWidth = defaultIsolationWidth;
      }

      var isolationWidth = precursor.IsolationWidth /= 2;

      var minMz = precursor.IsolationMass - isolationWidth;
      var maxMz = precursor.IsolationMass + isolationWidth;

      result = RawReader.GetPeakList(precursor.MasterScan, minMz, maxMz);
      result.Precursor = precursor;

      return result;
    }

    public static double GetPrecursorPercentage(this PeakList<Peak> isolationPeaks, double ppmTolerance)
    {
      var defaultValue = 0.0;

      if (isolationPeaks.Count == 0)
      {
        return defaultValue;
      }

      //parentPkl.ForEach(m => Console.WriteLine("new Peak({0:0.0000},{1:0.0},{2}),",m.Mz, m.Intensity,m.Charge));

      var totalIntensity = isolationPeaks.Sum(m => m.Intensity);

      if (0.0 == totalIntensity)
      {
        throw new ArgumentException("There is no intensity information in argument isolationPeaks");
      }

      var precursorPeak = isolationPeaks.Precursor;
      var mzTolerance = PrecursorUtils.ppm2mz(precursorPeak.MonoIsotopicMass, ppmTolerance);
      if (precursorPeak.Charge == 0)
      {
        var pPeak = isolationPeaks.FindAll(m => Math.Abs(m.Mz - precursorPeak.MonoIsotopicMass) < mzTolerance);
        if (0 == pPeak.Count)
        {
          return defaultValue;
        }

        var maxIntensity = pPeak.Max(m => m.Intensity);
        pPeak.Find(m => m.Intensity == maxIntensity).Tag = 1;
        return maxIntensity  / totalIntensity;
      }
      else
      {
        var pPeak = isolationPeaks.FindEnvelope(precursorPeak.MonoIsotopicMass, precursorPeak.Charge, mzTolerance, true);
        if (0 == pPeak.Count)
        {
          return defaultValue;
        }

        for (int i = 0; i < pPeak.Count; i++)
        {
          pPeak[i].Tag = i + 1;
        }

        return pPeak.GetTotalIntensity() / totalIntensity;
      }
    }

    public static double GetPrecursorPercentageInIsolationWindow(this IRawFile2 RawReader, int scan, double ppmTolerance)
    {
      var parentPkl = RawReader.GetPeakInIsolationWindow(scan);
      return parentPkl.GetPrecursorPercentage(ppmTolerance);
    }
  }
}
