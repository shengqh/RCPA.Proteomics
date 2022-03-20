using RCPA.Proteomics.PeptideProphet;
using RCPA.Proteomics.Summary;
using System.Xml.Linq;

namespace RCPA.Proteomics.MyriMatch
{
  public class MyriMatchXmlParser : AbstractPepXmlParser
  {
    public MyriMatchXmlParser() { }

    public override SearchEngineType Engine
    {
      get { return SearchEngineType.MyriMatch; }
    }

    #region IFileReader<List<IIdentifiedSpectrum>> Members

    public override void ParseScoreAndOtherInformation(IIdentifiedSpectrum sph, XElement searchHit)
    {
      var scores = searchHit.FindDescendants("search_score");

      foreach (var item in scores)
      {
        var name = item.Attribute("name").Value;
        if (name.Equals("mvh"))
        {
          sph.Score = MyConvert.ToDouble(item.Attribute("value").Value);
        }
        else if (name.Equals("mzFidelity"))
        {
          sph.SpScore = MyConvert.ToDouble(item.Attribute("value").Value);
        }
      }
    }

    #endregion
  }
}
