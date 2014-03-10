using RCPA.Proteomics.Summary;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace RCPA.Proteomics.Quantification
{
  public interface IGetRatioIntensity
  {
    string RatioKey { get; }

    string ReferenceKey { get; }

    string SampleKey { get; }

    bool HasRatio(IAnnotation si);

    double GetRatio(IAnnotation si);

    double GetReferenceIntensity(IAnnotation si);

    double GetSampleIntensity(IAnnotation si);
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