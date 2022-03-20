using System;
using System.Collections.Generic;
using System.Linq;

namespace RCPA.Proteomics.Summary.Uniform
{
  public class OptimalItem
  {
    public OptimalItem()
    {
      Condition = new OptimalResultCondition(-1, -1, -1, -1);
      Spectra = new List<IIdentifiedSpectrum>();
      Result = new OptimalResult();
    }

    public OptimalResultCondition Condition { get; set; }

    public List<IIdentifiedSpectrum> Spectra { get; set; }

    public OptimalResult Result { get; set; }

    public void FilterByFdr(IOptimalResultCalculator calc, double fdrValue, bool hasDuplicatedSpectrum)
    {
      calc.FdrValue = fdrValue;
      this.Spectra = calc.Calculate(this.Spectra, this.Result, hasDuplicatedSpectrum);
    }

    public void KeepOptimalResultInSetOnly(HashSet<IIdentifiedSpectrum> spectra)
    {
      this.Spectra.RemoveAll(m => !spectra.Contains(m));
    }

    public void CalculateToleranceScore(IScoreFunction scoreFunc)
    {
      scoreFunc.SortSpectrum(this.Spectra);

      this.Result.Score = this.Spectra.Count > 0 ? scoreFunc.GetScore(this.Spectra.Last()) : 0.0;
    }

    public void CalculateFdr(IFalseDiscoveryRateCalculator calc)
    {
      int decoy = 0;
      int target = 0;

      HashSet<string> fileName = new HashSet<string>();
      foreach (var spectrum in this.Spectra)
      {
        if (fileName.Contains(spectrum.Query.FileScan.LongFileName))
        {
          continue;
        }

        fileName.Add(spectrum.Query.FileScan.LongFileName);

        if (spectrum.FromDecoy)
        {
          decoy++;
        }
        else
        {
          target++;
        }
      }
      this.Result.PeptideCountFromDecoyDB = decoy;
      this.Result.PeptideCountFromTargetDB = target;
      this.Result.FalseDiscoveryRate = calc.Calculate(decoy, target);
    }
  }

  public class Dataset
  {
    public double Fdr { get; set; }

    public int PSMPassedFixedCriteriaCount { get; set; }

    public IDatasetOptions Options { get; set; }

    public List<IIdentifiedSpectrum> Spectra { get; set; }

    public List<Tuple<string, List<OptimalItem>>> SavedOptimalResults { get; private set; }

    public List<OptimalItem> OptimalResults { get; set; }

    public HashSet<string> Experimentals { get; set; }

    public bool HasDuplicatedSpectrum { get; set; }

    public Dataset(IDatasetOptions options)
    {
      this.Options = options;
      this.Experimentals = new HashSet<string>();
      this.HasDuplicatedSpectrum = false;
      this.PSMPassedFixedCriteriaCount = 0;
      this.SavedOptimalResults = new List<Tuple<string, List<OptimalItem>>>();
    }

    public void InitExperimentals()
    {
      this.Experimentals = new HashSet<string>();
      foreach (var spectrum in this.Spectra)
      {
        this.Experimentals.Add(spectrum.Query.FileScan.Experimental);
      }
    }

    public void BuildSpectrumBin()
    {
      this.OptimalResults = Options.Parent.Classification.BuildSpectrumBin(this.Spectra);
    }

    public void FilterByFdr(double fdrValue)
    {
      var optimalCalc = Options.GetOptimalResultCalculator();
      this.OptimalResults.ForEach(m => m.FilterByFdr(optimalCalc, fdrValue, HasDuplicatedSpectrum));
    }

    public void BuildSpectrumBinAndFilterByFdr(double fdrValue)
    {
      this.BuildSpectrumBin();
      this.FilterByFdr(fdrValue);
    }

    public void PushCurrentOptimalResults(string title)
    {
      var or = new List<OptimalItem>();
      foreach (var item in this.OptimalResults)
      {
        var newitem = new OptimalItem()
        {
          Condition = new OptimalResultCondition(item.Condition),
          Result = new OptimalResult(item.Result),
          Spectra = new List<IIdentifiedSpectrum>()
        };
        or.Add(newitem);
      }
      this.SavedOptimalResults.Add(new Tuple<string, List<OptimalItem>>(title, or));
    }

