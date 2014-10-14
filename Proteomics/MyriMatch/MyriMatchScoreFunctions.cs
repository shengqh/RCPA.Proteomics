using RCPA.Proteomics.Summary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RCPA.Proteomics.MyriMatch
{
  public class MyriMatchScoreFunctions : AbstractScoreFunctions
  {
    public MyriMatchScoreFunctions() : base("MyriMatch:MVH") { }

    public override double GetScoreBin(IIdentifiedSpectrum spectrum)
    {
      return Math.Floor(spectrum.Score * 10) / 10;
    }
  }
}
