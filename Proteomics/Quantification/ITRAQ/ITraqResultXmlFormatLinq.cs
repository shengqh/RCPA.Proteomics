using RCPA.Proteomics.Spectrum;
using System;
using System.Linq;
using System.Xml.Linq;

namespace RCPA.Proteomics.Quantification.ITraq
{
  /// <summary>
  /// 通过Linq读写ITraqResult文件。
  /// </summary>
  public class ITraqResultXmlFormatLinq : AbstractITraqResultXmlFormat
  {
    #region IFileFormat<ITraqResult> Members

    public override IsobaricResult ReadFromFile(string fileName)
    {
      XElement root = XElement.Load(fileName);
      IsobaricResult result = new IsobaricResult();
      foreach (var ele in root.Elements("ITraqScan"))
      {
        var item = new IsobaricItem();

        item.PlexType = EnumUtils.StringToEnum(ele.Element("PlexType").Value, IsobaricType.PLEX4);
        item.Experimental = ele.Element("Experimental").Value;
        item.ScanMode = ele.Element("ScanMode").Value;

        item.Scan = new ScanTime(Convert.ToInt32(ele.Element("Scan").Value),
          MyConvert.ToDouble(ele.Element("RetentionTime").Value));
        item.Scan.IonInjectionTime = MyConvert.ToDouble(ele.Element("IonInjectionTime").Value);

        var ppEle = ele.Element("PrecursorPercentage");
        if (null != ppEle)
        {
          item.PrecursorPercentage = MyConvert.ToDouble(ppEle.Value);
        }

        if (!Accept(item))
        {
          continue;
        }

        var ions = ele.Element("Ions");

        var defs = item.Definition.Items;
        foreach (var def in defs)
        {
          item[def.Index] = MyConvert.ToDouble(ions.Element(def.Name).Value);
        }

        if (ReadPeaks)
        {
          item.RawPeaks = ElementToPeakList(ele, "RawPeaks", false);
          item.PeakInIsolationWindow = ElementToPeakList(ele, "PeakInIsolationWindow", true);
        }

        result.Add(item);
      }

      return result;
    }

    private static PeakList<Peak> ElementToPeakList(XElement ele, string peakname, bool isPeakInWindow)
    {
      PeakList<Peak> result = new PeakList<Peak>();

      var rawpeaks = ele.Element(peakname);
      if (rawpeaks != null)
      {
        foreach (var peakEle in rawpeaks.Elements("Peak"))
        {
          var peak = new Peak()
          {
            Mz = MyConvert.ToDouble(peakEle.Attribute("mz").Value),
            Intensity = MyConvert.ToDouble(peakEle.Attribute("intensity").Value)
          };

          var chargeTag = peakEle.Attribute("charge");
          if (chargeTag != null)
          {
            peak.Charge = int.Parse(chargeTag.Value);
          }

          if (isPeakInWindow)
          {
            var attrTag = peakEle.Attribute("tag");
            if (attrTag != null)
            {
              peak.Tag = int.Parse(attrTag.Value);
            }
          }

          result.Add(peak);
        }

        if (isPeakInWindow)
        {
          var precursor = rawpeaks.Element("Precursor");
          if (precursor != null)
          {
            result.Precursor.MasterScan = int.Parse(precursor.Element("MasterScan").Value);
            result.Precursor.MonoIsotopicMass = MyConvert.ToDouble(precursor.Element("MonoIsotopicMass").Value);
            result.Precursor.IsolationMass = MyConvert.ToDouble(precursor.Element("IsolationMass").Value);
            result.Precursor.IsolationWidth = MyConvert.ToDouble(precursor.Element("IsolationWidth").Value);
            result.Precursor.Charge = int.Parse(precursor.Element("Charge").Value);
            result.Precursor.Intensity = MyConvert.ToDouble(precursor.Element("Intensity").Value);
          }
        }
      }

      return result;
    }

    public override void WriteToFile(string fileName, IsobaricResult t)
    {
      var xle = new XElement("ITraqResult",
        from item in t
        let ions = new XElement("Ions", from def in item.Definition.Items
                                        select new XElement(def.Name, MyConvert.Format("{0:0.0}", item[def.Index])))
        select new XElement("ITraqScan",
          new XElement("PlexType", item.PlexType),
          new XElement("Experimental", item.Experimental),
          new XElement("ScanMode", item.ScanMode),
          new XElement("Scan", item.Scan.Scan),
          new XElement("IonInjectionTime", MyConvert.Format("{0:0.000}", item.Scan.IonInjectionTime)),
          new XElement("PrecursorPercentage", MyConvert.Format("{0:0.000}", item.PrecursorPercentage)),
          new XElement("RetentionTime", MyConvert.Format("{0:0.0}", item.Scan.RetentionTime)),
          ions,
          PeakListToElement(item.RawPeaks, "RawPeaks", false),
          PeakListToElement(item.PeakInIsolationWindow, "PeakInIsolationWindow", true)
          ));
      xle.Save(fileName);
    }

    private static XElement PeakListToElement(PeakList<Peak> pkls, string pklName, bool isPeakInWindow)
    {
      var result = new XElement(pklName,
                  from p in pkls
                  select new XElement("Peak",
                    new XAttribute("mz", MyConvert.Format("{0:0.#####}", p.Mz)),
                    new XAttribute("intensity", MyConvert.Format("{0:0.0}", p.Intensity)),
                    new XAttribute("charge", p.Charge),
                    isPeakInWindow ? new XAttribute("tag", p.Tag) : null
                    ));

      if (isPeakInWindow)
      {
        result.Add(new XElement("Precursor",
          new XElement("MasterScan", pkls.Precursor.MasterScan),
          new XElement("MonoIsotopicMass", string.Format("{0:0.00000}", pkls.Precursor.MonoIsotopicMass)),
          new XElement("IsolationMass", string.Format("{0:0.00000}", pkls.Precursor.IsolationMass)),
          new XElement("IsolationWidth", string.Format("{0:0.00}", pkls.Precursor.IsolationWidth)),
          new XElement("Charge", pkls.Precursor.Charge),
          new XElement("Intensity", string.Format("{0:0.0}", pkls.Precursor.Intensity))));
      }

      return result;
    }

    #endregion
  }
}


