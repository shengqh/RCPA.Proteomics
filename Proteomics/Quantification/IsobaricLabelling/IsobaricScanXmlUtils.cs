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
    public static String GetMode(string fileName)
    {
      using (var stream = new FileStream(fileName, FileMode.Open))
      {
        using (var reader = XmlReader.Create(stream))
        {
          if (reader.MoveToElement("IsobaricResult"))
          {
            if (reader.HasAttributes)
            {
              return reader.GetAttribute("Mode");
            }
          }
        }
      }
      return string.Empty;
    }

    public static IsobaricType GetIsobaricType(string fileName)
    {
      using (var stream = new FileStream(fileName, FileMode.Open))
      {
        using (var reader = XmlReader.Create(stream))
        {
          if (reader.MoveToElement("IsobaricResult"))
          {
            if (reader.HasAttributes)
            {
              var result = reader.GetAttribute("IsobaricType");
              if (string.IsNullOrEmpty(result))
              {
                if (reader.MoveToElement("IsobaricScan"))
                {
                  reader.ReadStartElement("IsobaricScan");
                  result = reader.ReadElementAsString("PlexType");
                }
              }

              if (!string.IsNullOrEmpty(result))
              {
                return IsobaricTypeFactory.Find(result);
              }
            }
          }
        }
      }
      return null;
    }

    private static XmlReaderSettings setting = new XmlReaderSettings()
    {
      IgnoreComments = true,
      IgnoreWhitespace = true,
      IgnoreProcessingInstructions = true
    };

    public static void ReadChannels(XmlReader reader, IsobaricType plexType, IsobaricScan item)
    {
      if (reader.MoveToElement("Ions"))
      {
        reader.ReadStartElement("Ions");

        for (int i = 0; i < plexType.Channels.Count; i++)
        {
          item[i] = reader.ReadElementAsDouble(plexType.Channels[i].Name);
        }
      }
    }

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

    public static IsobaricScan Parse(string xml, IsobaricType plexType, bool readReporters, bool readPeaks, Predicate<IsobaricScan> accept)
    {
      return Parse(Encoding.ASCII.GetBytes(xml), plexType, readReporters, readPeaks, accept);
    }

    public static IsobaricScan Parse(byte[] bytes, IsobaricType plexType, bool readReporters, bool readPeaks, Predicate<IsobaricScan> accept)
    {
      var reader = XmlReader.Create(new MemoryStream(bytes), setting);
      try
      {
        return Parse(reader, plexType, readReporters, readPeaks, accept);
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
    /// <param name="plexType">Isobaric Type</param>
    /// <param name="readReporters">是否读取Reporter信息</param>
    /// <param name="readPeaks">是否读取Peak信息</param>
    /// <param name="accept">在读取Peak前，对IsobaricItem进行筛选</param>
    /// <param name="untilSucceed">如果筛选失败，是否进行读取下一个IsobaricItem</param>
    /// <returns></returns>
    public static IsobaricScan Parse(XmlReader reader, IsobaricType plexType, bool readReporters, bool readPeaks, Predicate<IsobaricScan> accept, bool untilSucceed = false)
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
          XElement ions;
          if (dic.TryGetValue("Ions", out ions))
          {
            var ionsElement = ions.Elements().ToArray();
            if (ionsElement.Length != plexType.Channels.Count)
            {
              throw new Exception(string.Format("Ions element number {0} is not equals to channel number {1} of plex type {2}",
                ionsElement.Length,
                plexType.Channels.Count,
                 plexType.Name));
            }

            for (int i = 0; i < plexType.Channels.Count; i++)
            {
              result[i] = double.Parse(ionsElement[i].Value);
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
              if (!string.IsNullOrEmpty(hasUsedChannel) && hasUsedChannel.Equals("True"))
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
  }
}
