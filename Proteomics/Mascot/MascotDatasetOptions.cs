using RCPA.Proteomics.Summary;
using RCPA.Proteomics.Summary.Uniform;
using System.Windows.Forms;

namespace RCPA.Proteomics.Mascot
{
  public class MascotDatasetOptions : AbstractExpectValueDatasetOptions
  {
    public MascotDatasetOptions()
    {
      this.SearchEngine = SearchEngineType.MASCOT;
    }

    public override IDatasetBuilder GetBuilder()
    {
      return new MascotDatasetBuilder(this);
    }

    public override UserControl CreateControl()
    {
      var result = new MascotDatasetPanel();

      result.Options = this;

      return result;
    }
  }
}
