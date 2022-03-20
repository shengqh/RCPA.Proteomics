using RCPA.Proteomics.Summary;

namespace RCPA.Proteomics.Percolator
{
  public class PercolatorScoreFunction : AbstractScoreFunction
  {
    public PercolatorScoreFunction() : base("SVMScore") { }

    public override double GetScore(IIdentifiedSpectrum si)
    {
      return si.SpScore;
    }

    public override SortSpectrumFunc SortSpectrum
    {
      get { return IdentifiedSpectrumUtils.SortBySpScore; }
    }
  }
}
