﻿namespace RCPA.Proteomics.Summary
{
  public interface IScoreFunction
  {
    string ScoreName { get; }

    double GetScore(IIdentifiedSpectrum si);

    SortSpectrumFunc SortSpectrum { get; }
  }

  public abstract class AbstractScoreFunction : IScoreFunction
  {
    private string scoreName;

    public AbstractScoreFunction(string scoreName)
    {
      this.scoreName = scoreName;
    }

    public string ScoreName
    {
      get { return scoreName; }
    }

    public override string ToString()
    {
      return this.scoreName;
    }

    public abstract SortSpectrumFunc SortSpectrum { get; }

    public abstract double GetScore(IIdentifiedSpectrum si);
  }

  public class ScoreFunction : AbstractScoreFunction
  {
    public ScoreFunction() : base("Score") { }

    public ScoreFunction(string scoreName) : base(scoreName) { }

    public override double GetScore(IIdentifiedSpectrum si)
    {
      return si.Score;
    }

    public override SortSpectrumFunc SortSpectrum
    {
      get { return IdentifiedSpectrumUtils.SortByScore; }
    }
  }

  public class ExpectValueFunction : AbstractScoreFunction
  {
    public ExpectValueFunction() : base("ExpectValue") { }

    public ExpectValueFunction(string scoreName) : base(scoreName) { }

    public override double GetScore(IIdentifiedSpectrum si)
    {
      return si.ExpectValue;
    }

    public override SortSpectrumFunc SortSpectrum
    {
      get { return IdentifiedSpectrumUtils.SortByExpectValue; }
    }
  }

  public class ProbabilityFunction : AbstractScoreFunction
  {
    public ProbabilityFunction() : base("Probability") { }

    public ProbabilityFunction(string scoreName) : base(scoreName) { }

    public override double GetScore(IIdentifiedSpectrum si)
    {
      return si.Probability;
    }

    public override SortSpectrumFunc SortSpectrum
    {
      get { return IdentifiedSpectrumUtils.SortByProbability; }
    }
  }
}
