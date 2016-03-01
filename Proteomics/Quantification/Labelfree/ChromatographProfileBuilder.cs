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
using RCPA.Proteomics.Mascot;
using System.Xml.Linq;
using RCPA.R;

namespace RCPA.Proteomics.Quantification.Labelfree
{
  public class ChromatographProfileBuilder : AbstractThreadProcessor
  {
    private class FullMS
    {
      public int Scan { get; set; }
      public double RetentionTime { get; set; }
      public PeakList<Peak> Peaks { get; set; }
    }

    private ChromatographProfileBuilderOptions options;

    private static string BoundaryR = Path.Combine(FileUtils.GetTemplateDir().FullName, "ProfileChroBoundary.r");

    public ChromatographProfileBuilder(ChromatographProfileBuilderOptions options)
    {
      this.options = options;
    }

    public override IEnumerable<string> Process()
    {
      var format = new MascotPeptideTextFormat();
      var spectra = format.ReadFromFile(options.InputFile);
      var peptideMap = spectra.ToGroupDictionary(m => m.Query.FileScan.Experimental.ToLower());
      var rawfiles = Directory.GetFiles(options.RawDirectory, "*.raw", SearchOption.AllDirectories).ToDictionary(m => Path.GetFileNameWithoutExtension(m).ToLower());
      var rententionWindow = options.RetentionTimeWindow;

      var missed = peptideMap.Keys.Except(rawfiles.Keys).ToArray();
      if (missed.Length > 0)
      {
        throw new Exception(string.Format("Cannot find raw file of {0} in directory {1}", missed.Merge("/"), options.RawDirectory));
      }

      string targetDir = GetTargetDirectory();
      if (!Directory.Exists(targetDir))
      {
        Directory.CreateDirectory(targetDir);
      }

      string subDir = GetTargetDirectory() + "/sub";
      if (!Directory.Exists(subDir))
      {
        Directory.CreateDirectory(subDir);
      }

      var option = new ParallelOptions()
      {
        MaxDegreeOfParallelism = Math.Min(Environment.ProcessorCount, peptideMap.Count),
      };

      Parallel.ForEach(peptideMap, option, raw =>
      {
        //foreach (var raw in peptideMap)
        //{
        var peptides = raw.Value;

        var waitingPeaks = new List<ChromatographProfile>();
        foreach (var peptide in peptides)
        {
          string file = GetTargetFile(peptide);
          var chro = new ChromatographProfile()
          {
            Experimental = peptide.Query.FileScan.Experimental,
            IdentifiedScan = peptide.Query.FileScan.FirstScan,
            ObservedMz = peptide.GetPrecursorMz(),
            TheoreticalMz = peptide.GetTheoreticalMz(),
            Charge = peptide.Query.Charge,
            Sequence = peptide.Peptide.PureSequence,
            FileName = Path.GetFileName(file)
          };
          chro.InitializeIsotopicIons(options.MzTolerancePPM);
          waitingPeaks.Add(chro);
        }

        if (waitingPeaks.Count == 0)
        {
          //continue;
          return;
        }

        Dictionary<string, List<ChromatographProfile>> resultMap = new Dictionary<string, List<ChromatographProfile>>();

        List<FullMS> fullMSList = new List<FullMS>();
        Progress.SetMessage("Reading full ms list from " + rawfiles[raw.Key] + "...");
        using (var rawReader = new CacheRawFile(RawFileFactory.GetRawFileReader(rawfiles[raw.Key])))
        {
          var firstScan = rawReader.GetFirstSpectrumNumber();
          var lastScan = rawReader.GetLastSpectrumNumber();
          for (int scan = firstScan; scan <= lastScan; scan++)
          {
            var mslevel = rawReader.GetMsLevel(scan);
            if (mslevel == 1)
            {
              fullMSList.Add(new FullMS()
              {
                Scan = scan,
                RetentionTime = rawReader.ScanToRetentionTime(scan),
                Peaks = null
              });
            }
          }

          var chroGroups = waitingPeaks.GroupBy(chro => string.Format("{0}_{1:0.0000}", chro.Sequence, chro.TheoreticalMz));
          foreach (var chroGroup in chroGroups)
          {
            List<ChromatographProfile> profileChros = new List<ChromatographProfile>();
            foreach (var chro in chroGroup.OrderBy(m => m.IdentifiedScan))
            {
              var masterScanIndex = 0;
              for (int i = 1; i < fullMSList.Count; i++)
              {
                if (chro.IdentifiedScan < fullMSList[i].Scan)
                {
                  break;
                }
                masterScanIndex = i;
              }
              var masterScan = fullMSList[masterScanIndex].Scan;
              var masterRetentionTime = fullMSList[masterScanIndex].RetentionTime;

              bool bExist = false;
              foreach (var profileChro in profileChros)
              {
                foreach (var pkl in profileChro.Profiles)
                {
                  if (pkl.Scan == fullMSList[masterScanIndex].Scan)
                  {
                    pkl.Identified = true;
                    bExist = true;
                    break;
                  }
                }

                if (bExist)
                {
                  break;
                }
              }

              if (bExist)
              {
                continue;
              }

              Progress.SetMessage("Processing {0} : {1:0.#####} : {2} : {3}", chro.Sequence, chro.ObservedMz, chro.IdentifiedScan, Path.GetFileName(chro.FileName));

              for (int scanIndex = masterScanIndex; scanIndex >= 0; scanIndex--)
              {
                if (Progress.IsCancellationPending())
                {
                  throw new UserTerminatedException();
                }

                var curRetentionTime = fullMSList[scanIndex].RetentionTime;
                if (masterRetentionTime - curRetentionTime > rententionWindow)
                {
                  break;
                }

                if (!AddEnvelope(chro, rawReader, fullMSList, scanIndex))
                {
                  break;
                }

                if (scanIndex == masterScanIndex)
                {
                  chro.Profiles.Last().Identified = true;
                }
              }
              chro.Profiles.Reverse();

              for (int scanIndex = masterScanIndex + 1; scanIndex < fullMSList.Count; scanIndex++)
              {
                if (Progress.IsCancellationPending())
                {
                  throw new UserTerminatedException();
                }

                var curRetentionTime = fullMSList[scanIndex].RetentionTime;
                if (curRetentionTime - masterRetentionTime > rententionWindow)
                {
                  break;
                }

                if (!AddEnvelope(chro, rawReader, fullMSList, scanIndex))
                {
                  break;
                }
              }

              profileChros.Add(chro);
            }

            profileChros.RemoveAll(l => l.Profiles.Count < options.MinimumScanCount);
            profileChros.Sort((m1, m2) => m2.Profiles.Count.CompareTo(m1.Profiles.Count));

            bool bMain = true;
            foreach (var chro in profileChros)
            {
              var filename = bMain ? Path.Combine(targetDir, chro.FileName) : Path.Combine(subDir, Path.ChangeExtension(chro.FileName, ".sub" + Path.GetExtension(chro.FileName)));
              bMain = false;

              new ChromatographProfileTextWriter().WriteToFile(filename, chro);
              new ChromatographProfileXmlFormat().WriteToFile(filename + ".xml", chro);
            }
          }
        }
      }
      );

      Progress.SetMessage("Finding boundaries ...");
      var boundaryOptions = new RTemplateProcessorOptions()
      {
        InputFile = targetDir,
        OutputFile = options.OutputFile,
        RTemplate = BoundaryR,
        RExecute = SystemUtils.GetRExecuteLocation()
      };
      new RTemplateProcessor(boundaryOptions).Process();

      return new string[] { options.OutputFile };
    }

