using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;

namespace RCPA.Proteomics.Quantification.IsobaricLabelling
{
  /// <summary>
  /// Read isobaric definition from xml format file.
  /// </summary>
  public static class IsobaricTypeXmlFileReader
  {
    public static List<IsobaricType> ReadFromFile(string fileName)
    {
      var ele = XElement.Load(fileName);
      var result = new List<IsobaricType>();
      foreach (var iso in ele.FindElements("isobaric"))
      {
        var item = ReadIsobaricType(iso);
        result.Add(item);
      }
      return result;
    }

    public static IsobaricType ReadIsobaricType(XElement iso)
    {
      var item = new IsobaricType();
      item.Name = iso.FindElement("name").Value;
      foreach (var tag in iso.FindElements("tag_mz"))
      {
        item.TagMassH.Add(double.Parse(tag.Value));
      }

      var channels = iso.FindElements("channel");

      for (int index = 0; index < channels.Count; index++)
      {
        var channel = channels[index];
        var cha = new IsobaricChannel();
        cha.Index = index;
        cha.Name = channel.FindAttribute("name").Value;
        cha.Mz = double.Parse(channel.FindAttribute("mz").Value);
        cha.Percentage = double.Parse(channel.FindAttribute("percentage").Value);
        item.Channels.Add(cha);

        foreach (var isotopic in channel.FindElements("isotopic"))
        {
          var isot = new IsobaricIsotope();
          isot.Name = isotopic.FindAttribute("name").Value;
          isot.Percentage = double.Parse(isotopic.FindAttribute("percentage").Value);
          cha.Isotopics.Add(isot);
        }
      }

      item.Initialize();
      return item;
    }

    public static void WriteIsobaricType(XElement iso, IsobaricType item)
    {
      iso.Add(new XElement("name", item.Name));
      foreach (var tag in item.TagMassH)
      {
        iso.Add(new XElement("tag_mass", tag));
      }

      foreach (var channel in item.Channels)
      {
        iso.Add(new XElement("channel",
          new XAttribute("name", channel.Name),
          new XAttribute("mz", channel.Mz),
          new XAttribute("percentage", channel.Percentage),
          (from isotopic in channel.Isotopics
           select new XElement("isotopic",
             new XAttribute("name", isotopic.Name),
             new XAttribute("percentage", isotopic.Percentage)))));
      }
    }
  }
}
