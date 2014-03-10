using System;
using RCPA.Proteomics.Summary;

namespace RCPA.Proteomics.Mascot
{
  public class MascotScoreFunctions : AbstractScoreFunctions
  {
    public MascotScoreFunctions() : base("Score")
    {
    }

    public override double GetScoreBin(IIdentifiedSpectrum spectrum)
    {
      return Math.Floor(spectrum.Score);
    }
  }
}