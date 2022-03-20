using RCPA.Proteomics.Summary;
using System;

namespace RCPA.Proteomics.PFind
{
  public class PFindExpectValueFunctions : IScoreFunction
  {
    public PFindExpectValueFunctions()
    { }

    #region IScoreFunctions Members

    public string ScoreName
    {
      get { return "ExpectValue"; }
    }

    public double GetScore(IIdentifiedSpectrum si)
    {
      return si.MinusLogExpectValue;
    }

    public SortSpectrumFunc SortSpectrum
    {
      get { return IdentifiedSpectrumUtils.SortByExpectValue; }
    }

    public double GetScoreBin(IIdentifiedSpectrum spectrum)
    {
      return Math.Floor(spectrum.MinusLogExpectValue);
    }

    #endregion
  }
}