using RCPA.Proteomics.Spectrum;
using System;
using System.Collections.Generic;
using System.Linq;

namespace RCPA.Proteomics.Processor
{
  public class PeakListRemovePrecursorDaltonProcessor<T> : IProcessor<PeakList<T>> where T : IPeak, new()
  {
    private double[] offsets;
    private double massTolerance;

    public PeakListRemovePrecursorDaltonProcessor(double[] offsets, double massTolerance)
    {
      if (Array.IndexOf(offsets, 0.0) == -1)
      {
        this.offsets = new double[] { 0.0 }.Union(offsets).ToArray();
      }
      else
      {
        this.offsets = offsets;
      }
      this.massTolerance = massTolerance;
    }

    /// <summary>
    /// Construction of PeakListRemovePrecursorDaltonProcessor
    /// </summary>
    /// <param name="offsetString">Either atom composition or offset values, seperated by ','</param>
    /// <param name="massTolerance">Mass tolerance in Dalton</param>
    public PeakListRemovePrecursorDaltonProcessor(string offsetString, double massTolerance)
    {
      var acs = offsetString.Split(new char[] { ',', ';', ' ', '\t' }, System.StringSplitOptions.RemoveEmptyEntries);

      double v;
      List<double> masses;
      if (acs.All(m => double.TryParse(m, out v)))
      {
        masses = (from ac in acs
                  select double.Parse(ac)).ToList();
      }
      else
      {
        masses = (from acStr in acs
                  let ac = new AtomComposition(acStr)
                  select Atom.GetMonoMass(ac)).ToList();
      }

      var c13 = Atom.C13.MonoMass - Atom.C.MonoMass;
      masses.Insert(0, Atom.C13.MonoMass - Atom.C.MonoMass);
      masses.Insert(0, Atom.C13.MonoMass - Atom.C.MonoMass);
      masses.Insert(0, 0.0);
      this.offsets = masses.ToArray();
      this.massTolerance = massTolerance;
    }

    #region IProcessor<PeakList<T>> Members

    public PeakList<T> Process(PeakList<T> t)
    {
      if (t.PrecursorCharge > 0)
      {
        var mzWindow = massTolerance / t.PrecursorCharge;
        foreach (var offset in offsets)
        {
          var mz = t.PrecursorMZ + offset / t.PrecursorCharge;

          var minMz = mz - mzWindow;
          var maxMz = mz + mzWindow;

          t.RemoveAll(m => m.Mz >= minMz && m.Mz <= maxMz);
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