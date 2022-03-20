using RCPA.Proteomics.Spectrum;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RCPA.Proteomics.Processor
{
  public class PeakListDeconvolutionByChargeProcessor<T> : IProcessor<PeakList<T>> where T : IPeak
  {
    private double ppmTolerance;
    public PeakListDeconvolutionByChargeProcessor(double ppmTolerance)
    {
      this.ppmTolerance = ppmTolerance;
    }

    #region IProcessor<PeakList<T>> Members

    public PeakList<T> Process(PeakList<T> t)
    {
      if (!t.Any(m => m.Charge > 0))
      {
        return t;
      }

      Parallel.ForEach(t, m =>
      {
        if (m.Charge > 1)
        {
          m.Mz = m.Mz * m.Charge - Atom.H.MonoMass * (m.Charge - 1);
          m.Charge = 1;
        }
      });

      var peaks = (from p in t
                   where p.Charge == 1
                   orderby p.Intensity
                   select p).ToList();

      var deleted = new HashSet<T>();
      while (peaks.Count > 0)
      {
        var mz = peaks[0].Mz;
        var mzTolerance = PrecursorUtils.ppm2mz(mz, ppmTolerance);
        var minMz = mz - mzTolerance;
        var maxMz = mz + mzTolerance;
        var curPeaks = (from p in peaks
                        where p.Mz >= minMz && p.Mz <= maxMz
                        select p).ToList();
        if (curPeaks.Count > 1)
        {
          var nIntensity = curPeaks.Sum(m => m.Intensity);
          var nMz = curPeaks.Sum(m => m.Mz * m.Intensity) / nIntensity;
          peaks[0].Mz = nMz;
          peaks[0].Intensity = nIntensity;
          curPeaks.Remove(peaks[0]);
          deleted.UnionWith(curPeaks);
        }
        peaks.RemoveAll(m => curPeaks.Contains(m));
      }

      t.RemoveAll(m => deleted.Contains(m));

      t.SortByMz();
      return t;
    }
    #endregion

    public override string ToString()
    {
      return "DeconvolutionByCharge=True";
    }
  }

}