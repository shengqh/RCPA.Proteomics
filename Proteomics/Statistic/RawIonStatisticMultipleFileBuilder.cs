using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using RCPA.Proteomics.Raw;
using RCPA.Proteomics.Spectrum;
using System.IO;
using System.Threading.Tasks;
using MathNet.Numerics.Statistics;
using RCPA.Utils;
using RCPA.R;
using RCPA.Gui;

namespace RCPA.Proteomics.Statistic
{
  public class RawIonStatisticMultipleFileBuilder : AbstractThreadFileProcessor
  {
    private const int FULLMS_CHARGE = -1;

    private RawIonStatisticTaskBuilderOptions options;
    private string[] sourceFiles;

    public RawIonStatisticMultipleFileBuilder(RawIonStatisticTaskBuilderOptions options, string[] sourceFiles)
    {
      this.options = options;
      this.sourceFiles = sourceFiles;
    }

    private class SourcePeak
    {
      public SourcePeak(string fileScan, double mz, double intensity)
      {
        this.FileScan = fileScan;
        this.Mz = mz;
        this.Intensity = intensity;
      }

      public string FileScan { get; set; }
      public double Mz { get; set; }
      public double Intensity { get; set; }
    }

    private class PeakEntry
    {
      public PeakEntry()
      {
        this.Intensities = new List<SourcePeak>();
      }

      public Peak Ion { get; set; }
      public double FromMz { get; set; }
      public double ToMz { get; set; }
      public List<SourcePeak> Intensities { get; set; }
      public override string ToString()
      {
        return string.Format("{0:0.0000}", Ion.Mz);
      }
    }

