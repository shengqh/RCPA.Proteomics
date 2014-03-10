using System;
using System.Collections.Generic;
using System.Text;

namespace RCPA.Proteomics.Summary
{
  public interface IScoreFunctions
  {
    string ScoreName { get; }

    double GetScore(IIdentifiedSpectrum si);

    double GetScoreBin(IIdentifiedSpectrum spectrum);

    SortSpectrumFunc SortSpectrum { get; }
  }

  public abstract class AbstractScoreFunctions : IScoreFunctions
  {
    private string scoreName;

    public AbstractScoreFunctions(string scoreName)
    {
      this.scoreName = scoreName;
    }

    #region IScoreFunctions Members

    public string ScoreName
    {
      get { return scoreName; }
    }

    public double GetScore(IIdentifiedSpectrum si)
    {
      return si.Score;
    }

    public SortSpectrumFunc SortSpectrum
    {
      get { return IdentifiedSpectrumUtils.SortByScore; }
    }

    public abstract double GetScoreBin(IIdentifiedSpectrum spectrum);

    #endregion
  }

  public class DeltaScoreFunctions : IScoreFunctions
  {
    #region IGetScoreFunc Members

    public string ScoreName
    {
      get { return "DeltaScore"; }
    }

    public double GetScore(IIdentifiedSpectrum si)
    {
      return si.DeltaScore;
    }

    public SortSpectrumFunc SortSpectrum
    {
      get { return IdentifiedSpectrumUtils.SortByDeltaScore; }
    }

    public double GetScoreBin(IIdentifiedSpectrum spectrum)
    {
      return Math.Floor(spectrum.DeltaScore * 100) / 100;
    }

    #endregion
  }
}
