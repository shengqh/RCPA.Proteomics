using System;

namespace RCPA.Proteomics.Spectrum
{
  public class PeakListDeductionByITRAQ : IPeakListDeduction
  {
    #region IPeakListDeduction Members

    public void Deduct<T>(PeakList<T> pkl) where T : Peak
    {
      throw new NotImplementedException();
    }

    #endregion
  }
}
