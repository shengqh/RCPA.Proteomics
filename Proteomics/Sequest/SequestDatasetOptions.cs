using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using System.Windows.Forms;
using RCPA.Proteomics.Sequest;
using RCPA.Proteomics.Summary.Uniform;
using RCPA.Proteomics.Summary;

namespace RCPA.Proteomics.Sequest
{
  public class SequestDatasetOptions : AbstractDatasetOptions
  {
    public SequestDatasetOptions()
    {
      this.SkipSamePeptideButDifferentModificationSite = true;
      this.MaxModificationDeltaCn = 0.08;
      this.SearchEngine = SearchEngineType.SEQUEST;
    }

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

    private bool _filterBySpRank;

    public bool FilterBySpRank
    {
      get { return _filterBySpRank; }
      set { _filterBySpRank = value; }
    }

    private int _spRank;

    public int MaxSpRank
    {
      get { return _spRank; }
      set { _spRank = value; }
    }

    public bool SkipSamePeptideButDifferentModificationSite { get; set; }

    public double MaxModificationDeltaCn { get; set; }

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

      if (FilterBySpRank)
      {
        filters.Add(new IdentifiedSpectrumSpRankFilter(MaxSpRank));
      }

    }

    protected override List<XElement> GetPeptideFilters()
    {
      return new XElement[]{
        OptionUtils.FilterToXml("Xcorr1", _filterByXcorr, _xcorr1),
        OptionUtils.FilterToXml("Xcorr2", _filterByXcorr, _xcorr2),
        OptionUtils.FilterToXml("Xcorr3", _filterByXcorr, _xcorr3),
        OptionUtils.FilterToXml("DeltaCn", _filterByDeltaCn, _deltaCn),
        OptionUtils.FilterToXml("SpRank", _filterBySpRank, _spRank),
      }.ToList();
    }

    protected override void ParsePeptideFilters(XElement filterXml)
    {
      OptionUtils.XmlToFilter(filterXml, "Xcorr1", out _filterByXcorr, out _xcorr1);
      OptionUtils.XmlToFilter(filterXml, "Xcorr2", out _filterByXcorr, out _xcorr2);
      OptionUtils.XmlToFilter(filterXml, "Xcorr3", out _filterByXcorr, out _xcorr3);
      OptionUtils.XmlToFilter(filterXml, "DeltaCn", out _filterByDeltaCn, out _deltaCn);
      OptionUtils.XmlToFilter(filterXml, "SpRank", out _filterBySpRank, out _spRank);
    }

    protected override List<XElement> GetOtherParams()
    {
      return new XElement[]{
        new XElement("DeltaCnCalculation",
          new XElement("SkipSamePeptideButDifferentModificationSite", SkipSamePeptideButDifferentModificationSite),
          new XElement("MaxModificationDeltaCn", MaxModificationDeltaCn))
      }.ToList();
    }

    protected override void ParseOtherParams(XElement parentNode)
    {
      var xml = parentNode.Element("DeltaCnCalculation");

      SkipSamePeptideButDifferentModificationSite = Convert.ToBoolean(xml.Element("SkipSamePeptideButDifferentModificationSite").Value);
      MaxModificationDeltaCn = MyConvert.ToDouble(xml.Element("MaxModificationDeltaCn").Value);
    }

    public override IDatasetBuilder GetBuilder()
    {
      return new SequestDatasetBuilder(this);
    }

    public override UserControl CreateControl()
    {
      var result = new SequestDatasetPanel();

      result.Options = this;

      return result;
    }
  }
}
