using System;
using System.Collections.Generic;
using System.Text;

namespace RCPA.Proteomics.Summary
{
  public class OptimalResultCalculator : IOptimalResultCalculator
  {
    public IScoreFunctions ScoreFunc { get; set; }

    public double FdrValue { get; set; }

    public IFalseDiscoveryRateCalculator FdrCalc { get; set; }

    public CalculateQValueFunc QValueFunc { get; set; }

    public OptimalResultCalculator(IScoreFunctions scoreFunctions)
    {
      this.ScoreFunc = scoreFunctions;
    }

    #region IOptimalResultCalculator Members

    public virtual List<IIdentifiedSpectrum> Calculate(List<IIdentifiedSpectrum> preFiltered, OptimalResult optimalResult, bool hasDuplicatedSpectrum)
    {
      if (hasDuplicatedSpectrum)
      {
        return DoCalculateDuplicated(preFiltered, optimalResult);
      }
      else
      {
        return DoCalculateNoDuplicated(preFiltered, optimalResult);
      }
    }

    private List<IIdentifiedSpectrum> DoCalculateDuplicated(List<IIdentifiedSpectrum> preFiltered, OptimalResult optimalResult)
    {
      preFiltered.ForEach(m => m.QValue = -1);

      ScoreFunc.SortSpectrum(preFiltered);

      var topSpectra = new List<IIdentifiedSpectrum>(preFiltered);
      IdentifiedSpectrumUtils.KeepTopPeptideFromSameEngineDifferentParameters(topSpectra);

      //计算QValue。
      QValueFunc(topSpectra, ScoreFunc, FdrCalc);

      //将非top的肽段的QValue填充。
      for (int i = 1; i < preFiltered.Count; i++)
      {
        if (preFiltered[i].QValue == -1)
        {
          preFiltered[i].QValue = preFiltered[i - 1].QValue;
        }
      }

      List<IIdentifiedSpectrum> result = new List<IIdentifiedSpectrum>();

      optimalResult.PeptideCountFromDecoyDB = 0;
      optimalResult.PeptideCountFromTargetDB = 0;

      for (int i = preFiltered.Count - 1; i >= 0; i--)
      {
        if (preFiltered[i].QValue <= FdrValue)
        {
          result.AddRange(preFiltered.GetRange(0, i + 1));

          optimalResult.Score = ScoreFunc.GetScore(preFiltered[i]);
          optimalResult.ExpectValue = preFiltered[i].ExpectValue;

          optimalResult.FalseDiscoveryRate = preFiltered[i].QValue;

          int decoyCount = 0;
          int targetCount = 0;

          HashSet<string> filenames = new HashSet<string>();
          foreach (IIdentifiedSpectrum spectrum in result)
          {
            if (filenames.Contains(spectrum.Query.FileScan.LongFileName))
            {
              continue;
            }
            filenames.Add(spectrum.Query.FileScan.LongFileName);

            if (spectrum.FromDecoy)
            {
              decoyCount++;
            }
            else
            {
              targetCount++;
            }
          }

          optimalResult.PeptideCountFromDecoyDB = decoyCount;
          optimalResult.PeptideCountFromTargetDB = targetCount;

          Console.WriteLine("{0} -> {1} ==> {2} / {3}", preFiltered.Count, topSpectra.Count, decoyCount, targetCount);

          break;
        }
      }

      return result;
    }

    private List<IIdentifiedSpectrum> DoCalculateNoDuplicated(List<IIdentifiedSpectrum> preFiltered, OptimalResult optimalResult)
    {
      QValueFunc(preFiltered, ScoreFunc, FdrCalc);

      List<IIdentifiedSpectrum> result = new List<IIdentifiedSpectrum>();

      optimalResult.PeptideCountFromDecoyDB = 0;
      optimalResult.PeptideCountFromTargetDB = 0;

      for (int i = preFiltered.Count - 1; i >= 0; i--)
      {
        if (preFiltered[i].QValue <= FdrValue)
        {
          result.AddRange(preFiltered.GetRange(0, i + 1));

          optimalResult.Score = ScoreFunc.GetScore(preFiltered[i]);
          optimalResult.ExpectValue = preFiltered[i].ExpectValue;

          optimalResult.FalseDiscoveryRate = preFiltered[i].QValue;

          int decoyCount = 0;
          int targetCount = 0;

          foreach (IIdentifiedSpectrum spectrum in result)
          {
            if (spectrum.FromDecoy)
            {
              decoyCount++;
            }
            else
            {
              targetCount++;
            }
          }

          optimalResult.PeptideCountFromDecoyDB = decoyCount;
          optimalResult.PeptideCountFromTargetDB = targetCount;

          Console.WriteLine("{0} ==> {1} / {2}", preFiltered.Count, decoyCount, targetCount);

          break;
        }
      }

      return result;
    }

    public string OptimalResultHeader
    {
      get { return MyConvert.Format("{0}\tTargetCount\tDecoyCount\tFDR", ScoreFunc.ScoreName); }
    }

    public string OptimalResultToString(OptimalResult or)
    {
      if (null == or)
      {
        return "-\t0\t0\t0";
      }
      else
      {
        return MyConvert.Format("{0:0.####}\t{1}\t{2}\t{3:0.####}", or.Score, or.PeptideCountFromTargetDB, or.PeptideCountFromDecoyDB, or.FalseDiscoveryRate);
      }
    }

    #endregion
  }
}
