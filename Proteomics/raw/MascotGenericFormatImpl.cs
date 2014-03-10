using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using RCPA.Proteomics.Mascot;
using System.Text.RegularExpressions;
using RCPA.Utils;
using RCPA.Proteomics.Summary;
using RCPA.Proteomics.Spectrum;

namespace RCPA.Proteomics.Raw
{
  public class MascotGenericFormatImpl : AbstractRawFile
  {
    class PositionClass
    {
      public long Position { get; set; }
      public double RetentionTimeInSecond { get; set; }
    }

    public MascotGenericFormatImpl()
    {
      this.FileName = string.Empty;
    }

    private MascotGenericFormatIterator<Peak> iter;

    private readonly Regex scanRegex = new Regex(@"([0-9]+)");

    private StreamReader sr = null;

    private int firstScan, lastScan;

    private Dictionary<int, PositionClass> positions = new Dictionary<int, PositionClass>();

    #region IRawFile Members

    public override bool IsScanValid(int scan)
    {
      return positions.ContainsKey(scan);
    }

    public override int GetFirstSpectrumNumber()
    {
      return firstScan;
    }

    public override int GetLastSpectrumNumber()
    {
      return lastScan;
    }

    public override bool IsProfileScanForScanNum(int scan)
    {
      return false;
    }

    public override bool IsCentroidScanForScanNum(int scan)
    {
      return true;
    }

    public override Peak GetPrecursorPeak(int scan)
    {
      long position = positions[scan].Position;
      sr.SetCharpos(position);

      PeakList<Peak> result = iter.Next();
      return new Peak(result.PrecursorMZ, result.PrecursorIntensity, result.PrecursorCharge);
    }

    public override void Open(string fileName)
    {
      Close();
      sr = new StreamReader(fileName);

      this.FileName = fileName;
      iter = new MascotGenericFormatIterator<Peak>(sr);

      string line;
      int scan = 1;
      long position = 0;
      while ((line = sr.ReadLine()) != null)
      {
        if (line.StartsWith(MascotGenericFormatConstants.MSMS_SCAN_TAG))
        {
          Match m = scanRegex.Match(line);
          scan = Convert.ToInt32(m.Groups[1].Value);
          positions[scan] = new PositionClass()
          {
            Position = position
          };
        }
        else if (line.StartsWith(MascotGenericFormatConstants.RETENTION_TIME_TAG))
        {
          positions[scan].RetentionTimeInSecond = MyConvert.ToDouble(line.Substring(MascotGenericFormatConstants.RETENTION_TIME_TAG.Length + 1));
        }
        else if (line.StartsWith(MascotGenericFormatConstants.END_PEAK_LIST_TAG))
        {
          position = StreamUtils.GetCharpos(sr);
        }
      }

      List<int> scans = new List<int>(positions.Keys);
      if (scans.Count > 0)
      {
        firstScan = scans[0];
        lastScan = scans[scans.Count - 1];
      }
      else
      {
        firstScan = 0;
        lastScan = -1;
      }
    }

    public override bool Close()
    {
      if (sr != null)
      {
        sr.Close();
        sr = null;
        this.FileName = string.Empty;
      }
      return true;
    }

    public override int GetNumSpectra()
    {
      return positions.Count;
    }

    public override bool IsValid()
    {
      return sr != null;
    }

    public override int GetMsLevel(int scan)
    {
      return 2;
    }

    public override double ScanToRetentionTime(int scan)
    {
      var result = positions[scan].RetentionTimeInSecond;
      if (0.0 == result)
      {
        return scan;
      }

      return result;
    }

    public override PeakList<Peak> GetPeakList(int scan)
    {
      long position = positions[scan].Position;
      sr.SetCharpos(position);
      return iter.Next();
    }

    public override PeakList<Peak> GetPeakList(int scan, double minMz, double maxMz)
    {
      long position = positions[scan].Position;
      sr.SetCharpos(position);
      PeakList<Peak> result = iter.Next();
      result.RemoveAll(m => { return m.Mz < minMz || m.Mz > maxMz; });
      return result;
    }

    #endregion

    public override string GetScanMode(int scan)
    {
      return string.Empty;
    }
  }
}
