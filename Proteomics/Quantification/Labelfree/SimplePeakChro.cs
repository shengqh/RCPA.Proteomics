using RCPA.Proteomics.Spectrum;
using System.Collections.Generic;
using System.Linq;

namespace RCPA.Proteomics.Quantification.Labelfree
{
  public class ScanPeak : Peak
  {
    public ScanPeak()
    {
      IonInjectionTime = 1.0;
    }

    public int Scan { get; set; }

    public double RetentionTime { get; set; }

    public double IonInjectionTime { get; set; }

    public bool Identified { get; set; }

    public double PPMDistance { get; set; }
  }

  public class SimplePeakChro
  {
    public string Sequence { get; set; }

    public double Mz { get; set; }

    public double MzTolerance { get; set; }

    public int Charge { get; set; }

    public double MaxRetentionTime { get; set; }

    public List<ScanPeak> Peaks { get; set; }

    public SimplePeakChro()
    {
      this.Peaks = new List<ScanPeak>();
    }

    public void InitalizePPMTolerance()
    {
      foreach (var peak in Peaks)
      {
        peak.PPMDistance = PrecursorUtils.mz2ppm(peak.Mz, peak.Mz - this.Mz);
      }
    }

    private void SetMinContinuesPeaks(int minCount)
    {
      int count = 0;
      for (int i = 0; i <= Peaks.Count; i++)
      {
        if (i == Peaks.Count || Peaks[i].Intensity == 0.0)
        {
          if (count == 0 || count >= minCount)
          {
            count = 0;
            continue;
          }

          int j = i - 1;
          while (count > 0)
          {
            Peaks[j--].Intensity = 0.0;
            count--;
          }
        }
        else
        {
          count++;
        }
      }
    }

    private void TrimPeaksFromTop()
    {
      while (Peaks.Count > 0)
      {
        if (Peaks[0].Intensity == 0)
        {
          Peaks.RemoveAt(0);
          continue;
        }

        int count = 1;
        for (int i = 1; i < Peaks.Count; i++)
        {
          if (Peaks[i].Intensity == 0)
          {
            break;
          }
          count++;
          if (count > 3)
          {
            break;
          }
        }

        if (count == Peaks.Count || count > 3)
        {
          return;
        }

        Peaks.RemoveAt(0);
      }
    }

    public void TrimPeaks(double minRetentionTime)
    {
      SetMinContinuesPeaks(3);

      TrimPeaksFromTop();

      Peaks.Reverse();

      TrimPeaksFromTop();

      Peaks.Reverse();

      if (Peaks.Count > 0)
      {
        if (Peaks.Last().RetentionTime - Peaks.First().RetentionTime < minRetentionTime)
        {
          Peaks.Clear();
        }
      }
    }

    public override string ToString()
    {
      return MyConvert.Format("[{0:0.0000}, {1}]", this.Mz, this.Charge);
    }
  }
}
