using RCPA.Proteomics.Summary;
using System.Linq;
using System.Xml.Linq;

namespace RCPA.Proteomics.Comet
{
  public class CometXmlRank2Parser : CometXmlParser
  {
    public CometXmlRank2Parser()
    { }

    protected override XElement FindSearchHit(XElement searchResult)
    {
      var hits = searchResult.FindElements("search_hit");

      var top = hits.FirstOrDefault();
      if (top == null)
      {
        return null;
      }
      var sequence = top.Attribute("peptide").Value;

      return hits.Where(m => !m.Attribute("hit_rank").Value.Equals("1") && !m.Attribute("peptide").Value.Equals(sequence)).FirstOrDefault();
    }

    public override SearchEngineType Engine
    {
      get { return SearchEngineType.Comet; }
    }
  }
}
