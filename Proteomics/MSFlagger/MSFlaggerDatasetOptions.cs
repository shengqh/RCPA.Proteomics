using RCPA.Proteomics.Summary;
using RCPA.Proteomics.Summary.Uniform;
using System.Windows.Forms;

namespace RCPA.Proteomics.MSFlagger
{
  public class MSFlaggerDatasetOptions : AbstractExpectValueDatasetOptions
  {
    public MSFlaggerDatasetOptions()
    {
      this.SearchEngine = SearchEngineType.MSFlagger;
    }

    public override IDatasetBuilder GetBuilder()
    {
      return new MSFlaggerDatasetBuilder(this);
    }

    public override UserControl CreateControl()
    {
      var result = new MSFlaggerDatasetPanel();

      result.Options = this;

      return result;
    }
  }
}
