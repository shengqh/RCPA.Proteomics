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
  public class MascotPepXmlParser : PepXmlParser
  {
    public MascotPepXmlParser()
    { }

    protected override void ParseScore(IIdentifiedSpectrum sph, XElement searchHit)
    {
      var scores = searchHit.FindDescendants("search_score");

      foreach (var item in scores)
      {
        var name = item.Attribute("name").Value;
        if (name.Equals("ionscore") || name.Equals("IonScore"))
        {
          sph.Score = MyConvert.ToDouble(item.Attribute("value").Value);
        }
        else if (name.Equals("expect") || name.Equals("Exp Value"))
        {
          sph.ExpectValue = MyConvert.ToDouble(item.Attribute("value").Value);
        }
      }
    }

    public override SearchEngineType Engine
    {
      get { return SearchEngineType.MASCOT; }
    }
  }
}
