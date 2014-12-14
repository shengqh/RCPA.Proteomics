using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using RCPA.Proteomics.Summary;
using System.Windows.Forms;

namespace RCPA.Proteomics.Summary.Uniform
{
  public abstract class AbstractScoreDatasetOptions : AbstractTitleDatasetOptions
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

    protected override void AddAdditionalFilterTo(List<IFilter<IIdentifiedSpectrum>> filters)
    {
      if (FilterByScore)
      {
        filters.Add(new IdentifiedSpectrumScoreFilter(MinScore));
      }
    }

    protected override List<XElement> GetPeptideFilters()
    {
      return new XElement[]{
        OptionUtils.FilterToXml("Score", _filterByScore, _minScore),
      }.ToList();
    }

    protected override void ParsePeptideFilters(XElement filterXml)
    {
      OptionUtils.XmlToFilter(filterXml, "Score", out _filterByScore, out _minScore);
    }
  }
}
