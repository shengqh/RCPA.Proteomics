using RCPA.Proteomics.Summary;
using RCPA.Proteomics.Summary.Uniform;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Xml.Linq;

namespace RCPA.Proteomics.XTandem
{
  public class XTandemDatasetOptions : AbstractExpectValueDatasetOptions
  {
    public XTandemDatasetOptions()
    {
      this.SearchEngine = SearchEngineType.XTandem;
    }

    public bool IgnoreUnanticipatedPeptide { get; set; }

    protected override void AddAdditionalFilterTo(List<IFilter<IIdentifiedSpectrum>> filters)
    {
      base.AddAdditionalFilterTo(filters);
    }

    protected override List<XElement> GetPeptideFilters()
    {
      var result = base.GetPeptideFilters();
      result.Add(new XElement("IgnoreUnanticipatedPeptide", IgnoreUnanticipatedPeptide));
      return result;
    }

    protected override void ParsePeptideFilters(XElement filterXml)
    {
      base.ParsePeptideFilters(filterXml);

      IgnoreUnanticipatedPeptide = filterXml.GetChildValue("IgnoreUnanticipatedPeptide", true);
    }

    public override IDatasetBuilder GetBuilder()
    {
      return new XTandemDatasetBuilder(this);
    }

    public override UserControl CreateControl()
    {
      var result = new XTandemDatasetPanel();

      result.Options = this;

      return result;
    }
  }
}
