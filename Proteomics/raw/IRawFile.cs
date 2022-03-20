using RCPA.Proteomics.Spectrum;
using System;
using System.Collections.Generic;

namespace RCPA.Proteomics.Raw
{
  public interface IRawFile : IDisposable
  {
    string FileName { get; }

    bool IsScanValid(int scan);

    bool IsBadDataScan(int scan);

    int GetFirstSpectrumNumber();

    int GetLastSpectrumNumber();

    bool IsProfileScanForScanNum(int scan);

    bool IsCentroidScanForScanNum(int scan);

    Peak GetPrecursorPeak(int scan);

    Peak GetPrecursorPeak(List<ScanTime> scanTimes);

    void Open(string fileName);

    bool Close();

    int GetNumSpectra();

    bool IsValid();

    int GetMsLevel(int scan);

    string GetScanMode(int scan);

    double ScanToRetentionTime(int scan);

    PeakList<Peak> GetPeakList(int scan);

    PeakList<Peak> GetPeakList(int scan, double minMz, double maxMz);

    ScanTime GetScanTime(int scan);

    string GetFileNameWithoutExtension(string rawFileName);

    double GetIonInjectionTime(int scan);

    double GetIsolationWidth(int scan);

    double GetIsolationMass(int scan);
  }

  public static class IRawFileExtension
  {
    public static List<PeakList<Peak>> GetMS1List(this IRawFile reader)
    {
      var result = new List<PeakList<Peak>>();
      var first = reader.GetFirstSpectrumNumber();
      var last = reader.GetLastSpectrumNumber();
      for (int i = first; i <= last; i++)
      {
        var mslevel = reader.GetMsLevel(i);
        if (mslevel == 1)
        {
          var pkl = reader.GetPeakList(i);
          result.Add(pkl);
        }
      }
      return result;
    }
  }
}