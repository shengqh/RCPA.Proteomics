using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using RCPA.Proteomics.Spectrum;
using RCPA.Proteomics.Raw;
using RCPA.Utils;
using RCPA.Proteomics.Mascot;

namespace RCPA.Proteomics.Format
{
  public abstract class AbstractRawTandemSpectrumConverter : AbstractParallelTaskFileProcessor
  {
    public IProcessor<PeakList<Peak>> PeakListProcessor { get; set; }

    public string TargetDirectory { get; set; }

    public AbstractRawTandemSpectrumConverter()
    { }

    protected string GetIgnoreScanFile(string rawFilename)
    {
      return rawFilename + ".ignorescan";
    }

    protected IEnumerable<string> DoProcess(string fileName, List<int> ignoreScans)
    {
      var result = new List<string>();

      DoInitialize(fileName);

      bool bReadAgain = false;

      using (var rawReader = RawFileFactory.GetRawFileReader(fileName))
      {
        try
        {
          string experimental = rawReader.GetFileNameWithoutExtension(fileName);

          SetMessage("Processing " + fileName + " ...");

          int firstSpectrumNumber = rawReader.GetFirstSpectrumNumber();
          int lastSpectrumNumber = rawReader.GetLastSpectrumNumber();

          SetRange(firstSpectrumNumber, lastSpectrumNumber);

          for (int scan = firstSpectrumNumber; scan <= lastSpectrumNumber; scan++)
          {
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

            if (msLevel == 1)
            {
              continue;
            }

            //Console.WriteLine("Reading scan {0}", scan);

            PeakList<Peak> pkl;
            try
            {
              pkl = rawReader.GetPeakList(scan);
            }
            catch (RawReadException ex)
            {
              ignoreScans.Add(ex.Scan);
              File.WriteAllLines(GetIgnoreScanFile(fileName), (from i in ignoreScans
                                                               let s = i.ToString()
                                                               select s).ToArray());
              bReadAgain = true;
              break;
            }

            pkl.Precursor = new PrecursorPeak(rawReader.GetPrecursorPeak(scan));

            pkl.MsLevel = msLevel;
            pkl.Experimental = experimental;
            pkl.ScanTimes.Add(new ScanTime(scan, rawReader.ScanToRetentionTime(scan)));

            pkl.ScanMode = rawReader.GetScanMode(scan);

            if (pkl.PrecursorCharge == 0)
            {
              pkl.PrecursorCharge = PrecursorUtils.GuessPrecursorCharge(pkl, pkl.PrecursorMZ);
            }

            PeakList<Peak> pklProcessed = this.PeakListProcessor.Process(pkl);

            if (null != pklProcessed && pklProcessed.Count > 0)
            {
              DoWritePeakList(rawReader, pklProcessed, fileName, result);
            }
          }
        }
        finally
        {
          DoFinalize(bReadAgain, rawReader, fileName, result);
        }
      }

      if (bReadAgain)
      {
        return DoProcess(fileName, ignoreScans);
      }
      else
      {
        return result;
      }
    }

    protected abstract void DoInitialize(string rawFileName);

    protected abstract void DoWritePeakList(IRawFile rawReader, PeakList<Peak> pkl, string rawFileName, List<string> result);

    protected abstract void DoFinalize(bool bReadAgain, IRawFile rawReader, string rawFileName, List<string> result);

    public override IEnumerable<string> Process(string fileName)
    {
      var ignoreFile = GetIgnoreScanFile(fileName);
      if (File.Exists(ignoreFile))
      {
        var ignoreScans = (from s in File.ReadAllLines(ignoreFile)
                           let scan = Convert.ToInt32(s)
                           select scan).ToList();
        return DoProcess(fileName, ignoreScans);
      }
      else
      {
        return DoProcess(fileName, new List<int>());
      }
    }
  }
}