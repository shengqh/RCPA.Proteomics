using RCPA.Proteomics.Spectrum;
using System;
using System.Linq;
using System.Xml.Linq;

namespace RCPA.Proteomics.Quantification.Labelfree
{
  public class RetentionTimePeakXmlFormat : IFileFormat<RetentionTimePeak>
  {
    #region IFileReader<RetentionTimePeak> Members

    public RetentionTimePeak ReadFromFile(string fileName)
    {
      var result = new RetentionTimePeak();

      var root = XElement.Load(fileName);

      result.Mz = MyConvert.ToDouble(root.Element("Mz").Value);
      result.Charge = Convert.ToInt32(root.Element("Charge").Value);
      result.Intensity = MyConvert.ToDouble(root.Element("Intensity").Value);
      result.MzTolerance = MyConvert.ToDouble(root.Element("MzTolerance").Value);
      result.RententionTime = MyConvert.ToDouble(root.Element("RententionTime").Value);
      result.Initialize();

      var chro = root.Element("Chro");
      foreach (var scan in chro.Elements("Scan"))
      {
        PeakList<Peak> pkl = new PeakList<Peak>(
          (from p in scan.Elements("Peak")
           let mz = MyConvert.ToDouble(p.Attribute("m").Value)
           let intensity = MyConvert.ToDouble(p.Attribute("i").Value)
           select new Peak(mz, intensity)).ToList());

        pkl.ScanTimes.Add(new ScanTime(
          Convert.ToInt32(scan.Attribute("s").Value),
          MyConvert.ToDouble(scan.Attribute("r").Value)));

        result.Chromotographs.Add(pkl);
      }

      return result;
    }

    #endregion

    #region IFileWriter<RetentionTimePeak> Members

    public void WriteToFile(string fileName, RetentionTimePeak t)
    {
      XElement root = new XElement("RetensionTimePeak",
        new XElement("Mz", MyConvert.Format("{0:0.00000}", t.Mz)),
        new XElement("Charge", t.Charge),
        new XElement("Intensity", MyConvert.Format("{0:0.00000}", t.Intensity)),
        new XElement("MzTolerance", MyConvert.Format("{0:0.00000}", t.MzTolerance)),
        new XElement("RententionTime", MyConvert.Format("{0:0.00000}", t.RententionTime)),
        new XElement("Chro",
          from pkl in t.Chromotographs
          select new XElement("Scan",
            new XAttribute("s", pkl.ScanTimes[0].Scan),
            new XAttribute("r", MyConvert.Format("{0:0.000}", pkl.ScanTimes[0].RetentionTime)),
            from p in pkl
            select new XElement("Peak",
              new XAttribute("m", MyConvert.Format("{0:0.00000}", p.Mz)),
              new XAttribute("i", MyConvert.Format("{0:0.00000}", p.Intensity))))));
      root.Save(fileName);
    }

    #endregion
  }
}
