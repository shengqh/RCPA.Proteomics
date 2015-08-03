using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace RCPA.Proteomics.Snp
{
  public class MS2ItemXmlFormat : IFileFormat<List<MS2Item>>
  {
    public List<MS2Item> ReadFromFile(string fileName)
    {
      if (!File.Exists(fileName))
      {
        throw new FileNotFoundException(fileName);
      }

      XElement root = XElement.Load(fileName);

      List<MS2Item> result = new List<MS2Item>();

      return result;
    }

    public void WriteToFile(string fileName, List<MS2Item> items)
    {
      XElement root = new XElement("Library",
        from ms2 in items
        select new XElement("MS2",
          new XElement("Seq", ms2.Peptide),
          new XElement("MZ", string.Format("{0:0.#####}", ms2.Precursor)),
          new XElement("Z", ms2.Charge),
          new XElement("Mod", ms2.Modification),
          from ms3 in ms2.MS3Spectra
          orderby ms3.PrecursorMZ
          select new XElement("MS3",
            new XAttribute("MZ", string.Format("{0:0.#####}", ms3.PrecursorMZ)),
            from peak in ms3
            select new XElement("P",
              new XAttribute("MZ", string.Format("{0:0.#####}", peak.Mz)),
              new XAttribute("I", string.Format("{0:0.#}", peak.Intensity))))));

      root.Save(fileName);
    }
  }
}
