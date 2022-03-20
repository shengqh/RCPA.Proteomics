using RCPA.Proteomics.MzIdent;
using RCPA.Proteomics.Summary;

namespace RCPA.Proteomics.MSGF
{
  public class MSGFDatasetOptions : AbstractMzIdentDatasetOptions
  {
    protected override SearchEngineType GetSearchEngine()
    {
      return SearchEngineType.MSGF;
    }
  }
}
