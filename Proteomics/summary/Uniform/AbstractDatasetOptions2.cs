using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using System.Windows.Forms;
using RCPA.Proteomics.Sequest;
using RCPA.Proteomics.Summary.Uniform;
using RCPA.Proteomics.Summary;

namespace RCPA.Proteomics.Summary.Uniform
{
  public abstract class AbstractDatasetOptions2 : AbstractDatasetOptions
  {
    public AbstractDatasetOptions2()
    { }

    private bool _filterByXcorr;

    public bool FilterByXcorr
    {
      get { return _filterByXcorr; }
      set { _filterByXcorr = value; }
    }

    private double _xcorr1;

    public double MinXcorr1
    {
      get { return _xcorr1; }
      set { _xcorr1 = value; }
    }

    private double _xcorr2;

    public double MinXcorr2
    {
      get { return _xcorr2; }
      set { _xcorr2 = value; }
    }

    private double _xcorr3;

    public double MinXcorr3
    {
      get { return _xcorr3; }
      set { _xcorr3 = value; }
    }

    private bool _filterByDeltaCn;

    public bool FilterByDeltaCn
    {
      get { return _filterByDeltaCn; }
      set { _filterByDeltaCn = value; }
    }

    private double _deltaCn;

    public double MinDeltaCn
    {
      get { return _deltaCn; }
      set { _deltaCn = value; }
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
      if (FilterByXcorr)
      {
        filters.Add(new IdentifiedSpectrumChargeScoreFilter(new[] { MinXcorr1, MinXcorr2, MinXcorr3 }));
      }

      if (FilterByDeltaCn)
      {
        filters.Add(new IdentifiedSpectrumDeltaScoreFilter(MinDeltaCn));
      }

      if (FilterByEvalue)
      {
        filters.Add(new IdentifiedSpectrumExpectValueFilter(MaxEvalue));
      }
    }

    protected override List<XElement> GetPeptideFilters()
    {
      return new XElement[]{
        OptionUtils.FilterToXml("Xcorr1", _filterByXcorr, _xcorr1),
        OptionUtils.FilterToXml("Xcorr2", _filterByXcorr, _xcorr2),
        OptionUtils.FilterToXml("Xcorr3", _filterByXcorr, _xcorr3),
        OptionUtils.FilterToXml("DeltaCn", _filterByDeltaCn, _deltaCn),
        OptionUtils.FilterToXml("Evalue", _filterByEvalue, _maxEvalue)
      }.ToList();
    }

    protected override void ParsePeptideFilters(XElement filterXml)
    {
      OptionUtils.XmlToFilter(filterXml, "Xcorr1", out _filterByXcorr, out _xcorr1);
      OptionUtils.XmlToFilter(filterXml, "Xcorr2", out _filterByXcorr, out _xcorr2);
      OptionUtils.XmlToFilter(filterXml, "Xcorr3", out _filterByXcorr, out _xcorr3);
      OptionUtils.XmlToFilter(filterXml, "DeltaCn", out _filterByDeltaCn, out _deltaCn);
      if (OptionUtils.HasFilter(filterXml, "Evalue"))
      {
        OptionUtils.XmlToFilter(filterXml, "Evalue", out _filterByEvalue, out _maxEvalue);
      }
      else
      {
        _filterByEvalue = false;
        _maxEvalue = 0.05;
      }
    }
  }
}
