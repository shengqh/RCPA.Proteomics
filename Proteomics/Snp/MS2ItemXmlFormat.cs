using RCPA.Proteomics.Spectrum;
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
      foreach (var ms2ele in root.Elements("MS2"))
      {
        var item = new MS2Item();
        result.Add(item);

        item.CombinedCount = int.Parse(ms2ele.Attribute("PKL").Value);
        item.Precursor = double.Parse(ms2ele.Attribute("MZ").Value);
        item.Charge = int.Parse(ms2ele.Attribute("Z").Value);
        if (ms2ele.Attribute("Seq") != null)
        {
          item.Peptide = ms2ele.Attribute("Seq").Value;
        }
        if (ms2ele.Attribute("Mod") != null)
        {
          item.Modification = ms2ele.Attribute("Mod").Value;
        }
        if (ms2ele.Attribute("FileScan") != null)
        {
          item.FileScan = ms2ele.Attribute("FileScan").Value;
        }
        foreach (var ele in ms2ele.Elements("TerminalLoss"))
        {
          item.TerminalLoss.Add(new TerminalLossItem(bool.Parse(ele.Attribute("IsNterminal").Value), ele.Attribute("Seq").Value, double.Parse(ele.Attribute("MZ").Value)));
        }

        foreach (var ms3ele in ms2ele.Elements("MS3"))
        {
          var ms3 = new PeakList<Peak>();
          item.MS3Spectra.Add(ms3);

          if (item.CombinedCount > 1)
          {
            ms3.CombinedCount = int.Parse(ms3ele.Attribute("PKL").Value);
          }
          else
          {
            ms3.CombinedCount = 1;
          }
          ms3.PrecursorMZ = double.Parse(ms3ele.Attribute("MZ").Value);
          foreach (var pEle in ms3ele.Elements("P"))
          {
            var peak = new Peak();
            ms3.Add(peak);

            peak.Mz = double.Parse(pEle.Attribute("MZ").Value);
            peak.Intensity = double.Parse(pEle.Attribute("I").Value);
            if (ms3.CombinedCount > 1)
            {
              peak.CombinedCount = int.Parse(pEle.Attribute("PKL").Value);
            }
            else
            {
              peak.CombinedCount = 1;
            }
          }
        }
      }

      return result;
    }

    public void WriteToFile(string fileName, List<MS2Item> items)
    {
      XElement root = new XElement("Library",
        from ms2 in items
        let ms3tag = ms2.CombinedCount > 1
        select new XElement("MS2",
          new XAttribute("PKL", ms2.CombinedCount),
          new XAttribute("MZ", string.Format("{0:0.#####}", ms2.Precursor)),
          new XAttribute("Z", ms2.Charge),
          string.IsNullOrEmpty(ms2.Peptide) ? null : new XAttribute("Seq", ms2.Peptide),
          string.IsNullOrEmpty(ms2.Modification) ? null : new XAttribute("Mod", ms2.Modification),
          new XAttribute("FileScan", ms2.FileScan),
          from nl in ms2.TerminalLoss
          select new XElement("TerminalLoss", new XAttribute("IsNterminal", nl.IsNterminal), new XAttribute("Seq", nl.Sequence), new XAttribute("MZ", nl.Precursor)),
          from ms3 in ms2.MS3Spectra
          let peaktag = ms3tag && ms3.CombinedCount > 1
          orderby ms3.PrecursorMZ
          select new XElement("MS3",
            ms3tag ? new XAttribute("PKL", ms3.CombinedCount) : null,
            new XAttribute("MZ", string.Format("{0:0.#####}", ms3.PrecursorMZ)),
            from peak in ms3
            select new XElement("P",
              new XAttribute("MZ", string.Format("{0:0.#####}", peak.Mz)),
              new XAttribute("I", string.Format("{0:0.#}", peak.Intensity)),
              peaktag ? new XAttribute("PKL", peak.CombinedCount) : null))));

      root.Save(fileName);
    }
  }
}
