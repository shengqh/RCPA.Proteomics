using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using System.Windows.Forms;
using RCPA.Proteomics.Sequest;
using RCPA.Proteomics.Summary.Uniform;
using RCPA.Proteomics.Summary;

namespace RCPA.Proteomics.Comet
{
  public class CometDatasetOptions : SequestDatasetOptions
  {
    public CometDatasetOptions()
    {
      this.SearchEngine = SearchEngineType.Comet;
    }

    private bool _filterByEvalue;
    public bool FilterByEvalue
    {
      get { return _filterByEvalue; }
      set { _filterByEvalue = value; }
    }

    private double _maxEvalue;
    public double MaxEvalue
    {
      get { return _maxEvalue; }
      set { _maxEvalue = value; }
    }

    protected override void AddAdditionalFilterTo(List<IFilter<IIdentifiedSpectrum>> filters)
    {
      base.AddAdditionalFilterTo(filters);

      if (FilterByEvalue)
      {
        filters.Add(new IdentifiedSpectrumExpectValueFilter(MaxEvalue));
      }
    }

    protected override List<XElement> GetPeptideFilters()
    {
      var result = base.GetPeptideFilters();
      result.Add(OptionUtils.FilterToXml("ExpectValue", _filterByEvalue, _maxEvalue));
      return result;
    }

    protected override void ParsePeptideFilters(XElement filterXml)
    {
      base.ParsePeptideFilters(filterXml);

      if (OptionUtils.HasFilter(filterXml, "Evalue"))
      {
        OptionUtils.XmlToFilter(filterXml, "Evalue", out _filterByEvalue, out _maxEvalue);
      }
      else if (OptionUtils.HasFilter(filterXml, "ExpectValue"))
      {
        OptionUtils.XmlToFilter(filterXml, "ExpectValue", out _filterByEvalue, out _maxEvalue);
      }
      else
      {
        _filterByEvalue = false;
        _maxEvalue = 0.05;
      }
    }

    public override IDatasetBuilder GetBuilder()
    {
      return new CometDatasetBuilder(this);
    }

    public override UserControl CreateControl()
    {
      var result = new CometDatasetPanel();

      result.Options = this;

      return result;
    }
  }
}
