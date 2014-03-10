using System;
using System.Collections.Generic;
using System.Text;
using RCPA.Proteomics.Summary;

namespace RCPA.Proteomics.XTandem
{
  public class XTandemScoreFunctions : AbstractScoreFunctions
  {
    public XTandemScoreFunctions() : base("Score") { }

    public override double GetScoreBin(IIdentifiedSpectrum spectrum)
    {
      return Math.Floor(spectrum.Score);
    }
  }
}
