using RCPA.Proteomics.Spectrum;
using System;
using System.IO;
using System.Text;
using System.Xml;

namespace RCPA.Proteomics.Quantification.ITraq
{
  public static class ITraqItemXmlUtils
  {
    private static XmlReaderSettings setting = new XmlReaderSettings()
    {
      IgnoreComments = true,
      IgnoreWhitespace = true,
      IgnoreProcessingInstructions = true
    };

    public static void ReadChannels(XmlReader reader, IsobaricItem item)
    {
      reader.MoveToElement("Ions");
      reader.ReadStartElement("Ions");

      foreach (var def in item.Definition.Items)
      {
        item[def.Index] = reader.ReadElementAsDouble(def.Name);
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

    public static IsobaricItem Parse(string xml, bool readPeaks, Predicate<IsobaricItem> accept)
    {
      return Parse(Encoding.ASCII.GetBytes(xml), readPeaks, accept);
    }

    public static IsobaricItem Parse(byte[] bytes, bool readPeaks, Predicate<IsobaricItem> accept)
    {
      var reader = XmlReader.Create(new MemoryStream(bytes), setting);
      try
      {
        return Parse(reader, readPeaks, accept);
      }
      finally
      {
        reader.Close();
      }
    }

    /// <summary>
    /// 从XmlReader中读取ITraqItem信息。
    /// </summary>
    /// <param name="reader">XmlReader</param>
    /// <param name="readPeaks">是否读取Peak信息</param>
    /// <param name="accept">在读取Peak前，对ITraqItem进行筛选</param>
    /// <param name="untilSucceed">如果筛选失败，是否进行读取下一个ITraqItem</param>
    /// <returns></returns>
    public static IsobaricItem Parse(XmlReader reader, bool readPeaks, Predicate<IsobaricItem> accept, bool untilSucceed = false)
    {
      IsobaricItem result = null;

      while (reader.MoveToElement("ITraqScan"))
      {
        reader.ReadStartElement();

        result = new IsobaricItem();

        result.PlexType = EnumUtils.StringToEnum(reader.ReadElementAsString("PlexType"), IsobaricType.PLEX4);
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

        ReadChannels(reader, result);

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
