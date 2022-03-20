using RCPA.Proteomics.Spectrum;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace RCPA.Proteomics.Raw
{
  public abstract class AbstractRawFile : IRawFile2
  {
    private IMasterScanFinder _finder = new MasterScanDefaultFinder();

    public IMasterScanFinder MasterScanFinder
    {
      get
      {
        return _finder;
      }
      set
      {
        if (value == null)
        {
          _finder = new MasterScanDefaultFinder();
        }
        else
        {
          _finder = value;
        }
      }
    }

    #region IRawFile Members

    public string FileName
    {
      get;
      protected set;
    }

    public virtual bool IsScanValid(int scan)
    {
      return (scan >= GetFirstSpectrumNumber() && scan <= GetLastSpectrumNumber());
    }

    public virtual string GetFileNameWithoutExtension(string rawFileName)
    {
      return Path.GetFileNameWithoutExtension(rawFileName);
    }

    public abstract int GetFirstSpectrumNumber();

    public abstract int GetLastSpectrumNumber();

    public abstract bool IsProfileScanForScanNum(int scan);

    public abstract bool IsCentroidScanForScanNum(int scan);

    public abstract Peak GetPrecursorPeak(int scan);

    public virtual Peak GetPrecursorPeak(List<ScanTime> scanTimes)
    {
      if (null == scanTimes || 0 == scanTimes.Count)
      {
        throw new ArgumentException("scanTimes cannot be null or empty.");
      }

      List<Peak> pkls = new List<Peak>();

      scanTimes.ForEach(st => pkls.Add(GetPrecursorPeak(st.Scan)));

      return (from p in pkls
              orderby p.Charge descending
              orderby p.Mz descending
              select p).First();
    }

    public abstract void Open(string fileName);

    public abstract bool Close();

    public abstract int GetNumSpectra();

    public abstract bool IsValid();

    public abstract int GetMsLevel(int scan);

    public abstract string GetScanMode(int scan);

    public abstract double ScanToRetentionTime(int scan);

    public abstract PeakList<Spectrum.Peak> GetPeakList(int scan);

    public abstract PeakList<Spectrum.Peak> GetPeakList(int scan, double minMz, double maxMz);

    public virtual ScanTime GetScanTime(int scan)
    {
      return new ScanTime(scan, ScanToRetentionTime(scan));
    }

    public virtual double GetIonInjectionTime(int scan)
    {
      return 0.0;
    }

    public virtual double GetIsolationWidth(int scan)
    {
      return 0.0;
    }

    public virtual double GetIsolationMass(int scan)
    {
      return GetPrecursorPeak(scan).Mz;
    }

    #endregion

    #region IDisposable Members

    public virtual void Dispose()
    {
      Dispose(true);
      GC.SuppressFinalize(true);
    }

    #endregion

    private bool _alreadyDisposed = false;

    protected virtual void Dispose(bool isDisposing)
    {
      // 不要多次处理
      if (_alreadyDisposed)
        return;

      if (isDisposing)
      {
        Close();
      }

      _alreadyDisposed = true;
    }

    ~AbstractRawFile()
    {
      Dispose(false);
    }

    #region IRawFile2 Members

    public virtual PrecursorPeak GetPrecursorPeakWithMasterScan(int scan)
    {
      var result = new PrecursorPeak(GetPrecursorPeak(scan))
      {
        IsolationMass = GetIsolationMass(scan),
        MasterScan = GetMasterScan(scan),
        IsolationWidth = GetIsolationWidth(scan)
      };
      result.MonoIsotopicMass = result.IsolationMass;
      return result;
    }

    public virtual int GetMasterScan(int scan)
    {
      return MasterScanFinder.Find(this, scan);
    }

    public PeakList<Peak> GetMasterScanPeakList(int childScan)
    {
      var masterScan = GetMasterScan(childScan);
      if (masterScan != 0)
      {
        return GetPeakList(masterScan);
      }
      return null;
    }

    #endregion

    public virtual bool IsBadDataScan(int scan)
    {
      return false;
    }
  }
}