    /// <summary>
    /// 获取筛选后的谱图列表。
    /// </summary>
    /// <returns></returns>
    public List<IIdentifiedSpectrum> GetOptimalSpectra()
    {
      List<IIdentifiedSpectrum> result = new List<IIdentifiedSpectrum>();

      OptimalResults.ForEach(m => result.AddRange(m.Spectra));

      return result;
    }

    /// <summary>
    /// 获取筛选后，去除相同谱图，假设为不同电荷谱图以后的谱图列表。
    /// </summary>
    /// <returns></returns>
    public List<IIdentifiedSpectrum> GetUnconflictedOptimalSpectra()
    {
      List<IIdentifiedSpectrum> result = GetOptimalSpectra();

      IdentifiedSpectrumUtils.FilterSameSpectrumWithDifferentCharge(result);

      return result;
    }

    public void RemoveConflictSpectrum(List<IIdentifiedSpectrum> conflicted)
    {
      if (conflicted.Count > 0)
      {
        IFalseDiscoveryRateCalculator calc = Options.Parent.FalseDiscoveryRate.GetFalseDiscoveryRateCalculator();

        var map = OptimalResults.ToDictionary(m => m.Condition);
        var bin = Options.Parent.Classification.BuildSpectrumBin(conflicted);
        foreach (var oi in bin)
        {
          var oldOi = map[oi.Condition];
          foreach (var s in oi.Spectra)
          {
            if (s.FromDecoy)
            {
              oldOi.Result.PeptideCountFromDecoyDB--;
            }
            else
            {
              oldOi.Result.PeptideCountFromTargetDB--;
            }
            oldOi.Spectra.Remove(s);
          }

          oldOi.Result.FalseDiscoveryRate = calc.Calculate(oldOi.Result.PeptideCountFromDecoyDB, oldOi.Result.PeptideCountFromTargetDB);
        }
      }
    }

    public void KeepOptimalResultInSetOnly(HashSet<IIdentifiedSpectrum> spectra)
    {
      OptimalResults.ForEach(m => m.KeepOptimalResultInSetOnly(spectra));
      CalculateToleranceScore();
    }

    public void CalculateToleranceScore()
    {
      var scoreFunc = this.Options.ScoreFunction;
      OptimalResults.ForEach(m => m.CalculateToleranceScore(scoreFunc));
    }

    public void CalculateCurrentFdr()
    {
      IFalseDiscoveryRateCalculator calc = Options.Parent.FalseDiscoveryRate.GetFalseDiscoveryRateCalculator();

      foreach (var item in OptimalResults)
      {
        item.CalculateFdr(calc);
      }
      CalculateToleranceScore();
    }

    public OptimalItem GetConditionItem(OptimalResultCondition cond)
    {
      return this.OptimalResults.Find(m => m.Condition == cond);
    }

    public int GetConditionSpectrumCount(OptimalResultCondition cond)
    {
      foreach (var oi in this.OptimalResults)
      {
        if (oi.Condition == cond)
        {
          return oi.Spectra.Count;
        }
      }

      return 0;
    }
  }

  public static class DatasetExtensions
  {
    public static List<IIdentifiedSpectrum> GetOptimalSpectra(this IEnumerable<Dataset> dsList)
    {
      List<IIdentifiedSpectrum> result = new List<IIdentifiedSpectrum>();
      foreach (var ds in dsList)
      {
        result.AddRange(ds.GetOptimalSpectra());
      }
      return result;
    }

    public static List<IIdentifiedSpectrum> GetUnconflictedOptimalSpectra(this IEnumerable<Dataset> dsList)
    {
      List<IIdentifiedSpectrum> result = new List<IIdentifiedSpectrum>();
      foreach (var ds in dsList)
      {
        result.AddRange(ds.GetUnconflictedOptimalSpectra());
      }
      return result;
    }

    public static List<IIdentifiedSpectrum> GetSpectra(this IEnumerable<Dataset> dsList)
    {
      List<IIdentifiedSpectrum> result = new List<IIdentifiedSpectrum>();
      foreach (var ds in dsList)
      {
        result.AddRange(ds.Spectra);
      }
      return result;
    }
  }
}
