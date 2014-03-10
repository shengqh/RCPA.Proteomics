using System;
using System.Collections.Generic;
using MathNet.Numerics.Statistics;
using RCPA.Proteomics.Spectrum;
using MathNet.Numerics.Distributions;

namespace RCPA.Proteomics.Score
{
  public class XCorrelationSpectrumScore<U> : ISpectrumScore where U : IPeak, new()
  {
    private readonly double[] _experimentalIntensities;

    private readonly int _maxIndex;

    public XCorrelationSpectrumScore(PeakList<U> experimentalPkl)
    {
      var tmp = new PeakList<U>(experimentalPkl);
      tmp.Sort((m1, m2) => m1.Mz.CompareTo(m2.Mz));

      //A 10-u window around the precursor ion is removed
      double minPrecursor = tmp.PrecursorMZ - 10.0;
      double maxPrecursor = tmp.PrecursorMZ + 10.0;
      for (int i = tmp.Count - 1; i >= 0; i--)
      {
        if (tmp[i].Mz > maxPrecursor)
        {
          continue;
        }

        if (tmp[i].Mz < minPrecursor)
        {
          break;
        }

        tmp.RemoveAt(i);
      }

      //The spectrum is then divided into 10 equal sections and the ion abundances in each section are normalized to 50.0;
      double maxMz = (int)Math.Round(tmp[tmp.Count - 1].Mz + 2.0);

      double stepMz = (maxMz) / 10.0;

      var peakBin = new List<PeakList<U>>();
      for (int i = 0; i < 10; i++)
      {
        peakBin.Add(new PeakList<U>());
      }

      foreach (U p in tmp)
      {
        var index = (int)(p.Mz / stepMz);
        peakBin[index].Add(p);
      }

      foreach (var peaks in peakBin)
      {
        if (peaks.Count > 0)
        {
          double maxIntensity = peaks.FindMaxIntensityPeak().Intensity;
          foreach (U p in peaks)
          {
            p.Intensity = p.Intensity * 50 / maxIntensity;
          }
        }
      }

      //convert to discrte signals
      this._maxIndex = GetMinPowerOfTwo(maxMz);

      this._experimentalIntensities = new double[this._maxIndex];
      for (int i = 0; i < this._maxIndex; i++)
      {
        this._experimentalIntensities[i] = 0.0;
      }

      foreach (U p in tmp)
      {
        var index = (int)Math.Round(p.Mz);

        AssignIntensity(this._experimentalIntensities, index, (int)p.Intensity);
      }
    }

    #region ISpectrumScore Members

    public double Calculate<T>(List<T> theoreticalPkl) where T : IPeak
    {
      double cZIntensity = 50;
      double cZAroundIntensity = 25;

      var theoreticalIntensities = new double[this._maxIndex];
      for (int i = 0; i < this._maxIndex; i++)
      {
        theoreticalIntensities[i] = 0;
      }

      foreach (T p in theoreticalPkl)
      {
        var index = (int)Math.Round(p.Mz);
        if (index >= this._maxIndex)
        {
          break;
        }

        AssignIntensity(theoreticalIntensities, index, cZIntensity);

        AssignIntensity(theoreticalIntensities, index - 1, cZAroundIntensity);

        AssignIntensity(theoreticalIntensities, index + 1, cZAroundIntensity);
      }

      return CalculateFFT(this._experimentalIntensities, theoreticalIntensities);
    }

    #endregion

    private int GetMinPowerOfTwo(double maxMz)
    {
      int result = 1024;
      while (result < maxMz)
      {
        result *= 2;
      }
      return result;
    }

    private static double CalculateFFT(double[] _experimentalIntensities, double[] theoreticalIntensities)
    {
      var values = new double[151];

      {
        double value = 0.0;
        for (int i = 0; i < _experimentalIntensities.Length; i++)
        {
          value += _experimentalIntensities[i] * theoreticalIntensities[i];
        }

        if (value == 0.0)
        {
          return 0.0;
        }

        values[75] = value;
      }

      for (int shift = -75; shift < 0; shift++)
      {
        int eFrom = -shift;
        int eEnd = _experimentalIntensities.Length + shift;
        int tFrom = 0;

        double value = 0.0;
        while (eFrom < eEnd)
        {
          value += _experimentalIntensities[eFrom] * theoreticalIntensities[tFrom];
          eFrom++;
          tFrom++;
        }
        values[75 + shift] = value;
      }

      for (int shift = 1; shift <= 75; shift++)
      {
        int eFrom = 0;
        int eEnd = _experimentalIntensities.Length - shift;
        int tFrom = shift;

        double value = 0.0;
        while (eFrom < eEnd)
        {
          value += _experimentalIntensities[eFrom] * theoreticalIntensities[tFrom];
          eFrom++;
          tFrom++;
        }
        values[75 + shift] = value;
      }

      //for (int i = 0; i < values.Length; i++)
      //{
      //  Console.Out.WriteLine("{0}\t{1}", i - 75, values[i]);
      //}

      var acc = new MeanStandardDeviation(values);
      double mean = acc.Mean;
      if (acc.Mean == 0)
      {
        return 0;
      }
      else
      {
        return values[75] / mean;
      }
    }

    private void AssignIntensity(double[] theoreticalIntensities, int index, double cZIntensity)
    {
      if (index < 0 || index >= theoreticalIntensities.Length)
      {
        return;
      }

      if (theoreticalIntensities[index] < cZIntensity)
      {
        theoreticalIntensities[index] = cZIntensity;
      }
    }
  }
}