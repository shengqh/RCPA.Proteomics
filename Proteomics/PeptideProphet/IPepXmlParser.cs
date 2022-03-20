using RCPA.Proteomics.Summary;
using System.Xml.Linq;

namespace RCPA.Proteomics.PeptideProphet
{
  public interface IPepXmlParser : ISpectrumParser
  {
    void ParseScoreAndOtherInformation(IIdentifiedSpectrum sph, XElement searchHit);
  }
}
