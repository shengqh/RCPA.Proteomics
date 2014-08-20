using RCPA.Proteomics.Spectrum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RCPA.Proteomics.Summary
{
  //unfinished!
  public static class PeakAlignment
  {
    public static void Alignment<T, U>(PeakList<T> lst1, PeakList<U> lst2, double ppmTolerance)
      where T : Peak
      where U : Peak
    {
      //Initialize score matrix
      var scores = new double[lst1.Count, lst2.Count];
      for (int i = 0; i < lst1.Count; i++)
      {
        var mz = lst1[i].Mz;
        var delta = PrecursorUtils.ppm2mz(mz, ppmTolerance);
        var minMz = mz - delta;
        var maxMz = mz + delta;
        for (int j = 0; j < lst2.Count; j++)
        {
          if (lst2[j].Mz < minMz)
          {
            scores[i, j] = 0;
            continue;
          }

          if (lst2[j].Mz > maxMz)
          {
            for (int k = j; k < lst2.Count; k++)
            {
              scores[i, k] = 0;
            }
            break;
          }

          scores[i, j] = lst1[i].Intensity * lst2[j].Intensity;
        }
      }
    }
  }
}
