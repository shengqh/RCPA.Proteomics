using System.Collections.Generic;
using System.Xml.Linq;

namespace RCPA.Proteomics.Summary.Uniform
{
  public abstract class AbstractExpectValueDatasetOptions : AbstractScoreDatasetOptions
  {
    private bool _filterByExpectValue;

    public bool FilterByExpectValue
    {
      get { return _filterByExpectValue; }
      set { _filterByExpectValue = value; }
    }

    private double _maxExpectValue;

    public double MaxExpectValue
    {
      get { return _maxExpectValue; }
      set { _maxExpectValue = value; }
    }

    protected override void AddAdditionalFilterTo(List<IFilter<IIdentifiedSpectrum>> filters)
    {
      base.AddAdditionalFilterTo(filters);

      if (FilterByExpectValue)
      {
        filters.Add(new IdentifiedSpectrumExpectValueFilter(MaxExpectValue));
      }
    }

    protected override List<XElement> GetPeptideFilters()
    {
      var result = base.GetPeptideFilters();
      result.Add(OptionUtils.FilterToXml("ExpectValue", _filterByExpectValue, _maxExpectValue));
      return result;
    }

    protected override void ParsePeptideFilters(XElement filterXml)
    {
      base.ParsePeptideFilters(filterXml);
      OptionUtils.XmlToFilter(filterXml, "ExpectValue", out _filterByExpectValue, out _maxExpectValue);
    }
  }
}
