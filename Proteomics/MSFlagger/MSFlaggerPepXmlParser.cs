using RCPA.Proteomics.PeptideProphet;
using RCPA.Proteomics.Summary;
using System;
using System.Xml.Linq;

namespace RCPA.Proteomics.MSFlagger
{
  public class MSFlaggerPepXmlParser : AbstractPepXmlParser
  {
    public MSFlaggerPepXmlParser()
    { }

    protected override string FindModificationChar(PepXmlModifications ppmods, ModificationAminoacidMass modaa, string pureSeq)
    {
      if (!ppmods.HasModification(modaa.Mass))
      {
        var newmod = new PepXmlModificationItem()
        {
          Mass = modaa.Mass,
          IsVariable = true,
        };

        var aa = pureSeq[modaa.Position - 1];
        newmod.Aminoacid = aa.ToString();
        newmod.MassDiff = Math.Round(modaa.Mass - new Aminoacids()[aa].MonoMass);
        if (modaa.Position == 1)
        {
          newmod.IsTerminalN = true;
        }

        ppmods.Add(newmod);
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
      get { return SearchEngineType.MSFlagger; }
    }
  }
}
