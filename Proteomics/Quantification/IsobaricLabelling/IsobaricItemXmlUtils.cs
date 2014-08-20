using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using RCPA.Proteomics.Spectrum;
using System.IO;

namespace RCPA.Proteomics.Quantification.IsobaricLabelling
{
  public static class IsobaricItemXmlUtils
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

    private static XmlReaderSettings setting = new XmlReaderSettings()
    {
      IgnoreComments = true,
      IgnoreWhitespace = true,
      IgnoreProcessingInstructions = true
    };

    public static void ReadChannels(XmlReader reader, IsobaricItem item)
    {
      if (reader.MoveToElement("Ions"))
      {
        reader.ReadStartElement("Ions");

        for (int i = 0; i < item.PlexType.Channels.Count; i++)
        {
          item[i] = reader.ReadElementAsDouble(item.PlexType.Channels[i].Name);
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

    public static IsobaricItem Parse(string xml, bool readReporters, bool readPeaks, Predicate<IsobaricItem> accept)
    {
      return Parse(Encoding.ASCII.GetBytes(xml), readReporters, readPeaks, accept);
    }

    public static IsobaricItem Parse(byte[] bytes, bool readReporters, bool readPeaks, Predicate<IsobaricItem> accept)
    {
      var reader = XmlReader.Create(new MemoryStream(bytes), setting);
      try
      {
        return Parse(reader, readReporters, readPeaks, accept);
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
    /// <param name="readReporters">是否读取Reporter信息</param>
    /// <param name="readPeaks">是否读取Peak信息</param>
    /// <param name="accept">在读取Peak前，对IsobaricItem进行筛选</param>
    /// <param name="untilSucceed">如果筛选失败，是否进行读取下一个IsobaricItem</param>
    /// <returns></returns>
    public static IsobaricItem Parse(XmlReader reader, bool readReporters, bool readPeaks, Predicate<IsobaricItem> accept, bool untilSucceed = false)
    {
      IsobaricItem result = null;

      while (reader.MoveToElement("IsobaricScan"))
      {
        reader.ReadStartElement();

        result = new IsobaricItem();

        result.PlexType = IsobaricTypeFactory.Find(reader.ReadElementAsString("PlexType"));
        result.Experimental = reader.ReadElementAsString("Experimental");
        result.ScanMode = reader.ReadElementAsString("ScanMode");
        result.Scan = new ScanTime(reader.ReadElementAsInt("Scan"), reader.ReadElementAsDouble("RetentionTime"));
        result.Scan.IonInjectionTime = reader.ReadElementAsDouble("IonInjectionTime");
        result.PrecursorPercentage = reader.ReadElementAsDouble("PrecursorPercentage");

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
          ReadChannels(reader, result);
        }

        if (readPeaks)
        {
          result.RawPeaks = ReadElementPeakList(reader, "RawPeaks", false);
          result.PeakInIsolationWindow = ReadElementPeakList(reader, "PeakInIsolationWindow", true);
        }

        break;
      }

      return result;
    }
  }
}
