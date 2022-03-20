using RCPA.Proteomics.Spectrum;
using System;
using System.Collections.Generic;
using System.Linq;

namespace RCPA.Proteomics.Quantification.ITraq
{
  public enum IsobaricType { PLEX4, PLEX8, TMT6 };

  /// <summary>
  /// 对应于一个peptide的isobaric信息。
  /// </summary>
  public class IsobaricItem
  {
    public IsobaricItem()
    {
      this.PlexType = IsobaricType.PLEX4;
      this.ScanMode = string.Empty;
      this.Valid = true;
      this.ValidProbability = 1;
      this.PrecursorPercentage = 1.0;
    }

    private IsobaricItemImpl _item;

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

    private IsobaricType _plexType;

    public IsobaricType PlexType
    {
      get
      {
        return _plexType;
      }
      set
      {
        if (_plexType != value || _item == null)
        {
          _plexType = value;
          _item = _plexType.CreateItem(this);
        }
      }
    }

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

    public IsobaricDefinition Definition
    {
      get
      {
        return _item.Definition;
      }
    }

    public double[] ObservedIons()
    {
      return _item.ObservedIons();
    }

    public double this[int index]
    {
      get
      {
        return _item[index];
      }
      set
      {
        _item[index] = value;
      }
    }

    public int PeakCount()
    {
      return _item.PeakCount();
    }

    public void DevideIntensityByInjectionTime()
    {
      _item.DevideIntensityByInjectionTime();
    }

    public void MultiplyIntensityByInjectionTime()
    {
      _item.MultiplyIntensityByInjectionTime();
    }

    public override string ToString()
    {
      return this.Experimental + "." + this.Scan.Scan.ToString();
    }
  }

  public class IsobaricResult : List<IsobaricItem>
  {
    public IsobaricResult() { }

    public IsobaricResult(IEnumerable<IsobaricItem> items)
      : base(items)
    { }

    public Dictionary<string, Dictionary<int, IsobaricItem>> ToExperimentalScanDictionary()
    {
      return this.ToDoubleDictionary((m => m.Experimental), (m => m.Scan.Scan));
    }

    public bool HasIonInjectionTime()
    {
      return this.Count > 0 && this[0].Scan != null && this[0].Scan.IonInjectionTime != 0.0;
    }

    public IsobaricType PlexType
    {
      get
      {
        if (this.Count == 0)
        {
          return IsobaricType.PLEX4;
        }
        return this[0].PlexType;
      }
    }

    public String Mode { get; set; }
  }

  public static class IsobaricItemExtension
  {
    public static IsobaricItem FindIsobaricItem(this IAnnotation ann)
    {
      if (ann.Annotations.ContainsKey(ITraqConsts.ITRAQ_TYPE))
      {
        return ann.Annotations[ITraqConsts.ITRAQ_TYPE] as IsobaricItem;
      }

      return null;
    }

    public static IsobaricItem FindOrCreateITsobaricItem(this IAnnotation ann)
    {
      if (ann.Annotations.ContainsKey(ITraqConsts.ITRAQ_TYPE))
      {
        return ann.Annotations[ITraqConsts.ITRAQ_TYPE] as IsobaricItem;
      }
      else
      {
        var result = new IsobaricItem();
        ann.Annotations[ITraqConsts.ITRAQ_TYPE] = result;
        return result;
      }
    }

    public static void SetIsobaricItem(this IAnnotation ann, IsobaricItem item)
    {
      ann.Annotations[ITraqConsts.ITRAQ_TYPE] = item;
    }

    public static void RemoveIsobaricItem(this IAnnotation ann)
    {
      if (ann.Annotations.ContainsKey(ITraqConsts.ITRAQ_TYPE))
      {
        ann.Annotations.Remove(ITraqConsts.ITRAQ_TYPE);
      }
    }

    public static IsobaricType Find(string isobaricTypeName)
    {
      var types = EnumUtils.EnumToArray<IsobaricType>();
      return types.FirstOrDefault(m => m.ToString().Equals(isobaricTypeName));
    }
  }
}
