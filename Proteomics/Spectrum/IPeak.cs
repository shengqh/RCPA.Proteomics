using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RCPA.Proteomics.Spectrum
{
  public interface IPeak
  {
    double Mz { get; set; }

    double Intensity { get; set; }

    int Charge { get; set; }

    Dictionary<string, object> Annotations { get; }
  }
}
