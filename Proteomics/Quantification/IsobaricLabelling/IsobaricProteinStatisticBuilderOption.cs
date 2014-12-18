using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using RCPA.Numerics;

namespace RCPA.Proteomics.Quantification.IsobaricLabelling
{
  public class IsobaricProteinStatisticBuilderOptions : IXml
  {
    public IsobaricProteinStatisticBuilderOptions()
    {
      this.PerformNormalizition = true;
    }

    public IsobaricType PlexType { get; set; }

    public string ProteinFileName { get; set; }

    public string IsobaricFileName { get; set; }

    public bool PerformNormalizition { get; set; }

    public string PeptideToProteinMethod { get; set; }

    public List<IsobaricIndex> References { get; set; }

    public List<IsobaricIndex> GetSamples()
    {
      var result = new List<IsobaricIndex>();
      var used = IsobaricScanXmlUtils.GetUsedChannels(this.IsobaricFileName);
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

    public double MinimumProbability { get; set; }

    public bool QuantifyModifiedPeptideOnly { get; set; }

    public string ModificationChars { get; set; }

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
        new XElement("ProteinFileName", ProteinFileName),
        new XElement("IsobaricFileName", IsobaricFileName),
        new XElement("References", from refFunc in References
                                   select new XElement("Reference", new XAttribute("Name", refFunc.Name), new XAttribute("Index", refFunc.Index))
        ),
        new XElement("PeptideToProteinMethod", PeptideToProteinMethod),
        new XElement("PerformNormalization", PerformNormalizition),
        new XElement("MinProbability", MinimumProbability),
        new XElement("QuantifyModifiedPeptideOnly", QuantifyModifiedPeptideOnly),
        new XElement("ModificationChars", ModificationChars),
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
      IsobaricFileName = parentNode.Element("IsobaricFileName").Value;
      References = (from reffunc in parentNode.Element("References").Elements("Reference")
                    select new IsobaricIndex(reffunc.Attribute("Name").Value, int.Parse(reffunc.Attribute("Index").Value))).ToList();
      PeptideToProteinMethod = parentNode.Element("PeptideToProteinMethod").Value;
      PerformNormalizition = bool.Parse(parentNode.Element("PerformNormalization").Value);
      MinimumProbability = MyConvert.ToDouble(parentNode.Element("MinProbability").Value);

      if (parentNode.Element("QuantifyModifiedPeptideOnly") != null)
      {
        QuantifyModifiedPeptideOnly = bool.Parse(parentNode.Element("QuantifyModifiedPeptideOnly").Value);
      }
      else
      {
        QuantifyModifiedPeptideOnly = false;
      }

      if (parentNode.Element("ModificationChars") != null)
      {
        ModificationChars = parentNode.Element("ModificationChars").Value;
      }
      else
      {
        ModificationChars = "!#";
      }

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
