using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using RCPA.Proteomics.Raw;
using RCPA.Utils;
using RCPA.Proteomics.Spectrum;

namespace RCPA.Proteomics.IO
{
  public abstract class AbstractRawConverterProcessor : AbstractThreadFileProcessor
  {
    //The program information which will be writen to mgf file.

    protected IProcessor<PeakList<Peak>> pklProcessor;

    protected double ppmPeakTolerance;
    protected double ppmPrecursorTolerance;
    protected string programInfo;

    protected double retentionTimeTolerance;

    protected StreamWriter sw;
    protected IPeakListWriter<Peak> writer;

    protected IRawFile2 rawReader;

    public AbstractRawConverterProcessor(IRawFile2 rawReader, IPeakListWriter<Peak> writer, double retentionTimeTolerance,
                                         double ppmPrecursorTolerance, double ppmPeakTolerance,
                                         IProcessor<PeakList<Peak>> pklProcessor)
    {
      this.rawReader = rawReader;
      this.writer = writer;
      this.retentionTimeTolerance = retentionTimeTolerance;
      this.ppmPrecursorTolerance = ppmPrecursorTolerance;
      this.ppmPeakTolerance = ppmPeakTolerance;
      this.pklProcessor = pklProcessor;
    }

    public override IEnumerable<string> Process(string rawFilename)
    {
      if (null == rawReader)
      {
        rawReader = RawFileFactory.GetRawFileReader(rawFilename);
      }

      var rawFile = new FileInfo(rawFilename);
      if (!rawFile.Exists)
      {
        throw new ArgumentException("Raw file not exists : " + rawFilename);
      }

      Progress.SetMessage("Reading peak list from " + rawFilename + " ...");
      List<PeakList<Peak>> pklList = ReadTandemMassFromRaw(rawFile);

      List<PeakList<Peak>> mergedPklList;

      bool mergeScans = this.retentionTimeTolerance > 0;
      if (mergeScans)
      {
        Progress.SetMessage("Merging peak list, total " + pklList.Count + " ...");
        mergedPklList = MergePeakList(pklList);

        Progress.SetMessage("Merging peak list finished, " + mergedPklList.Count + " kept.");
      }
      else
      {
        mergedPklList = pklList;
      }

      var result = new HashSet<string>();
      if (mergedPklList.Count > 0)
      {
        result.Union(WritePeakLists(rawFile, mergedPklList));
      }

      Progress.SetMessage("Succeed!");

      return new List<string>(result);
    }

    protected abstract List<string> WritePeakLists(FileInfo rawFile, List<PeakList<Peak>> mergedPklList);

    protected List<PeakList<Peak>> MergePeakList(List<PeakList<Peak>> pklList)
    {
      int index = 0;

      Progress.SetRange(0, pklList.Count);
      Progress.Begin();
      try
      {
        while (index < pklList.Count)
        {
          if (Progress.IsCancellationPending())
          {
            throw new UserTerminatedException();
          }
          Progress.SetPosition(index);

          PeakList<Peak> currentPkl = pklList[index];
          double maxGap = PrecursorUtils.ppm2mz(currentPkl.PrecursorMZ, this.ppmPrecursorTolerance);

          int next = index + 1;
          while (next < pklList.Count)
          {
            PeakList<Peak> nextPkl = pklList[next];
            double retentionTimeGap = nextPkl.ScanTimes[0].RetentionTime -
                                      currentPkl.ScanTimes[0].RetentionTime;
            if (retentionTimeGap > this.retentionTimeTolerance)
            {
              break;
            }

            if (nextPkl.PrecursorCharge != currentPkl.PrecursorCharge)
            {
              next++;
              continue;
            }

            double precursorMzGap = Math.Abs(nextPkl.PrecursorMZ - currentPkl.PrecursorMZ);
            if (precursorMzGap < maxGap)
            {
              currentPkl.MergeByMZFirst(nextPkl, this.ppmPeakTolerance);
              pklList.RemoveAt(next);
              continue;
            }

            next++;
          }
          index++;
        }
        Progress.SetPosition(pklList.Count);
      }
      finally
      {
        Progress.End();
      }

      return pklList;
    }

    protected string GetIgnoreScanFile(FileInfo rawFilename)
    {
      return rawFilename.FullName + ".ignorescan";
    }

    protected List<PeakList<Peak>> ReadTandemMassFromRaw(FileInfo rawFilename)
    {
      var ignoreFile = GetIgnoreScanFile(rawFilename);
      if (File.Exists(ignoreFile))
      {
        var ignoreScans = (from s in File.ReadAllLines(ignoreFile)
                           let scan = Convert.ToInt32(s)
                           select scan).ToList();
        return ReadTandemMassFromRaw(rawFilename, ignoreScans);
      }
      else
      {
        return ReadTandemMassFromRaw(rawFilename, new List<int>());
      }
    }

    protected List<PeakList<Peak>> ReadTandemMassFromRaw(FileInfo rawFilename, List<int> ignoreScans)
    {
      string experimental = FileUtils.ChangeExtension(rawFilename.Name, "");

      var result = new List<PeakList<Peak>>();

      bool bReadAgain = false;

      rawReader.Open(rawFilename.FullName);
      try
      {
        int firstSpectrumNumber = rawReader.GetFirstSpectrumNumber();
        int lastSpectrumNumber = rawReader.GetLastSpectrumNumber();

        Progress.SetRange(firstSpectrumNumber, lastSpectrumNumber);
        Progress.Begin();
        try
        {
          for (int scan = firstSpectrumNumber; scan <= lastSpectrumNumber; scan++)
          {
            if (Progress.IsCancellationPending())
            {
              throw new UserTerminatedException();
            }

            if (ignoreScans.Contains(scan))
            {
              continue;
            }

            Progress.SetPosition(scan);

            int msLevel = rawReader.GetMsLevel(scan);

            if (msLevel > 1)
            {
              PeakList<Peak> pkl;
              try
              {
                pkl = rawReader.GetPeakList(scan);
              }
              catch (RawReadException ex)
              {
                ignoreScans.Add(ex.Scan);
                File.WriteAllLines(GetIgnoreScanFile(rawFilename), (from i in ignoreScans
                                                                    let s = i.ToString()
                                                                    select s).ToArray());
                bReadAgain = true;
                break;
              }
              pkl.Precursor = rawReader.GetPrecursorPeakWithMasterScan(scan);
              pkl.MsLevel = msLevel;
              pkl.Experimental = experimental;
              pkl.ScanTimes.Add(new ScanTime(scan, rawReader.ScanToRetentionTime(scan)));
              pkl.ScanMode = rawReader.GetScanMode(scan);

              if (pkl.PrecursorCharge == 0)
              {
                pkl.PrecursorCharge = PrecursorUtils.GuessPrecursorCharge(pkl, pkl.PrecursorMZ);
              }

              PeakList<Peak> pklProcessed = this.pklProcessor.Process(pkl);
              if (null != pklProcessed && pklProcessed.Count > 0)
              {
                result.Add(pklProcessed);
              }
            }
          }
        }
        finally
        {
          Progress.End();
        }
      }
      finally
      {
        rawReader.Close();
      }

      if (bReadAgain)
      {
        return ReadTandemMassFromRaw(rawFilename, ignoreScans);
      }
      else
      {
        return result;
      }
    }
  }
}