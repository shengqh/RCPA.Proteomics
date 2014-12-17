using MathNet.Numerics.Statistics;
using RCPA.Proteomics.Spectrum;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace RCPA.Proteomics.Quantification.IsobaricLabelling
{
  public class UsedChannel
  {
    public int Index { get; set; }

    public double Mz { get; set; }

    public string Name { get; set; }

    public double MinMz { get; set; }

    public double MaxMz { get; set; }

    public override string ToString()
    {
      return Name;
    }
  }

  public static class UsedChannelExtension
  {
    private static int FindMinimumDistanceIndex(List<UsedChannel> channels, double mz, HashSet<int> ignore)
    {
      var minDistance = double.MaxValue;
      var result = -1;
      for (int i = 0; i < channels.Count; i++)
      {
        if (ignore.Contains(i))
        {
          continue;
        }
        var dis = Math.Abs(channels[i].Mz - mz);
        if (dis < minDistance)
        {
          result = i;
          minDistance = dis;
        }
      }

      return result;
    }

    /// <summary>
    /// Calibrate the mass of each ion
    /// </summary>
    /// <typeparam name="T">Peak</typeparam>
    /// <param name="peaks">List of peak list</param>
    public static void CalibrateMass<T>(this IEnumerable<UsedChannel> channels, IEnumerable<PeakList<T>> peaks, string calibrationFile) where T : Peak
    {
      Dictionary<UsedChannel, List<Peak>> map = GetChannelPeaks<T>(channels, peaks);

      var updates = (from cha in map
                     let oldMz = cha.Key.Mz
                     let newmz = cha.Value.Sum(l => l.Mz * l.Intensity) / cha.Value.Sum(l => l.Intensity)
                     let mean = Statistics.MeanStandardDeviation(cha.Value.ConvertAll(m => m.Mz))
                     select new { Channel = cha.Key, CalibratedMz = newmz, MeanMz = mean.Item1, StandardDeviation = mean.Item2 }).OrderBy(m => m.Channel.Mz).ToList();

      if (!string.IsNullOrEmpty(calibrationFile))
      {
        using (var sw = new StreamWriter(calibrationFile))
        {
          sw.WriteLine("Name\tTheoreticalMz\tWeightedMz\tMeanMz\tStandardDeviation");
          foreach (var up in updates)
          {
            sw.WriteLine("{0}\t{1:0.#####}\t{2:0.#####}\t{3:0.#####}\t{4:0.#####}",
              up.Channel.Name,
              up.Channel.Mz,
              up.CalibratedMz,
              up.MeanMz,
              up.StandardDeviation);
          }
        }
      }

      foreach (var up in updates)
      {
        up.Channel.Mz = up.CalibratedMz;
      }
    }

    public static Dictionary<UsedChannel, List<Peak>> GetChannelPeaks<T>(this IEnumerable<UsedChannel> channels, IEnumerable<PeakList<T>> peaks) where T : Peak
    {
      List<List<UsedChannel>> used = new List<List<UsedChannel>>();
      Dictionary<UsedChannel, List<Peak>> map = new Dictionary<UsedChannel, List<Peak>>();
      foreach (var cha in channels)
      {
        map[cha] = new List<Peak>();

        if (used.Count == 0)
        {
          used.Add(new List<UsedChannel>());
          used.Last().Add(cha);
          continue;
        }

        var lastC = used.Last().Last();
        if (cha.MinMz < lastC.MaxMz)
        {
          used.Last().Add(cha);
        }
        else
        {
          used.Add(new List<UsedChannel>());
          used.Last().Add(cha);
        }
      }

      foreach (var cc in used)
      {
        if (cc.Count > 1)
        {
          var ccList = new List<List<Peak>>();
          foreach (var ccc in cc)
          {
            ccList.Add(map[ccc]);
          }

          var minMz = cc.Min(l => l.MinMz);
          var maxMz = cc.Max(l => l.MaxMz);

          HashSet<int> assigned = new HashSet<int>();
          foreach (var pkl in peaks)
          {
            //For each peak list, find potential peaks matched to channels
            var findpeaks = (from p in pkl.GetRange(minMz, maxMz) orderby p.Intensity descending select p).ToList();

            //If there are enough peaks, select top N peaks and assigned them to channels by order of M/Z
            if (findpeaks.Count >= cc.Count)
            {
              var assignPeaks = findpeaks.Take(cc.Count).OrderBy(m => m.Mz).ToArray();
              for (int i = 0; i < assignPeaks.Length; i++)
              {
                assignPeaks[i].Tag = pkl.FirstScan;
                ccList[i].Add(assignPeaks[i]);
              }
              continue;
            }

            //If there not enough peaks, assign peak to closest channel
            assigned.Clear();
            while (findpeaks.Count > 0)
            {
              findpeaks[0].Tag = pkl.FirstScan;
              var index = FindMinimumDistanceIndex(cc, findpeaks[0].Mz, assigned);
              ccList[index].Add(findpeaks[0]);
              assigned.Add(index);
              findpeaks.RemoveAt(0);
            }
          }
        }
        else
        {
          var found = map[cc[0]];
          var minMz = cc[0].MinMz;
          var maxMz = cc[0].MaxMz;

          HashSet<int> assigned = new HashSet<int>();
          foreach (var pkl in peaks)
          {
            //For each peak list, find potential peaks (within 0.5 Dalton) matched to channels
            var peak = pkl.FindMaxIntensityPeak(minMz, maxMz);
            if (peak != null)
            {
              peak.Tag = pkl.FirstScan;
              found.Add(peak);
            }
          }
        }
      }
      return map;
    }
  }
}
