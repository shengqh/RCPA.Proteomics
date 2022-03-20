using RCPA.Proteomics.Summary;
using RCPA.Proteomics.Summary.Uniform;
using System.Windows.Forms;

namespace RCPA.Proteomics.MzIdent
{
  public abstract class AbstractMzIdentDatasetOptions : AbstractExpectValueDatasetOptions
  {
    public AbstractMzIdentDatasetOptions()
    {
      this.SearchEngine = GetSearchEngine();
    }

    protected abstract SearchEngineType GetSearchEngine();

    public override IDatasetBuilder GetBuilder()
    {
      return new MzIdentDatasetBuilder(this);
    }

    public override UserControl CreateControl()
    {
      var result = new MzIdentDatasetPanel();
      result.Options = this;
      return result;
    }
  }
}
