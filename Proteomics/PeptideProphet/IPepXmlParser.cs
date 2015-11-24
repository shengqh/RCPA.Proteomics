using RCPA.Gui;
using RCPA.Proteomics.Summary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace RCPA.Proteomics.PeptideProphet
{
  public interface IPepXmlParser : ISpectrumParser
  {
    void ParseScoreAndOtherInformation(IIdentifiedSpectrum sph, XElement searchHit);
  }
}
