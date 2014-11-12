using RCPA.Proteomics.Summary;
using RCPA.Proteomics.Summary.Uniform;
using System.Windows.Forms;

namespace RCPA.Proteomics.Percolator
{
  public class PercolatorDatasetOptions : AbstractExpectValueDatasetOptions
  {
    public PercolatorDatasetOptions()
    {
      this.SearchEngine = SearchEngineType.Percolator;
    }

    public override IDatasetBuilder GetBuilder()
    {
      return new PercolatorDatasetBuilder(this);
    }

    public override UserControl CreateControl()
    {
      var result = new PercolatorDatasetPanel();

      result.Options = this;

      return result;
    }
  }
}
