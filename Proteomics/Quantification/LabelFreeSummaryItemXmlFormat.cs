using System;
using System.IO;
using System.Linq;
using System.Xml.Linq;

namespace RCPA.Proteomics.Quantification
{
  public class LabelFreeSummaryItemXmlFormat : IFileFormat<LabelFreeSummaryItem>
  {
    #region IFileFormat<LabelFreeSummaryItemXmlFormat> Members

    public void WriteToFile(string fileName, LabelFreeSummaryItem item)
    {
      XElement root = new XElement("LabelFreeSummaryItem",
        new XElement("RawFilename", item.RawFilename),
        new XElement("Sequence", item.Sequence),
        from cond in item
        select new XElement("Item",
          new XElement("Scan", cond.Scan),
          new XElement("RetentionTime", cond.RetentionTime),
          new XElement("Mz", cond.Mz),
          new XElement("DeltaMzPPM", cond.DeltaMzPPM),
          new XElement("Intensity", cond.Intensity),
          new XElement("Enabled", cond.Enabled),
          new XElement("Identified", cond.Identified),
          new XElement("AdjustIntensity", cond.AdjustIntensity)
          ));
      root.Save(fileName);
    }

    public LabelFreeSummaryItem ReadFromFile(string fileName)
    {
      if (!File.Exists(fileName))
      {
        throw new FileNotFoundException(fileName);
      }

      XElement root = XElement.Load(fileName);

      return ReadFromXml(root);
    }

    #endregion

    public LabelFreeSummaryItem ReadFromXml(XElement root)
    {
      LabelFreeSummaryItem result = new LabelFreeSummaryItem();
      result.RawFilename = root.Element("RawFilename").Value;
      result.Sequence = root.Element("Sequence").Value;

      var conds =
        from c in root.Descendants("Item")
        select c;

      foreach (var c in conds)
      {
        LabelFreeItem item = new LabelFreeItem()
        {
          Scan = Convert.ToInt32(c.Element("Scan").Value),
          RetentionTime = MyConvert.ToDouble(c.Element("RetentionTime").Value),
          Mz = MyConvert.ToDouble(c.Element("Mz").Value),
          DeltaMzPPM = MyConvert.ToDouble(c.Element("DeltaMzPPM").Value),
          Intensity = MyConvert.ToDouble(c.Element("Intensity").Value),
          Enabled = Convert.ToBoolean(c.Element("Enabled").Value),
          Identified = Convert.ToBoolean(c.Element("Identified").Value)
        };

        XElement adjustIntensity = c.Element("AdjustIntensity");
        if (null != adjustIntensity)
        {
          item.AdjustIntensity = MyConvert.ToDouble(adjustIntensity.Value);
        }

        result.Add(item);
      }
      return result;
    }
  }
}