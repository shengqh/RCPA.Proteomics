using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RCPA.Proteomics.Spectrum
{
  public interface IPeakListDeduction
  {
    void Deduct<T>(PeakList<T> pkl) where T : Peak;
  }
}
