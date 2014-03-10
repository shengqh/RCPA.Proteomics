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
        var precursorMass = t.PrecursorMZ * t.PrecursorCharge - Atom.H.MonoMass * (t.PrecursorCharge - 1);

        t.RemoveAll(m =>
        {
          double mass;
          if (m.Charge > 1)
          {
            mass = m.Mz * m.Charge - Atom.H.MonoMass * (m.Charge - 1);
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