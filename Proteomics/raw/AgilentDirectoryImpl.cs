using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Agilent.MassSpectrometry.DataAnalysis;
using RCPA.Proteomics.Spectrum;

namespace RCPA.Proteomics.Raw
{
  public class AgilentDirectoryImpl : AbstractRawFile
  {
    private bool fileOpened;

    private double[] retentionTimes;

    public IMsdrDataReader Reader { get; private set; }

    public AgilentDirectoryImpl()
    {
      Reader = new MassSpecDataReader();
      fileOpened = false;
      retentionTimes = new double[0];
    }

    #region IRawFile Members

    public override bool IsScanValid(int scan)
    {
      return fileOpened && scan > 0 && scan <= retentionTimes.Length;
    }

    public override int GetFirstSpectrumNumber()
    {
      return 1;
    }

    public override int GetLastSpectrumNumber()
    {
      return retentionTimes.Length;
    }

    public override bool IsProfileScanForScanNum(int scan)
    {
      throw new NotImplementedException();
    }

    public override bool IsCentroidScanForScanNum(int scan)
    {
      throw new NotImplementedException();
    }

    public override Peak GetPrecursorPeak(int scan)
    {
      return null;
    }

    public override Peak GetPrecursorPeak(List<ScanTime> scanTimes)
    {
      return null;
    }

    public override void Open(string directory)
    {
      Close();

      if (!Reader.OpenDataFile(directory))
      {
        throw new ArgumentException(MyConvert.Format("Cannot read directory {0}.", directory));
      }

      fileOpened = true;

      retentionTimes = Reader.GetRetentionTimes();
    }

    public override bool Close()
    {
      if (fileOpened)
      {
        Reader.CloseDataFile();
        fileOpened = false;
      }

      return true;
    }

    public override int GetNumSpectra()
    {
      return retentionTimes.Length;
    }

    public override bool IsValid()
    {
      return fileOpened;
    }

    public override int GetMsLevel(int scan)
    {
      return Reader.GetMsLevel(retentionTimes[scan - 1]);
    }

    public override string GetScanMode(int scan)
    {
      throw new NotImplementedException();
    }

    public override double ScanToRetentionTime(int scan)
    {
      return retentionTimes[scan - 1];
    }

    public override PeakList<Peak> GetPeakList(int scan)
    {
      return Reader.GetPeakList(retentionTimes[scan - 1]);
    }

    public override PeakList<Peak> GetPeakList(int scan, double minMz, double maxMz)
    {
      PeakList<Peak> result = GetPeakList(scan);

      result.RemoveAll(m => m.Mz < minMz);
      result.RemoveAll(m => m.Mz > maxMz);

      return result;
    }

    #endregion

    public IBDAChromData GetTIC()
    {
      return Reader.GetTIC();
    }

    public double GetTotalIntensity()
    {
      IBDAChromData data = GetTIC();
      return (double)data.YArray.Sum();
    }
  }
}
