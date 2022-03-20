using RCPA.Proteomics.Summary;

namespace RCPA.Proteomics.Quantification
{
  public interface IPairQuantificationOptions
  {
    IGetRatioIntensity Func { get; }

    IProteinRatioCalculator GetProteinRatioCalculator();

    bool HasPeptideRatio(IIdentifiedSpectrum ann);

    bool IsPeptideRatioValid(IIdentifiedSpectrum ann);

    string GetRatioFile(IIdentifiedSpectrum mph);

    string SummaryFile { get; }

    string GetDetailDirectory();
  }
}
