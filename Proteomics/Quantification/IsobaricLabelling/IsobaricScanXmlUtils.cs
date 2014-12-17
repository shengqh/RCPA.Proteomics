using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using RCPA.Proteomics.Spectrum;
using System.IO;
using System.Xml.Linq;
using System.Xml;

namespace RCPA.Proteomics.Quantification.IsobaricLabelling
{
  public static class IsobaricScanXmlUtils
  {
    public static IsobaricType GetIsobaricType(string fileName)
    {
      using (var stream = new FileStream(fileName, FileMode.Open))
      {
        using (var reader = XmlReader.Create(stream))
        {
          if (reader.MoveToElement("IsobaricResult"))
          {
            var result = reader.GetAttribute("IsobaricType");
            return IsobaricTypeFactory.Find(result);
          }
        }
      }

      throw new Exception(string.Format("Cannot find isobaric type in file {0}", fileName));
    }

    private static XmlReaderSettings setting = new XmlReaderSettings()
    {
      IgnoreComments = true,
      IgnoreWhitespace = true,
      IgnoreProcessingInstructions = true
    };

    public static PeakList<Peak> ReadElementPeakList(XmlReader reader, string pklName, bool isPeakInWindow)
    {
      var result = new PeakList<Peak>();

      reader.MoveToElement(pklName);
      reader.ReadStartElement();
      while (reader.LocalName.Equals("Peak"))
      {
        Peak p = new Peak();
        p.Mz = reader.ReadAttributeAsDouble("mz");
        p.Intensity = reader.ReadAttributeAsDouble("intensity");
        p.Charge = reader.ReadAttributeAsInt("charge");
        if (isPeakInWindow)
        {
          p.Tag = reader.ReadAttributeAsInt("tag");
        }
        result.Add(p);
        reader.Read();
      }

      if (isPeakInWindow)
      {
        reader.MoveToElement("Precursor");
        reader.ReadStartElement("Precursor");
        result.Precursor.MasterScan = reader.ReadElementAsInt("MasterScan");
        result.Precursor.MonoIsotopicMass = reader.ReadElementAsDouble("MonoIsotopicMass");
        result.Precursor.IsolationMass = reader.ReadElementAsDouble("IsolationMass");
        result.Precursor.IsolationWidth = reader.ReadElementAsDouble("IsolationWidth");
        result.Precursor.Charge = reader.ReadElementAsInt("Charge");
        result.Precursor.Intensity = reader.ReadElementAsDouble("Intensity");
      }

      return result;
    }

    public static IsobaricScan Parse(string xml, List<UsedChannel> used, bool readReporters, bool readPeaks, Predicate<IsobaricScan> accept)
    {
      return Parse(Encoding.ASCII.GetBytes(xml), used, readReporters, readPeaks, accept);
    }

    public static IsobaricScan Parse(byte[] bytes, List<UsedChannel> used, bool readReporters, bool readPeaks, Predicate<IsobaricScan> accept)
    {
      var reader = XmlReader.Create(new MemoryStream(bytes), setting);
      try
      {
        return Parse(reader, used, readReporters, readPeaks, accept);
      }
      finally
      {
        reader.Close();
      }
    }
    /// <summary>
    /// 从XmlReader中读取IsobaricItem信息。
    /// </summary>
    /// <param name="reader">XmlReader</param>
    /// <param name="used">Used channels</param>
    /// <param name="readReporters">是否读取Reporter信息</param>
    /// <param name="readPeaks">是否读取Peak信息</param>
    /// <param name="accept">在读取Peak前，对IsobaricItem进行筛选</param>
    /// <param name="untilSucceed">如果筛选失败，是否进行读取下一个IsobaricItem</param>
    /// <returns></returns>
    public static IsobaricScan Parse(XmlReader reader, List<UsedChannel> used, bool readReporters, bool readPeaks, Predicate<IsobaricScan> accept, bool untilSucceed = false)
    {
      IsobaricScan result = null;

      while (reader.MoveToElement("IsobaricScan"))
      {
        XElement el = XNode.ReadFrom(reader) as XElement;
        var dic = el.ToDictionary();

        result = new IsobaricScan();

        result.Experimental = dic["Experimental"].Value;
        result.ScanMode = dic["ScanMode"].Value;
        result.Scan = new ScanTime(int.Parse(dic["Scan"].Value), double.Parse(dic["RetentionTime"].Value));
        result.Scan.IonInjectionTime = double.Parse(dic["IonInjectionTime"].Value);
        result.PrecursorPercentage = double.Parse(dic["PrecursorPercentage"].Value);

        if (null != accept && !accept(result))
        {
          result = null;
          if (untilSucceed)
          {
            continue;
          }
          else
          {
            break;
          }
        }

        if (readReporters)
        {
          result.Reporters = new Peak[used.Count];

          XElement ions;
          if (dic.TryGetValue("Reporters", out ions))
          {
            var ionsElement = ions.FindElements("Reporter");
            if (ionsElement.Count != used.Count)
            {
              throw new Exception(string.Format("Reporter element number {0} is not equals to used channel number {1} in scan {2}",
                ionsElement.Count,
                used.Count,
                result.Scan));
            }

            for (int i = 0; i < used.Count; i++)
            {
              var ionEle = ionsElement[i];
              result[i] = new Peak(double.Parse(ionEle.FindAttribute("mz").Value), double.Parse(ionEle.FindAttribute("intensity").Value));
            }
          }
        }

        if (readPeaks)
        {
          result.RawPeaks = ReadElementPeakList(dic["RawPeaks"], false);
          result.RawPeaks.ScanTimes.Add(result.Scan);

          result.PeakInIsolationWindow = ReadElementPeakList(dic["PeakInIsolationWindow"], true);
        }

        break;
      }

      return result;
    }

