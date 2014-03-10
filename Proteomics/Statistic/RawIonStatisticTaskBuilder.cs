using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using RCPA.Proteomics.Raw;
using RCPA.Proteomics.Spectrum;
using System.IO;
using System.Threading.Tasks;
using MathNet.Numerics.Statistics;

namespace RCPA.Proteomics.Statistic
{
  public class RawIonStatisticTaskBuilder : AbstractParallelTaskProcessor
  {
    private const int FULLMS_CHARGE = -1;

    private string targetDir;

    private double productIonPPM;

    private double minRelativeIntensity;

    public RawIonStatisticTaskBuilder(string targetDir, double productIonPPM, double minRelativeIntensity)
    {
      this.targetDir = targetDir;
      this.productIonPPM = productIonPPM;
      this.minRelativeIntensity = minRelativeIntensity;
    }

    private class PeakEntry
    {
      public PeakEntry()
      {
        this.Intensities = new List<double>();
      }

      public Peak Ion { get; set; }
      public double FromMz { get; set; }
      public double ToMz { get; set; }
      public List<double> Intensities { get; set; }
    }

    public override IEnumerable<string> Process(string fileName)
    {
      Dictionary<int, Dictionary<int, List<PeakEntry>>> maps = new Dictionary<int, Dictionary<int, List<PeakEntry>>>();
      Dictionary<int, Dictionary<int, List<PeakEntry>>> compmaps = new Dictionary<int, Dictionary<int, List<PeakEntry>>>();
      Dictionary<int, int> scanCounts = new Dictionary<int, int>();
      using (var reader = RawFileFactory.GetRawFileReader(fileName))
      {
        var firstScan = reader.GetFirstSpectrumNumber();
        var lastScan = reader.GetLastSpectrumNumber();

        for (int i = firstScan; i <= lastScan; i++)
        {
          Peak precursor;
          if (reader.GetMsLevel(i) == 1)
          {
            precursor = new Peak(0.0, 0.0, FULLMS_CHARGE);
          }
          else
          {
            precursor = reader.GetPrecursorPeak(i);
          }

          if (!scanCounts.ContainsKey(precursor.Charge))
          {
            scanCounts[precursor.Charge] = 1;
          }
          else
          {
            scanCounts[precursor.Charge] = scanCounts[precursor.Charge] + 1;
          }

          var pkl = reader.GetPeakList(i);
          if (pkl.Count == 0)
          {
            continue;
          }

          if (Progress.IsCancellationPending() || IsLoopStopped)
          {
            return null;
          }

          if (!maps.ContainsKey(precursor.Charge))
          {
            maps[precursor.Charge] = new Dictionary<int, List<PeakEntry>>();
            compmaps[precursor.Charge] = new Dictionary<int, List<PeakEntry>>();
          }

          var map = maps[precursor.Charge];
          var compmap = compmaps[precursor.Charge];

          var maxPeak = pkl.FindMaxIntensityPeak();
          var minIntensity = maxPeak.Intensity * this.minRelativeIntensity;

          double precursorMass = precursor.Charge > 0 ? PrecursorUtils.MzToMass(precursor.Mz, precursor.Charge, true) : 0.0;
          foreach (var peak in pkl)
          {
            if (peak.Intensity > minIntensity)
            {
              AddPeak(map, maxPeak.Intensity, peak);

              if (precursor.Charge > 0)
              {
                var peakMass = peak.Charge == 0 ? peak.Mz : PrecursorUtils.MzToMass(peak.Mz, peak.Charge, true);
                peakMass = precursorMass - peakMass;
                AddPeak(compmap, maxPeak.Intensity, new Peak(peakMass, peak.Intensity, peak.Charge));
              }
            }
          }
        }
      }

      var keys = (from charge in maps.Keys
                  orderby charge
                  select charge).ToList();

      var resultFile1 = new FileInfo(targetDir + "//" + new FileInfo(fileName).Name + ".forward.ionfrequency").FullName;
      using (StreamWriter sw = new StreamWriter(resultFile1))
      {
        WriteMap(scanCounts, keys, sw, maps);
      }
      var resultFile2 = new FileInfo(targetDir + "//" + new FileInfo(fileName).Name + ".backward.ionfrequency").FullName;
      using (StreamWriter sw = new StreamWriter(resultFile2))
      {
        WriteMap(scanCounts, keys, sw, compmaps);
      }
      return new string[] { resultFile1, resultFile2 };
    }

