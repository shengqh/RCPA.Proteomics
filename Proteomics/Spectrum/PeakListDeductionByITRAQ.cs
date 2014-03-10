using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RCPA.Proteomics.Spectrum
{
  public class PeakListDeductionByITRAQ:IPeakListDeduction
  {
    #region IPeakListDeduction Members

    public void Deduct<T>(PeakList<T> pkl) where T : Peak
    {
      throw new NotImplementedException();
    }

    #endregion
  }
}
