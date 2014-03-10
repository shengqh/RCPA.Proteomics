using RCPA.Proteomics.Summary;

namespace RCPA.Proteomics.PeptideProphet
{
  public interface IPeptideProphetItem<T> where T : IIdentifiedSpectrum
  {
    double PeptideProphetProbability { get; }

    int NumTotalProteins { get; }

    T SearchResult { get; }
  }
}