using System;
using System.Collections.Generic;
using System.Linq;

namespace RCPA.Proteomics.Snp
{
  /// <summary>
  /// The peaks shared with most peak list will be selected. When ties found, the one with highest intensity will be selected.
  /// </summary>
  public class BestSpectrumTopSharedPeaksBuilder : IBestSpectrumBuilder
  {
    private double fragmentPPMTolerance;
    private int maxPeakCount;

    public BestSpectrumTopSharedPeaksBuilder(double fragmentPPMTolerance, int maxPeakCount)
    {
      this.fragmentPPMTolerance = fragmentPPMTolerance;
      this.maxPeakCount = maxPeakCount;
    }

    public MS3Item Build(List<MS3Item> g)
    {
      var result = new MS3Item();

      result.PrecursorMZ = g.Sum(m => m.PrecursorMZ * m.Sum(p => p.Intensity)) / g.Sum(m => m.Sum(p => p.Intensity));

      List<MS3Peak> peaks = new List<MS3Peak>();
      for (int i = 0; i < g.Count; i++)
      {
        peaks.AddRange(from peak in g[i]
                       select new MS3Peak() { Mz = peak.Mz, Intensity = peak.Intensity, Tag = i, CombinedCount = peak.CombinedCount });
      }
      peaks.Sort((m1, m2) => m1.Mz.CompareTo(m2.Mz));

      //group peak by m/z
      var pgroups = new List<List<MS3Peak>>();
      var currentPkl = new List<MS3Peak>();
      currentPkl.Add(peaks.First());
      for (int i = 1; i < peaks.Count; i++)
      {
        foreach (var peak in currentPkl)
        {
          var diff = Math.Abs(peaks[i].Mz - peak.Mz);
          var ppmdiff = PrecursorUtils.mz2ppm(peak.Mz, diff);
          if (ppmdiff <= fragmentPPMTolerance)
          {
            currentPkl.Add(peaks[i]);
            break;
          }
        }

        if (peaks[i] != currentPkl.Last())
        {
          pgroups.Add(currentPkl);
          currentPkl = new List<MS3Peak>();
          currentPkl.Add(peaks[i]);
        }
      }
      pgroups.Add(currentPkl);

      result.Clear();
      foreach (var pg in pgroups)
      {
        if (pg.Count == 1)
        {
          result.Add(pg.First());
        }
        else
        {
          //for each spectrum, only the peak with most intensity will be used for merge
          var waiting = (from t in pg.GroupBy(m => m.Tag)
                         select (from tt in t
                                 orderby tt.Intensity descending
                                 select tt).First()).ToArray();

          var combinedpeak = new MS3Peak()
          {
            Mz = waiting.Sum(m => m.Mz * m.Intensity) / waiting.Sum(m => m.Intensity),
            Intensity = waiting.Sum(m => m.Intensity),
            CombinedCount = waiting.Sum(m => m.CombinedCount)
          };

          result.Add(combinedpeak);
        }
      }

      result.CombinedCount = g.Count;

      if (result.Count > maxPeakCount)
      {
        result.Sort((m1, m2) =>
        {
          var res = m2.CombinedCount.CompareTo(m1.CombinedCount);
          if (res == 0)
          {
            res = m2.Intensity.CompareTo(m1.Intensity);
          }
          return res;
        });

        for (int i = result.Count - 1; i >= maxPeakCount; i--)
        {
          result.RemoveAt(i);
        }

        result.SortByMz();
      }

      return result;
    }
  }
}
