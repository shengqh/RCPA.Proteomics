using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RCPA.Proteomics.Spectrum
{
  public class IonTypePeak : Peak, IIonTypePeak
  {
    public IonTypePeak()
      : base(0.0, 0.0, 0)
    { }

    public IonTypePeak(IonTypePeak p)
      : base(p)
    {
      this.PeakType = p.PeakType;
      this.PeakIndex = p.PeakIndex;
    }

    public IonTypePeak(double aMz, double aIntensity)
      : base(aMz, aIntensity, 0)
    { }

    public IonTypePeak(double aMz, double aIntensity, int aCharge)
      : base(aMz, aIntensity, aCharge)
    { }

    #region IIonTypePeak Members

    public IonType PeakType { get; set; }

    public int PeakIndex { get; set; }

    #endregion
  }
}
