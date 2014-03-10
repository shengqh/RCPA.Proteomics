using System.Collections.Generic;
using System.Linq;
using RCPA.Proteomics.Spectrum;

namespace RCPA.Proteomics.Processor
{
  public class PeakListDeisotopicProcessor<T> : IProcessor<PeakList<T>> where T : IPeak
  {
    private readonly double ppmTolerance;

    public PeakListDeisotopicProcessor(double ppmTolerance)
    {
      this.ppmTolerance = ppmTolerance;
    }

    #region IProcessor<PeakList<T>> Members

    public PeakList<T> Process(PeakList<T> t)
    {
      List<PeakList<T>> envelopes = t.GetEnvelopes(this.ppmTolerance);

      ((List<T>)t).Clear();

      foreach (var envelope in envelopes)
      {
        t.Add(envelope[0]);
      }

      t.Sort((m1, m2) => m1.Mz.CompareTo(m2.Mz));

      return t;
    }

    #endregion

    public override string ToString()
    {
      return "Deisotopic=True";
    }
  }
}