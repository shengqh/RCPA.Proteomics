using System;
using System.Collections.Generic;
using System.Xml.Linq;
using System.Linq;

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
      foreach (var iso in ele.Elements("isobaric"))
      {
        var item = ReadIsobaricType(iso);

        result.Add(item);
      }
      return result;
    }

    public static IsobaricType ReadIsobaricType(XElement iso)
    {
      var item = new IsobaricType();
      item.Name = iso.Element("name").Value;
      foreach (var tag in iso.Elements("tag_mass"))
      {
        item.TagMass.Add(double.Parse(tag.Value));
      }

      foreach (var channel in iso.Elements("channel"))
      {
        var cha = new IsobaricChannel();
        cha.Name = channel.Attribute("name").Value;
        cha.Mz = double.Parse(channel.Attribute("mz").Value);
        cha.Percentage = double.Parse(channel.Attribute("percentage").Value);
        item.Channels.Add(cha);

        foreach (var isotopic in channel.Elements("isotopic"))
        {
          var isot = new IsobaricIsotope();
          isot.Name = isotopic.Attribute("name").Value;
          isot.Percentage = double.Parse(isotopic.Attribute("percentage").Value);
          cha.Isotopics.Add(isot);
        }
      }

      item.Initialize();
      return item;
    }

    public static void WriteIsobaricType(XElement iso, IsobaricType item)
    {
      iso.Add(new XElement("name", item.Name));
      foreach (var tag in item.TagMass)
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
