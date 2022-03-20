using System.Collections.Generic;
using System.Linq;

namespace RCPA.Proteomics.Quantification.Labelfree
{
  public class LabelfreeValue
  {
    public LabelfreeValue()
    {
    }

    public LabelfreeValue(int count, double value)
    {
      this.Count = count;
      this.Value = value;
    }

    public int Count { get; set; }
    public double Value { get; set; }
  }

  public class LabelfreeResult : Dictionary<string, LabelfreeValue>
  {
    public bool HasCountLargerThan(int count)
    {
      return this.Values.Any(m => m.Count > count);
    }
  }

  public static class LabelfreeResultExtension
  {
    public static string KEY = "LF_COUNT";

    public static bool HasLabelfreeResult(this IAnnotation ann)
    {
      return ann.Annotations.ContainsKey(KEY);
    }

    public static LabelfreeResult GetLabelfreeResult(this IAnnotation ann)
    {
      if (ann.Annotations.ContainsKey(KEY))
      {
        return ann.Annotations[KEY] as LabelfreeResult;
      }

      return null;
    }

    public static void SetLabelfreeResult(this IAnnotation ann, LabelfreeResult item)
    {
      ann.Annotations[KEY] = item;
    }
  }
}
