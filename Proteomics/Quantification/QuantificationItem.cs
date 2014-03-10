using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace RCPA.Proteomics.Quantification
{
  public class QuantificationItem : QuantificationItemBase
  {
    public const string KEY_RATIO = "S_RATIO";
    public const string KEY_ENABLED = "Enabled";
    public const string KEY_CORRELATION = "S_CORR";
    public const string KEY_REFERENCE_INTENSITY = "INT_REF";
    public const string KEY_SAMPLE_INTENSITY = "INT_SAM";
    public const string KEY_FILE = "S_FILE";
    public const string KEY_SCANS = "S_SCANS";

    public double SampleIntensity { get; set; }

    public double ReferenceIntensity { get; set; }

    public double Correlation { get; set; }

    public string Filename { get; set; }

    public int ScanCount { get; set; }

    public string SampleIntensityStr
    {
      get
      {
        if (!HasRatio)
        {
          return string.Empty;
        }

        return MyConvert.Format("{0:0.0}", SampleIntensity);
      }
      set
      {
        if (!string.IsNullOrEmpty(value))
        {
          SampleIntensity = MyConvert.ToDouble(value);
        }
        else
        {
          SampleIntensity = 0;
        }
      }
    }

    public string ReferenceIntensityStr
    {
      get
      {
        if (!HasRatio)
        {
          return string.Empty;
        }

        return MyConvert.Format("{0:0.0}", ReferenceIntensity);
      }
      set
      {
        if (!string.IsNullOrEmpty(value))
        {
          ReferenceIntensity = MyConvert.ToDouble(value);
        }
        else
        {
          ReferenceIntensity = 0;
        }
      }
    }

    public string CorrelationStr
    {
      get
      {
        if (!HasRatio)
        {
          return string.Empty;
        }

        return MyConvert.Format("{0:0.0000}", Correlation);
      }
      set
      {
        if (!string.IsNullOrEmpty(value))
        {
          Correlation = MyConvert.ToDouble(value);
        }
        else
        {
          Correlation = 0;
        }

      }
    }

    public string ScanCountStr
    {
      get
      {
        if (!HasRatio)
        {
          return string.Empty;
        }

        return ScanCount.ToString();
      }
      set
      {
        if (!string.IsNullOrEmpty(value))
        {
          ScanCount = Convert.ToInt32(value);
        }
        else
        {
          ScanCount = 0;
        }
      }
    }
  }

  public static class QuantificationItemExtention
  {
    public static void SetQuantificationItem(this IAnnotation ann, QuantificationItem value)
    {
      ann.Annotations[QuantificationItem.KEY_RATIO] = value;
    }

    public static QuantificationItem GetQuantificationItem(this IAnnotation ann)
    {
      if (ann.Annotations.ContainsKey(QuantificationItem.KEY_RATIO))
      {
        return ann.Annotations[QuantificationItem.KEY_RATIO] as QuantificationItem;
      }

      return null;
    }

    public static QuantificationItem GetOrCreateQuantificationItem(this IAnnotation ann)
    {
      if (ann.Annotations.ContainsKey(QuantificationItem.KEY_RATIO))
      {
        return ann.Annotations[QuantificationItem.KEY_RATIO] as QuantificationItem;
      }

      var result = new QuantificationItem();
      ann.Annotations[QuantificationItem.KEY_RATIO] = result;

      return result;
    }

    public static void ClearQuantificationItem(this IAnnotation ann)
    {
      if (ann.Annotations.ContainsKey(QuantificationItem.KEY_RATIO))
      {
        ann.Annotations.Remove(QuantificationItem.KEY_RATIO);
      }
    }

    public static string GetRatioFile(this IAnnotation ann, string detailDir)
    {
      var item = ann.GetQuantificationItem();

      if (null == item || string.IsNullOrEmpty(item.Filename))
      {
        return null;
      }

      return new FileInfo(detailDir + "\\" + item.Filename).FullName;
    }

    public static bool HasRatio(this IAnnotation ann)
    {
      var item = ann.GetQuantificationItem();

      return null != item && item.HasRatio;
    }

    public static bool GetRatioEnabled(this IAnnotation ann)
    {
      var item = ann.GetQuantificationItem();

      return null != item && item.Enabled;
    }

    public static void SetRatioEnabled(this IAnnotation ann, bool value)
    {
      ann.GetOrCreateQuantificationItem().Enabled = value;
    }

    public static void InitializeRatioEnabled(this IAnnotation ann)
    {
      ann.SetRatioEnabled(ann.HasRatio());
    }
  }
}

