using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using RCPA.Proteomics.Spectrum;

namespace RCPA.Proteomics.Quantification.Labelfree
{
  public class SimplePeakChroXmlFormat : IFileFormat<SimplePeakChro>
  {
    #region IFileReader<SimplePeakChro> Members

    public SimplePeakChro ReadFromFile(string fileName)
    {
      var result = new SimplePeakChro();

      var root = XElement.Load(fileName);

      result.Sequence = root.Element("Sequence").Value;
      result.Mz = MyConvert.ToDouble(root.Element("Mz").Value);
      result.MzTolerance = MyConvert.ToDouble(root.Element("MzTolerance").Value);
      result.Charge = Convert.ToInt32(root.Element("Charge").Value);
      result.MaxRetentionTime = MyConvert.ToDouble(root.Element("MaxRT").Value);

      var chro = root.Element("Chro");
      result.Peaks = (from p in chro.Elements("Peak")
                      let scan = Convert.ToInt32(p.Attribute("s").Value)
                      let mz = MyConvert.ToDouble(p.Attribute("m").Value)
                      let intensity = MyConvert.ToDouble(p.Attribute("i").Value)
                      let charge = Convert.ToInt32(p.Attribute("c").Value)
                      let rt = MyConvert.ToDouble(p.Attribute("r").Value)
                      let ioninjectiontime = MyConvert.ToDouble(p.Attribute("t").Value)
                      let identified = Convert.ToBoolean(p.Attribute("d").Value)
                      let ppmdistance = MyConvert.ToDouble(p.Attribute("p").Value)
                      select new ScanPeak()
                      {
                        Scan = scan,
                        Mz = mz,
                        Intensity = intensity,
                        Charge = charge,
                        RetentionTime = rt,
                        IonInjectionTime = ioninjectiontime,
                        Identified = identified,
                        PPMDistance = ppmdistance
                      }).ToList();

      return result;
    }

    #endregion

    #region IFileWriter<SimplePeakChro> Members

    public void WriteToFile(string fileName, SimplePeakChro t)
    {
      XElement root = new XElement("SimplePeakChro",
        new XElement("Sequence", t.Sequence),
        new XElement("Mz", MyConvert.Format("{0:0.00000}", t.Mz)),
        new XElement("MzTolerance", MyConvert.Format("{0:0.00000}", t.MzTolerance)),
        new XElement("Charge", t.Charge),
        new XElement("MaxRT", MyConvert.Format("{0:0.0000}", t.MaxRetentionTime)),
        new XElement("Chro",
            from p in t.Peaks
            select new XElement("Peak",
              new XAttribute("s", p.Scan),
              new XAttribute("m", MyConvert.Format("{0:0.00000}", p.Mz)),
              new XAttribute("i", MyConvert.Format("{0:0.00000}", p.Intensity)),
              new XAttribute("c", p.Charge),
              new XAttribute("r", MyConvert.Format("{0:0.000}", p.RetentionTime)),
              new XAttribute("t", MyConvert.Format("{0:0.000}", p.IonInjectionTime)),
              new XAttribute("d", p.Identified.ToString()),
              new XAttribute("p", MyConvert.Format("{0:0.000}", p.PPMDistance))
              )));

      root.Save(fileName);
    }

    #endregion
  }
}
