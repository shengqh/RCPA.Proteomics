using RCPA.Proteomics.Summary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RCPA.Proteomics.MSGF
{
  public class MSGFScoreFunctions : AbstractScoreFunctions
  {
    public MSGFScoreFunctions() : base("MS-GF:RawScore") { }

    public override double GetScoreBin(IIdentifiedSpectrum spectrum)
    {
      return Math.Floor(spectrum.Score * 10) / 10;
    }
  }
}
