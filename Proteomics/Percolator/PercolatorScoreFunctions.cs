using RCPA.Proteomics.Summary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RCPA.Proteomics.Percolator
{
  public class PercolatorScoreFunctions : IScoreFunctions
  {
    public double GetScoreBin(IIdentifiedSpectrum spectrum)
    {
      return Math.Abs(spectrum.SpScore);
    }

    public string ScoreName
    {
      get { return "SVMScore"; }
    }

    public double GetScore(IIdentifiedSpectrum si)
    {
      return si.SpScore;
    }

    public SortSpectrumFunc SortSpectrum
    {
      get { return IdentifiedSpectrumUtils.SortBySpScore; }
    }
  }
}
