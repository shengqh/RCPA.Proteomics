using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml.Linq;

namespace RCPA.Proteomics.Database
{
  public class SequenceMutation
  {
    public int BeginPosition { get; set; }
    public int EndPosition { get; set; }
    public string Original { get; set; }
    public string Variation { get; set; }
    public string Description { get; set; }
    public string ID { get; set; }
    public string Evidence { get; set; }

    public bool IsSingleAminoacidVariant
    {
      get
      {
        return this.EndPosition == this.BeginPosition;
      }
    }

    public int Position
    {
      get
      {
        return this.BeginPosition;
      }
      set
      {
        this.BeginPosition = value;
        this.EndPosition = value;
      }
    }
  }
  public class UniprotEntry
  {
    public string Name { get; set; }

    public List<SequenceMutation> SequenceVariants { get; set; }

    public List<SequenceMutation> SequenceConflicts { get; set; }

    private string GetAttribute(XElement item, string attributeName, string defaultValue = "")
    {
      var attr = item.Attribute(attributeName);
      if (attr == null)
      {
        return defaultValue;
      }
      else
      {
        return attr.Value;
      }
    }

    public void ParseXml(byte[] bytes)
    {
      XElement root = XElement.Load(new MemoryStream(bytes), LoadOptions.None);

      SequenceVariants = ParseMutation(root, "sequence variant", "evidence");

      SequenceConflicts = ParseMutation(root, "sequence conflict", "ref");
    }

    private List<SequenceMutation> ParseMutation(XElement root, string featureName, string evidenceName)
    {
      var result = new List<SequenceMutation>();

      var features = GetFeatures(root, featureName);
      foreach (var feature in features)
      {
        var sv = new SequenceMutation();
        sv.Description = feature.GetAttributeValue("description", "");
        sv.ID = feature.GetAttributeValue("id", "");
        sv.Evidence = GetAttribute(feature, evidenceName);
        sv.Original = feature.GetChildValue("original", " ");
        sv.Variation = feature.GetChildValue("variation", " ");
        var loc = feature.Element("location");
        if (loc.Element("begin") != null)
        {
          sv.BeginPosition = Convert.ToInt32(loc.Element("begin").Attribute("position").Value);
          sv.EndPosition = Convert.ToInt32(loc.Element("end").Attribute("position").Value);
        }
        else
        {
          sv.Position = Convert.ToInt32(loc.Element("position").Attribute("position").Value);
        }
        result.Add(sv);
      }

      return result;
    }

    private static List<XElement> GetFeatures(XElement root, string featureName)
    {
      var features = (from ele in root.Elements("feature")
                      where ele.Attribute("type").Value.Equals(featureName)
                      select ele).ToList();
      return features;
    }
  }
}
