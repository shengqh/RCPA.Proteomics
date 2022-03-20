using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;

namespace RCPA.Proteomics.Quantification.IsobaricLabelling
{
  public class IsobaricLabelingExperimentalDesign : IXml
  {
    public IsobaricLabelingExperimentalDesign()
    { }

    public IsobaricType PlexType { get; set; }

    public string IsobaricFile { get; set; }

    public List<IsobaricIndex> References { get; set; }

    public string GetReferenceNames(string delimiter)
    {
      if (References == null)
      {
        return string.Empty;
      }

      return (from r in References
              select r.Name).Merge(delimiter);
    }

    public List<IsobaricIndex> GetSamples()
    {
      var result = new List<IsobaricIndex>();
      var used = IsobaricScanXmlUtils.GetUsedChannels(this.IsobaricFile);
      for (int i = 0; i < used.Count; i++)
      {
        var channel = used[i];
        if (References.Any(m => m.Name.Equals(channel.Name)))
        {
          continue;
        }
        result.Add(new IsobaricIndex(used[i].Name, i));
      }

      return result;
    }

    public Dictionary<string, List<string>> DatasetMap { get; set; }

    public double GetReference(IsobaricScan item)
    {
      return (from f in References
              select f.GetValue(item)).Average();
    }

    #region IXml Members

    public void Save(System.Xml.Linq.XElement parentNode)
    {
      parentNode.Add(
        new XElement("IsobaricType", PlexType.Name),
        new XElement("IsobaricFileName", IsobaricFile),
        new XElement("References", from refFunc in References
                                   select new XElement("Reference", new XAttribute("Name", refFunc.Name), new XAttribute("Index", refFunc.Index))
        ),
        new XElement("DatasetMap",
          from ds in DatasetMap
          select new XElement("Dataset",
            new XElement("Name", ds.Key),
            new XElement("Values",
            from d in ds.Value
            select new XElement("Value", d)))));
    }

    public void Load(System.Xml.Linq.XElement parentNode)
    {
      PlexType = IsobaricTypeFactory.Find(parentNode.Element("IsobaricType").Value);
      IsobaricFile = parentNode.Element("IsobaricFileName").Value;
      References = (from reffunc in parentNode.Element("References").Elements("Reference")
                    select new IsobaricIndex(reffunc.Attribute("Name").Value, int.Parse(reffunc.Attribute("Index").Value))).ToList();
      DatasetMap = new Dictionary<string, List<string>>();
      foreach (var ds in parentNode.Element("DatasetMap").Elements("Dataset"))
      {
        var name = ds.Element("Name").Value;
        var value = (from v in ds.Element("Values").Elements("Value") select v.Value).ToList();
        DatasetMap[name] = value;
      }
    }

    #endregion
  }
}
