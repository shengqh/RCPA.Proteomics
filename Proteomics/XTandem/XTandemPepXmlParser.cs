using RCPA.Proteomics.PeptideProphet;
using RCPA.Proteomics.Summary;
using System.Xml.Linq;

namespace RCPA.Proteomics.XTandem
{
  public class XTandemPepXmlParser : AbstractPepXmlParser
  {
    public XTandemPepXmlParser()
    { }

    protected override string FindModificationChar(PepXmlModifications ppmods, ModificationAminoacidMass modaa)
    {
      if (!ppmods.HasModification(modaa.Mass))
      {
        ppmods.Add(new PepXmlModificationItem()
        {
          Mass = modaa.Mass,
          IsVariable = true,
        });
        ppmods.AssignModificationChar();
      }
      return ppmods.FindModificationChar(modaa.Mass);
    }

    public override void ParseScoreAndOtherInformation(IIdentifiedSpectrum sph, XElement searchHit)
    {
      var scores = searchHit.FindDescendants("search_score");

      foreach (var item in scores)
      {
        var name = item.Attribute("name").Value;
        if (name.Equals("hyperscore"))
        {
          sph.Score = MyConvert.ToDouble(item.Attribute("value").Value);
        }
        else if (name.Equals("nextscore"))
        {
          var nextScore = MyConvert.ToDouble(item.Attribute("value").Value);
          sph.DeltaScore = sph.Score - nextScore;
        }
        else if (name.Equals("expect"))
        {
          sph.ExpectValue = MyConvert.ToDouble(item.Attribute("value").Value);
        }
      }
    }

    public override SearchEngineType Engine
    {
      get { return SearchEngineType.XTandem; }
    }
  }
}
