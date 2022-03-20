using RCPA.Gui;
using RCPA.Proteomics.Raw;
using RCPA.Proteomics.Spectrum;
using System;
using System.Collections.Generic;
using System.Linq;

namespace RCPA.Proteomics.Quantification.ITraq
{
  /// <summary>
  /// 从Raw文件中读取iTraq对应的minMz-maxMz区间的Peak，要求这区间至少包含minPeakCount离子
  /// </summary>
  public abstract class AbstractITraqRawReader : ProgressClass, IITraqRawReader
  {
    private static double defaultIsolationWidth = 0.5;

    /// <summary>
    /// iTraq区间的最小m/z
    /// </summary>
    public double MinMz { get; set; }

    /// <summary>
    /// iTraq区间的最大m/z
    /// </summary>
    public double MaxMz { get; set; }

    /// <summary>
    /// 该区间中最少离子数
    /// </summary>
    public int MinPeakCount { get; set; }

    private IRawFile2 _rawReader;

    /// <summary>
    /// Raw文件读取格式
    /// </summary>
    public IRawFile2 RawReader
    {
      get
      {
        return _rawReader;
      }
      set
      {
        if ((null != _rawReader) && _rawReader.IsValid())
        {
          _rawReader.Close();
        }
        _rawReader = value;

        if (this.IsTandemMS3)
        {
          _rawReader.MasterScanFinder = new MasterScanParallelMS3Finder();
        }
      }
    }

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
        this.MinMz = _plexType.GetDefinition().MinIndex - 0.5;
        this.MaxMz = _plexType.GetDefinition().MaxIndex + 0.5;
      }
    }

    public double PrecursorPPM { get; set; }

    public int MsLevel { get; set; }

    public String Name { get; private set; }

    protected AbstractITraqRawReader(int msLevel, string name)
    {
      this.MsLevel = msLevel;
      this.MinPeakCount = 1;
      this.Name = name;
      PlexType = IsobaricType.PLEX4;
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
    protected virtual int GetIsolationScan(int scan)
    {
      var mslevel = RawReader.GetMsLevel(scan);
      var targetlevel = mslevel - 1;
      scan--;
      while (scan >= FirstScan)
      {
        if (targetlevel == RawReader.GetMsLevel(scan))
        {
          return scan;
        }
      }
      return scan;
    }


    protected void AppendScan(List<IsobaricItem> result, int scan, string mode, double isolationWidth)
    {
      IsobaricItem item = new IsobaricItem();

      PeakList<Peak> pkl = RawReader.GetPeakList(scan, MinMz, MaxMz);

      if (pkl.Count < MinPeakCount)
      {
        return;
      }

      var recordScan = GetIdentificationScan(scan);

      if (pkl.ScanTimes.Count == 0)
      {
        pkl.ScanTimes.Add(RawReader.GetScanTime(recordScan));
      }

      item.ScanMode = mode;
      item.RawPeaks = pkl;
      item.PeakInIsolationWindow = RawReader.GetPeakInIsolationWindow(scan, isolationWidth);
      item.PlexType = this.PlexType;

      result.Add(item);
    }

    protected void AppendScan(List<IsobaricItem> result, int scan, string mode)
    {
      AppendScan(result, scan, mode, defaultIsolationWidth);
    }

    #region IFileReader<List<ITraqItem>> Members

    protected virtual string[] GetScanMode()
    {
      return new string[] { };
    }

    public virtual List<IsobaricItem> ReadFromFile(string fileName)
    {
      var result = new List<IsobaricItem>();

      var askedScanMode = GetScanMode();
      var lowerScanMode = new HashSet<string>(from a in askedScanMode
                                              select a.ToLower());


      RawReader.Open(fileName);
      try
      {
        FirstScan = RawReader.GetFirstSpectrumNumber();
        EndScan = RawReader.GetLastSpectrumNumber();

        DoAfterFileOpen();

        var firstIsolationWidth = 0.0;

        var icount = 0;
        for (int scan = FirstScan; scan <= EndScan; scan++)
        {
          if (!RawReader.IsScanValid(scan))
          {
            continue;
          }

          if (this.MsLevel == RawReader.GetMsLevel(scan))
          {
            firstIsolationWidth = RawReader.GetIsolationWidth(scan);
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

          if (!RawReader.IsScanValid(scan))
          {
            continue;
          }

          Progress.SetPosition(scan);

          if (this.MsLevel == RawReader.GetMsLevel(scan))
          {
            string scanMode = RawReader.GetScanMode(scan).ToLower();
            if (string.IsNullOrEmpty(scanMode))
            {
              AppendScan(result, scan, "UNKNOWN", firstIsolationWidth);
            }
            else if (lowerScanMode.Count == 0 || lowerScanMode.Contains(scanMode))
            {
              AppendScan(result, scan, scanMode.ToUpper(), firstIsolationWidth);
            }
            else
            {
              Console.WriteLine("Scan {0} is skipped with mode {1}", scan, scanMode);
            }
          }
        }
      }
      finally
      {
        RawReader.Close();
      }

      return result;
    }

    protected virtual void DoAfterFileOpen()
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
