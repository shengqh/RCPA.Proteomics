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
        this.Intensities = new List<Pair<int, double>>();
      }

      public Peak Ion { get; set; }
      public double FromMz { get; set; }
      public double ToMz { get; set; }
      public List<Pair<int, double>> Intensities { get; set; }
      public override string ToString()
      {
        return string.Format("{0:0.0000}", Ion.Mz);
      }
    }

    public override IEnumerable<string> Process(string fileName)
    {
      Dictionary<int, List<PeakEntry>> maps = new Dictionary<int, List<PeakEntry>>();
      Dictionary<int, List<PeakEntry>> compmaps = new Dictionary<int, List<PeakEntry>>();
      Dictionary<int, int> scanCounts = new Dictionary<int, int>();
      using (var reader = RawFileFactory.GetRawFileReader(fileName))
      {
        //var firstScan = 17047;
        //var lastScan = 17047;
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
            maps[precursor.Charge] = new List<PeakEntry>();
            compmaps[precursor.Charge] = new List<PeakEntry>();
          }

          var ions = maps[precursor.Charge];
          var compIons = compmaps[precursor.Charge];

          var maxPeak = pkl.FindMaxIntensityPeak();
          var minIntensity = maxPeak.Intensity * this.minRelativeIntensity;

          double precursorMass = precursor.Charge > 0 ? PrecursorUtils.MzToMass(precursor.Mz, precursor.Charge, true) : 0.0;
          foreach (var peak in pkl)
          {
            if (peak.Intensity > minIntensity)
            {
              AddPeak(ions, maxPeak.Intensity, i, peak);

              if (precursor.Charge > 0)
              {
                var peakMass = peak.Charge == 0 ? peak.Mz : PrecursorUtils.MzToMass(peak.Mz, peak.Charge, true);
                peakMass = precursorMass - peakMass;
                AddPeak(compIons, maxPeak.Intensity, i, new Peak(peakMass, peak.Intensity, peak.Charge));
              }
            }
          }
          pkl.Clear();
        }
      }

      var keys = (from charge in maps.Keys
                  orderby charge
                  select charge).ToList();

      var resultFile1 = new FileInfo(targetDir + "//" + new FileInfo(fileName).Name + ".forward.ionfrequency").FullName;
      WriteMap(scanCounts, keys, resultFile1, maps, true);

      var resultFile2 = new FileInfo(targetDir + "//" + new FileInfo(fileName).Name + ".backward.ionfrequency").FullName;
      WriteMap(scanCounts, keys, resultFile2, compmaps, false);

      return new string[] { resultFile1, resultFile2 };
    }

    private void WriteMap(Dictionary<int, int> scanCounts, List<int> keys, string filename, Dictionary<int, List<PeakEntry>> curMaps, bool exportIndividualIon)
    {
      using (var sw = new StreamWriter(filename))
      {
        foreach (var key in keys)
        {
          var totalCount = scanCounts[key];
          string subfile = string.Empty;

          if (key == FULLMS_CHARGE)
          {
            sw.WriteLine("FullMS,ScanCount={1}", key, totalCount);
            subfile = filename + ".fullms";
          }
          else if (key == 0)
          {
            sw.WriteLine("Charge=UNKNOWN,ScanCount={1}", key, totalCount);
            subfile = filename + ".unknown";
          }
          else
          {
            sw.WriteLine("Charge={0},ScanCount={1}", key, totalCount);
            subfile = filename + ".ms2charge" + key.ToString();
          }

          sw.WriteLine("Ion\tCount\tFrequency\tMeanIntensity\tSD\tMedianIntensity");

          var e = curMaps[key];
          //combine all ions with identical m/z
          e.Sort((m1, m2) => m2.Ion.Mz.CompareTo(m1.Ion.Mz));
          for (int i = e.Count - 1; i > 0; i--)
          {
            if (e[i].Ion.Mz == e[i - 1].Ion.Mz)
            {
              e[i - 1].Intensities.AddRange(e[i].Intensities);
              e[i - 1].Ion.Intensity += e[i].Ion.Intensity;
              e.RemoveAt(i);
            }
          }

          //combine ions within product ion tolerance
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

          foreach (var ee in e)
          {
            ee.Intensities = (from g in ee.Intensities.GroupBy(m => m.First)
                              select g.Count() > 1 ? (from gg in g orderby gg.Second descending select gg).First() : g.First()).OrderBy(l => l.First).ToList();

          }

          if (exportIndividualIon)
          {
            using (var sw2 = new StreamWriter(subfile))
            {
              var ens = (from en in e
                         orderby en.Ion.Mz
                         select en).ToList();
              sw2.WriteLine("mz\tscan\tintensity");
              int index = 0;
              foreach (var ion in ens)
              {
                index++;
                foreach (var value in ion.Intensities)
                {
                  sw2.WriteLine("{0:0.00000}\t{1}\t{2:0.000}", index, ion.Ion.Mz, value.First, value.Second);
                }
              }
            }
          }

          var totalentries = (from en in e
                              orderby en.Intensities.Count descending
                              select en).Take(50).ToList();

          totalentries.ForEach(m =>
          {
            var ints = (from i in m.Intensities select i.Second).ToArray();
            var mean = Statistics.Mean(ints);
            var sd = Statistics.StandardDeviation(ints);
            var median = Statistics.Median(ints);

            sw.WriteLine("{0:0.0000}\t{1}\t{2:0.00}%\t{3:0.000}\t{4:0.000}\t{5:0.000}", m.Ion.Mz, m.Intensities.Count, m.Intensities.Count * 100.0 / totalCount, mean, sd, median);
          });
          sw.WriteLine();
        }
      }
    }

    private void AddPeak(List<PeakEntry> map, double maxPeakIntensity, int scan, Peak peak)
    {
      var relativeIntensity = peak.Intensity / maxPeakIntensity;

      PeakEntry entry = new PeakEntry()
      {
        Ion = peak,
        Intensities = new List<Pair<int, double>>()
      };

      entry.Intensities.Add(new Pair<int, double>(scan, relativeIntensity));

      var ppm = PrecursorUtils.ppm2mz(peak.Mz, productIonPPM);
      entry.FromMz = peak.Mz - ppm;
      entry.ToMz = peak.Mz + ppm;

      map.Add(entry);
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
