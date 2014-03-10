using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RCPA.Proteomics.MaxQuant
{
  public class MaxQuantItem
  {
    public string Name { get; set; }

    public double Prob
    {
      get
      {
        double result;
        if (MyConvert.TryParse(this.LocalizationProb, out result))
        {
          return result;
        }
        return 1.0;
      }
    }

    public string Experiment { get; set; }
    public string UniqueExperiment { get; set; }
    public string LocalizationProb { get; set; }
    public string ScoreDiff { get; set; }
    public string PEP { get; set; }
    public string MascotScore { get; set; }
    public string PTMScore { get; set; }

    public string Ratio { get; set; }
    public string Ratio_1 { get; set; }
    public string Ratio_2 { get; set; }
    public string Ratio_3 { get; set; }
    public string Ratio_Norm { get; set; }
    public string Ratio_Norm_1 { get; set; }
    public string Ratio_Norm_2 { get; set; }
    public string Ratio_Norm_3 { get; set; }
    public string Ratio_Sign_A { get; set; }
    public string Ratio_Sign_B { get; set; }

    public string Ratio_Unmod { get; set; }
    public string Ratio_Localized { get; set; }
    public string Ratio_Nmods { get; set; }
    public string Ratio_Variability { get; set; }
    public string Ratio_Count { get; set; }
    public string OccupancyL { get; set; }
    public string OccupancyH { get; set; }

    public string Intensity { get; set; }
    public string IntensityL { get; set; }
    public string IntensityH { get; set; }

    public double RetentionTime { get; set; }
    public double RetentionLength { get; set; }
  }

  public class MaxQuantItemList : List<MaxQuantItem>
  {
    public MaxQuantItem BestItem { get; set; }

    public MaxQuantItemList()
    { }

    public int QuantifiedExperimentCount
    {
      get
      {
        return (from m in this
                where m.Ratio_Norm != string.Empty
                select m).Count();
      }
    }

    public MaxQuantItem MinLocalizationProbItem
    {
      get
      {
        if (this.Count == 0)
        {
          return null;
        }

        var result = this[0];
        var prob = result.Prob;
        for (int i = 1; i < this.Count; i++)
        {
          if (this[i].Prob < prob)
          {
            result = this[i];
            prob = result.Prob;
          }
        }

        return result;
      }
    }

    public int MinLocalizationProbIndex
    {
      get
      {
        var item = MinLocalizationProbItem;
        if (item == null)
        {
          return -1;
        }

        return this.IndexOf(item);
      }
    }

    public List<string> GetDatasetNames()
    {
      return (from item in this
              select item.Name).Distinct().ToList();
    }
  }

  public static class MaxQuantItemListExtention
  {
    public static string KEY = "Ratio H/L";

    public static void SetMaxQuantItemList(this IAnnotation ann, MaxQuantItemList value)
    {
      ann.Annotations[KEY] = value;
    }

    public static MaxQuantItemList GetMaxQuantItemList(this IAnnotation ann)
    {
      if (ann.Annotations.ContainsKey(KEY))
      {
        return ann.Annotations[KEY] as MaxQuantItemList;
      }

      return null;
    }
  }
}

