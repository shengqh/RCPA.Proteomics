using System.Collections.Generic;

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
