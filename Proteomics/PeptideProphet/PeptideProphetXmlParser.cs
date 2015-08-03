using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using RCPA.Proteomics.Summary;
using System.Xml.Linq;
using RCPA.Gui;
using System.Text.RegularExpressions;

namespace RCPA.Proteomics.PeptideProphet
{
  public class PeptideProphetXmlParser : PepXmlParser
  {
    public PeptideProphetXmlParser()
    { }

    protected override void ParseScore(IIdentifiedSpectrum sph, XElement searchHit)
    {
      var ip = searchHit.FindFirstDescendant("analysis_result", "analysis", "peptideprophet");
      if (ip == null)
      {
        throw new Exception("Cannot find peptideprophet at " + searchHit.ToString());
      }

      var sr = ip.FindFirstDescendant("peptideprophet_result");
      if (sr == null)
      {
        throw new Exception("Cannot find peptideprophet_result at " + ip.ToString());
      }

      sph.Probability = double.Parse(sr.Attribute("probability").Value);
    }

    public override SearchEngineType Engine
    {
      get { return SearchEngineType.PeptidePhophet; }
    }
  }
}
