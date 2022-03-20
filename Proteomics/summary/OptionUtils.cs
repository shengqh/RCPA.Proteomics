using System;
using System.Linq;
using System.Xml.Linq;

namespace RCPA.Proteomics.Summary
{
  public static class OptionUtils
  {
    public static XElement FilterToXml(string name, bool active, object value)
    {
      return new XElement("Filter",
          new XAttribute("Name", name),
          new XAttribute("Active", active.ToString()),
          new XAttribute("Value", MyConvert.Format(value)));
    }

    public static bool HasFilter(XElement parentNode, string name)
    {
      return (from x in parentNode.Elements("Filter")
              where x.Attribute("Name").Value == name
              select x).Count() > 0;
    }

    public static void XmlToFilter(XElement parentNode, string name, out bool active, out string value)
    {
      XElement seqLenEle = (from x in parentNode.Elements("Filter")
                            where x.Attribute("Name").Value == name
                            select x).First();
      active = Convert.ToBoolean(seqLenEle.Attribute("Active").Value);
      value = seqLenEle.Attribute("Value").Value;
    }

    public static void XmlToFilter(XElement parentNode, string name, out bool active, out bool value)
    {
      string strValue;
      XmlToFilter(parentNode, name, out active, out strValue);
      value = Convert.ToBoolean(strValue);
    }

    public static void XmlToFilter(XElement parentNode, string name, out bool active, out int value)
    {
      string strValue;
      XmlToFilter(parentNode, name, out active, out strValue);
      value = Convert.ToInt32(strValue);
    }

    public static void XmlToFilter(XElement parentNode, string name, out bool active, out double value)
    {
      string strValue;
      XmlToFilter(parentNode, name, out active, out strValue);
      value = MyConvert.ToDouble(strValue);
    }
  }
}
