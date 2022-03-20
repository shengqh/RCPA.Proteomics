using RCPA.Proteomics.Mascot;
using RCPA.Proteomics.Raw;
using RCPA.Proteomics.Spectrum;
using System;
using System.Collections.Generic;
using System.IO;

namespace RCPA.Proteomics.Format
{
  public class Raw2MgfProcessor : AbstractRawConverter
  {
    public static string version = "1.0.1";

    private MultipleRaw2MgfOptions options { get; set; }

    public MascotGenericFormatWriter<Peak> Writer { get; set; }

    private Dictionary<string, StreamWriter> swMap;

    private List<string> outputFiles;

    public Raw2MgfProcessor(MultipleRaw2MgfOptions options) : base(options)
    {
      this.options = options;
      this.Writer = options.GetMGFWriter();
      this.swMap = new Dictionary<string, StreamWriter>();
      this.outputFiles = new List<string>();
    }

    private StreamWriter GetStreamWriter(IRawFile rawReader, string peakMode, int msLevel, string fileName)
    {
      string mgfFile = GetPeakModeFileName(rawReader, peakMode, msLevel, fileName) + ".tmp";

      if (swMap.ContainsKey(mgfFile))
      {
        return swMap[mgfFile];
      }

      outputFiles.Add(mgfFile);

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

    protected override void DoInitialize(IRawFile2 rawReader, string rawFileName)
    {
      this.swMap = new Dictionary<string, StreamWriter>();
      this.outputFiles = new List<string>();
    }

    protected override void DoWritePeakList(IRawFile rawReader, PeakList<Peak> pkl, string rawFileName, List<string> result)
    {
      var sw = GetStreamWriter(rawReader, pkl.ScanMode, pkl.MsLevel, rawFileName);

      Writer.Write(sw, pkl);
    }

    protected override void DoFinalize(bool bReadAgain, IRawFile rawReader, string rawFileName, List<string> result)
    {
      foreach (var sw in swMap.Values)
      {
        sw.Close();
      }

      if (!Progress.IsCancellationPending() && !IsLoopStopped && !bReadAgain)
      {
        if (outputFiles.Count == 1 && (options.GroupByMode || options.GroupByMsLevel))
        {
          var resultFile = GetResultFile(rawReader, rawFileName);
          if (!resultFile.Equals(outputFiles[0]))
          {
            if (File.Exists(resultFile))
            {
              File.Delete(resultFile);
            }

            File.Move(outputFiles[0], resultFile);
          }
          result.Add(resultFile);
        }
        else
        {
          foreach (var mgf in outputFiles)
          {
            if (mgf.EndsWith(".tmp"))
            {
              var res = FileUtils.ChangeExtension(mgf, "");
              if (File.Exists(res))
              {
                File.Delete(res);
              }
              File.Move(mgf, res);
              result.Add(res);
            }
            else
            {
              result.Add(mgf);
            }
          }
        }
      }
      else
      {
        foreach (var m in outputFiles)
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

    protected override bool DoAcceptMsLevel(int msLevel)
    {
      return msLevel > 1;
    }
  }
}