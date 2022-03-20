using RCPA.Utils;
using System.Collections.Generic;

namespace RCPA.Proteomics.Summary
{
  public abstract class AbstractForwardSummaryBuilderFactory : ISummaryBuilderFactory
  {
    protected ISummaryBuilderFactory factory;

    public AbstractForwardSummaryBuilderFactory(ISummaryBuilderFactory beforeFdrFactory)
    {
      this.factory = beforeFdrFactory;
    }

    #region ISummaryBuilderFactory Members

    public AbstractSummaryConfiguration GetConfiguration(string parameterFile)
    {
      return this.factory.GetConfiguration(parameterFile);
    }

    public abstract IIdentifiedSpectrumBuilder GetSpectrumBuilder(IProgressCallback iProgressCallback);

    public abstract bool SavePeptidesFile { get; }

    public IFileFormat<List<IIdentifiedSpectrum>> GetIdentifiedSpectrumFormat()
    {
      return this.factory.GetIdentifiedSpectrumFormat();
    }

    public IFileFormat<IIdentifiedResult> GetIdetifiedResultFormat()
    {
      return this.factory.GetIdetifiedResultFormat();
    }

    public IScoreFunction GetScoreFunctions()
    {
      return this.factory.GetScoreFunctions();
    }

    #endregion
  }
}