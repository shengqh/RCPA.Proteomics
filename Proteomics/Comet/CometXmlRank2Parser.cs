using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using RCPA.Proteomics.Summary;
using System.Xml.Linq;
using RCPA.Gui;
using System.Text.RegularExpressions;
using RCPA.Proteomics.PeptideProphet;

namespace RCPA.Proteomics.Comet
{
  public class CometXmlRank2Parser : PeptideProphetXmlParser
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
