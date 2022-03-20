using RCPA.Proteomics.Summary;
using RCPA.Utils;
using System.Xml;

namespace RCPA.Proteomics.XTandem
{
  public class XTandemXmlSummaryConfiguration : AbstractExpectValueSummaryConfiguration
  {
    public bool IgnoreUnanticipatedPeptide { get; set; }

    public XTandemXmlSummaryConfiguration(string applicationTitle)
      : base(applicationTitle, "XTANDEM", "Score")
    { }

    protected override void LoadSpecialDefinition(XmlNode docRoot)
    {
      base.LoadSpecialDefinition(docRoot);

      var xmlHelper = new XmlHelper(docRoot.OwnerDocument);

      this.IgnoreUnanticipatedPeptide = bool.Parse(xmlHelper.GetChildValue(docRoot, "IgnoreUnanticipatedPeptide"));
    }
  }
}
