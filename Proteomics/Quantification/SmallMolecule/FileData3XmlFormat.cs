using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using System.IO;

namespace RCPA.Proteomics.Quantification.SmallMolecule
{
  public class FileData3XmlFormat : IFileFormat<FileData3>
  {
    #region IFileReader<FileData3> Members

    public FileData3 ReadFromFile(string fileName)
    {
      if (!File.Exists(fileName))
      {
        throw new FileNotFoundException(fileName);
      }

      XElement root = XElement.Load(fileName);

      FileData3 result = new FileData3();
      result.FileName = root.Attribute("filename").Value;
      result.MinMz = Convert.ToInt32(root.Attribute("minpeak").Value);
      result.MaxMz = Convert.ToInt32(root.Attribute("maxpeak").Value);

      foreach (var pElement in root.Descendants("feature"))
      {
        string peakName = pElement.Attribute("peakname").Value;
        result[peakName] = (from peak in pElement.Descendants("p")
                            select new PeakItem(Convert.ToInt32(peak.Attribute("s").Value),
                              MyConvert.ToDouble(peak.Attribute("m").Value),
                              MyConvert.ToDouble(peak.Attribute("i").Value))).ToList();
      }

      return result;
    }

    #endregion

    #region IFileWriter<FileData3> Members

    public void WriteToFile(string fileName, FileData3 data)
    {
      IEnumerable<string> peakNames = data.PeakNames;

      XElement rootElement = new XElement("filedata",
        new XAttribute("filename", data.FileName),
        new XAttribute("minpeak", data.MinMz),
        new XAttribute("maxpeak", data.MaxMz),
        from peakName in peakNames
        let peaks = data[peakName]
        select new XElement("feature",
          new XAttribute("peakname", peakName),
          from peak in peaks
          select new XElement("p",
            new XAttribute("s", peak.Scan),
            new XAttribute("m", MyConvert.Format("{0:0.0000}", peak.Mz)),
            new XAttribute("i", MyConvert.Format("{0:0.0}", peak.Intensity))
            )));

      rootElement.Save(fileName, SaveOptions.None);
    }

    #endregion
  }
}
