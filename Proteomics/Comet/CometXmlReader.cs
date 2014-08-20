using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using RCPA.Proteomics.Summary;
using System.Xml.Linq;
using RCPA.Gui;
using RCPA.Proteomics.PeptideProphet;

namespace RCPA.Proteomics.Comet
{
  public class CometXmlReader : PeptideProphetXmlReader
  {
    public CometXmlReader(ITitleParser parser)
      : base(parser)
    { }
    #region IFileReader<List<IIdentifiedSpectrum>> Members

    protected override void ParseScore(IIdentifiedSpectrum sph, XElement searchHit)
    {
      base.ParseScore(sph, searchHit);

      var scores = searchHit.FindChildren("search_score");

      foreach (var item in scores)
      {
        var name = item.Attribute("name").Value;
        if (name.Equals("xcorr"))
        {
          sph.Score = MyConvert.ToDouble(item.Attribute("value").Value);
        }
        else if (name.Equals("deltacn"))
        {
          sph.DeltaScore = MyConvert.ToDouble(item.Attribute("value").Value);
        }
        else if (name.Equals("sprank"))
        {
          sph.SpRank = int.Parse(item.Attribute("value").Value);
        }
      }
    }

    #endregion
  }
}
