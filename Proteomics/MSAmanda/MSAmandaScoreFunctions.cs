using RCPA.Proteomics.Summary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RCPA.Proteomics.MSAmanda
{
  public class MSAmandaScoreFunctions : AbstractScoreFunctions
  {
    public MSAmandaScoreFunctions() : base("MSAmanda:Score") { }

    public override double GetScoreBin(IIdentifiedSpectrum spectrum)
    {
      return Math.Round(spectrum.Score / 10);
    }
  }
}
