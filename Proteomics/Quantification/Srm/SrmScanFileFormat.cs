using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Xml.Linq;

namespace RCPA.Proteomics.Quantification.Srm
{
  public class SrmScanFileFormat : IFileFormat<List<SrmScan>>
  {
    public static XElement ScanToElement(SrmScan c, string name)
    {
      if (null == c)
      {
        return null;
      }

      return new XElement(name,
        new XElement("PrecursorMz", MyConvert.Format("{0:0.0000}", c.PrecursorMz)),
        new XElement("ProductIon", MyConvert.Format("{0:0.0000}", c.ProductMz)),
        new XElement("RetentionTime", MyConvert.Format("{0:0.0000}", c.RetentionTime)),
        new XElement("Intensity", MyConvert.Format("{0:0.0000}", c.Intensity)),
        new XElement("Enabled", c.Enabled.ToString()));
    }

    public static SrmScan ElementToScan(XElement c)
    {
      if (c == null)
      {
        return null;
      }

      return new SrmScan(MyConvert.ToDouble(c.Element("PrecursorMz").Value),
                MyConvert.ToDouble(c.Element("ProductIon").Value),
                MyConvert.ToDouble(c.Element("RetentionTime").Value),
                MyConvert.ToDouble(c.Element("Intensity").Value),
                bool.Parse(c.Element("Enabled").Value));
    }

    #region IFileReader<List<MRMScan>> Members

    public virtual List<SrmScan> ReadFromFile(string fileName)
    {
      if (!File.Exists(fileName))
      {
        throw new FileNotFoundException(fileName);
      }

      XElement root = XElement.Load(fileName);

      return FromElement(root);
    }

    #endregion

    #region IFileWriter<List<MRMScan>> Members

    public virtual void WriteToFile(string fileName, List<SrmScan> scans)
    {
      XElement root = ToElement(scans);

      root.Save(fileName);
    }

    #endregion

    protected virtual string ItemName { get { return "Item"; } }

    public virtual XElement ToElement(List<SrmScan> scans)
    {
      XElement root = new XElement("MRM", from c in scans
                                          select ScanToElement(c, ItemName));
      return root;
    }

    public virtual List<SrmScan> FromElement(XElement root)
    {
      return (from c in root.Descendants(ItemName)
              select ElementToScan(c)).ToList();
    }

  }
}
