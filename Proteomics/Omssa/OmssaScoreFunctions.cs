using RCPA.Proteomics.Summary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RCPA.Proteomics.Omssa
{
  public class OmssaScoreFunctions : AbstractScoreFunctions
  {
    public OmssaScoreFunctions() : base("Omssa:-log(evalue)") { }

    public override double GetScoreBin(IIdentifiedSpectrum spectrum)
    {
      return Math.Floor(spectrum.Score * 10) / 10;
    }
  }
}
