using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using RCPA.Numerics;

namespace RCPA.Proteomics.Quantification.IsobaricLabelling
{
  public class IsobaricProteinStatisticBuilderOption : IXml
  {
    public IsobaricProteinStatisticBuilderOption()
    {    }

    public string ProteinFileName { get; set; }

    public string QuanPeptideFileName { get; set; }

    public string ExpermentalDesignFile { get; set; }

    public string PeptideToProteinMethod { get; set; }

    #region IXml Members

    public void Save(System.Xml.Linq.XElement parentNode)
    {
      parentNode.Add(
        new XElement("ProteinFileName", ProteinFileName),
        new XElement("QuanPeptideFileName", QuanPeptideFileName),
        new XElement("ExpermentalDesignFile", ExpermentalDesignFile),
        new XElement("PeptideToProteinMethod", PeptideToProteinMethod));
    }

    public void Load(System.Xml.Linq.XElement parentNode)
    {
      ProteinFileName = parentNode.Element("ProteinFileName").Value;
      QuanPeptideFileName = parentNode.Element("QuanPeptideFileName").Value;
      ExpermentalDesignFile = parentNode.Element("ExpermentalDesignFile").Value;
      PeptideToProteinMethod = parentNode.Element("PeptideToProteinMethod").Value;
    }

    #endregion
  }
}
