using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using RCPA.Proteomics.Spectrum;
using RCPA.Proteomics.Raw;
using System.IO;
using RCPA.Utils;
using RCPA.Proteomics.Summary;
using System.Threading.Tasks;
using System.Threading;

namespace RCPA.Proteomics.Quantification.Labelfree
{
  public class ChromotographBuilder : AbstractThreadFileProcessor
  {
    public static string ChroKey = "CHRO";

    private Dictionary<string, string> rawFiles;
    private double ppmTolerance;
    private bool force;

    private CancellationToken token = new CancellationToken();

    private List<IGrouping<string, IIdentifiedSpectrum>> spectra;

    public ChromotographBuilder(IEnumerable<IIdentifiedSpectrum> targetPeaks, IEnumerable<string> rawFiles, double ppmTolerance, bool force)
    {
      this.ppmTolerance = ppmTolerance;
      this.force = force;
      this.spectra = targetPeaks.GroupBy(m => m.Query.FileScan.Experimental.ToLower()).ToList();
      this.rawFiles = RawFileFactory.GetExperimentalMap(rawFiles).ToDictionary(m => m.Key.ToLower(), m => m.Value);
    }

    private bool AddPeaks(IRawFile rawReader, int scan, SimplePeakChro chro, bool bIdentified)
    {
      var pkl = rawReader.GetPeakList(scan, chro.Mz - chro.MzTolerance, chro.Mz + chro.MzTolerance);

      if (pkl.Count == 0)
      {
        return false;
      }

      var p = (from o in pkl
               where (o.Charge == chro.Charge) || (o.Charge == 0)
               orderby o.Intensity descending
               select o).FirstOrDefault();

      if (null == p)
      {
        return false;
      }

      var iit = rawReader.GetIonInjectionTime(scan);
      if (iit <= 0)
      {
        iit = 1.0;
      }

      chro.Peaks.Add(new ScanPeak()
      {
        Mz = p.Mz,
        Intensity = p.Intensity,
        Charge = p.Charge,
        Scan = scan,
        RetentionTime = rawReader.ScanToRetentionTime(scan),
        IonInjectionTime = iit,
        Identified = bIdentified
      });

      return true;
    }

    public override IEnumerable<string> Process(string targetDir)
    {
      if (!Directory.Exists(targetDir))
      {
        Directory.CreateDirectory(targetDir);
      }

      var option = new ParallelOptions()
      {
        MaxDegreeOfParallelism = Environment.ProcessorCount,
        CancellationToken = token
      };

      foreach (var entry in this.spectra)
      {
        if (!this.rawFiles.ContainsKey(entry.Key))
        {
          throw new Exception(string.Format("Cannot find raw file for {0}", entry.Key));
        }
      }

      SimplePeakChroPngWriter writer = new SimplePeakChroPngWriter();

      Parallel.ForEach(spectra, option, raw =>
      {
        var peptides = raw.ToList();
        var waitingPeaks = new List<IIdentifiedSpectrum>();

        foreach (var peak in peptides)
        {
          string file = GetTargetFile(targetDir, peak);
          if (force || !File.Exists(file))
          {
            waitingPeaks.Add(peak);
          }
        }

        if (waitingPeaks.Count == 0)
        {
          return;
        }

        using (var rawReader = new CacheRawFile(RawFileFactory.GetRawFileReader(this.rawFiles[raw.Key.ToLower()])))
        {
          int firstScan = rawReader.GetFirstSpectrumNumber();
          int lastScan = rawReader.GetLastSpectrumNumber();

          foreach (var peak in waitingPeaks)
          {
            //if (peak.Query.FileScan.FirstScan != 7628)
            //{
            //  continue;
            //}

            var chro = new SimplePeakChro();
            chro.Mz = peak.ObservedMz;
            chro.Charge = peak.Query.Charge;
            chro.MzTolerance = PrecursorUtils.ppm2mz(chro.Mz, this.ppmTolerance);
            chro.Sequence = peak.Sequence;

            bool bFirst = true;

            for (int scan = peak.Query.FileScan.FirstScan - 1; scan >= firstScan; scan--)
            {
              if (Progress.IsCancellationPending())
              {
                return;
              }

              if (rawReader.GetMsLevel(scan) == 1)
              {
                if (!AddPeaks(rawReader, scan, chro, bFirst))
                {
                  break;
                }

                bFirst = false;
              }
            }

            chro.Peaks.Reverse();

            for (int scan = peak.Query.FileScan.FirstScan + 1; scan <= lastScan; scan++)
            {
              if (Progress.IsCancellationPending())
              {
                return;
              }

              if (rawReader.GetMsLevel(scan) == 1)
              {
                if (!AddPeaks(rawReader, scan, chro, false))
                {
                  break;
                }
              }
            }

            string file = GetTargetFile(targetDir, peak);

            new SimplePeakChroXmlFormat().WriteToFile(file, chro);

            var pngFile = GetTargetImageFile(targetDir, peak);
            writer.WriteToFile(pngFile, chro);
          }
        }
      });

      return new string[] { targetDir };
    }

    public static string GetTargetFile(string targetDir, IIdentifiedSpectrum peak)
    {
      var result = MyConvert.Format(@"{0}\{1}.{2}.chro", targetDir, peak.Query.FileScan.Experimental, peak.Query.FileScan.FirstScan);
      return new FileInfo(result).FullName;
    }

    public static string GetTargetImageFile(string targetDir, IIdentifiedSpectrum peak)
    {
      return GetTargetFile(targetDir, peak) + ".png";
    }
  }
}
