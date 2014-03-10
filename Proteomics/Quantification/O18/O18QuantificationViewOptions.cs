using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;

namespace RCPA.Proteomics.Quantification.O18
{
  public class O18QuantificationViewOptions : AbstractO18QuantificationOption, IXml
  {
    public string RawDir { get; set; }

    public double PPMTolerance { get; set; }

    public double PurityOfO18Water { get; set; }

    public bool IsPostDigestionLabelling { get; set; }

    public double PeptideR2Tolerance { get; set; }

    public double ProteinR2Tolerance { get; set; }

    public O18QuantificationViewOptions()
    {
      IsPostDigestionLabelling = false;
    }

    public override IFileFormat<O18QuantificationSummaryItem> GetIndividualFileFormat()
    {
      return new O18QuantificationSummaryItemXmlFormat();
    }

    #region IXml Members

    public void Save(XElement parentNode)
    {
      parentNode.Add(new XElement("O18QuantificationOption",
        new XElement("RawDir", RawDir),
        new XElement("PPMTolerance", PPMTolerance),
        new XElement("PurityOfO18Water", PurityOfO18Water),
        new XElement("IsPostDigestionLabelling", IsPostDigestionLabelling),
        new XElement("PeptideR2Tolerance", PeptideR2Tolerance),
        new XElement("ProteinR2Tolerance", ProteinR2Tolerance)));
    }

    public void Load(XElement parentNode)
    {
      var doc = parentNode.Element("O18QuantificationOption");
      RawDir = doc.Element("RawDir").Value;
      PPMTolerance = MyConvert.ToDouble(doc.Element("PPMTolerance").Value);
      PurityOfO18Water = MyConvert.ToDouble(doc.Element("PurityOfO18Water").Value);
      IsPostDigestionLabelling = Convert.ToBoolean(doc.Element("IsPostDigestionLabelling").Value);
      PeptideR2Tolerance = MyConvert.ToDouble(doc.Element("PeptideR2Tolerance").Value);
      ProteinR2Tolerance = MyConvert.ToDouble(doc.Element("ProteinR2Tolerance").Value);
    }

    #endregion
  }
}
