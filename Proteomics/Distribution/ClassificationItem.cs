using RCPA.Proteomics.Summary;
using RCPA.Proteomics.Utils;
using System;
using System.Collections.Generic;
using System.Linq;

namespace RCPA.Proteomics.Distribution
{
  public class CalculationItem
  {
    private Func<Count, int> GetCount;

    public object Key { get; set; }

    public IEnumerable<IIdentifiedPeptide> Peptides { get; set; }

    public double TheoreticalValue { get; set; }

    public double ExperimentValue { get; set; }

    private PeptideFilterType filterType;
    public PeptideFilterType FilterType
    {
      get { return filterType; }
      set
      {
        filterType = value;
        if (value == PeptideFilterType.PeptideCount)
        {
          GetCount = (m => m.PeptideCount);
        }
        else
        {
          GetCount = (m => m.UniquePeptideCount);
        }
      }
    }

    private Dictionary<string, Count> classifications = new Dictionary<string, Count>();

    public Dictionary<string, Count> Classifications
    {
      get { return classifications; }
    }

    public CalculationItem()
    {
      FilterType = PeptideFilterType.PeptideCount;
    }

    public int GetClassifiedCount(string classifiedName)
    {
      return GetCount(classifications[classifiedName]);
    }

    /**
     * 根据给定的sphc、pephits，以及classifiedNames进行统计各个classifiedNames对应的肽段个数。
     */
    public void ClassifyPeptideHit(Func<IIdentifiedPeptide, string> classifyFunc, string[] classifiedNames)
    {
      classifications = new Dictionary<string, Count>();

      if (classifiedNames != null)
      {
        foreach (string name in classifiedNames)
        {
          classifications[name] = new Count();
        }
      }

      var s = from p in Peptides
              let key = classifyFunc(p)
              let seq = PeptideUtils.GetPureSequence(p.Sequence)
              group seq by key into ss
              select new { Key = ss.Key, PeptideCount = ss.Count(), UniquePeptideCount = ss.Distinct().Count() };

      foreach (var item in s)
      {
        classifications[item.Key] = new Count()
        {
          PeptideCount = item.PeptideCount,
          UniquePeptideCount = item.UniquePeptideCount
        };
      }
    }

    public void ClassifyPeptideHit(Func<IIdentifiedPeptide, string> classifyFunc)
    {
      ClassifyPeptideHit(classifyFunc, null);
    }
  }
}
