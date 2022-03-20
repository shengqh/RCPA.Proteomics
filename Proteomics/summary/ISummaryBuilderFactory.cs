using RCPA.Utils;

namespace RCPA.Proteomics.Summary
{
  public interface ISummaryBuilderFactory : ISummaryBuilderBaseFactory
  {
    AbstractSummaryConfiguration GetConfiguration(string parameterFile);

    IIdentifiedSpectrumBuilder GetSpectrumBuilder(IProgressCallback iProgressCallback);

    IScoreFunction GetScoreFunctions();
  }
}
