using System.Collections.Generic;
using System.Linq;

namespace RCPA.Proteomics.Spectrum
{
  public class PeakListCountProcessor : IProcessor<PeakList<Peak>>
  {
    private int count;

    public PeakListCountProcessor(int count)
    {
      this.count = count;
    }

    #region IProcessor<PeakList<Peak>> Members

    public PeakList<Peak> Process(PeakList<Peak> t)
    {
      if (t.Count == 0)
      {
        return t;
      }

      t.SortByMz();

      List<Peak> final = new List<Peak>();

      List<Peak> kept = new List<Peak>();

      double minMz = 0;
      double lastMz = t.Last().Mz;

      int index = 0;
      while (index < t.Count)
      {
        var maxMz = minMz + 100;
        kept.Clear();
        while (index < t.Count)
        {
          if (t[index].Mz > maxMz)
          {
            break;
          }

          kept.Add(t[index]);
          index++;
        }

        if (kept.Count > count)
        {
          kept.Sort((m1, m2) => m2.Intensity.CompareTo(m1.Intensity));
          kept.RemoveRange(count, kept.Count - count);
        }

        final.AddRange(kept);
        minMz += 100;
      }

      t.Clear();
      t.AddRange(final);

      return t;
    }

    #endregion
  }
}
