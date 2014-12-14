using RCPA.Proteomics.Spectrum;
using System;
using System.Collections.Generic;
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
    private static int FindMinimumDistanceIndex(UsedChannel[] channels, double mz, HashSet<int> ignore)
    {
      var minDistance = double.MaxValue;
      var result = -1;
      for (int i = 0; i < channels.Length; i++)
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
    public static void CalibrateMass<T>(this IEnumerable<UsedChannel> channels, IEnumerable<PeakList<T>> peaks, double maxShift) where T : Peak
    {
      //Group channels by rounded m/z
      var c = (from g in channels.GroupBy(m => Math.Round(m.Mz))
               select new { Mz = g.Key, Channels = (from l in g orderby l.Mz select l).ToArray() }).OrderBy(m => m.Mz).ToList();

      foreach (var cc in c)
      {
        if (cc.Channels.Length > 1)
        {
          var ccList = new List<List<Peak>>();
          foreach (var ccc in cc.Channels)
          {
            ccList.Add(new List<Peak>());
          }

          var minMz = cc.Channels.Min(l => l.Mz) - maxShift;
          var maxMz = cc.Channels.Max(l => l.Mz) + maxShift;

          HashSet<int> assigned = new HashSet<int>();
          foreach (var pkl in peaks)
          {
            //For each peak list, find potential peaks matched to channels
            var findpeaks = (from p in pkl.GetRange(minMz, maxMz) orderby p.Intensity descending select p).ToList();

            //If there are enough peaks, select top N peaks and assigned them to channels by order of M/Z
            if (findpeaks.Count >= cc.Channels.Length)
            {
              var assignPeaks = findpeaks.Take(cc.Channels.Length).OrderBy(m => m.Mz).ToArray();
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
              var index = FindMinimumDistanceIndex(cc.Channels, findpeaks[0].Mz, assigned);
              ccList[index].Add(findpeaks[0]);
              assigned.Add(index);
              findpeaks.RemoveAt(0);
            }
          }

          for (int i = 0; i < cc.Channels.Length; i++)
          {
            //var newmz = MathNet.Numerics.Statistics.Statistics.Median(from peak in ccList[i] select peak.Mz);
            var newmz = ccList[i].Sum(l => l.Mz * l.Intensity) / ccList[i].Sum(l => l.Intensity);
            Console.WriteLine("{0} : {1} => {2}", cc.Channels[i].Name,
              cc.Channels[i].Mz,
              newmz);
            cc.Channels[i].Mz = newmz;
          }
        }
        else
        {
          var found = new List<Peak>();
          var minMz = cc.Channels[0].Mz - maxShift;
          var maxMz = cc.Channels[0].Mz + maxShift;

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


          var newmz = found.Sum(l => l.Mz * l.Intensity) / found.Sum(l => l.Intensity);
          Console.WriteLine("{0} : {1} => {2}", cc.Channels[0].Name,
            cc.Channels[0].Mz,
            newmz);
          cc.Channels[0].Mz = newmz;
        }
      }
    }
  }
}
