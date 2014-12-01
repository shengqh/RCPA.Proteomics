using System;
using RCPA.Proteomics.Spectrum;
using System.Linq;
using System.Collections.Generic;

namespace RCPA.Proteomics.Processor
{
  public class PeakListRemovePrecursorProcessor<T> : IProcessor<PeakList<T>> where T : IPeak, new()
  {
    private double[] offsets;
    private double ppmTolerance;
    private bool removeLessOneCharge;
    private bool removeIonLargerThanPrecursor;

    public PeakListRemovePrecursorProcessor(double[] neutralLoss, double ppmTolerance, bool removeLessOneCharge, bool removeIonLargerThanPrecursor)
    {
      this.offsets = neutralLoss;
      this.ppmTolerance = ppmTolerance;
      this.removeLessOneCharge = removeLessOneCharge;
      this.removeIonLargerThanPrecursor = removeIonLargerThanPrecursor;
    }

    public PeakListRemovePrecursorProcessor(string neutralLoss, double ppmTolerance)
    {
      var acs = neutralLoss.Split(new char[] { ',', ';', ' ', '\t' }, System.StringSplitOptions.RemoveEmptyEntries);
      var masses = new List<double>();
      foreach (var acStr in acs)
      {
        double mass;
        if (double.TryParse(acStr, out mass))
        {
          masses.Add(mass);
        }
        else
        {
          var ac = new AtomComposition(acStr);
          masses.Add(Atom.GetMonoMass(ac));
        }
      }
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