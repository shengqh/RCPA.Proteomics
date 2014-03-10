using System.Collections.Generic;
using System.IO;
using RCPA.Gui;

namespace RCPA.Proteomics.Summary
{
  public abstract class AbstractFdrSpectrumBuilder : ProgressClass, IIdentifiedSpectrumBuilder
  {
    protected ISummaryBuilderFactory factory;

    public AbstractFdrSpectrumBuilder(ISummaryBuilderFactory factory)
    {
      this.factory = factory;
    }

    #region IIdentifiedSpectrumBuilder Members

    public abstract List<IIdentifiedSpectrum> Build(string parameterFile);

    #endregion

    protected ScoreDistribution GetScoreDistribution(Dictionary<OptimalResultCondition, List<IIdentifiedSpectrum>> pepBin)
    {
      var result = new ScoreDistribution();
      foreach (OptimalResultCondition cond in pepBin.Keys)
      {
        List<IIdentifiedSpectrum> mphs = pepBin[cond];

        var scoreBin = new Dictionary<double, List<IIdentifiedSpectrum>>();
        foreach (IIdentifiedSpectrum spectrum in mphs)
        {
          double binScore = this.factory.GetScoreFunctions().GetScoreBin(spectrum);
          if (!scoreBin.ContainsKey(binScore))
          {
            scoreBin[binScore] = new List<IIdentifiedSpectrum>();
          }
          scoreBin[binScore].Add(spectrum);
        }

        var scores = new List<double>(scoreBin.Keys);
        scores.Sort();

        var scoreOrs = new List<OptimalResult>();
        foreach (double score in scores)
        {
          List<IIdentifiedSpectrum> spectra = scoreBin[score];
          var scoreOr = new OptimalResult();
          scoreOr.Score = score;
          scoreOr.PeptideCountFromDecoyDB = 0;
          scoreOr.PeptideCountFromTargetDB = 0;
          foreach (IIdentifiedSpectrum spectrum in spectra)
          {
            if (spectrum.FromDecoy)
            {
              scoreOr.PeptideCountFromDecoyDB = scoreOr.PeptideCountFromDecoyDB + 1;
            }
            else
            {
              scoreOr.PeptideCountFromTargetDB = scoreOr.PeptideCountFromTargetDB + 1;
            }
          }
          scoreOrs.Add(scoreOr);
        }
        result[cond] = scoreOrs;
      }
      return result;
    }

    protected void WriteScoreDistribution(StreamWriter sw,
                                          Dictionary<OptimalResultCondition, List<OptimalResult>> scoreOrMap)
    {
      var conds = new List<OptimalResultCondition>(scoreOrMap.Keys);
      conds.Sort();

      bool showClassification = OptimalResultCondition.HasClassification(conds);

      foreach (OptimalResultCondition cond in conds)
      {
        sw.WriteLine();
        if (showClassification)
        {
          sw.WriteLine("Classification={0}", cond.Classification);
        }
        sw.WriteLine("PrecursorCharge={0}{1}",
                     cond.PrecursorCharge,
                     cond.PrecursorCharge == 3 ? "+" : "");
        sw.WriteLine("MissCleavageSiteCount={0}{1}",
                     cond.MissCleavageSiteCount,
                     cond.MissCleavageSiteCount == 3 ? "+" : "");
        sw.WriteLine("ModificationCount={0}{1}",
                     cond.ModificationCount,
                     cond.ModificationCount == 2 ? "+" : "");
        List<OptimalResult> scoreOrs = scoreOrMap[cond];
        sw.WriteLine("Score\tTarget\tDecoy");
        foreach (OptimalResult or in scoreOrs)
        {
          sw.WriteLine("{0:0.####}\t{1}\t{2}", or.Score, or.PeptideCountFromTargetDB, or.PeptideCountFromDecoyDB);
        }
      }
    }
  }
}