    public static PeakList<Peak> ReadElementPeakList(XElement ions, bool isPeakInWindow)
    {
      var result = new PeakList<Peak>();
      foreach (var p in ions.FindElements("Peak"))
      {
        var mz = double.Parse(p.FindAttribute("mz").Value);
        var intensity = double.Parse(p.FindAttribute("intensity").Value);
        var charge = int.Parse(p.FindAttribute("charge").Value);
        var peak = new Peak(mz, intensity, charge);
        if (isPeakInWindow)
        {
          peak.Tag = int.Parse(p.FindAttribute("tag").Value);
        }
        result.Add(peak);
      }

      if (isPeakInWindow)
      {
        var precursor = ions.FindElement("Precursor");
        var dic = precursor.ToDictionary();
        result.Precursor.MasterScan = int.Parse(dic["MasterScan"].Value);
        result.Precursor.MonoIsotopicMass = double.Parse(dic["MonoIsotopicMass"].Value);
        result.Precursor.IsolationMass = double.Parse(dic["IsolationMass"].Value);
        result.Precursor.IsolationWidth = double.Parse(dic["IsolationWidth"].Value);
        result.Precursor.Charge = int.Parse(dic["Charge"].Value);
        result.Precursor.Intensity = double.Parse(dic["Intensity"].Value);
      }

      return result;
    }

    public static List<UsedChannel> GetUsedChannels(string fileName)
    {
      return GetUsedChannels(fileName, GetIsobaricType(fileName));
    }

    public static List<UsedChannel> GetUsedChannels(string fileName, IsobaricType isobaricType)
    {
      using (var stream = new FileStream(fileName, FileMode.Open))
      {
        using (var reader = XmlReader.Create(stream))
        {
          if (reader.MoveToElement("IsobaricResult"))
          {
            if (reader.HasAttributes)
            {
              var hasUsedChannel = reader.GetAttribute("HasUsedChannel");
              if (!string.IsNullOrEmpty(hasUsedChannel) && hasUsedChannel.Equals(true.ToString()))
              {
                reader.MoveToElement("UsedChannels");
                XElement el = XNode.ReadFrom(reader) as XElement;

                List<UsedChannel> result = new List<UsedChannel>();
                foreach (var ele in el.FindElements("UsedChannel"))
                {
                  var item = new UsedChannel();
                  item.Index = int.Parse(ele.Attribute("Index").Value);
                  item.Name = ele.Attribute("Name").Value;
                  item.Mz = double.Parse(ele.Attribute("Mz").Value);
                  result.Add(item);
                }

                return result;
              }
            }
          }
        }
      }

      return null;
    }

    public static Dictionary<string, string> GetComments(string fileName)
    {
      var result = new Dictionary<string, string>();

      using (var stream = new FileStream(fileName, FileMode.Open))
      {
        using (var reader = XmlReader.Create(stream))
        {
          if (reader.MoveToElement("Comments"))
          {
            XElement el = XNode.ReadFrom(reader) as XElement;

            foreach (var ele in el.FindElements("Comment"))
            {
              result[ele.Attribute("Key").Value] = ele.Attribute("Value").Value;
            }
          }
        }
      }

      return result;
    }
  }
}
