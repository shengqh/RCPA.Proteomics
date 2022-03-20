using System.Xml.Linq;

namespace RCPA.Proteomics.Quantification.IsobaricLabelling
{
  public class IsobaricPeptideStatisticBuilderOptions : IXml
  {
    public IsobaricPeptideStatisticBuilderOptions()
    {
      this.PerformNormalizition = true;
    }

    public string PeptideFile { get; set; }

    public string DesignFile { get; set; }

    public bool PerformNormalizition { get; set; }

    public QuantifyMode Mode { get; set; }

    public string ModifiedAminoacids { get; set; }

    public double MinimumSiteProbability { get; set; }

    #region IXml Members

    public void Save(System.Xml.Linq.XElement parentNode)
    {
      parentNode.Add(
        new XElement("PeptideFile", PeptideFile),
        new XElement("DesignFile", DesignFile),
        new XElement("PerformNormalization", PerformNormalizition),
        new XElement("QuantifyMode", Mode),
        new XElement("ModifiedAminoacids", ModifiedAminoacids),
        new XElement("MinimumSiteProbability", MinimumSiteProbability));
    }

    public void Load(System.Xml.Linq.XElement parentNode)
    {
      PeptideFile = parentNode.Element("PeptideFile").Value;
      DesignFile = parentNode.Element("DesignFile").Value;
      PerformNormalizition = bool.Parse(parentNode.Element("PerformNormalization").Value);
      Mode = EnumUtils.StringToEnum<QuantifyMode>(parentNode.FindElement("QuantifyMode").Value, QuantifyMode.qmAll);
      ModifiedAminoacids = parentNode.Element("ModifiedAminoacids").Value;
      MinimumSiteProbability = double.Parse(parentNode.Element("MinimumSiteProbability").Value);
    }

    #endregion
  }
}
