using System;
using System.Collections.Generic;
using System.Text;
using System.Configuration;
using RCPA.Utils;
using System.IO;
using RCPA.Proteomics.Modification;
using RCPA.Proteomics.Summary;
using System.Xml;
using RCPA.Seq;
using RCPA.Proteomics.Mascot;

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
