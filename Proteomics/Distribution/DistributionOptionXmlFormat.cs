using System;
using System.Xml.Linq;

namespace RCPA.Proteomics.Distribution
{
  public class DistributionOptionXmlFormat : IFileFormat<DistributionOption>
  {
    #region IFileReader<DistributionOption> Members

    public DistributionOption ReadFromFile(string fileName)
    {
      XElement root = XElement.Load(fileName);

      var result = new DistributionOption();

      result.SourceFileName = root.Element("SourceFile").Element("fileName").Value;

      result.DistributionType = (DistributionType)Enum.Parse(typeof(DistributionType), root.Element("distributionType").Value);

      result.ClassificationPrinciple = root.Element("ClassificationInfo").Element("classificationPrinciple").Value;

      XElement filter = root.Element("FilterByPeptide");
      result.FilterType = (PeptideFilterType)Enum.Parse(typeof(PeptideFilterType), filter.Element("filterType").Value);
      result.FilterFrom = Convert.ToInt32(filter.Element("from").Value);
      result.FilterTo = Convert.ToInt32(filter.Element("to").Value);
      result.FilterStep = Convert.ToInt32(filter.Element("step").Value);

      result.ModifiedPeptideOnly = Convert.ToBoolean(root.Element("modifiedPeptideOnly").Value);
      result.ModifiedPeptide = root.Element("modifiedAminoacid").Value;

      result.ClassificationSet.LoadFromXml(root, "ClassificationSet");

      result.ClassifiedByTag = Convert.ToBoolean(root.Element("ClassifiedByTag").Value);
      return result;
    }

    #endregion

    #region IFileWriter<DistributionOption> Members

    public void WriteToFile(string fileName, DistributionOption t)
    {
      XElement element = new XElement("DistributionOption",
        new XElement("SourceFile",
          new XElement("fileName", t.SourceFileName)),
        new XElement("distributionType", t.DistributionType.ToString()),
        new XElement("ClassificationInfo",
          new XElement("classificationPrinciple", t.ClassificationPrinciple)),
        new XElement("FilterByPeptide",
          new XElement("filterType", t.FilterType.ToString()),
          new XElement("from", t.FilterFrom),
          new XElement("to", t.FilterTo),
          new XElement("step", t.FilterStep)),
        new XElement("modifiedPeptideOnly", t.ModifiedPeptideOnly),
        new XElement("modifiedAminoacid", t.ModifiedPeptide),
        new XElement("ClassifiedByTag", t.ClassifiedByTag)
        );

      t.ClassificationSet.SaveToXml(element, "ClassificationSet", null);

      element.Save(fileName);
    }

    #endregion
  }
}
