using RCPA.Proteomics.Raw;
using RCPA.Proteomics.Spectrum;
using System;
using System.Collections.Generic;
using System.IO;

namespace RCPA.Proteomics.Format
{
  public class Raw2MSnProcessor : AbstractRawConverter
  {
    private MultipleRaw2MSnProcessorOptions options;
    private Dictionary<string, StreamWriter> swMap;
    private List<string> outputFiles;

    private StreamWriter sw1Index;

    public Raw2MSnProcessor(MultipleRaw2MSnProcessorOptions options) : base(options)
    {
      this.options = options;
    }

    protected override string GetPeakModeFileName(IRawFile rawReader, string peakMode, int msLevel, string fileName)
    {
      var result = GetResultFile(rawReader, fileName);

      if (msLevel > 1 && options.GroupByMode)
      {
        result = FileUtils.ChangeExtension(result, peakMode + "." + options.Extension);
      }

      result = string.Format("{0}{1}", result, msLevel);

      return result;
    }

    private StreamWriter GetStreamWriter(IRawFile rawReader, string peakMode, int msLevel, string fileName)
    {
      string resultFile = GetPeakModeFileName(rawReader, peakMode, msLevel, fileName);
      string tmpFile = resultFile + ".tmp";

      if (swMap.ContainsKey(tmpFile))
      {
        return swMap[tmpFile];
      }

      outputFiles.Add(tmpFile);

      var result = new StreamWriter(tmpFile);
      swMap[tmpFile] = result;

      if (1 == msLevel)
      {
        int firstScan = rawReader.GetFirstSpectrumNumber();
        int lastScan = rawReader.GetLastSpectrumNumber();

        result.Write("H\tCreationDate\t{0:m/dd/yyyy HH:mm:ss}\n", DateTime.Now);
        result.Write("H\tExtractor\tProteomicsTools\n");
        result.Write("H\tExtractorVersion\t4.1.9\n");
        result.Write("H\tComments\tProteomicsTools written by Quanhu Sheng, 2008-2018\n");
        result.Write("H\tExtractorOptions\tMS1\n");
        result.Write("H\tAcquisitionMethod\tData-Dependent\n");
        result.Write("H\tDataType\tCentriod\n");
        result.Write("H\tScanType\tMS1\n");
        result.Write("H\tFirstScan\t{0}\n", firstScan);
        result.Write("H\tLastScan\t{0}\n", lastScan);

        var ms1IndexFilename = resultFile + ".index";
        sw1Index = new StreamWriter(ms1IndexFilename);
        swMap[ms1IndexFilename] = sw1Index;
      }

      return result;
    }

    protected override void DoInitialize(IRawFile2 rawReader, string fileName)
    {
      this.swMap = new Dictionary<string, StreamWriter>();
      this.outputFiles = new List<string>();

      int firstScan = rawReader.GetFirstSpectrumNumber();
      int lastScan = rawReader.GetLastSpectrumNumber();
      Progress.SetRange(firstScan, lastScan);
    }

    protected override bool DoAcceptMsLevel(int msLevel)
    {
      return true;
    }

    protected override void DoWritePeakList(IRawFile rawReader, PeakList<Peak> pkl, string rawFileName, List<string> result)
    {
      var sw = GetStreamWriter(rawReader, pkl.ScanMode, pkl.MsLevel, rawFileName);
      var scan = pkl.ScanTimes[0].Scan;
      if (pkl.MsLevel == 1)
      {
        sw.Flush();

        sw1Index.Write("{0}\t{1}\n", scan, sw.BaseStream.Position);

        sw.Write("S\t{0}\t{0}\n", scan);
        sw.Write("I\tRetTime\t{0:0.####}\n", pkl.ScanTimes[0].RetentionTime);

        foreach (Peak p in pkl)
        {
          sw.Write("{0:0.#####} {1:0.#} {2}\n", p.Mz, p.Intensity, p.Charge);
        }
      }
      else
      {
        sw.WriteLine("S\t{0}\t{1}\t{2}", scan, scan, pkl.PrecursorMZ);
        int[] charges = 0 != pkl.PrecursorCharge ? new[] { pkl.PrecursorCharge } : new[] { 2, 3 };
        foreach (var charge in charges)
        {
          sw.WriteLine("Z\t{0}\t{1:0.#####}", charge, PrecursorUtils.MzToMH(pkl.PrecursorMZ, charge, true));
        }
        foreach (var peak in pkl)
        {
          sw.WriteLine("{0:0.#####}\t{1:0.#}", peak.Mz, peak.Intensity);
        }
      }
    }

    protected override void DoFinalize(bool bReadAgain, IRawFile rawReader, string rawFileName, List<string> result)
    {
      foreach (var sw in swMap.Values)
      {
        sw.Close();
      }

      if (!Progress.IsCancellationPending() && !IsLoopStopped && !bReadAgain)
      {
        foreach (var outputFile in outputFiles)
        {
          if (outputFile.EndsWith(".tmp"))
          {
            var res = FileUtils.ChangeExtension(outputFile, "");
            if (File.Exists(res))
            {
              File.Delete(res);
            }
            File.Move(outputFile, res);
            result.Add(res);
          }
          else
          {
            result.Add(outputFile);
          }
        }
      }
      else
      {
        foreach (var m in swMap.Keys)
        {
          try
          {
            File.Delete(m);
          }
          catch (Exception)
          {
          }
        }
      }
    }
  }
}