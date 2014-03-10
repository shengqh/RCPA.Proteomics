using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using RCPA.Proteomics.Spectrum;

namespace RCPA.Proteomics.Fragmentation
{
  public class CIDPeptideB2SeriesBuilder<T> : AbstractPeptideBSeriesBuilder<T> where T : IIonTypePeak, new()
  {
    protected override double GetTerminalMass()
    {
      return NtermMass + MassH ;
    }

    protected override int IonCharge
    {
      get
      {
        return 2;
      }
    }

    public override IonType SeriesType { get { return IonType.B2; } }
  }
}
