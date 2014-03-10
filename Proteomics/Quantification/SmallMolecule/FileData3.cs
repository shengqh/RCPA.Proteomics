using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using RCPA.Proteomics.Spectrum;

namespace RCPA.Proteomics.Quantification.SmallMolecule
{
  public class FileData3
  {
    private Dictionary<string, Dictionary<int, PeakItem>> Peaks = new Dictionary<string, Dictionary<int, PeakItem>>();

    public int MinMz { get; set; }

    public int MaxMz { get; set; }

    public string FileName { get; set; }

    public FileData3()
    { }

    public void AddPeaks(int minMz, int maxMz, int scan, PeakList<Peak> pkl)
    {
      this.MinMz = minMz;

      this.MaxMz = maxMz;

      List<Peak> peaks = new List<Peak>();
      for (int i = 0; i <= maxMz; i++)
      {
        peaks.Add(new Peak(0.0, 0.0));
      }

      pkl.SortByMz();

      foreach (var p in pkl)
      {
        var mz = (int)Math.Round(p.Mz);
        if (mz < minMz)
        {
          continue;
        }

        if (mz > maxMz)
        {
          break;
        }

        peaks[mz] = p;
      }

      for (int i = minMz; i <= maxMz; i++)
      {
        string peak = i.ToString();
        this[peak, scan] = new PeakItem(scan, peaks[i].Mz, peaks[i].Intensity);
      }
    }

    public void Clear()
    {
      Peaks.Clear();
    }

    public IEnumerable<string> PeakNames
    {
      get { return Peaks.Keys; }
    }

    public PeakItem this[string peak, int scan]
    {
      get
      {
        if (!Peaks.ContainsKey(peak))
        {
          return null;
        }

        var dic = Peaks[peak];
        if (!dic.ContainsKey(scan))
        {
          return null;
        }

        return dic[scan];
      }
      set
      {
        if (!Peaks.ContainsKey(peak))
        {
          Peaks[peak] = new Dictionary<int, PeakItem>();
        }
        Peaks[peak][scan] = value;
      }
    }

    public List<PeakItem> this[string peak]
    {
      get
      {
        if (!Peaks.ContainsKey(peak))
        {
          return new List<PeakItem>();
        }

        return (from p in Peaks[peak].Values
                orderby p.Scan
                select p).ToList();
      }
      set
      {
        var dic = new Dictionary<int, PeakItem>();
        Peaks[peak] = dic;
        value.ForEach(p => dic[p.Scan] = p);
      }
    }

    public void CalculatePPMToMaxIntensity()
    {
      foreach (var dic in Peaks.Values)
      {
        var maxIntensityPeak = (from p in dic.Values
                                orderby p.Intensity descending
                                select p).FirstOrDefault();

        foreach (var peak in dic.Values)
        {
          peak.PPM = PrecursorUtils.mz2ppm(peak.Mz, peak.Mz - maxIntensityPeak.Mz);
        }
      }
    }
  }
}
