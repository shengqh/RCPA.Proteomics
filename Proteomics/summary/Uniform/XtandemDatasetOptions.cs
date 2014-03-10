using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using RCPA.Proteomics.Summary;
using System.Windows.Forms;
using RCPA.Proteomics.XTandem;

namespace RCPA.Proteomics.Summary.Uniform
{
  public class XtandemDatasetOptions : AbstractExpectValueDatasetOptions
  {
    public bool IgnoreUnanticipatedPeptide { get; set; }

    //public bool IgnoreEQNterminalModification { get; set; }

    public override SearchEngineType SearchEngine
    {
      get { return SearchEngineType.XTANDEM; }
    }

    protected override void AddAdditionalFilterTo(List<IFilter<IIdentifiedSpectrum>> filters)
    {
      base.AddAdditionalFilterTo(filters);
    }

    protected override List<XElement> GetPeptideFilters()
    {
      var result = base.GetPeptideFilters();
      result.Add(new XElement("IgnoreUnanticipatedPeptide", IgnoreUnanticipatedPeptide));
      //result.Add(new XElement("IgnoreEQNterminalModification", IgnoreEQNterminalModification));
      return result;
    }

    protected override void ParsePeptideFilters(XElement filterXml)
    {
      base.ParsePeptideFilters(filterXml);

      IgnoreUnanticipatedPeptide = filterXml.GetChildValue("IgnoreUnanticipatedPeptide", true);
      //IgnoreEQNterminalModification = filterXml.GetChildValue("IgnoreEQNterminalModification",false);
    }

    public override IDatasetBuilder GetBuilder()
    {
      return new XtandemDatasetBuilder(this);
    }

    public override UserControl CreateControl()
    {
      var result = new XtandemDatasetPanel();

      result.Option = this;

      return result;
    }

    protected override OptimalResultCalculator NewOptimalResultCalculator()
    {
      return new XTandemOptimalScoreCalculator();
    }
  }
}
