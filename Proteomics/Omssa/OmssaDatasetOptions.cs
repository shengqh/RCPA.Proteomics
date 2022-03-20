using RCPA.Proteomics.Summary;
using RCPA.Proteomics.Summary.Uniform;
using System.Windows.Forms;

namespace RCPA.Proteomics.Omssa
{
  public class OmssaDatasetOptions : AbstractExpectValueDatasetOptions
  {
    public OmssaDatasetOptions()
    {
      this.SearchEngine = SearchEngineType.OMSSA;
    }

    private bool _filterByMinPValue;
    public bool FilterByMinPValue
    {
      get { return _filterByMinPValue; }
      set { _filterByMinPValue = value; }
    }

    private double _minPValue;
    public double MinPValue
    {
      get { return _minPValue; }
      set { _minPValue = value; }
    }

    public override IDatasetBuilder GetBuilder()
    {
      return new OmssaDatasetBuilder(this);
    }

    public override UserControl CreateControl()
    {
      var result = new OmssaDatasetPanel();

      result.Options = this;

      return result;
    }
  }
}
