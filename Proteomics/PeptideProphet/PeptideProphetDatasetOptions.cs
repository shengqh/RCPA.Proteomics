using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using RCPA.Proteomics.Summary;
using System.Windows.Forms;
using RCPA.Proteomics.Mascot;
using RCPA.Proteomics.PeptideProphet;
using RCPA.Proteomics.Summary.Uniform;

namespace RCPA.Proteomics.PeptideProphet
{
  public class PeptideProphetDatasetOptions : AbstractTitleDatasetOptions
  {
    public PeptideProphetDatasetOptions()
    {
      this.SearchEngine = SearchEngineType.PeptidePhophet;
    }

    private bool _filterByMinProbability;
    public bool FilterByMinProbability
    {
      get { return _filterByMinProbability; }
      set { _filterByMinProbability = value; }
    }

    private double _minPValue;
    public double MinPValue
    {
      get { return _minPValue; }
      set { _minPValue = value; }
    }

    public override IDatasetBuilder GetBuilder()
    {
      return new PeptideProphetDatasetBuilder(this);
    }

    public override UserControl CreateControl()
    {
      var result = new PeptideProphetDatasetPanel();

      result.Options = this;

      return result;
    }

    protected override void AddAdditionalFilterTo(List<IFilter<IIdentifiedSpectrum>> filters)
    {
      if (_filterByMinProbability)
      {
        filters.Add(new IdentifiedSpectrumMinProbabilityFilter(_minPValue));
      }
    }

    protected override List<XElement> GetPeptideFilters()
    {
      return new XElement[]{
        OptionUtils.FilterToXml("MinProbability", _filterByMinProbability, _minPValue),
      }.ToList();
    }

    protected override void ParsePeptideFilters(XElement filterXml)
    {
      OptionUtils.XmlToFilter(filterXml, "MinProbability", out _filterByMinProbability, out _minPValue);
    }
  }
}
