using System.Collections.Generic;

namespace RCPA.Proteomics.Quantification
{
  public interface IGetRatioIntensity
  {
    string RatioKey { get; }

    string ReferenceKey { get; }

    string SampleKey { get; }

    string PValueKey { get; }

    bool HasRatio(IAnnotation si);

    double GetRatio(IAnnotation si);

    double GetReferenceIntensity(IAnnotation si);

    double GetSampleIntensity(IAnnotation si);

    double GetPValue(IAnnotation si);

    void LoadFromAnnotation(IAnnotation ann, LinearRegressionRatioResult lrrr);

    void SaveToAnnotation(IAnnotation ann, LinearRegressionRatioResult lrrr);

    void RemoveFromAnnotation(IAnnotation ann);
  }

  public abstract class AbstractGetRatioIntensity : IGetRatioIntensity
  {
    public abstract string RatioKey { get; }

    public abstract string ReferenceKey { get; }

    public abstract string SampleKey { get; }

    public abstract string PValueKey { get; }

    public abstract bool HasRatio(IAnnotation si);

    public abstract double GetRatio(IAnnotation si);

    public abstract double GetReferenceIntensity(IAnnotation si);

    public abstract double GetSampleIntensity(IAnnotation si);

    public abstract double GetPValue(IAnnotation si);

    public virtual void LoadFromAnnotation(IAnnotation ann, LinearRegressionRatioResult lrrr)
    {
      if (ann.IsEnabled(false))
      {
        if (ann.Annotations[this.RatioKey] is LinearRegressionRatioResult)
        {
          var oldLrrr = ann.Annotations[this.RatioKey] as LinearRegressionRatioResult;
          lrrr.CopyFrom(oldLrrr);
        }
        else
        {
          lrrr.Ratio = double.Parse(ann.Annotations[this.RatioKey].ToString());
        }
        lrrr.ReferenceIntensity = double.Parse(ann.Annotations[this.ReferenceKey].ToString());
        lrrr.SampleIntensity = double.Parse(ann.Annotations[this.SampleKey].ToString());
        lrrr.PValue = double.Parse(ann.Annotations[this.PValueKey].ToString());
      }
      else
      {
        lrrr.Ratio = 1.0;
        lrrr.ReferenceIntensity = 0.0;
        lrrr.SampleIntensity = 0.0;
        lrrr.PValue = 1.0;
      }
    }

    public virtual void SaveToAnnotation(IAnnotation ann, LinearRegressionRatioResult lrrr)
    {
      ann.SetEnabled(true);
      ann.Annotations[this.RatioKey] = lrrr;
      ann.Annotations[this.ReferenceKey] = string.Format("{0:0.0}", lrrr.ReferenceIntensity);
      ann.Annotations[this.SampleKey] = string.Format("{0:0.0}", lrrr.SampleIntensity);
      ann.Annotations[this.PValueKey] = string.Format("{0:0.##E+0}", lrrr.PValue);
    }

    public virtual void RemoveFromAnnotation(IAnnotation ann)
    {
      ann.SetEnabled(false);
      ann.Annotations.Remove(this.RatioKey);
      ann.Annotations.Remove(this.ReferenceKey);
      ann.Annotations.Remove(this.SampleKey);
      ann.Annotations.Remove(this.PValueKey);
    }
  }

  public static class GetRatioIntensityExtension
  {
    public static double[][] ConvertToArray<T>(this IGetRatioIntensity func, IEnumerable<T> anns) where T : IAnnotation
    {
      var refs = new List<double>();
      var samples = new List<double>();

      foreach (IAnnotation ann in anns)
      {
        refs.Add(func.GetReferenceIntensity(ann));
        samples.Add(func.GetSampleIntensity(ann));
      }

      var result = new double[2][];
      result[0] = refs.ToArray();
      result[1] = samples.ToArray();

      return result;
    }
  }

}