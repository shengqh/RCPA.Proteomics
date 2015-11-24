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
  public abstract class AbstractPostSearchPepXmlParser : AbstractPepXmlParser
  {
    private IPepXmlParser baseParser;

    public AbstractPostSearchPepXmlParser()
    {
      this.baseParser = null;
    }

    public AbstractPostSearchPepXmlParser(IPepXmlParser baseParser)
    {
      this.baseParser = baseParser;
    }

    public override void ParseScoreAndOtherInformation(IIdentifiedSpectrum sph, XElement searchHit)
    {
      if (this.baseParser != null)
      {
        this.baseParser.ParseScoreAndOtherInformation(sph, searchHit);
      }
    }
  }
}
