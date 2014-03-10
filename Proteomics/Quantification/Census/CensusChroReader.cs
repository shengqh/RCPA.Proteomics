using System.Collections.Generic;
using System.Xml;
using RCPA.Utils;

namespace RCPA.Proteomics.Quantification.Census
{
  public class CensusChroReader
  {
    private readonly List<Pair<string, string>> authors = new List<Pair<string, string>>();
    private XmlNode nextProtein;
    public string Version { get; set; }

    public List<Pair<string, string>> Authors
    {
      get { return this.authors; }
    }

    public string CreatedDate { get; set; }

    public string DataDependancy { get; set; }

    public string QuantLevel { get; set; }

    public void Open(string filename)
    {
      var doc = new XmlDocument();
      doc.Load(filename);

      var xmlHelper = new XmlHelper(doc);

      XmlNode root = doc.DocumentElement;
      Version = xmlHelper.GetValidChild(root, "version").InnerText;

      Authors.Clear();
      List<XmlNode> authors = xmlHelper.GetChildren(root, "author");
      foreach (XmlNode node in authors)
      {
        var author = new Pair<string, string>(node.Attributes["name"].InnerText, node.Attributes["email"].InnerText);
        Authors.Add(author);
      }

      CreatedDate = xmlHelper.GetValidChild(root, "created_date").InnerText;

      DataDependancy = xmlHelper.GetValidChild(root, "data_dependency").InnerText;

      QuantLevel = xmlHelper.GetValidChild(root, "quantLevel").InnerText;

      XmlNode profile = xmlHelper.GetValidChild(root, "PeptideProfile");

      this.nextProtein = xmlHelper.GetFirstChild(root, "Protein");
    }

    public bool HasNext()
    {
      return null != this.nextProtein;
    }

    public XmlNode Next()
    {
      XmlNode last = this.nextProtein;

      this.nextProtein = last.NextSibling;

      return last;
    }
  }
}