    private void WriteMap(Dictionary<int, int> scanCounts, List<int> keys, StreamWriter sw, Dictionary<int, Dictionary<int, List<PeakEntry>>> curMaps)
    {
      foreach (var key in keys)
      {
        var totalCount = scanCounts[key];

        if (key == FULLMS_CHARGE)
        {
          sw.WriteLine("FullMS,ScanCount={1}", key, totalCount);
        }
        else if (key == 0)
        {
          sw.WriteLine("Charge=UNKNOWN,ScanCount={1}", key, totalCount);
        }
        else
        {
          sw.WriteLine("Charge={0},ScanCount={1}",key, totalCount);
        }
        var map = curMaps[key];

        sw.WriteLine("Ion\tCount\tFrequency\tMeanIntensity\tSD\tMedianIntensity");
        foreach (var e in map.Values)
        {
          e.Sort((m1, m2) => m2.Intensities.Count.CompareTo(m1.Intensities.Count));
          for (int i = e.Count - 1; i >= 0; i--)
          {
            for (int j = 0; j < i; j++)
            {
              if (e[i].Ion.Mz >= e[j].FromMz && e[i].Ion.Mz <= e[j].ToMz)
              {
                var allIntensity = e[i].Ion.Intensity + e[j].Ion.Intensity;

                e[j].Ion.Mz = (e[i].Ion.Mz * e[i].Ion.Intensity + e[j].Ion.Mz * e[j].Ion.Intensity) / allIntensity;
                e[j].Ion.Intensity = allIntensity;
                e[j].Intensities.AddRange(e[i].Intensities);

                var gap = PrecursorUtils.ppm2mz(e[j].Ion.Mz, this.productIonPPM);
                e[j].FromMz = e[j].Ion.Mz - gap;
                e[j].ToMz = e[j].Ion.Mz + gap;

                e.RemoveAt(i);

                break;
              }
            }
          }
        }

        var totalentries = (from e in map.Values
                            from en in e
                            orderby en.Intensities.Count descending
                            select en).Take(50).ToList();

        totalentries.ForEach(m =>
        {
          var mean = Statistics.Mean(m.Intensities);
          var sd = Statistics.StandardDeviation(m.Intensities);
          var median = Statistics.Median(m.Intensities);

          sw.WriteLine("{0:0.0000}\t{1}\t{2:0.00}%\t{3:0.000}\t{4:0.000}\t{5:0.000}", m.Ion.Mz, m.Intensities.Count, m.Intensities.Count * 100.0 / totalCount, mean, sd, median);
        });
        sw.WriteLine();
      }
    }

    private void AddPeak(Dictionary<int, List<PeakEntry>> map, double maxPeakIntensity, Peak peak)
    {
      var mz = (int)(Math.Round(peak.Mz));
      if (!map.ContainsKey(mz))
      {
        map[mz] = new List<PeakEntry>();
      }

      var entries = map[mz];
      var entryIndex = FindEntryIndex(peak, entries);
      PeakEntry entry;
      if (entryIndex != -1)
      {
        entry = entries[entryIndex];

        var relativeIntensity = peak.Intensity / maxPeakIntensity;
        var allIntensity = entry.Ion.Intensity + relativeIntensity;
        entry.Ion.Mz = (entry.Ion.Mz * entry.Ion.Intensity + peak.Mz * relativeIntensity) / allIntensity;
        entry.Ion.Intensity = allIntensity;
        entry.Intensities.Add(relativeIntensity);
        var gap = PrecursorUtils.ppm2mz(peak.Mz, this.productIonPPM);
        entry.FromMz = peak.Mz - gap;
        entry.ToMz = peak.Mz + gap;

        while (entryIndex > 0)
        {
          if (entries[entryIndex].Ion.Mz < entries[entryIndex - 1].Ion.Mz)
          {
            entries.Swap(entryIndex, entryIndex - 1);
            entryIndex--;
          }
          else
          {
            break;
          }
        }

        while (entryIndex < entries.Count - 1)
        {
          if (entries[entryIndex].Ion.Mz > entries[entryIndex + 1].Ion.Mz)
          {
            entries.Swap(entryIndex, entryIndex + 1);
            entryIndex++;
          }
          else
          {
            break;
          }
        }
      }
      else
      {
        var gap = PrecursorUtils.ppm2mz(peak.Mz, this.productIonPPM);
        entry = new PeakEntry()
        {
          Ion = new Peak(peak.Mz, peak.Intensity / maxPeakIntensity),
          FromMz = peak.Mz - gap,
          ToMz = peak.Mz + gap
        };
        entry.Intensities.Add(entry.Ion.Intensity);
        AddEntry(entries, entry);
      }
    }

    private static void AddEntry(List<PeakEntry> entries, PeakEntry entry)
    {
      for (int i = 0; i < entries.Count; i++)
      {
        if (entries[i].FromMz > entry.FromMz)
        {
          entries.Insert(i, entry);
          return;
        }
      }

      entries.Add(entry);
    }

    private static int FindEntryIndex(Peak peak, List<PeakEntry> entries)
    {
      int result = -1;
      for (int i = 0; i < entries.Count; i++)
      {
        var entry = entries[i];

        if (peak.Mz > entry.ToMz)
        {
          continue;
        }

        if (peak.Mz < entry.FromMz)
        {
          break;
        }

        if (result == -1)
        {
          result = i;
        }
        else if (entry.Ion.Intensity > entries[i].Ion.Intensity)
        {
          result = i;
        }
      }
      return result;
    }
  }
}
