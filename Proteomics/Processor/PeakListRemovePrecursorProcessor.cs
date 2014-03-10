using System;
using RCPA.Proteomics.Spectrum;
using System.Linq;

namespace RCPA.Proteomics.Processor
{
  public class PeakListRemovePrecursorProcessor<T> : IProcessor<PeakList<T>> where T : IPeak, new()
  {
    private double[] offsets;
    private double ppmTolerance;

    public PeakListRemovePrecursorProcessor(double[] offsets, double ppmTolerance)
    {
      this.offsets = offsets;
      this.ppmTolerance = ppmTolerance;
    }

    public PeakListRemovePrecursorProcessor(string atomCompositions, double ppmTolerance)
    {
      var acs = atomCompositions.Split(new char[] { ',', ';', ' ', '\t' }, System.StringSplitOptions.RemoveEmptyEntries);
      var masses = (from acStr in acs
                    let ac = new AtomComposition(acStr)
                    select Atom.GetMonoMass(ac)).ToList();
      masses.Insert(0, 0.0);
      this.offsets = masses.ToArray();
      this.ppmTolerance = ppmTolerance;
    }

    #region IProcessor<PeakList<T>> Members

    public PeakList<T> Process(PeakList<T> t)
    {
      if (t.PrecursorCharge > 0)
      {
        foreach (var offset in offsets)
        {
          if (offset != 0.0)
          {
            var mz = t.PrecursorMZ + offset / t.PrecursorCharge;
            var mzWindow = PrecursorUtils.ppm2mz(mz, ppmTolerance);

            var minMz = mz - mzWindow;
            var maxMz = mz + mzWindow;

            t.RemoveAll(m => m.Mz >= minMz && m.Mz <= maxMz);
          }
          else
          {
            var mzWindow = PrecursorUtils.ppm2mz(t.PrecursorMZ, ppmTolerance);

            var minMz = t.PrecursorMZ - mzWindow;
            var maxMz = t.PrecursorMZ + mzWindow;

            t.RemoveAll(m => m.Mz >= minMz && m.Mz <= maxMz);
          }
        }
      }

      return t;
    }

    #endregion
    public override string ToString()
    {
      return "RemovePrecursorAndOffsetIons=True," + (from offset in offsets
                                                     select string.Format("{0:0.####}", offset)).Merge(',');
    }
  }
}