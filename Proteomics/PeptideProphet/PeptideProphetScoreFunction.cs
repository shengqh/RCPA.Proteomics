using RCPA.Proteomics.Summary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RCPA.Proteomics.PeptideProphet
{
  public class PeptideProphetScoreFunctions : IScoreFunction
  {
    public PeptideProphetScoreFunctions(){}

    public static void SortByPValue(List<IIdentifiedSpectrum> spectra)
    {
      spectra.Sort(delegate(IIdentifiedSpectrum s1, IIdentifiedSpectrum s2)
      {
        return -s1.PValue.CompareTo(s2.PValue);
      });
    }

    #region IScoreFunctions Members

    public string ScoreName
    {
      get { return "PValue"; }
    }

    public double GetScore(IIdentifiedSpectrum si)
    {
      return si.PValue;
    }

    public SortSpectrumFunc SortSpectrum
    {
      get { return SortByPValue; }
    }

    public double GetScoreBin(IIdentifiedSpectrum spectrum)
    {
      return Math.Round(Math.Log(spectrum.PValue));
    }
    #endregion
  }
}
