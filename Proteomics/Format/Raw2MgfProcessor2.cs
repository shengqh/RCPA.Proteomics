using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using RCPA.Proteomics.Spectrum;
using RCPA.Proteomics.Raw;
using RCPA.Utils;
using RCPA.Proteomics.Mascot;
using RCPA.Proteomics.Statistic;
using RCPA.Proteomics.Processor;

namespace RCPA.Proteomics.Format
{
  public class Raw2MgfProcessor2 : AbstractRawTandemSpectrumConverter
  {
    public static string version = "1.0.1";

    public MascotGenericFormatWriter<Peak> Writer { get; set; }

    private Dictionary<string, StreamWriter> swMap;

    private List<string> mgfFiles;

    private string lastScanMode;

    private StreamWriter lastWriter = null;

    public bool GroupByScanMode { get; set; }

    public bool GroupByMsLevel { get; set; }

    public Raw2MgfProcessor2()
    {
      this.swMap = new Dictionary<string, StreamWriter>();
      this.mgfFiles = new List<string>();
    }

    public string GetResultFile(IRawFile rawReader, string rawFileName)
    {
      return new FileInfo(this.TargetDirectory + "\\" + rawReader.GetFileNameWithoutExtension(rawFileName) + ".mgf").FullName;
    }

    private StreamWriter GetStreamWriter(IRawFile rawReader, string peakMode, int msLevel, string fileName)
    {
      string mgfFile = GetPeakModeFileName(rawReader, peakMode, msLevel, fileName);

      if (swMap.ContainsKey(mgfFile))
      {
        return swMap[mgfFile];
      }

      mgfFiles.Add(mgfFile);

      var result = new StreamWriter(mgfFile);
      swMap[mgfFile] = result;

      foreach (var comment in Writer.Comments)
      {
        result.WriteLine("###" + comment);
      }

      foreach (var comment in this.PeakListProcessor.ToString().Split('\n'))
      {
        result.WriteLine("###" + comment);
      }

      return result;
    }

    private string GetPeakModeFileName(IRawFile rawReader, string peakMode, int msLevel, string fileName)
    {
      var resultFile = GetResultFile(rawReader, fileName);

      string mgfFile;
      if (GroupByScanMode)
      {
        mgfFile = FileUtils.ChangeExtension(resultFile, peakMode + ".mgf");
      }
      else
      {
        mgfFile = FileUtils.ChangeExtension(resultFile, ".mgf");
      }

      if (GroupByMsLevel && (msLevel != 2))
      {
        mgfFile = FileUtils.ChangeExtension(mgfFile, string.Format("ms{0}.mgf", msLevel));
      }

      return mgfFile;
    }

    protected override void DoInitialize(string rawFileName)
    {
      this.lastScanMode = string.Empty;
      this.lastWriter = null;
    }

    protected override void DoWritePeakList(IRawFile rawReader, PeakList<Peak> pkl, string rawFileName, List<string> result)
    {
      if (pkl.ScanMode != lastScanMode)
      {
        lastWriter = GetStreamWriter(rawReader, pkl.ScanMode, pkl.MsLevel, rawFileName);

        lastScanMode = pkl.ScanMode;
      }

      Writer.Write(lastWriter, pkl);
    }

    protected override void DoFinalize(bool bReadAgain, IRawFile rawReader, string rawFileName, List<string> result)
    {
      foreach (var sw in swMap.Values)
      {
        sw.Close();
      }

      if (!Progress.IsCancellationPending() && !IsLoopStopped && !bReadAgain)
      {
        if (mgfFiles.Count == 1 && GroupByScanMode)
        {
          var resultFile = GetResultFile(rawReader, rawFileName);
          if (File.Exists(resultFile))
          {
            File.Delete(resultFile);
          }

          File.Move(mgfFiles[0], resultFile);
          result.Add(resultFile);
        }
        else
        {
          result.AddRange(mgfFiles);
        }
      }
      else
      {
        foreach (var m in mgfFiles)
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