    public string GetTargetDirectory()
    {
      return Path.GetDirectoryName(options.OutputFile) + "/chros";
    }

    private bool AddEnvelope(ChromatographProfile chro, CacheRawFile rawReader, List<FullMS> fullMSList, int scanIndex)
    {
      PeakList<Peak> fullMS = ReadFullMS(rawReader, fullMSList, scanIndex);
      return AddEnvelope(chro, fullMS);
    }

    private static PeakList<Peak> ReadFullMS(CacheRawFile rawReader, List<FullMS> fullMSList, int scanIndex)
    {
      if (fullMSList[scanIndex].Peaks == null)
      {
        fullMSList[scanIndex].Peaks = rawReader.GetPeakList(fullMSList[scanIndex].Scan);
      }

      return fullMSList[scanIndex].Peaks;
    }

    private bool AddEnvelope(ChromatographProfile chro, PeakList<Peak> curPeaks)
    {
      ChromatographProfileScan envelope = FindEnvelope(curPeaks, chro.IsotopicIons, options.MzTolerancePPM);
      if (envelope.Count < options.ProfileLength)
      {
        return false;
      }

      if (envelope.CalculateProfileCorrelation(chro.IsotopicIons) < options.MinimumCorrelation)
      {
        return false;
      }

      chro.Profiles.Add(envelope);
      return true;
    }

    private ChromatographProfileScan FindEnvelope(PeakList<Peak> curPeaks, IsotopicIon[] isotopicIons, double mzTolerancePPM)
    {
      var result = new ChromatographProfileScan();
      result.Scan = curPeaks.FirstScan;
      result.RetentionTime = curPeaks.ScanTimes[0].RetentionTime;
      int peakIndex = 0;
      foreach (var peak in isotopicIons)
      {
        Peak findPeak = null;
        while (peakIndex < curPeaks.Count)
        {
          var curPeak = curPeaks[peakIndex];
          if (curPeak.Mz < peak.MinimumMzWithinTolerance)
          {
            peakIndex++;
            continue;
          }

          if (curPeak.Mz > peak.MaximumMzWithinTolerance)
          {
            break;
          }

          if (findPeak == null || findPeak.Intensity < curPeak.Intensity)
          {
            findPeak = curPeak;
          }

          peakIndex++;
        }

        if (findPeak == null)
        {
          break;
        }
        else
        {
          result.Add(new ChromatographProfileScanPeak()
          {
            Mz = findPeak.Mz,
            Intensity = findPeak.Intensity,
            Charge = findPeak.Charge,
            Noise = findPeak.Noise,
            PPMDistance = PrecursorUtils.mz2ppm(findPeak.Mz, findPeak.Mz - peak.Mz)
          });
        }
      }

      return result;
    }

    public string GetTargetFile(IIdentifiedSpectrum peptide)
    {
      var result = MyConvert.Format(@"{0}\{1}_{2}_{3}_{4}.chro.tsv",
        GetTargetDirectory(),
        peptide.Query.FileScan.Experimental.Replace(" ", "_"),
        peptide.Peptide.PureSequence,
        Math.Round(peptide.GetTheoreticalMz()),
        peptide.Query.FileScan.FirstScan);
      return new FileInfo(result).FullName;
    }

    public string GetTargetImageFile(IIdentifiedSpectrum peptide)
    {
      return GetTargetFile(peptide) + ".png";
    }
  }
}
