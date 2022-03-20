using RCPA.Proteomics.Summary;
using System;
using System.Xml.Linq;

namespace RCPA.Proteomics.PeptideProphet
{
  public class InterProphetXmlParser : AbstractPostSearchPepXmlParser
  {
    public InterProphetXmlParser() { }

    public InterProphetXmlParser(IPepXmlParser baseParser) : base(baseParser) { }

    public override void ParseScoreAndOtherInformation(IIdentifiedSpectrum sph, XElement searchHit)
    {
      base.ParseScoreAndOtherInformation(sph, searchHit);

      var ip = searchHit.FindFirstDescendant("analysis_result", "analysis", "interprophet");
      if (ip == null)
      {
        throw new Exception("Cannot find interprophet at " + searchHit.ToString());
      }

      var sr = ip.FindFirstDescendant("interprophet_result");
      if (sr == null)
      {
        throw new Exception("Cannot find interprophet_result at " + ip.ToString());
      }

      sph.Probability = double.Parse(sr.Attribute("probability").Value);
    }

    public override SearchEngineType Engine
    {
      get { return SearchEngineType.InterProphet; }
    }
  }
}
