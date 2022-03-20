using System;
using System.Linq;

namespace RCPA.Proteomics.Spectrum
{
  //The xcorr calculation was adopted from MyriMatch
  public static class SpectrumXcorr
  {
    private const double binWidth = 1.007276;

    private static bool IsValidIndex(int index, int length)
    {
      return index >= 0 && index < length;
    }

    public static PeakList<T> NormalizePeakIntensities<T>(PeakList<T> pkl, double maxPeakMass) where T : IPeak, new()
    {
      PeakList<T> result = new PeakList<T>();
      result.AssignInforamtion(pkl);
      foreach (var p in pkl)
      {
        result.Add(new T()
        {
          Mz = p.Mz,
          Intensity = p.Intensity,
          Charge = p.Charge
        });
      }

      result.RemoveAll(m => m.Mz > maxPeakMass);

      int maxBins;
      if (maxPeakMass > 512)
      {
        maxBins = (int)Math.Ceiling(maxPeakMass / 1024) * 1024;
      }
      else
      {
        maxBins = 512;
      }

      // Section the original peak array in 10 regions and find the
      // base peak in each region. Also, square-root the peak intensities
      const int numberOfRegions = 10;

      double[] basePeakIntensityByRegion = new double[numberOfRegions];
      for (int i = 0; i < basePeakIntensityByRegion.Length; i++)
      {
        basePeakIntensityByRegion[i] = 1;
      }

      int regionSelector = (int)(maxPeakMass / numberOfRegions);

      foreach (var peak in result)
      {
        peak.Intensity = Math.Sqrt(peak.Intensity);

        int mzBin = (int)Math.Round(peak.Mz / binWidth);
        int normalizationIndex = mzBin / regionSelector;
        if (IsValidIndex(normalizationIndex, numberOfRegions))
        {
          basePeakIntensityByRegion[normalizationIndex] = Math.Max(basePeakIntensityByRegion[normalizationIndex], peak.Intensity);
        }
      }

      // Normalize peaks in each region from 0 to 50. 
      // Use base peak in each region for normalization. 
      foreach (var peak in result)
      {
        int mzBin = (int)Math.Round(peak.Mz / binWidth);
        int normalizationIndex = mzBin / regionSelector;
        if (IsValidIndex(normalizationIndex, numberOfRegions))
        {
          peak.Intensity = (peak.Intensity / basePeakIntensityByRegion[normalizationIndex]) * 50;
        }
      }

      return result;
    }

    /// <summary>
    /// Calculate XCorr based on implementation in MyriMatch
    /// </summary>
    /// <param name="pkl1">Normalized peak list 1</param>
    /// <param name="pkl2">Normalized peak list 2</param>
    /// <returns></returns>
    public static double CalculateUnnormalized<T>(PeakList<T> pkl1, PeakList<T> pkl2) where T : IPeak, new()
    {
      var maxPeakMass = Math.Max(pkl1.Last().Mz, pkl2.Last().Mz);
      var n1 = NormalizePeakIntensities(pkl1, maxPeakMass);
      var n2 = NormalizePeakIntensities(pkl2, maxPeakMass);
      return Calculate(n1, n2);
    }

    /// <summary>
    /// Calculate XCorr based on implementation in MyriMatch
    /// </summary>
    /// <param name="pkl1">Normalized peak list 1</param>
    /// <param name="pkl2">Normalized peak list 2</param>
    /// <returns></returns>
    public static double Calculate<T>(PeakList<T> pkl1, PeakList<T> pkl2) where T : IPeak, new()
    {
      double massCutOff = Math.Max(pkl1.Last().Mz, pkl2.Last().Mz) + 50;

      int maxBins = massCutOff > 512 ? (int)Math.Ceiling(massCutOff / 1024) * 1024 : 512;

      double[] data1 = GetIntensityArray(pkl1, maxBins);

      //Compute the cumulative spectrum
      for (int i = 0; i < data1.Length; i++)
      {
        for (int j = i - 75; j <= i + 75; j++)
        {
          if (IsValidIndex(j, maxBins))
          {
            data1[i] -= data1[j] / 151;
          }
        }
      }

      double[] data2 = GetIntensityArray(pkl2, maxBins);

      double result = 0.0;
      for (int index = 0; index < data1.Length; index++)
      {
        result += data1[index] * data2[index];
      }

      result = result / 10000;

      return result;
    }

    private static double[] GetIntensityArray<T>(PeakList<T> pkl1, int maxBins) where T : IPeak, new()
    {
      double[] data1 = new double[maxBins];
      foreach (var peak in pkl1)
      {
        int mzBin = (int)Math.Round(peak.Mz / binWidth);
        if (IsValidIndex(mzBin, maxBins))
        {
          data1[mzBin] = peak.Intensity;
        }
      }
      return data1;
    }
  }
}
