using RCPA.Proteomics.Spectrum;
using System.Collections.Generic;
using System.Linq;

namespace RCPA.Proteomics.Processor
{
  public class PeakListDeisotopicByChargeProcessor<T> : IProcessor<PeakList<T>> where T : IPeak
  {
    private enum IsotopicType { NONE, INCREASING, DECREASING };

    private readonly double ppmTolerance;

    public PeakListDeisotopicByChargeProcessor(double ppmTolerance)
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

      List<T> kept = (from p in t
                      where p.Charge == 0
                      select p).ToList();

      t.RemoveAll(m => m.Charge == 0);

      while (t.Count > 0)
      {
        var curPeaks = FindEnvelope(t, t[0], PrecursorUtils.ppm2mz(t[0].Mz, this.ppmTolerance));
        t.RemoveAll(m => curPeaks.Contains(m));

        if (curPeaks.Count == 1 || curPeaks[0].Intensity >= curPeaks[1].Intensity)
        {
          kept.Add(curPeaks[0]);
          continue;
        }

        var mass = curPeaks[0].Mz * curPeaks[0].Charge - Atom.H.MonoMass;
        if (mass > 1800)
        {
          kept.Add(curPeaks[0]);
          continue;
        }

        kept.Add(curPeaks[0]);
        kept.Add(curPeaks[1]);
      }

      t.AddRange(kept);
      t.Sort((m1, m2) => m1.Mz.CompareTo(m2.Mz));

      return t;
    }

    protected PeakList<T> FindEnvelope(PeakList<T> t, T peak, double mzTolerance)
    {
      IsotopicType itype = IsotopicType.NONE;
      var env = t.FindEnvelope(peak, mzTolerance, true);
      if (env.Count <= 2)
      {
        return env;
      }

      for (int i = 1; i < env.Count; i++)
      {
        if (env[i].Intensity >= env[i - 1].Intensity)//递增
        {
          if (itype == IsotopicType.DECREASING)//如果前面是递减，则这次递增无效。
          {
            env.RemoveRange(i, env.Count - i);
            break;
          }
        }
        else//递减
        {
          itype = IsotopicType.DECREASING;
        }
      }

      return env;
    }
    #endregion

    public override string ToString()
    {
      return "DeisotopicByCharge=True";
    }
  }
}