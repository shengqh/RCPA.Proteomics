using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using System.Windows.Forms;

namespace RCPA.Proteomics.Summary.Uniform
{
  public abstract class AbstractDatasetOptions : IDatasetOptions
  {
    public AbstractDatasetOptions()
    {
      Name = string.Empty;
      Enabled = true;
    }

    #region IDataset Members

    public string Name { get; set; }

    public bool Enabled { get; set; }

    public BuildSummaryOptions Parent { get; set; }

    public SearchEngineType SearchEngine { get; set; }

    private bool _filterByPrecursor;

    public bool FilterByPrecursor
    {
      get { return _filterByPrecursor; }
      set { _filterByPrecursor = value; }
    }

    private bool _filterByPrecursorSecondIsotopic;

    public bool FilterByPrecursorIsotopic
    {
      get { return _filterByPrecursorSecondIsotopic; }
      set { _filterByPrecursorSecondIsotopic = value; }
    }

    private bool _filterByDynamicPrecursorTolerance;

    public bool FilterByPrecursorDynamicTolerance
    {
      get { return _filterByDynamicPrecursorTolerance; }
      set { _filterByDynamicPrecursorTolerance = value; }
    }

    private double _precursorPPMTolerance;

    public double PrecursorPPMTolerance
    {
      get { return _precursorPPMTolerance; }
      set { _precursorPPMTolerance = value; }
    }

    public virtual IFilter<IIdentifiedSpectrum> GetFilter()
    {
      List<IFilter<IIdentifiedSpectrum>> filters = new List<IFilter<IIdentifiedSpectrum>>();

      if (Parent != null)
      {
        var afilter = Parent.PeptideFilter.GetFilter();
        if (afilter != null)
        {
          filters.Add(afilter);
        }
      }

      if (FilterByPrecursor && !FilterByPrecursorDynamicTolerance)
      {
        filters.Add(new IdentifiedSpectrumPrecursorFilter(PrecursorPPMTolerance, FilterByPrecursorIsotopic));
      }

      AddAdditionalFilterTo(filters);

      //在这里不进行contamination的过滤，以免导致无法进行母离子区间等计算。
      //if (Parent != null)
      //{
      //  IFilter<IIdentifiedSpectrum> contaminationFilter = Parent.Database.GetContaminationNameFilter();

      //  if (contaminationFilter != null)
      //  {
      //    filters.Add(new NotFilter<IIdentifiedSpectrum>(contaminationFilter));
      //  }
      //}

      if (filters.Count > 0)
      {
        return new AndFilter<IIdentifiedSpectrum>(filters);
      }
      else
      {
        return null;
      }
    }

    private List<string> pathNames = new List<string>();
    public List<string> PathNames
    {
      get
      {
        return pathNames;
      }
      set
      {
        pathNames = value;
      }
    }

    protected abstract void AddAdditionalFilterTo(List<IFilter<IIdentifiedSpectrum>> filters);

    public List<IIdentifiedSpectrum> Spectra { get; set; }

    public abstract IDatasetBuilder GetBuilder();

    public abstract UserControl CreateControl();

    #endregion

    #region IXml Members

    public virtual void Save(XElement parentNode)
    {
      parentNode.Add(
        new XElement("SearchEngine", SearchEngine),
        new XElement("Enabled",Enabled),
        new XElement("Name", Name),
        new XElement("PeptideFilter",
          new XElement("FilterByPrecursor", 
            new XElement("Active",FilterByPrecursor),
            new XElement("FilterByPrecursorIsotopic", FilterByPrecursorIsotopic),
            new XElement("FilterByDynamicPrecursorTolerance", FilterByPrecursorDynamicTolerance),
            new XElement("PrecursorPPMTolerance", PrecursorPPMTolerance)),
          GetPeptideFilters()),
          GetOtherParams(),
          new XElement("PathNames",
            from p in PathNames
            select new XElement("PathName", p)));
    }

    protected abstract List<XElement> GetPeptideFilters();

    protected virtual List<XElement> GetOtherParams()
    {
      return null;
    }

    public virtual void Load(XElement parentNode)
    {
      Enabled = parentNode.GetChildValue("Enabled", true);

      Name = parentNode.GetChildValue("Name", Name);

      var filterXml = parentNode.Element("PeptideFilter");
      var preXml = filterXml.Element("FilterByPrecursor");
      if (preXml != null)
      {
        FilterByPrecursor = preXml.GetChildValue("Active", false);
        FilterByPrecursorIsotopic = preXml.GetChildValue("FilterByPrecursorIsotopic", true);
        FilterByPrecursorDynamicTolerance = preXml.GetChildValue("FilterByDynamicPrecursorTolerance", true);
        PrecursorPPMTolerance = preXml.GetChildValue("PrecursorPPMTolerance", PrecursorPPMTolerance);
      }
      else
      {
        OptionUtils.XmlToFilter(filterXml, "PrecursorPPM", out _filterByPrecursor, out _precursorPPMTolerance);

        if (OptionUtils.HasFilter(filterXml, "PrecursorSecondIsotopic"))
        {
          bool tmp;
          OptionUtils.XmlToFilter(filterXml, "PrecursorSecondIsotopic", out _filterByPrecursorSecondIsotopic, out tmp);
        }
      }

      ParsePeptideFilters(filterXml);

      ParseOtherParams(parentNode);

      PathNames = (from x in parentNode.Element("PathNames").Elements("PathName")
                   select x.Value).ToList();

    }

    protected virtual void ParseOtherParams(XElement parentNode)
    { }

    protected abstract void ParsePeptideFilters(XElement filterXml);

    public virtual IOptimalResultCalculator GetOptimalResultCalculator()
    {
      OptimalResultCalculator result = new OptimalResultCalculator(this.SearchEngine.GetFactory().GetScoreFunctions());

      result.FdrCalc = Parent.FalseDiscoveryRate.GetFalseDiscoveryRateCalculator();

      if (Parent.FalseDiscoveryRate.FdrLevel == FalseDiscoveryRateLevel.UniquePeptide)
      {
        result.QValueFunc = IdentifiedSpectrumUtils.CalculateUniqueQValue;
      }
      else
      {
        result.QValueFunc = IdentifiedSpectrumUtils.CalculateQValue;
      }

      return result;
    }

    //protected abstract OptimalResultCalculator NewOptimalResultCalculator();

    #endregion
  }
}
