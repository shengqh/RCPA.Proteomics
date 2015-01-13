using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using RCPA.Numerics;

namespace RCPA.Proteomics.Quantification.IsobaricLabelling
{
  public class IsobaricPeptideStatisticBuilderOption : IXml
  {
    public IsobaricPeptideStatisticBuilderOption()
    {
      this.PerformNormalizition = true;
    }

    public string PeptideFile { get; set; }

    public string DesignFile { get; set; }

    public bool PerformNormalizition { get; set; }

    public QuantifyMode Mode { get; set; }

    public string ModifiedAminoacids { get; set; }

    #region IXml Members

    public void Save(System.Xml.Linq.XElement parentNode)
    {
      parentNode.Add(
        new XElement("PeptideFile", PeptideFile),
        new XElement("DesignFile", DesignFile),
        new XElement("PerformNormalization", PerformNormalizition),
        new XElement("QuantifyMode", Mode),
        new XElement("ModifiedAminoacids", ModifiedAminoacids));
    }

    public void Load(System.Xml.Linq.XElement parentNode)
    {
      PeptideFile = parentNode.Element("PeptideFile").Value;
      DesignFile = parentNode.Element("DesignFile").Value;
      PerformNormalizition = bool.Parse(parentNode.Element("PerformNormalization").Value);
      Mode = EnumUtils.StringToEnum<QuantifyMode>(parentNode.FindElement("QuantifyMode").Value, QuantifyMode.qmAll);
      ModifiedAminoacids = parentNode.Element("ModifiedAminoacids").Value;
    }

    #endregion
  }
}
