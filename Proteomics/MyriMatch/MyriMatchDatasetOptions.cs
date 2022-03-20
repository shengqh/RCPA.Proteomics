using RCPA.Proteomics.MzIdent;
using RCPA.Proteomics.Summary;

namespace RCPA.Proteomics.MyriMatch
{
  public class MyriMatchDatasetOptions : AbstractMzIdentDatasetOptions
  {
    protected override SearchEngineType GetSearchEngine()
    {
      return SearchEngineType.MyriMatch;
    }
  }
}
