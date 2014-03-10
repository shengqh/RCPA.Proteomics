using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using RCPA.Proteomics.Summary;
using System.Windows.Forms;

namespace RCPA.Proteomics.Summary.Uniform
{
  public abstract class AbstractExpectValueDatasetOptions : AbstractTitleDatasetOptions
  {
    private bool _filterByScore;

    public bool FilterByScore
    {
      get { return _filterByScore; }
      set { _filterByScore = value; }
    }

    private double _minScore;

    public double MinScore
    {
      get { return _minScore; }
      set { _minScore = value; }
    }

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
      if (FilterByScore)
      {
        filters.Add(new IdentifiedSpectrumScoreFilter(MinScore));
      }

      if (FilterByExpectValue)
      {
        filters.Add(new IdentifiedSpectrumExpectValueFilter(MaxExpectValue));
      }
    }

    protected override List<XElement> GetPeptideFilters()
    {
      return new XElement[]{
        OptionUtils.FilterToXml("Score", _filterByScore, _minScore),
        OptionUtils.FilterToXml("ExpectValue", _filterByExpectValue, _maxExpectValue)
      }.ToList();
    }

    protected override void ParsePeptideFilters(XElement filterXml)
    {
      OptionUtils.XmlToFilter(filterXml, "Score", out _filterByScore, out _minScore);
      OptionUtils.XmlToFilter(filterXml, "ExpectValue", out _filterByExpectValue, out _maxExpectValue);
    }
  }
}
