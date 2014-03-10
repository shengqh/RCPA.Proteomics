using System;
using RCPA.Proteomics.Summary;

namespace RCPA.Proteomics.Sequest
{
  public class SequestXcorrFunctions : AbstractScoreFunctions
  {
    public SequestXcorrFunctions() : base("Xcorr")
    {
    }

    public override double GetScoreBin(IIdentifiedSpectrum spectrum)
    {
      return Math.Floor(spectrum.Score*10)/10;
    }
  }
}