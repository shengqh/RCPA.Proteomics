using RCPA.Proteomics.Raw;
using RCPA.Proteomics.Spectrum;
using RCPA.Utils;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.ExceptionServices;

namespace RCPA.Proteomics.Format
{
  public abstract class AbstractRawConverter : AbstractParallelTaskFileProcessor
  {
    public IProcessor<PeakList<Peak>> PeakListProcessor { get; set; }

    private AbstractRawConverterOptions options;

    public AbstractRawConverter(AbstractRawConverterOptions options)
    {
      this.options = options;
    }

    protected string GetIgnoreScanFile(string rawFilename)
    {
      return rawFilename + ".ignorescan";
    }

    public string GetResultFile(IRawFile rawReader, string rawFileName)
    {
      return new FileInfo(options.TargetDirectory + "/" + rawReader.GetFileNameWithoutExtension(rawFileName) + "." + options.Extension).FullName;
    }

    protected virtual string GetPeakModeFileName(IRawFile rawReader, string peakMode, int msLevel, string fileName)
    {
      var resultFile = GetResultFile(rawReader, fileName);

      string result;
      if (options.GroupByMode)
      {
        result = FileUtils.ChangeExtension(resultFile, peakMode + "." + options.Extension);
      }
      else
      {
        result = FileUtils.ChangeExtension(resultFile, "." + options.Extension);
      }

      if (options.GroupByMsLevel && (msLevel != 2))
      {
        result = FileUtils.ChangeExtension(result, string.Format("ms{0}.{1}", msLevel, options.Extension));
      }

      return result;
    }

    [HandleProcessCorruptedStateExceptions]
    protected virtual IEnumerable<string> DoProcess(string fileName, List<int> ignoreScans, int lastScan, bool bContinue)
    {
      var result = new List<string>();

      bool bReadAgain = false;

      using (var rawReader = RawFileFactory.GetRawFileReader(fileName))
      {
        try
        {
          if (!bContinue)
          {
            DoInitialize(rawReader, fileName);
          }

          string experimental = rawReader.GetFileNameWithoutExtension(fileName);

          SetMessage("Processing " + fileName + " ...");

          int firstSpectrumNumber = rawReader.GetFirstSpectrumNumber();
          int lastSpectrumNumber = rawReader.GetLastSpectrumNumber();
          //int firstSpectrumNumber = 79800;

          SetRange(firstSpectrumNumber, lastSpectrumNumber);

          lastScan = Math.Max(lastScan, firstSpectrumNumber);
          for (int scan = lastScan; scan <= lastSpectrumNumber; scan++)
          {
            lastScan = scan;

            if (Progress.IsCancellationPending())
            {
              throw new UserTerminatedException();
            }

            if (IsLoopStopped)
            {
              return result;
            }

            if (ignoreScans.Contains(scan))
            {
              continue;
            }

            SetPosition(scan);

            int msLevel = rawReader.GetMsLevel(scan);

            if (!DoAcceptMsLevel(msLevel))
            {
              continue;
            }

            //Console.WriteLine("Reading scan {0}", scan);

            PeakList<Peak> pkl;
            try
            {
              PrecursorPeak precursor = null;
              if (msLevel > 1)
              {
                precursor = new PrecursorPeak(rawReader.GetPrecursorPeak(scan));
              }

              pkl = rawReader.GetPeakList(scan);
              pkl.MsLevel = msLevel;
              pkl.Experimental = experimental;
              pkl.ScanMode = rawReader.GetScanMode(scan);
              pkl.Precursor = precursor;

              if (msLevel > 1 && precursor.Charge == 0)
              {
                precursor.Charge = PrecursorUtils.GuessPrecursorCharge(pkl, pkl.PrecursorMZ);
              }
            }
            catch
            {
              Console.WriteLine("Scan {0} ignored.", scan);
              ignoreScans.Add(scan);
              File.WriteAllLines(GetIgnoreScanFile(fileName), (from i in ignoreScans
                                                               let s = i.ToString()
                                                               select s).ToArray());
              bReadAgain = true;
              break;
            }

            PeakList<Peak> pklProcessed;
            if (msLevel > 1)
            {
              if (null == this.PeakListProcessor || (options.ExtractRawMS3 && pkl.MsLevel >= 3))
              {
                pklProcessed = pkl;
              }
              else
              {
                pklProcessed = this.PeakListProcessor.Process(pkl);
              }
            }
            else
            {
              pklProcessed = pkl;
            }

            if (null != pklProcessed && pklProcessed.Count > 0)
            {
              DoWritePeakList(rawReader, pklProcessed, fileName, result);
            }
          }
        }
        finally
        {
          if (!bReadAgain)
          {
            DoFinalize(bReadAgain, rawReader, fileName, result);
          }
        }
      }

      if (bReadAgain)
      {
        return DoProcess(fileName, ignoreScans, lastScan, true);
      }
      else
      {
        return result;
      }
    }

    protected abstract void DoInitialize(IRawFile2 rawReader, string fileName);

    protected abstract bool DoAcceptMsLevel(int msLevel);

    protected abstract void DoWritePeakList(IRawFile rawReader, PeakList<Peak> pkl, string rawFileName, List<string> result);

    protected abstract void DoFinalize(bool bReadAgain, IRawFile rawReader, string rawFileName, List<string> result);

    public override IEnumerable<string> Process(string fileName)
    {
      var ignoreFile = GetIgnoreScanFile(fileName);

      List<int> ignoreScans = new List<int>();
      if (File.Exists(ignoreFile))
      {
        ignoreScans = (from s in File.ReadAllLines(ignoreFile)
                       let scan = Convert.ToInt32(s)
                       select scan).ToList();
      }
      return DoProcess(fileName, ignoreScans, 0, false);
    }
  }
}