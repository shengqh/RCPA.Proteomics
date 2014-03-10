using System.Collections.Generic;
using RCPA.Proteomics.Spectrum;

namespace RCPA.Proteomics.Utils
{
  public class EnvelopeUtils
  {
    public static List<PeakList<T>> GetEnvelopes<T>(PeakList<T> pkl, double ppmTolerance) where T : IPeak
    {
      var tmp = new PeakList<T>(pkl);

      var result = new List<PeakList<T>>();
      while (tmp.Count > 0)
      {
        double mzTolerance = 2 * PrecursorUtils.ppm2mz(tmp[0].Mz, ppmTolerance);
        PeakList<T> envelope = tmp.FindEnvelope(tmp[0], mzTolerance, true);
        result.Add(envelope);

        foreach (T peak in envelope)
        {
          //Remove all peaks around current peak
          PeakList<T> findPeaks = tmp.FindPeak(peak.Mz, peak.Charge, mzTolerance);
          foreach (T findPeak in findPeaks)
          {
            tmp.Remove(findPeak);
          }
        }
      }
      /*
            Console.Out.WriteLine("---before merge");
            foreach (PeakList<T> pkl in result)
            {
              Console.Out.WriteLine("---");
              foreach (Peak p in pkl)
              {
                Console.Out.WriteLine(p);
              }
            }

            result.Sort(new PeakListMzAscendingComparer<T>());

            int current = 0;

            while (current < result.Count - 1)
            {
              PeakList<T> currentPkl = result[current];
              if (currentPkl[0].Charge > 0)
              {
                double expectMz = currentPkl[currentPkl.Count - 1].Mz + ChargeDeconvolution.C_GAP / currentPkl[0].Charge;
                double mzTolerance = 4 * Precursor.ppm2mz(currentPkl[0].Mz, ppmTolerance);
                int next = current + 1;
                while (next < result.Count)
                {
                  double gap = result[next][0].Mz - expectMz;
                  if (gap >= 2.0)
                  {
                    break;
                  }

                  if (Math.Abs(gap) > mzTolerance)
                  {
                    next++;
                    continue;
                  }

                  if (result[next][0].Charge != currentPkl[0].Charge)
                  {
                    next++;
                    continue;
                  }

                  Console.Out.WriteLine("Current----");
                  foreach (T p in currentPkl)
                  {
                    Console.Out.WriteLine(p);
                  }
                  Console.Out.WriteLine("Add----");
                  foreach (T p in result[next])
                  {
                    Console.Out.WriteLine(p);
                  }

                  currentPkl.AddRange(result[next]);

                  Console.Out.WriteLine("To----");
                  foreach (T p in currentPkl)
                  {
                    Console.Out.WriteLine(p);
                  }

                  result.RemoveAt(next);
                }
              }

              current++;
            }

            Console.Out.WriteLine("---after merge");
            foreach (PeakList<T> pkl in result)
            {
              Console.Out.WriteLine("---");
              foreach (Peak p in pkl)
              {
                Console.Out.WriteLine(p);
              }
            }
      */
      return result;
    }

    public static void ExtendEnvelope<T>(PeakList<T> pkl, double ppmTolerance) where T : Peak
    {
      for (int i = 0; i < pkl.Count; i++)
      {
        if (0 == pkl[i].Charge)
        {
          continue;
        }

        double mzTolerance = 2 * PrecursorUtils.ppm2mz(pkl[i].Mz, ppmTolerance);

        double priorMz = pkl[i].Mz - ChargeDeconvolution.C_GAP / pkl[i].Charge;
        for (int j = i - 1; j >= 0; j--)
        {
          if (pkl[j].Charge > 0)
          {
            continue;
          }

          if (pkl[j].Mz > priorMz + mzTolerance)
          {
            continue;
          }

          if (pkl[j].Mz < priorMz - mzTolerance)
          {
            break;
          }

          pkl[j].Charge = pkl[i].Charge;
        }

        double nextMz = pkl[i].Mz + ChargeDeconvolution.C_GAP / pkl[i].Charge;
        for (int j = i + 1; j < pkl.Count; j++)
        {
          if (pkl[j].Charge > 0)
          {
            continue;
          }

          if (pkl[j].Mz < nextMz - mzTolerance)
          {
            continue;
          }

          if (pkl[j].Mz > nextMz + mzTolerance)
          {
            break;
          }

          pkl[j].Charge = pkl[i].Charge;
        }
      }

      return;
    }
  }
}