using RCPA.Proteomics.PeptideProphet;
using RCPA.Proteomics.Summary;
using System.Linq;
using System.Xml.Linq;

namespace RCPA.Proteomics.Comet
{
  public class CometXmlParser : AbstractPepXmlParser
  {
    public CometXmlParser() { }

    public override SearchEngineType Engine
    {
      get { return SearchEngineType.Comet; }
    }

    #region IFileReader<List<IIdentifiedSpectrum>> Members

    public override void ParseScoreAndOtherInformation(IIdentifiedSpectrum sph, XElement searchHit)
    {
      var map = searchHit.ToDictionary("search_score", "name", "value");
      string value;

      if (map.TryGetValue("xcorr", out value))
      {
        sph.Score = MyConvert.ToDouble(value);
      }

      if (map.TryGetValue("deltacn", out value))
      {
        sph.DeltaScore = MyConvert.ToDouble(value);
      }

      if (map.TryGetValue("spscore", out value))
      {
        sph.SpScore = MyConvert.ToDouble(value);
      }

      if (map.TryGetValue("spscore", out value))
      {
        sph.SpScore = MyConvert.ToDouble(value);
      }

      if (map.TryGetValue("sprank", out value))
      {
        sph.SpRank = int.Parse(value);
      }

      if (map.TryGetValue("expect", out value))
      {
        sph.ExpectValue = MyConvert.ToDouble(value);
      }

      sph.Query.MatchCount = int.Parse(searchHit.Attribute("num_matched_peptides").Value);
    }

    #endregion
  }
}
