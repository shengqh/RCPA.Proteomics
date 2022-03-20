using RCPA.Utils;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml.Linq;

namespace RCPA.Proteomics.Quantification.Lipid
{
  public class PrecursorAreaListXmlFormat : IFileFormat<List<PrecursorArea>>
  {
    #region IFileFormat<List<PrecursorArea>> Members

    public void WriteToFile(string fileName, List<PrecursorArea> items)
    {
      XElement root = new XElement("PrecursorAreaList",
        from item in items
        select new XElement("Item",
          new XElement("PrecursorMZ", MyConvert.Format("{0:0.0000}", item.PrecursorMz)),
          new XElement("ScanCount", item.ScanCount),
          new XElement("Area", MyConvert.Format("{0:0.0}", item.Area)),
          new XElement("Enabled", item.Enabled.ToString())));
      root.Save(fileName);
    }

    public List<PrecursorArea> ReadFromFile(string fileName)
    {
      if (!File.Exists(fileName))
      {
        throw new FileNotFoundException(fileName);
      }

      XElement root = XElement.Load(fileName);

      return ReadFromXml(root);
    }

    #endregion

    public List<PrecursorArea> ReadFromXml(XElement root)
    {
      return (from item in root.Descendants("Item")
              select new PrecursorArea()
              {
                PrecursorMz = MyConvert.ToDouble(item.Element("PrecursorMZ").Value),
                ScanCount = Convert.ToInt32(item.Element("ScanCount").Value),
                Area = MyConvert.ToDouble(item.Element("Area").Value),
                Enabled = Convert.ToBoolean(item.Element("Enabled").Value)
              }).ToList();
    }
  }
}