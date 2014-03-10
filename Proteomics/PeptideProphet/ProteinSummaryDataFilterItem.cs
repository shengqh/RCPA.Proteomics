using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RCPA.Proteomics.PeptideProphet
{
  public class ProteinSummaryDataFilterItem
  {
    public double MinProbability { get; set; }
    public double Sensitivity { get; set; }
    public double FalsePositiveErrorRate { get; set; }
    public double PredictedNumCorrect { get; set; }
    public double PredictedNumIncorrect { get; set; }
  }

  public class ProteinSummaryDataFilterList : List<ProteinSummaryDataFilterItem>
  { }

  public static class ProteinSummaryDataFilterListExtension
  {
    public static string KEY = "ProteinSummaryDataFilterList";

    public static ProteinSummaryDataFilterList GetProteinSummaryDataFilterList(this IAnnotation ann)
    {
      if (ann.Annotations.ContainsKey(KEY))
      {
        return ann.Annotations[KEY] as ProteinSummaryDataFilterList;
      }

      return null;
    }

    public static void SetProteinSummaryDataFilterList(this IAnnotation ann, ProteinSummaryDataFilterList value)
    {
      ann.Annotations[KEY] = value;
    }
  }
}
