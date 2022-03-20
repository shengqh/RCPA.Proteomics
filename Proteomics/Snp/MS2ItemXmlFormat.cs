using System.Collections.Generic;
using System.IO;
using System.Linq;
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
        item.FileScans = (from ele in ms2ele.Elements("FileScan")
                          let exp = ele.Attribute("F").Value
                          let firstscan = int.Parse(ele.Attribute("S").Value)
                          select new SequestFilename(exp, firstscan, firstscan, item.Charge, string.Empty)).ToList();
        item.Score = double.Parse(ms2ele.Attribute("Score").Value);
        item.ExpectValue = double.Parse(ms2ele.Attribute("ExpectValue").Value);
        if (ms2ele.Attribute("Seq") != null)
        {
          item.Peptide = ms2ele.Attribute("Seq").Value;
        }
        if (ms2ele.Attribute("Mod") != null)
        {
          item.Modification = ms2ele.Attribute("Mod").Value;
        }
        if (ms2ele.Attribute("Proteins") != null)
        {
          item.Proteins = ms2ele.Attribute("Proteins").Value;
        }
        foreach (var ele in ms2ele.Elements("TerminalLoss"))
        {
          item.TerminalLoss.Add(new TerminalLossItem(bool.Parse(ele.Attribute("IsNterminal").Value), ele.Attribute("Seq").Value, double.Parse(ele.Attribute("MZ").Value)));
        }

        foreach (var ms3ele in ms2ele.Elements("MS3"))
        {
          var ms3 = new MS3Item();
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
            var peak = new MS3Peak();
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
          new XAttribute("Score", ms2.Score),
          new XAttribute("ExpectValue", ms2.ExpectValue),
          from fs in ms2.FileScans
          select new XElement("FileScan", new XAttribute("F", fs.Experimental), new XAttribute("S", fs.FirstScan)),
          string.IsNullOrEmpty(ms2.Peptide) ? null : new XAttribute("Seq", ms2.Peptide),
          string.IsNullOrEmpty(ms2.Modification) ? null : new XAttribute("Mod", ms2.Modification),
          string.IsNullOrEmpty(ms2.Proteins) ? null : new XAttribute("Proteins", ms2.Proteins),
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
