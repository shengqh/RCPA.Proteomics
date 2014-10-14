using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using RCPA.Proteomics.Summary;
using System.Windows.Forms;
using RCPA.Proteomics.Mascot;
using RCPA.Proteomics.PeptideProphet;
using RCPA.Proteomics.Omssa;

namespace RCPA.Proteomics.Summary.Uniform
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

    protected override OptimalResultCalculator NewOptimalResultCalculator()
    {
      return new OptimalResultCalculator(new OmssaScoreFunctions());
    }
  }
}