    public override IEnumerable<string> Process(string targetPrefix)
    {
      var result = new List<string>();

      Dictionary<string, Dictionary<int, Dictionary<int, List<PeakEntry>>>> mode_maps = new Dictionary<string, Dictionary<int, Dictionary<int, List<PeakEntry>>>>();
      Dictionary<string, Dictionary<int, Dictionary<int, List<PeakEntry>>>> mode_compmaps = new Dictionary<string, Dictionary<int, Dictionary<int, List<PeakEntry>>>>();
      Dictionary<int, int> scanCounts = new Dictionary<int, int>();
      foreach (var fileName in sourceFiles)
      {
        //var fileName = fileNames.Skip(2).First();
        Console.WriteLine(fileName);
        var name = Path.GetFileNameWithoutExtension(fileName);

        using (var reader = RawFileFactory.GetRawFileReader(fileName))
        {
          var firstScan = reader.GetFirstSpectrumNumber();
          var lastScan = reader.GetLastSpectrumNumber();

          for (int i = firstScan; i <= lastScan; i++)
          {
            Peak precursor;
            string mode;
            if (reader.GetMsLevel(i) == 1)
            {
              precursor = new Peak(0.0, 0.0, FULLMS_CHARGE);
              mode = "hcd";
            }
            else
            {
              precursor = reader.GetPrecursorPeak(i);
              mode  =reader.GetScanMode(i);
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

            pkl.ScanMode = mode;

            Dictionary<int, Dictionary<int, List<PeakEntry>>> maps;
            Dictionary<int, Dictionary<int, List<PeakEntry>>> compmaps;
            if (!mode_maps.TryGetValue(pkl.ScanMode, out maps))
            {
              maps = new Dictionary<int, Dictionary<int, List<PeakEntry>>>();
              mode_maps[pkl.ScanMode] = maps;
            }

            if (!mode_compmaps.TryGetValue(pkl.ScanMode, out compmaps))
            {
              compmaps = new Dictionary<int, Dictionary<int, List<PeakEntry>>>();
              mode_compmaps[pkl.ScanMode] = compmaps;
            }

            if (!maps.ContainsKey(precursor.Charge))
            {
              maps[precursor.Charge] = new Dictionary<int, List<PeakEntry>>();
              compmaps[precursor.Charge] = new Dictionary<int, List<PeakEntry>>();
            }

            var map = maps[precursor.Charge];
            var compmap = compmaps[precursor.Charge];

            var maxPeak = pkl.FindMaxIntensityPeak();
            var minIntensity = maxPeak.Intensity * options.MinRelativeIntensity;

            double precursorMass = precursor.Charge > 0 ? PrecursorUtils.MzToMH(precursor.Mz, precursor.Charge, true) : 0.0;
            var filescan = string.Format("{0}_{1}", name, i);
            foreach (var peak in pkl)
            {
              if (peak.Intensity > minIntensity)
              {
                AddPeak(map, maxPeak.Intensity, filescan, peak);

                if (precursor.Charge > 0)
                {
                  var peakMass = peak.Charge == 0 ? peak.Mz : PrecursorUtils.MzToMH(peak.Mz, peak.Charge, true);
                  peakMass = precursorMass - peakMass;
                  AddPeak(compmap, maxPeak.Intensity, filescan, new Peak(peakMass, peak.Intensity, peak.Charge));
                }
              }
            }
          }
        }
        //break;
      }

      targetPrefix = Path.GetFileName(targetPrefix);

      foreach (var mode in mode_maps.Keys)
      {
        var maps = mode_maps[mode];
        var compmaps = mode_compmaps[mode];

        var keys = (from charge in maps.Keys
                    orderby charge
                    select charge).ToList();

        var resultFile1 = new FileInfo(string.Format("{0}/{1}.{2}.forward.ionfrequency",
          options.TargetDirectory,
          targetPrefix,
          mode)).FullName;

        WriteMap(scanCounts, keys, resultFile1, maps, true);

        result.Add(resultFile1);

        var resultFile2 = new FileInfo(string.Format("{0}/{1}.{2}.backward.ionfrequency",
          options.TargetDirectory,
          targetPrefix,
          mode)).FullName;

        WriteMap(scanCounts, keys, resultFile2, compmaps, false);

        result.Add(resultFile2);
      }

      return result;
    }

    private void WriteMap(Dictionary<int, int> scanCounts, List<int> keys, string filename, Dictionary<int, Dictionary<int, List<PeakEntry>>> curMaps, bool exportIndividualIon)
    {
      foreach (var key in keys)
      {
        var totalCount = scanCounts[key];
        string subfile = string.Empty;

        if (key == FULLMS_CHARGE)
        {
          subfile = filename + ".fullms";
        }
        else if (key == 0)
        {
          subfile = filename + ".unknown";
        }
        else
        {
          subfile = filename + ".ms2charge" + key.ToString();
        }
        var map = curMaps[key];

        foreach (var e in map.Values)
        {
          MergeIons(e);
        }

        var ens = (from e in map.Values from en in e select en).ToList();
        //MergeIons(ens);

        //remove the duplication
        foreach (var ee in ens)
        {
          ee.Intensities = (from intt in ee.Intensities.GroupBy(m => m.FileScan)
                            select (from inttt in intt
                                    orderby inttt.Intensity descending
                                    select inttt).First()).ToList();
        }

        using (var sw2 = new StreamWriter(subfile))
        {
          sw2.WriteLine("Ion\tCount\tFrequency\tMeanIntensity\tSD\tMedianIntensity\tIsHighFrequency");

          var totalentries = (from en in ens
                              orderby en.Intensities.Count descending
                              select en).ToList();

          totalentries.ForEach(m =>
          {
            var ints = (from i in m.Intensities select i.Intensity).ToArray();
            var mean = Statistics.Mean(ints);
            var sd = Statistics.StandardDeviation(ints);
            var median = Statistics.Median(ints);
            var freq = m.Intensities.Count * 1.0 / totalCount;
            var ishigh = median >= options.MinMedianRelativeIntensity && freq >= options.MinFrequency;
            sw2.WriteLine("{0:0.0000}\t{1}\t{2:0.0000}\t{3:0.000}\t{4:0.000}\t{5:0.000}\t{6}", m.Ion.Mz, m.Intensities.Count, freq, mean, sd, median, ishigh);
          });
          sw2.WriteLine();
        }
      }
    }

    private void MergeIons(List<PeakEntry> e)
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

            var gap = PrecursorUtils.ppm2mz(e[j].Ion.Mz, options.ProductIonPPM);
            e[j].FromMz = e[j].Ion.Mz - gap;
            e[j].ToMz = e[j].Ion.Mz + gap;

            e.RemoveAt(i);

            break;
          }
        }
      }
    }

    private void AddPeak(Dictionary<int, List<PeakEntry>> map, double maxPeakIntensity, string fileScan, Peak peak)
    {
      var mz = (int)(Math.Round(peak.Mz));

      List<PeakEntry> entries;
      if (!map.TryGetValue(mz, out entries))
      {
        entries = new List<PeakEntry>();
        map[mz] = entries;
      }

      var relativeIntensity = peak.Intensity / maxPeakIntensity;

      var gap = PrecursorUtils.ppm2mz(peak.Mz, options.ProductIonPPM);
      PeakEntry entry = new PeakEntry()
      {
        Ion = new Peak(peak.Mz, peak.Intensity),
        FromMz = peak.Mz - gap,
        ToMz = peak.Mz + gap
      };
      entry.Intensities.Add(new SourcePeak(fileScan, peak.Mz, relativeIntensity));
      entries.Add(entry);
    }
  }
}
