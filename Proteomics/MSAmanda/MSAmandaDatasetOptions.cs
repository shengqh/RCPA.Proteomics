using RCPA.Proteomics.Summary;
using RCPA.Proteomics.Summary.Uniform;
using System.Windows.Forms;

namespace RCPA.Proteomics.MSAmanda
{
  public class MSAmandaDatasetOptions : AbstractScoreDatasetOptions
  {
    public MSAmandaDatasetOptions()
    {
      this.SearchEngine = SearchEngineType.MSAmanda;
    }

    public override IDatasetBuilder GetBuilder()
    {
      return new MSAmandaDatasetBuilder(this);
    }

    public override UserControl CreateControl()
    {
      var result = new MSAmandaDatasetPanel();
      result.Options = this;
      return result;
    }
  }
}
