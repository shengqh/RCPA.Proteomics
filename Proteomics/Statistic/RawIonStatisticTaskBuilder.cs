using MathNet.Numerics.Statistics;
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
  public class RawIonStatisticTaskBuilder : AbstractParallelTaskFileProcessor
  {
    private const int FULLMS_CHARGE = -1;

    private RawIonStatisticTaskBuilderOptions options;

    public RawIonStatisticTaskBuilder(RawIonStatisticTaskBuilderOptions options)
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
      Dictionary<string, Dictionary<int, Dictionary<int, List<PeakEntry>>>> mode_compmaps = new Dictionary<string, Dictionary<int, Dictionary<int, List<PeakEntry>>>>();
      Dictionary<int, int> scanCounts = new Dictionary<int, int>();
      using (var reader = RawFileFactory.GetRawFileReader(fileName))
      {
        var firstScan = reader.GetFirstSpectrumNumber();
        var lastScan = reader.GetLastSpectrumNumber();
        //var firstScan = 17047;
        //var lastScan = 17047;

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

          pkl.ScanMode = reader.GetScanMode(i);

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

          //if (i == 17047)
          //{
          //  pkl.ForEach(m => Console.WriteLine("{0}\t{1}", m.Mz, m.Intensity));
          //  return null;
          //}

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
          var minIntensity = maxPeak.Intensity * options.MinRelativeIntensity;

          double precursorMass = precursor.Charge > 0 ? PrecursorUtils.MzToMH(precursor.Mz, precursor.Charge, true) : 0.0;
          foreach (var peak in pkl)
          {
            if (peak.Intensity > minIntensity)
            {
              AddPeak(map, maxPeak.Intensity, i, peak);

              if (precursor.Charge > 0)
              {
                var peakMass = peak.Charge == 0 ? peak.Mz : PrecursorUtils.MzToMH(peak.Mz, peak.Charge, true);
                peakMass = precursorMass - peakMass;
                AddPeak(compmap, maxPeak.Intensity, i, new Peak(peakMass, peak.Intensity, peak.Charge));
              }
            }
          }
        }
      }

      foreach (var mode in mode_maps.Keys)
      {
        var maps = mode_maps[mode];
        var compmaps = mode_compmaps[mode];

        var keys = (from charge in maps.Keys
                    orderby charge
                    select charge).ToList();

        var resultFile1 = new FileInfo(string.Format("{0}/{1}.{2}.forward.ionfrequency",
          options.TargetDirectory,
          new FileInfo(fileName).Name,
          mode)).FullName;

        WriteMap(scanCounts, keys, resultFile1, maps, true);

        result.Add(resultFile1);

        var resultFile2 = new FileInfo(string.Format("{0}/{1}.{2}.backward.ionfrequency",
          options.TargetDirectory,
          new FileInfo(fileName).Name,
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
        MergeIons(ens);

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


    private void WriteMap_old(Dictionary<int, int> scanCounts, List<int> keys, string filename, Dictionary<int, Dictionary<int, List<PeakEntry>>> curMaps, bool exportIndividualIon)
    {
      using (var sw = new StreamWriter(filename))
      {
        foreach (var key in keys)
        {
          var totalCount = scanCounts[key];
          string subfile = string.Empty;

          //if (key != 2)
          //{
          //  continue;
          //}

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
          var map = curMaps[key];

          sw.WriteLine("Ion\tCount\tFrequency\tMeanIntensity\tSD\tMedianIntensity");
          foreach (var e in map.Values)
          {
            MergeIons(e);
          }

          var ens = (from e in map.Values from en in e select en).ToList();
          //var ens = (from e in map.Values from en in e orderby en.Ion.Mz select en).ToList();
          //ens.ForEach(m => Console.WriteLine("{0}\t{1}", m.Ion.Mz, m.Intensities.Count));
          MergeIons(ens);

          ////remove the ions with less observations
          //var totalscan = (from en in ens
          //                 from intensity in en.Intensities
          //                 select intensity.Scan).Distinct().Count();
          //var minFrequencyCount = Math.Floor(totalscan * options.MinFrequency);
          //ens.RemoveAll(en => en.Intensities.Count < minFrequencyCount);

          //remove the duplication
          foreach (var ee in ens)
          {
            ee.Intensities = (from intt in ee.Intensities.GroupBy(m => m.Scan)
                              select (from inttt in intt
                                      orderby inttt.Intensity descending
                                      select inttt).First()).ToList();
          }

          //var ensgroup = ens.GroupBy(m => Math.Round(m.Ion.Mz)).OrderBy(m => m.Key).ToList();

          //ens = (from g in ensgroup
          //       select (from e in g
          //               orderby e.Intensities.Count descending
          //               select e).First()).ToList();

          if (exportIndividualIon)
          {
            using (var sw2 = new StreamWriter(subfile))
            {
              ens.Sort((m1, m2) => m1.Ion.Mz.CompareTo(m2.Ion.Mz));

              sw2.WriteLine("mz\tscan\tintensity");
              foreach (var ion in ens)
              {
                var grp = ion.Intensities.GroupBy(m => m.Scan).OrderBy(m => m.Key).ToList();
                foreach (var value in grp)
                {
                  if (value.Count() > 1)
                  {
                    Console.WriteLine("Multiple entry : {0}, {1}", ion.Ion.Mz, value.Key);
                  }

                  sw2.WriteLine("{0:0.00000}\t{1}\t{2:0.000}", ion.Ion.Mz, value.Key, value.First().Intensity);
                }
              }
            }

            //string outputfile;
            //var rfile = options.PrepareRFile(subfile, out outputfile);

            //new RProcessor(options.GetRCommand(), rfile, outputfile).Process();
          }

          var totalentries = (from en in ens
                              orderby en.Intensities.Count descending
                              select en).ToList();

          totalentries.ForEach(m =>
          {
            var ints = (from i in m.Intensities select i.Intensity).ToArray();
            var mean = Statistics.Mean(ints);
            var sd = Statistics.StandardDeviation(ints);
            var median = Statistics.Median(ints);

            sw.WriteLine("{0:0.0000}\t{1}\t{2:0.0000}\t{3:0.000}\t{4:0.000}\t{5:0.000}", m.Ion.Mz, m.Intensities.Count, m.Intensities.Count * 1.0 / totalCount, mean, sd, median);
          });
          sw.WriteLine();
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

    private void AddPeak(Dictionary<int, List<PeakEntry>> map, double maxPeakIntensity, int scan, Peak peak)
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
      entry.Intensities.Add(new SourcePeak(scan, peak.Mz, relativeIntensity));
      entries.Add(entry);
    }
  }
}
