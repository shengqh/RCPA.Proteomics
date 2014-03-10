using System.Collections.Generic;
namespace RCPA.Proteomics
{
  public class ScanTime
  {
    public ScanTime(int scan, double retentionTime)
    {
      this.Scan = scan;
      this.RetentionTime = retentionTime;
      this.IonInjectionTime = 1.0;
    }

    public double IonInjectionTime { get; set; }

    public int Scan { get; set; }

    public double RetentionTime { get; set; }

    public override string ToString()
    {
      return MyConvert.Format("[{0} : {1:0.00}]", this.Scan, this.RetentionTime);
    }
  }

  public static class ScanTimeExtension
  {
    public static bool HasRetentionTime(this List<ScanTime> scanTimes)
    {
      return scanTimes.Count > 0 && scanTimes[0].RetentionTime > 0;
    }

    public static string RetentionTimesInSecond(this List<ScanTime> scanTimes)
    {
      if (scanTimes.Count == 1)
      {
        return MyConvert.Format("{0:0.0}", scanTimes[0].RetentionTime * 60);
      }
      else if (scanTimes.Count > 1)
      {
        return MyConvert.Format("{0:0.0}-{1:0.0}", scanTimes[0].RetentionTime * 60, scanTimes[scanTimes.Count - 1].RetentionTime * 60);
      }
      else
      {
        return string.Empty;
      }
    }

    public static string Scans(this List<ScanTime> scanTimes)
    {
      if (scanTimes.Count == 1)
      {
        return scanTimes[0].Scan.ToString();
      }
      else if (scanTimes.Count > 1)
      {
        return MyConvert.Format("{0}-{1}", scanTimes[0].Scan, scanTimes[scanTimes.Count - 1].Scan);
      }
      else
      {
        return string.Empty;
      }
    }
  }
}