using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using RCPA.Gui;
using RCPA.Proteomics.Raw;
using RCPA.Proteomics.Spectrum;

namespace RCPA.Proteomics.Quantification.IsobaricLabelling
{
  /// <summary>
  /// 从Raw文件中读取Isobaric labelling对应的minMz-maxMz区间的Peak，要求这区间至少包含minPeakCount离子
  /// </summary>
  public abstract class AbstractIsobaricRawReader : ProgressClass, IIsobaricRawReader
  {
    private static double defaultIsolationWidth = 0.5;

    /// <summary>
    /// Isobaric labelling区间的最小m/z
    /// </summary>
    public double MinMz { get; set; }

    /// <summary>
    /// Isobaric labelling区间的最大m/z
    /// </summary>
    public double MaxMz { get; set; }

    /// <summary>
    /// 该区间中最少离子数
    /// </summary>
    public int MinPeakCount { get; set; }

    protected int FirstScan { get; set; }

    protected int EndScan { get; set; }

    private IsobaricType _plexType;
    public IsobaricType PlexType
    {
      get
      {
        return _plexType;
      }
      set
      {
        _plexType = value;
        this.MinMz = _plexType.Channels.First().Mz - 0.5;
        this.MaxMz = _plexType.Channels.Last().Mz + 0.5;
      }
    }

    public double PrecursorPPM { get; set; }

    public int MsLevel { get; set; }

    public String Name { get; private set; }

    protected AbstractIsobaricRawReader(int msLevel, string name)
    {
      this.MsLevel = msLevel;
      this.MinPeakCount = 1;
      this.Name = name;
    }

    /// <summary>
    /// Get corresponding identification scan
    /// </summary>
    /// <param name="scan"></param>
    /// <returns></returns>
    protected virtual int GetIdentificationScan(int scan)
    {
      return scan;
    }

    /// <summary>
    /// Get corresponding isolation scan
    /// </summary>
    /// <param name="scan"></param>
    /// <returns></returns>
    protected virtual int GetIsolationScan(IRawFile2 rawReader, int scan)
    {
      var mslevel = rawReader.GetMsLevel(scan);
      var targetlevel = mslevel - 1;
      scan--;
      while (scan >= FirstScan)
      {
        if (targetlevel == rawReader.GetMsLevel(scan))
        {
          return scan;
        }
      }
      return scan;
    }


    protected void AppendScan(IRawFile2 rawReader, List<IsobaricScan> result, int scan, string mode, double isolationWidth)
    {
      IsobaricScan item = new IsobaricScan();

      PeakList<Peak> pkl = rawReader.GetPeakList(scan, MinMz, MaxMz);

      if (pkl.Count < MinPeakCount)
      {
        return;
      }

      var recordScan = GetIdentificationScan(scan);

      item.ScanMode = mode;
      item.RawPeaks = pkl;
      item.PeakInIsolationWindow = rawReader.GetPeakInIsolationWindow(scan, isolationWidth);
      item.Scan = rawReader.GetScanTime(recordScan);

      result.Add(item);
    }

    protected void AppendScan(IRawFile2 rawReader, List<IsobaricScan> result, int scan, string mode)
    {
      AppendScan(rawReader, result, scan, mode, defaultIsolationWidth);
    }

    #region IFileReader<List<IsobaricItem>> Members

    protected virtual string[] GetScanMode()
    {
      return new string[] { };
    }

    public virtual List<IsobaricScan> ReadFromFile(string fileName)
    {
      var result = new List<IsobaricScan>();

      var askedScanMode = GetScanMode();
      var lowerScanMode = new HashSet<string>(from a in askedScanMode
                                              select a.ToLower());

      using (var rawReader = RawFileFactory.GetRawFileReader(fileName, this.IsTandemMS3))
      {
        FirstScan = rawReader.GetFirstSpectrumNumber();
        EndScan = rawReader.GetLastSpectrumNumber();

        DoAfterFileOpen(rawReader);

        var firstIsolationWidth = 0.0;

        var icount = 0;
        for (int scan = FirstScan; scan <= EndScan; scan++)
        {
          if (!rawReader.IsScanValid(scan))
          {
            continue;
          }

          if (this.MsLevel == rawReader.GetMsLevel(scan))
          {
            firstIsolationWidth = rawReader.GetIsolationWidth(scan);
            if (firstIsolationWidth > 0 && firstIsolationWidth < 5)
            {
              break;
            }
            icount++;
            if (icount > 10)
            {
              break;
            }
          }
        }

        if (firstIsolationWidth == 0.0)
        {
          firstIsolationWidth = defaultIsolationWidth;
        }

        Progress.SetMessage("Reading channel information ...");
        Progress.SetRange(FirstScan, EndScan);

        for (int scan = FirstScan; scan <= EndScan; scan++)
        {
          if (Progress.IsCancellationPending())
          {
            throw new UserTerminatedException();
          }

          if (!rawReader.IsScanValid(scan))
          {
            continue;
          }

          Progress.SetPosition(scan);

          if (this.MsLevel == rawReader.GetMsLevel(scan))
          {
            //Console.WriteLine("Scan : {0}", scan);

            string scanMode = rawReader.GetScanMode(scan).ToLower();
            if (string.IsNullOrEmpty(scanMode))
            {
              AppendScan(rawReader, result, scan, "UNKNOWN", firstIsolationWidth);
            }
            else if (lowerScanMode.Count == 0 || lowerScanMode.Contains(scanMode))
            {
              AppendScan(rawReader, result, scan, scanMode.ToUpper(), firstIsolationWidth);
            }
            else
            {
              Console.WriteLine("Scan {0} is skipped with mode {1}", scan, scanMode);
            }
          }
        }
      }

      return result;
    }

    protected virtual void DoAfterFileOpen(IRawFile2 rawReader)
    {
    }

    #endregion

    public override string ToString()
    {
      return GetScanMode().Merge("+") + " Mode";
    }

    public virtual bool IsTandemMS3
    {
      get { return false; }
    }
  }
}
