using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using RCPA.Proteomics.Image;

namespace RCPA.Proteomics.Spectrum
{
  public interface IIonTypePeak:IPeak
  {
    IonType PeakType { get; set; }

    int PeakIndex { get; set; }
  }
}
