using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace RCPA.Proteomics.MaxQuant
{
  public class MaxQuantItemConverterDefinition
  {
    public Regex DatasetRegex { get; private set; }
    public Func<MaxQuantItem, string> GetValue;
    public Action<MaxQuantItem, string> SetValue;

    public MaxQuantItemConverterDefinition(string pattern, Func<MaxQuantItem, string> GetValue, Action<MaxQuantItem, string> SetValue)
    {
      this.DatasetRegex = new Regex(pattern);
      this.GetValue = GetValue;
      this.SetValue = SetValue;
    }
  }

  public class MaxQuantItemListConverter<T> : AbstractPropertyConverter<T> where T : IAnnotation
  {
    private string name;
    private string dsName;

    private Func<MaxQuantItem, string> GetValue;

    private Action<MaxQuantItem, string> SetValue;

    public MaxQuantItemListConverter(string name, string dsName, Func<MaxQuantItem, string> getValue, Action<MaxQuantItem, string> setValue)
    {
      this.name = name;
      this.dsName = dsName;
      this.GetValue = getValue;
      this.SetValue = setValue;
    }

    public override string Name
    {
      get { return name; }
    }

    public override string GetProperty(T t)
    {
      var lst = t.GetMaxQuantItemList();

      if (lst == null)
      {
        return "";
      }

      MaxQuantItem item;
      if (dsName.Length == 0)
      {
        if (lst.BestItem == null)
        {
          lst.BestItem = lst.MinLocalizationProbItem;
        }
        item = lst.BestItem;
      }
      else
      {
        item = FindItem(lst);
      }

      return GetValue(item);
    }

    protected MaxQuantItem FindItem(MaxQuantItemList lst)
    {
      return lst.Find(m => m.Name.Equals(dsName));
    }

    public override void SetProperty(T t, string value)
    {
      var lst = t.GetMaxQuantItemList();

      if (lst == null)
      {
        lst = new MaxQuantItemList();
        t.SetMaxQuantItemList(lst);
      }

      MaxQuantItem item;
      if (dsName.Length == 0)
      {
        if (lst.BestItem == null)
        {
          lst.BestItem = new MaxQuantItem();
        }
        item = lst.BestItem;
      }
      else
      {
        item = FindItem(lst);
        if (item == null)
        {
          item = new MaxQuantItem()
          {
            Name = dsName
          };
          lst.Add(item);
        }
      }

      SetValue(item, value);
    }

    public override string ToString()
    {
      return Name;
    }
  }

  public class MaxQuantItemListRatioConverter<T> : MaxQuantItemListConverter<T> where T : IAnnotation
  {
    public MaxQuantItemListRatioConverter()
      : base("Ratio H/L", "", m => m.Ratio, (m, value) => m.Ratio = value)
    { }

    public override List<IPropertyConverter<T>> GetRelativeConverter(string header, char delimiter)
    {
      var normalized = this.Name + " Normalized";

      var parts = header.Split(delimiter);

      var dsNames = (from p in parts
                     where p.StartsWith(normalized)
                     let ds = p.Substring(normalized.Length).Trim()
                     select ds).ToList();

      return AddDataConverters(dsNames);
    }

    public override List<IPropertyConverter<T>> GetRelativeConverter(List<T> items)
    {
      var dsNames = (from item in items
                     let mqil = item.GetMaxQuantItemList()
                     where mqil != null
                     from ds in mqil.GetDatasetNames()
                     select ds).Distinct().ToList();

      if (!dsNames.Contains(""))
      {
        dsNames.Insert(0, "");
      }

      return AddDataConverters(dsNames);
    }

    private List<IPropertyConverter<T>> AddDataConverters(List<string> dsNames)
    {
      var result = new List<IPropertyConverter<T>>();

      foreach (var ds in dsNames)
      {
        AddDatasetConverter(result, ds);
      }

      return result;
    }

    private void AddDatasetConverter(List<IPropertyConverter<T>> result, string ds)
    {
      var added = (ds.Length == 0) ? "" : " " + ds;
      result.Add(new MaxQuantItemListConverter<T>("Experiment" + added, ds, m => m.Experiment, (m, value) => m.Experiment = value));
      result.Add(new MaxQuantItemListConverter<T>("Unique Experiment" + added, ds, m => m.UniqueExperiment, (m, value) => m.UniqueExperiment = value));
      result.Add(new MaxQuantItemListConverter<T>("Localization Prob" + added, ds, m => m.LocalizationProb, (m, value) => m.LocalizationProb = value));
      result.Add(new MaxQuantItemListConverter<T>("Score Diff" + added, ds, m => m.ScoreDiff, (m, value) => m.ScoreDiff = value));
      result.Add(new MaxQuantItemListConverter<T>("PEP" + added, ds, m => m.PEP, (m, value) => m.PEP = value));
      result.Add(new MaxQuantItemListConverter<T>("Mascot Score" + added, ds, m => m.MascotScore, (m, value) => m.MascotScore = value));
      result.Add(new MaxQuantItemListConverter<T>("PTM Score" + added, ds, m => m.PTMScore, (m, value) => m.PTMScore = value));
      result.Add(new MaxQuantItemListConverter<T>("Ratio H/L" + added, ds, m => m.Ratio, (m, value) => m.Ratio = value));
      result.Add(new MaxQuantItemListConverter<T>("Ratio H/L" + added + "_1", ds, m => m.Ratio_1, (m, value) => m.Ratio_1 = value));
      result.Add(new MaxQuantItemListConverter<T>("Ratio H/L" + added + "_2", ds, m => m.Ratio_2, (m, value) => m.Ratio_2 = value));
      result.Add(new MaxQuantItemListConverter<T>("Ratio H/L" + added + "_3", ds, m => m.Ratio_3, (m, value) => m.Ratio_3 = value));
      result.Add(new MaxQuantItemListConverter<T>("Ratio H/L Normalized" + added, ds, m => m.Ratio_Norm, (m, value) => m.Ratio_Norm = value));
      result.Add(new MaxQuantItemListConverter<T>("Ratio H/L Normalized" + added + "_1", ds, m => m.Ratio_Norm_1, (m, value) => m.Ratio_Norm_1 = value));
      result.Add(new MaxQuantItemListConverter<T>("Ratio H/L Normalized" + added + "_2", ds, m => m.Ratio_Norm_2, (m, value) => m.Ratio_Norm_2 = value));
      result.Add(new MaxQuantItemListConverter<T>("Ratio H/L Normalized" + added + "_3", ds, m => m.Ratio_Norm_3, (m, value) => m.Ratio_Norm_3 = value));
      result.Add(new MaxQuantItemListConverter<T>("Ratio H/L Significance(A)" + added, ds, m => m.Ratio_Sign_A, (m, value) => m.Ratio_Sign_A = value));
      result.Add(new MaxQuantItemListConverter<T>("Ratio H/L Significance(B)" + added, ds, m => m.Ratio_Sign_B, (m, value) => m.Ratio_Sign_B = value));
      result.Add(new MaxQuantItemListConverter<T>("Ratio H/L Unmod. Pep." + added, ds, m => m.Ratio_Unmod, (m, value) => m.Ratio_Unmod = value));
      result.Add(new MaxQuantItemListConverter<T>("Ratio H/L Localized" + added, ds, m => m.Ratio_Localized, (m, value) => m.Ratio_Localized = value));
      result.Add(new MaxQuantItemListConverter<T>("Ratio H/L Nmods" + added, ds, m => m.Ratio_Nmods, (m, value) => m.Ratio_Nmods = value));
      result.Add(new MaxQuantItemListConverter<T>("Ratio H/L Variability [%]" + added, ds, m => m.Ratio_Variability, (m, value) => m.Ratio_Variability = value));
      result.Add(new MaxQuantItemListConverter<T>("Ratio H/L Count" + added, ds, m => m.Ratio_Count, (m, value) => m.Ratio_Count = value));
      result.Add(new MaxQuantItemListConverter<T>("Occupancy L" + added, ds, m => m.OccupancyL, (m, value) => m.OccupancyL = value));
      result.Add(new MaxQuantItemListConverter<T>("Occupancy H" + added, ds, m => m.OccupancyH, (m, value) => m.OccupancyH = value));
      result.Add(new MaxQuantItemListConverter<T>("Intensity" + added, ds, m => m.Intensity, (m, value) => m.Intensity = value));
      result.Add(new MaxQuantItemListConverter<T>("Intensity L" + added, ds, m => m.IntensityL, (m, value) => m.IntensityL = value));
      result.Add(new MaxQuantItemListConverter<T>("Intensity H" + added, ds, m => m.IntensityH, (m, value) => m.IntensityH = value));
    }
  }
}