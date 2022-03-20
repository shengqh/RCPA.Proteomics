using RCPA.Proteomics.Spectrum;
using System;
using System.Collections.Generic;

namespace RCPA.Proteomics.Raw
{
  public class CacheRawFile : IRawFile2
  {
    private IRawFile2 rawFile;

    private Dictionary<int, PeakList<Peak>> cachePeaks = new Dictionary<int, PeakList<Peak>>();

    public CacheRawFile(IRawFile2 rawFile)
    {
      this.rawFile = rawFile;
    }

    public CacheRawFile(string fileName)
    {
      this.rawFile = RawFileFactory.GetRawFileReader(fileName);
    }

    #region IRawFile Members

    public string FileName
    {
      get { return rawFile.FileName; }
    }

    public bool IsScanValid(int scan)
    {
      return rawFile.IsScanValid(scan);
    }

    public int GetFirstSpectrumNumber()
    {
      return rawFile.GetFirstSpectrumNumber();
    }

    public int GetLastSpectrumNumber()
    {
      return rawFile.GetLastSpectrumNumber();
    }

    public bool IsProfileScanForScanNum(int scan)
    {
      return rawFile.IsProfileScanForScanNum(scan);
    }

    public bool IsCentroidScanForScanNum(int scan)
    {
      return rawFile.IsCentroidScanForScanNum(scan);
    }

    public RCPA.Proteomics.Spectrum.Peak GetPrecursorPeak(int scan)
    {
      return rawFile.GetPrecursorPeak(scan);
    }

    public RCPA.Proteomics.Spectrum.Peak GetPrecursorPeak(List<ScanTime> scanTimes)
    {
      return rawFile.GetPrecursorPeak(scanTimes);
    }

    public void Open(string fileName)
    {
      rawFile.Open(fileName);
    }

    public bool Close()
    {
      DoClearPeaks();

      return rawFile.Close();
    }

    public int GetNumSpectra()
    {
      return rawFile.GetNumSpectra();
    }

    public bool IsValid()
    {
      return rawFile.IsValid();
    }

    public int GetMsLevel(int scan)
    {
      return rawFile.GetMsLevel(scan);
    }

    public string GetScanMode(int scan)
    {
      return rawFile.GetScanMode(scan);
    }

    public double ScanToRetentionTime(int scan)
    {
      return rawFile.ScanToRetentionTime(scan);
    }

    public PeakList<Peak> GetPeakList(int scan)
    {
      if (cachePeaks.ContainsKey(scan))
      {
        return cachePeaks[scan];
      }

      return rawFile.GetPeakList(scan);
    }

    public PeakList<Peak> GetPeakList(int scan, double minMz, double maxMz)
    {
      return rawFile.GetPeakList(scan, minMz, maxMz);
    }

    #endregion

    private void DoClearPeaks()
    {
      foreach (var v in cachePeaks.Values)
      {
        v.Clear();
      }

      cachePeaks.Clear();
    }

    #region IDisposable Members

    public void Dispose()
    {
      Dispose(true);
      GC.SuppressFinalize(this);
    }

    protected virtual void Dispose(bool disposing)
    {
      if (!m_disposed)
      {
        if (disposing)
        {
          DoClearPeaks();

          rawFile.Dispose();
        }

        m_disposed = true;
      }
    }

    ~CacheRawFile()
    {
      Dispose(false);
    }

    private bool m_disposed;

    public ScanTime GetScanTime(int scan)
    {
      return rawFile.GetScanTime(scan);
    }

    public string GetFileNameWithoutExtension(string fileName)
    {
      return rawFile.GetFileNameWithoutExtension(fileName);
    }

    public double GetIonInjectionTime(int scan)
    {
      return rawFile.GetIonInjectionTime(scan);
    }

    public double GetIsolationWidth(int scan)
    {
      return rawFile.GetIsolationWidth(scan);
    }

    public double GetIsolationMass(int scan)
    {
      return rawFile.GetIsolationMass(scan);
    }

    public PrecursorPeak GetPrecursorPeakWithMasterScan(int scan)
    {
      return rawFile.GetPrecursorPeakWithMasterScan(scan);
    }

    #endregion

    #region IRawFile2 Members

    public int GetMasterScan(int scan)
    {
      return rawFile.GetMasterScan(scan);
    }

    public PeakList<Peak> GetMasterScanPeakList(int childScan)
    {
      return rawFile.GetMasterScanPeakList(childScan);
    }

    #endregion


    public bool IsBadDataScan(int scan)
    {
      return this.rawFile.IsBadDataScan(scan);
    }

    public IMasterScanFinder MasterScanFinder
    {
      get
      {
        return this.rawFile.MasterScanFinder;
      }
      set
      {
        this.rawFile.MasterScanFinder = value;
      }
    }
  }
}
