using RCPA.Proteomics.Summary;
using RCPA.Proteomics.Summary.Uniform;
using System.Windows.Forms;

namespace RCPA.Proteomics.PFind
{
  public class PFindDatasetOptions : AbstractExpectValueDatasetOptions
  {
    public PFindDatasetOptions()
    {
      this.SearchEngine = SearchEngineType.PFind;
    }

    public override IDatasetBuilder GetBuilder()
    {
      return new PFindDatasetBuilder(this);
    }

    public override UserControl CreateControl()
    {
      var result = new PFindDatasetPanel();

      result.Options = this;

      return result;
    }
  }
}
