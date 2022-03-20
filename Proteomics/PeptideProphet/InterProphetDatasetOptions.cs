using RCPA.Proteomics.Summary;
using RCPA.Proteomics.Summary.Uniform;

namespace RCPA.Proteomics.PeptideProphet
{
  public class InterProphetDatasetOptions : PeptideProphetDatasetOptions
  {
    public InterProphetDatasetOptions()
    {
      this.SearchEngine = SearchEngineType.InterProphet;
    }

    public override IDatasetBuilder GetBuilder()
    {
      return new InterProphetDatasetBuilder(this);
    }
  }
}
