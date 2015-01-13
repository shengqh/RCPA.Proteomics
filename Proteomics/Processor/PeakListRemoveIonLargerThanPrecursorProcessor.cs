using System.Collections.Generic;
using System.Linq;
using RCPA.Proteomics.Spectrum;
using System.Threading.Tasks;

namespace RCPA.Proteomics.Processor
{
  public class PeakListRemoveIonLargerThanPrecursorProcessor<T> : IProcessor<PeakList<T>> where T : IPeak
  {
    public PeakListRemoveIonLargerThanPrecursorProcessor()
    { }

    #region IProcessor<PeakList<T>> Members

    public PeakList<T> Process(PeakList<T> t)
    {
      if (t.PrecursorCharge > 0)
      {
        var precursorMass = PrecursorUtils.MzToMH(t.PrecursorMZ, t.PrecursorCharge, true);

        t.RemoveAll(m =>
        {
          double mass;
          if (m.Charge > 1)
          {
            mass = PrecursorUtils.MzToMH(m.Mz, m.Charge, true);
          }
          else
          {
            mass = m.Mz;
          }
          return mass > precursorMass;
        });
      }

      return t;
    }
    #endregion
    public override string ToString()
    {
      return string.Format("RemoveIonLargerThanPrecursor=True");
    }
  }

}