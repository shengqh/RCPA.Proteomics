using RCPA.Utils;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml.Linq;

namespace RCPA.Proteomics.Quantification.Lipid
{
  public class PrecursorItemListXmlFormat : IFileFormat<List<PrecursorItem>>
  {
    #region IFileFormat<List<PrecursorItem>> Members

    public void WriteToFile(string fileName, List<PrecursorItem> item)
    {
      XElement root = new XElement("PrecursorItemList",
        from cond in item
        select new XElement("Item",
          new XElement("Scan", cond.Scan),
          new XElement("PrecursorMZ", MyConvert.Format("{0:0.0000}", cond.PrecursorMZ)),
          new XElement("PrecursorIntensity", MyConvert.Format("{0:0.0}", cond.PrecursorIntensity)),
          new XElement("ProductIonRelativeIntensity", MyConvert.Format("{0:0.00}", cond.ProductIonRelativeIntensity)),
          new XElement("Enabled", cond.Enabled),
          new XElement("IsIsotopic", cond.IsIsotopic)
          ));
      root.Save(fileName);
    }

    public List<PrecursorItem> ReadFromFile(string fileName)
    {
      if (!File.Exists(fileName))
      {
        throw new FileNotFoundException(fileName);
      }

      XElement root = XElement.Load(fileName);

      return ReadFromXml(root);
    }

    #endregion

    public List<PrecursorItem> ReadFromXml(XElement root)
    {
      return (from c in root.Descendants("Item")
              select new PrecursorItem()
              {
                Scan = Convert.ToInt32(c.Element("Scan").Value),
                PrecursorMZ = MyConvert.ToDouble(c.Element("PrecursorMZ").Value),
                PrecursorIntensity = MyConvert.ToDouble(c.Element("PrecursorIntensity").Value),
                ProductIonRelativeIntensity = MyConvert.ToDouble(c.Element("ProductIonRelativeIntensity").Value),
                Enabled = Convert.ToBoolean(c.Element("Enabled").Value),
                IsIsotopic = Convert.ToBoolean(c.Element("IsIsotopic").Value)
              }).ToList();
    }
  }
}