using System.Collections.Generic;
using RCPA.Proteomics.Spectrum;

namespace RCPA.Proteomics
{
  public class ChargeDeconvolution
  {
    public const double C_GAP = C13 - C12;
    private const double C12 = 12.00;
    private const double C13 = 13.003355;

    private const int MIN_THREE_PEAKS_NEEDED_FOR_CHARGE_LARGER_THAN = 3;

    private readonly int maxCharge;
    private readonly double[] mzGaps;
    private readonly double ppmTolerance;

    public ChargeDeconvolution(double ppmTolerance, int maxCharge)
    {
      this.ppmTolerance = ppmTolerance;
      this.maxCharge = maxCharge;
      this.mzGaps = new double[maxCharge];
      for (int i = 1; i <= maxCharge; i++)
      {
        this.mzGaps[i - 1] = C_GAP / i;
      }
    }

    public double PPMTolerance
    {
      get { return this.ppmTolerance; }
    }

    public void Deconvolute<T>(List<T> peaks) where T : IPeak
    {
      peaks.Sort((m1, m2) => m1.Mz.CompareTo(m2.Mz));

      var peakEnvelopeMap = new Dictionary<T, List<PeakList<T>>>();
      var envelopes = new List<PeakList<T>>();
      for (int i = 0; i < peaks.Count - 1; i++)
      {
        double mzTolerance = PrecursorUtils.ppm2mz(peaks[i].Mz, this.ppmTolerance);

        if (peaks[i + 1].Mz - peaks[i].Mz > C_GAP + mzTolerance)
        {
          continue;
        }

        var envelopeList = new List<PeakList<T>>();
        for (int charge = 1; charge <= this.maxCharge; charge++)
        {
          //if peaks[i] has already been assigned a charge,
          //only that charge will be considered
          if (0 != peaks[i].Charge && charge != peaks[i].Charge)
          {
            continue;
          }

          double gap = this.mzGaps[charge - 1];

          var curPeaks = new PeakList<T>();
          curPeaks.Add(peaks[i]);

          //just use precursor charge to record the assumed charge
          curPeaks.PrecursorCharge = charge;

          int nextPeakIndex = i + 1;
          while (true)
          {
            double currMz = curPeaks[curPeaks.Count - 1].Mz;
            var nextMzRange = new Pair<double, double>(currMz + gap - mzTolerance, currMz + gap + mzTolerance);
            int findPeakIndex = FindPeak(peaks, nextPeakIndex, nextMzRange, charge);
            if (-1 == findPeakIndex)
            {
              break;
            }
            curPeaks.Add(peaks[findPeakIndex]);
            nextPeakIndex = findPeakIndex + 1;
          }

          if (charge > MIN_THREE_PEAKS_NEEDED_FOR_CHARGE_LARGER_THAN)
          {
            if (curPeaks.Count > 2)
            {
              envelopeList.Add(curPeaks);
            }
          }
          else if (curPeaks.Count > 1)
          {
            envelopeList.Add(curPeaks);
          }
        }

        if (envelopeList.Count > 0)
        {
          peakEnvelopeMap.Add(peaks[i], envelopeList);
          envelopes.AddRange(envelopeList);
        }
      }

      SortEnvelopesByCountChargeMz(envelopes);
      RemoveAllContainedEnvelopes(envelopes);

      while (envelopes.Count > 0)
      {
        SortEnvelopesByCountChargeMz(envelopes);

        if (1 >= envelopes[0].Count)
        {
          break;
        }

        envelopes[0].SetFragmentIonCharge(envelopes[0].PrecursorCharge);

        foreach (T p in envelopes[0])
        {
          for (int j = 1; j < envelopes.Count; j++)
          {
            if (envelopes[j].Contains(p))
            {
              if (envelopes[j].PrecursorCharge != envelopes[0].PrecursorCharge)
              {
                envelopes[j].Remove(p);
              }
            }
          }
        }
        envelopes.RemoveAt(0);

        for (int iEnv = envelopes.Count - 1; iEnv >= 0; iEnv--)
        {
          if (1 >= envelopes[iEnv].Count)
          {
            envelopes.RemoveAt(iEnv);
          }
        }
      }
    }

    private static void SortEnvelopesByCountChargeMz<T>(List<PeakList<T>> envelopes) where T : IPeak
    {
      envelopes.Sort(delegate(PeakList<T> p1, PeakList<T> p2)
                       {
                         int result = -p1.Count.CompareTo(p2.Count);
                         if (0 == result && 0 != p1.Count)
                         {
                           result = -p1.PrecursorCharge.CompareTo(p2.PrecursorCharge);
                           if (0 == result)
                           {
                             result = p1[0].Mz.CompareTo(p2[0].Mz);
                           }
                         }
                         return result;
                       });
    }

    private static void RemoveAllContainedEnvelopes<T>(List<PeakList<T>> envelopes) where T : IPeak
    {
      for (int i = 0; i < envelopes.Count - 1; i++)
      {
        for (int j = envelopes.Count - 1; j > i; j--)
        {
          bool bIncluded = true;
          foreach (T p in envelopes[j])
          {
            if (!envelopes[i].Contains(p))
            {
              bIncluded = false;
              break;
            }
          }
          if (bIncluded)
          {
            envelopes.RemoveAt(j);
          }
        }
      }
    }

    private int FindPeak<T>(List<T> peaks, int nextPeakIndex, Pair<double, double> nextMzRange, int charge) where T : IPeak
    {
      var candidates = new PeakList<T>();
      for (int i = nextPeakIndex; i < peaks.Count; i++)
      {
        if (peaks[i].Mz > nextMzRange.Second)
        {
          break;
        }

        if (peaks[i].Mz < nextMzRange.First)
        {
          continue;
        }

        //if peaks[i] has been assigned charge and the charge is not 
        //equals the assumed charge, just ignore it.
        if (0 != peaks[i].Charge && charge != peaks[i].Charge)
        {
          continue;
        }

        candidates.Add(peaks[i]);
      }

      if (candidates.Count > 0)
      {
        T peak = candidates.FindMaxIntensityPeak();
        return peaks.IndexOf(peak);
      }
      else
      {
        return -1;
      }
    }
  }
}