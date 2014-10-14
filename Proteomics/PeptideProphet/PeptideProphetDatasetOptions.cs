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
      if (_filterByMinPValue)
      {
        filters.Add(new IdentifiedSpectrumMinPValueFilter(_minPValue));
      }
    }

    protected override List<XElement> GetPeptideFilters()
    {
      return new XElement[]{
        OptionUtils.FilterToXml("MinPValue", _filterByMinPValue, _minPValue),
      }.ToList();
    }

    protected override void ParsePeptideFilters(XElement filterXml)
    {
      OptionUtils.XmlToFilter(filterXml, "MinPValue", out _filterByMinPValue, out _minPValue);
    }
  }
}
