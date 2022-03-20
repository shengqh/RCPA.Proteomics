﻿using MathNet.Numerics.Statistics;
using RCPA.Proteomics.Raw;
using RCPA.Proteomics.Spectrum;
using RCPA.R;
using RCPA.Utils;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace RCPA.Proteomics.Statistic
{
  public class PrecursorOffsetStatisticTaskBuilder : AbstractParallelTaskFileProcessor
  {
    private const int FULLMS_CHARGE = -1;

    private RawIonStatisticTaskBuilderOptions options;

    public PrecursorOffsetStatisticTaskBuilder(RawIonStatisticTaskBuilderOptions options)
    {
      this.options = options;
    }

    private class SourcePeak
    {
      public SourcePeak(int scan, double mz, double intensity)
      {
        this.Scan = scan;
        this.Mz = mz;
        this.Intensity = intensity;
      }

      public int Scan { get; set; }
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

    public override IEnumerable<string> Process(string fileName)
    {
      var result = new List<string>();

      Dictionary<string, Dictionary<int, Dictionary<int, List<PeakEntry>>>> mode_maps = new Dictionary<string, Dictionary<int, Dictionary<int, List<PeakEntry>>>>();
      Dictionary<int, int> scanCounts = new Dictionary<int, int>();
      using (var reader = RawFileFactory.GetRawFileReader(fileName))
      {
        var firstScan = reader.GetFirstSpectrumNumber();
        var lastScan = reader.GetLastSpectrumNumber();

        for (int i = firstScan; i <= lastScan; i++)
        {
          if (reader.GetMsLevel(i) != 2)
          {
            continue;
          }

          var precursor = reader.GetPrecursorPeak(i);

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

          pkl.ScanMode = reader.GetScanMode(i);

          Dictionary<int, Dictionary<int, List<PeakEntry>>> maps;
          if (!mode_maps.TryGetValue(pkl.ScanMode, out maps))
          {
            maps = new Dictionary<int, Dictionary<int, List<PeakEntry>>>();
            mode_maps[pkl.ScanMode] = maps;
          }

          if (Progress.IsCancellationPending() || IsLoopStopped)
          {
            return null;
          }

          if (!maps.ContainsKey(precursor.Charge))
          {
            maps[precursor.Charge] = new Dictionary<int, List<PeakEntry>>();
          }

          var map = maps[precursor.Charge];

          var maxPeak = pkl.FindMaxIntensityPeak();
          var minIntensity = maxPeak.Intensity * options.MinRelativeIntensity;

          foreach (var peak in pkl)
          {
            if (peak.Intensity > minIntensity)
            {
              var peakMass = peak.Mz - precursor.Mz;
              var mztolerance = PrecursorUtils.ppm2mz(peak.Mz, options.ProductIonPPM);
              AddPeak(map, maxPeak.Intensity, i, new Peak(peakMass, peak.Intensity, peak.Charge), mztolerance);
            }
          }
        }
      }

      foreach (var mode in mode_maps.Keys)
      {
        var maps = mode_maps[mode];

        var keys = (from charge in maps.Keys
                    orderby charge
                    select charge).ToList();

        var resultFile1 = new FileInfo(string.Format("{0}/{1}.{2}.precursor_delta.ionfrequency",
          options.TargetDirectory,
          new FileInfo(fileName).Name,
          mode)).FullName;

        WriteMap(scanCounts, keys, resultFile1, maps, true);

        result.Add(resultFile1);
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
        var map2 = ens.GroupBy(m => (int)Math.Round(m.Ion.Mz + 0.5)).ToDictionary(m => m.Key, m => m.ToList());
        foreach (var e in map2.Values)
        {
          MergeIons(e);
        }

        ens = (from e in map2.Values from en in e select en).ToList();

        //remove the duplication
        foreach (var ee in ens)
        {
          ee.Intensities = (from intt in ee.Intensities.GroupBy(m => m.Scan)
                            select (from inttt in intt
                                    orderby inttt.Intensity descending
                                    select inttt).First()).ToList();
        }

        using (var sw2 = new StreamWriter(subfile))
        {
          sw2.WriteLine("Ion\tCount\tFrequency\tMeanIntensity\tSD\tMedianIntensity");

          var totalentries = (from en in ens
                              orderby en.Intensities.Count descending
                              select en).ToList();

          totalentries.ForEach(m =>
          {
            var ints = (from i in m.Intensities select i.Intensity).ToArray();
            var mean = Statistics.Mean(ints);
            var sd = Statistics.StandardDeviation(ints);
            var median = Statistics.Median(ints);

            sw2.WriteLine("{0:0.0000}\t{1}\t{2:0.0000}\t{3:0.000}\t{4:0.000}\t{5:0.000}", m.Ion.Mz, m.Intensities.Count, m.Intensities.Count * 1.0 / totalCount, mean, sd, median);
          });
          sw2.WriteLine();
        }

        var options = new RTemplateProcessorOptions();

        options.InputFile = subfile;
        options.OutputFile = subfile + ".sig.tsv";
        options.RExecute = ExternalProgramConfig.GetExternalProgram("R");
        options.RTemplate = FileUtils.GetTemplateDir() + "/DetectSignificantIon.r";
        options.Parameters.Add("minfreq<-0.01");
        options.Parameters.Add("probability<-0.95");
        options.Parameters.Add("minMedianIntensity<-0.05");

        new RTemplateProcessor(options)
        {
          Progress = this.Progress
        }.Process();
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

    private void AddPeak(Dictionary<int, List<PeakEntry>> map, double maxPeakIntensity, int scan, Peak peak, double mzTolerance)
    {
      var mz = (int)(Math.Round(peak.Mz));

      List<PeakEntry> entries;
      if (!map.TryGetValue(mz, out entries))
      {
        entries = new List<PeakEntry>();
        map[mz] = entries;
      }

      var relativeIntensity = peak.Intensity / maxPeakIntensity;

      PeakEntry entry = new PeakEntry()
      {
        Ion = new Peak(peak.Mz, peak.Intensity),
        FromMz = peak.Mz - mzTolerance,
        ToMz = peak.Mz + mzTolerance
      };
      entry.Intensities.Add(new SourcePeak(scan, peak.Mz, relativeIntensity));
      entries.Add(entry);
    }
  }
}
