using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using RCPA.Proteomics.Spectrum;
using RCPA.Proteomics.Raw;
using System.IO;
using RCPA.Utils;

namespace RCPA.Proteomics.Quantification.Labelfree
{
  public class ProteinChromotographProcessor : AbstractThreadFileProcessor
  {
    private List<SimplePeakChro> targetPeaks;
    private List<string> rawFiles;
    private IRawFile rawReader;
    private double ppmTolerance;
    private bool force;
    private double minRetentionTime;

    public ProteinChromotographProcessor(IEnumerable<SimplePeakChro> targetPeaks, List<string> rawFiles, IRawFile rawReader, double ppmTolerance, double minRetentionTime, bool force)
    {
      this.targetPeaks = new List<SimplePeakChro>(targetPeaks);
      this.rawFiles = rawFiles;
      this.rawReader = rawReader;
      this.ppmTolerance = ppmTolerance;
      this.minRetentionTime = minRetentionTime;
      this.force = force;

      InitializeTargetPeaks();
    }

    private void InitializeTargetPeaks()
    {
      targetPeaks.ForEach(m =>
      {
        m.MzTolerance = PrecursorUtils.ppm2mz(m.Mz, ppmTolerance);
      });
    }

    public override IEnumerable<string> Process(string targetDir)
    {
      foreach (var raw in rawFiles)
      {
        List<SimplePeakChro> waitingPeaks = new List<SimplePeakChro>();
        foreach (var peak in targetPeaks)
        {
          string file = GetTargetFile(targetDir, raw, peak);
          if (force || !File.Exists(file))
          {
            waitingPeaks.Add(peak);
          }
          else
          {
            var p = new SimplePeakChroXmlFormat().ReadFromFile(file);
            peak.MaxRetentionTime = p.MaxRetentionTime;
            peak.Peaks = p.Peaks;
          }
        }

        if (waitingPeaks.Count == 0)
        {
          continue;
        }

        rawReader.Open(raw);
        try
        {
          int firstScan = rawReader.GetFirstSpectrumNumber();
          int lastScan = rawReader.GetLastSpectrumNumber();

          var lastRt = rawReader.ScanToRetentionTime(lastScan);

          waitingPeaks.ForEach(m =>
          {
            m.Peaks.Clear();
            m.MaxRetentionTime = lastRt;
          });

          Progress.SetMessage("Processing chromotograph extracting...");
          Progress.SetRange(firstScan, lastScan);
          for (int scan = firstScan; scan <= lastScan; scan++)
          {
            if (rawReader.GetMsLevel(scan) != 1)
            {
              continue;
            }
            Progress.SetPosition(scan);

            if (Progress.IsCancellationPending())
            {
              throw new UserTerminatedException();
            }

            PeakList<Peak> pkl = rawReader.GetPeakList(scan);
            double rt = rawReader.ScanToRetentionTime(scan);

            foreach (var peak in waitingPeaks)
            {
              var env = pkl.FindPeak(peak.Mz, peak.MzTolerance);

              Peak findPeak = new Peak(peak.Mz, 0.0, 0);
              if (env.Count > 0)
              {
                if (env.Count == 1)
                {
                  findPeak = env[0];
                }
                else
                {
                  var charge = env.FindCharge(peak.Charge);
                  if (charge.Count > 0)
                  {
                    if (charge.Count == 1)
                    {
                      findPeak = charge[0];
                    }
                    else
                    {
                      findPeak = charge.FindMaxIntensityPeak();
                    }
                  }
                  else
                  {
                    findPeak = env.FindMaxIntensityPeak();
                  }
                }
              }

              peak.Peaks.Add(new ScanPeak()
              {
                Mz = findPeak.Mz,
                Intensity = findPeak.Intensity,
                Scan = scan,
                RetentionTime = rt,
                Charge = findPeak.Charge,
                PPMDistance = PrecursorUtils.mz2ppm(peak.Mz, findPeak.Mz - peak.Mz)
              });
            }
          }

          waitingPeaks.ForEach(m => m.TrimPeaks(minRetentionTime));
        }
        finally
        {
          rawReader.Close();
        }

        Progress.SetMessage("Saving ... ");
        foreach (var peak in waitingPeaks)
        {
          string file = GetTargetFile(targetDir, raw, peak);
          new SimplePeakChroXmlFormat().WriteToFile(file, peak);
        }
        Progress.SetMessage("Finished.");
        Progress.End();
      }

      return new string[] { targetDir };
    }

    public static string GetTargetFile(string targetDir, string raw, SimplePeakChro peak)
    {
      var result = MyConvert.Format(@"{0}\{1}_{2}_{3:0.0000}_{4}.chro", targetDir, FileUtils.ChangeExtension(new FileInfo(raw).Name, ""), peak.Sequence, peak.Mz, peak.Charge);
      return new FileInfo(result).FullName;
    }
  }
}
