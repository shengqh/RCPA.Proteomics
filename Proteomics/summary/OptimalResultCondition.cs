using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Linq;
using RCPA.Proteomics.Summary.Uniform;

namespace RCPA.Proteomics
{
  public class OptimalResultCondition : IComparable<OptimalResultCondition>
  {
    private const string DEFAULT_CLASSIFICATION = "DEFAULT";

    public List<OptimalResultCondition> MergedConditions { get; private set; }

    public OptimalResultCondition(int precursorCharge, int missCleavageSiteCount, int numberOfProteaseTerminal, int modificationCount, string classification = DEFAULT_CLASSIFICATION)
    {
      this.PrecursorCharge = precursorCharge;
      this.MissCleavageSiteCount = missCleavageSiteCount;
      this.NumProteaseTermini = numberOfProteaseTerminal;
      this.ModificationCount = modificationCount;
      this.Classification = classification;
      this.MergedConditions = new List<OptimalResultCondition>();
    }

    public OptimalResultCondition(OptimalResultCondition source)
    {
      this.PrecursorCharge = source.PrecursorCharge;
      this.MissCleavageSiteCount = source.MissCleavageSiteCount;
      this.NumProteaseTermini = source.NumProteaseTermini;
      this.ModificationCount = source.ModificationCount;
      this.Classification = source.Classification;
      this.MergedConditions = new List<OptimalResultCondition>();
      foreach (var orc in source.MergedConditions)
      {
        this.MergedConditions.Add(new OptimalResultCondition(orc));
      }
    }

    private string classification;

    public string Classification
    {
      get
      {
        return classification;
      }
      set
      {
        if (value == null || value.Length == 0)
        {
          classification = DEFAULT_CLASSIFICATION;
        }
        else
        {
          classification = value;
        }
      }
    }

    public int PrecursorCharge { get; private set; }

    public int MissCleavageSiteCount{get;private set;}

    public int NumProteaseTermini { get; private set; }

    public int ModificationCount { get; private set; }

    public int ProteinTag { get; private set; }

    public static bool operator ==(OptimalResultCondition left, OptimalResultCondition right)
    {
      return left.PrecursorCharge == right.PrecursorCharge &&
        left.MissCleavageSiteCount == right.MissCleavageSiteCount &&
        left.NumProteaseTermini == right.NumProteaseTermini &&
        left.ModificationCount == right.ModificationCount &&
        left.Classification == right.Classification &&
        left.ProteinTag == right.ProteinTag;
    }

    public static bool operator !=(OptimalResultCondition left, OptimalResultCondition right)
    {
      return !(left == right);
    }

    public override bool Equals(object obj)
    {
      if (!(obj is OptimalResultCondition))
      {
        return false;
      }

      return this == (obj as OptimalResultCondition);
    }

    public override int GetHashCode()
    {
      int result = 7;
      result = 31 * result + PrecursorCharge;
      result = 31 * result + MissCleavageSiteCount;
      result = 31 * result + NumProteaseTermini;
      result = 31 * result + ModificationCount;
      result = 31 * result + ProteinTag;
      result = 31 * result + classification.GetHashCode();
      return result;
    }

    public override string ToString()
    {
      return MyConvert.Format("Classification={0}; Charge={1}; MissCleavage={2}; Modification={3}; NumProteaseTermini={4}; ProteinTag={5}",
        Classification,
              PrecursorCharge,
              MissCleavageSiteCount,
              ModificationCount,
              NumProteaseTermini,
              ProteinTag);
    }

    public string ChargeString
    {
      get { return MyConvert.Format("{0}{1}", this.PrecursorCharge, this.PrecursorCharge == 3 ? "+" : ""); }
    }

    public string NumMissCleavageString
    {
      get { return MyConvert.Format("{0}{1}", this.MissCleavageSiteCount, this.MissCleavageSiteCount == 3 ? "+" : ""); }
    }

    public string ModificationCountString
    {
      get { return MyConvert.Format("{0}{1}", this.ModificationCount, this.ModificationCount == 2 ? "+" : ""); }
    }

    #region IComparable<OptimalResultCondition> Members

    public int CompareTo(OptimalResultCondition other)
    {
      int result = this.classification.CompareTo(other.classification);
      if (result != 0)
      {
        return result;
      }

      result = this.PrecursorCharge - other.PrecursorCharge;
      if (result != 0)
      {
        return result;
      }

      result = this.MissCleavageSiteCount - other.MissCleavageSiteCount;
      if (result != 0)
      {
        return result;
      }

      result = this.NumProteaseTermini - other.NumProteaseTermini;
      if (result != 0)
      {
        return result;
      }

      return this.ModificationCount - other.ModificationCount;
    }

    #endregion

    public static bool HasClassification(List<OptimalResultCondition> conds)
    {
      bool result = false;
      foreach (OptimalResultCondition cond in conds)
      {
        if (cond.Classification != null && cond.Classification.Length > 0 && !cond.Classification.Equals(DEFAULT_CLASSIFICATION))
        {
          result = true;
          break;
        }
      }
      return result;
    }
  }

  public static class OptimalResultConditionUtils
  {
    public static void WriteSpectrumBin<T>(StreamWriter sw, T source, Func<T, List<OptimalResultCondition>> condFunc, Func<T, OptimalResultCondition, OptimalItem> acceptCount)
    {
      sw.WriteLine("Classification\tCharge\tNumMissCleavage\tNumProteaseTermini\tModification\tProteinTag\tScore\tExpectValue\tTargetCount\tDecoyCount");
      var conds = condFunc(source);
      conds.Sort();
      foreach (OptimalResultCondition cond in conds)
      {
        var item = acceptCount(source, cond);
        int targetCount = 0;
        int decoyCount = 0;
        double acceptedScore = 0.0;
        double acceptedEvalue = 0.0;
        if (item != null)
        {
          decoyCount = item.Spectra.Count(m => m.FromDecoy);
          targetCount = item.Spectra.Count - decoyCount;
          acceptedScore = item.Result.Score;
          acceptedEvalue = item.Result.ExpectValue;
        }

        sw.WriteLine(MyConvert.Format("{0}\t{1}\t{2}\t{3}\t{4}\t{5}\t{6:E3}\t{7:E3}\t{8}\t{9}",
          cond.Classification,
          cond.ChargeString,
          cond.NumMissCleavageString,
          cond.NumProteaseTermini,
          cond.ModificationCountString,
          cond.ProteinTag,
          acceptedScore,
          acceptedEvalue,
          targetCount,
          decoyCount));
      }
      sw.WriteLine();
    }
  }
}
