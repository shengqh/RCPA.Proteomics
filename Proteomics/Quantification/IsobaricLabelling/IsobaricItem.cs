using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using RCPA.Utils;
using RCPA.Proteomics.Spectrum;
using System.Xml;

namespace RCPA.Proteomics.Quantification.IsobaricLabelling
{
  /// <summary>
  /// 对应于一个peptide的isobaric信息。
  /// </summary>
  public class IsobaricItem
  {
    public IsobaricItem()
    {
      this.PlexType = null;
      this.ScanMode = string.Empty;
      this.Valid = true;
      this.ValidProbability = 1;
      this.PrecursorPercentage = 1.0;
    }

    /// <summary>
    /// source和当前对象将共享rawpeaks和peakinisolationwindow
    /// </summary>
    /// <param name="source"></param>
    public IsobaricItem(IsobaricItem source)
    {
      this.PlexType = source.PlexType;
      this.ScanMode = source.ScanMode;
      this.RawPeaks = source.RawPeaks;
      this.PeakInIsolationWindow = source.PeakInIsolationWindow;
      this.Valid = source.Valid;
      this.ValidProbability = source.ValidProbability;
      this.PrecursorPercentage = source.PrecursorPercentage;
    }

    public IsobaricType PlexType { get; set; }

    public string Experimental { get; set; }

    public ScanTime Scan { get; set; }

    public double PrecursorPercentage { get; set; }

    public string ScanMode { get; set; }

    private PeakList<Peak> _rawPeaks;

    public PeakList<Peak> RawPeaks
    {
      get
      {
        if (null == _rawPeaks)
        {
          _rawPeaks = new PeakList<Peak>();
        }
        return _rawPeaks;
      }
      set
      {
        _rawPeaks = value;
      }
    }

    private PeakList<Peak> _peakInIsolationWindow;

    public PeakList<Peak> PeakInIsolationWindow
    {
      get
      {
        if (null == _peakInIsolationWindow)
        {
          _peakInIsolationWindow = new PeakList<Peak>();
        }
        return _peakInIsolationWindow;
      }
      set
      {
        _peakInIsolationWindow = value;
      }
    }

    /// <summary>
    /// Reporter ion intensities
    /// </summary>
    private double[] _reporters;
    public double[] Reporters
    {
      get
      {
        if (_reporters == null)
        {
          _reporters = new double[PlexType.Channels.Count];
        }
        return _reporters;
      }
      set
      {
        _reporters = value;
      }
    }

    public void DetectReporter(double ppmTolerance)
    {
      if (_rawPeaks == null)
      {
        throw new Exception("Assign RawPeaks first!");
      }

      if (PlexType == null)
      {
        throw new Exception("Assign PlexType first!");
      }

      var map = PlexType.GetChannelMzToleranceMap(ppmTolerance);

      for (int i = 0; i < PlexType.Channels.Count; i++)
      {
        var peak = _rawPeaks.FindPeak(PlexType.Channels[i].Mz, map[PlexType.Channels[i]]).FindMaxIntensityPeak();
        if (peak == null)
        {
          Reporters[i] = IsobaricConsts.NULL_INTENSITY;
        }
        else
        {
          Reporters[i] = peak.Intensity;
        }
      }
    }

    public bool Valid { get; set; }

    public double ValidProbability { get; set; }

    public string FileScan
    {
      get
      {
        if (null == Scan)
        {
          return string.Empty;
        }
        else
        {
          return string.Format("{0},{1}", Experimental, Scan.Scan);
        }
      }
    }

    public double this[int index]
    {
      get
      {
        return Reporters[index];
      }
      set
      {
        Reporters[index] = value;
      }
    }

    public override string ToString()
    {
      return this.Experimental + "." + this.Scan.Scan.ToString();
    }

    public int PeakCount()
    {
      return this.Reporters.Count(m => m > IsobaricConsts.NULL_INTENSITY);
    }
  }

  public static class IsobaricItemExtension
  {
    public static IsobaricItem FindIsobaricItem(this IAnnotation ann)
    {
      if (ann.Annotations.ContainsKey(IsobaricConsts.TYPE))
      {
        return ann.Annotations[IsobaricConsts.TYPE] as IsobaricItem;
      }

      return null;
    }

    public static IsobaricItem FindOrCreateITsobaricItem(this IAnnotation ann)
    {
      if (ann.Annotations.ContainsKey(IsobaricConsts.TYPE))
      {
        return ann.Annotations[IsobaricConsts.TYPE] as IsobaricItem;
      }
      else
      {
        var result = new IsobaricItem();
        ann.Annotations[IsobaricConsts.TYPE] = result;
        return result;
      }
    }

    public static void SetIsobaricItem(this IAnnotation ann, IsobaricItem item)
    {
      ann.Annotations[IsobaricConsts.TYPE] = item;
    }

    public static void RemoveIsobaricItem(this IAnnotation ann)
    {
      if (ann.Annotations.ContainsKey(IsobaricConsts.TYPE))
      {
        ann.Annotations.Remove(IsobaricConsts.TYPE);
      }
    }
  }
}
