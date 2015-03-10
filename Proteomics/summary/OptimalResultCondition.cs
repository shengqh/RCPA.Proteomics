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
      this._precursorCharge = precursorCharge;
      this._missCleavageSiteCount = missCleavageSiteCount;
      this._numProteaseTermini = numberOfProteaseTerminal;
      this._modificationCount = modificationCount;
      this.Classification = classification;
      this.MergedConditions = new List<OptimalResultCondition>();
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

    private int _precursorCharge;

    public int PrecursorCharge
    {
      get { return _precursorCharge; }
    }

    private int _missCleavageSiteCount;

    public int MissCleavageSiteCount
    {
      get { return _missCleavageSiteCount; }
    }

    private int _numProteaseTermini;

    public int NumProteaseTermini
    {
      get { return _numProteaseTermini; }
    }

    private int _modificationCount;

    public int ModificationCount
    {
      get { return _modificationCount; }
    }

    public static bool operator ==(OptimalResultCondition left, OptimalResultCondition right)
    {
      return left._precursorCharge == right._precursorCharge &&
        left._missCleavageSiteCount == right._missCleavageSiteCount &&
        left._numProteaseTermini == right._numProteaseTermini &&
        left._modificationCount == right._modificationCount &&
        left.Classification == right.Classification;
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
      result = 31 * result + _precursorCharge;
      result = 31 * result + _missCleavageSiteCount;
      result = 31 * result + _numProteaseTermini;
      result = 31 * result + _modificationCount;
      result = 31 * result + classification.GetHashCode();
      return result;
    }

    public override string ToString()
    {
      return MyConvert.Format("Classification={0}; Charge={1}; MissCleavage={2}; Modification={3}; NumProteaseTermini={4}",
        Classification,
              _precursorCharge,
              _missCleavageSiteCount,
              _modificationCount,
              _numProteaseTermini);
    }

    public string ChargeString
    {
      get { return MyConvert.Format("{0}{1}", this._precursorCharge, this._precursorCharge == 3 ? "+" : ""); }
    }

    public string NumMissCleavageString
    {
      get { return MyConvert.Format("{0}{1}", this._missCleavageSiteCount, this._missCleavageSiteCount == 3 ? "+" : ""); }
    }

    public string ModificationCountString
    {
      get { return MyConvert.Format("{0}{1}", this._modificationCount, this._modificationCount == 2 ? "+" : ""); }
    }

    #region IComparable<OptimalResultCondition> Members

    public int CompareTo(OptimalResultCondition other)
    {
      int result = this.classification.CompareTo(other.classification);
      if (result != 0)
      {
        return result;
      }

      result = this._precursorCharge - other._precursorCharge;
      if (result != 0)
      {
        return result;
      }

      result = this._missCleavageSiteCount - other._missCleavageSiteCount;
      if (result != 0)
      {
        return result;
      }

      result = this._numProteaseTermini - other._numProteaseTermini;
      if (result != 0)
      {
        return result;
      }

      return this._modificationCount - other._modificationCount;
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
      sw.WriteLine("Classification\tCharge\tNumMissCleavage\tModification\tNumProteaseTermini\tScore\tExpectValue\tTargetCount\tDecoyCount");
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

        sw.WriteLine(MyConvert.Format("{0}\t{1}\t{2}\t{3}\t{4}\t{5:E3}\t{6:E3}\t{7}\t{8}",
          cond.Classification,
          cond.ChargeString,
          cond.NumMissCleavageString,
          cond.ModificationCountString,
          cond.NumProteaseTermini,
          acceptedScore,
          acceptedEvalue,
          targetCount,
          decoyCount));
      }
      sw.WriteLine();
    }
  }
}
