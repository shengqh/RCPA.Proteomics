using System.Collections.Generic;

namespace RCPA.Proteomics.Spectrum
{
  public class PeakListDeductionByWindow : IPeakListDeduction
  {
    private static readonly double WINDOW_SIZE = 100.0;

    private int maxCount;

    public PeakListDeductionByWindow(int maxCount)
    {
      this.maxCount = maxCount;
    }

    #region IPeakListDeduction Members

    public void Deduct<T>(PeakList<T> pkl) where T : Peak
    {
      pkl.SortByMz();

      List<T> currPkl = new List<T>();
      currPkl.Add(pkl[0]);
      int end = 1;

      HashSet<T> deletePkl = new HashSet<T>();

      while (true)
      {
        while (end < pkl.Count && pkl[end].Mz - currPkl[0].Mz < WINDOW_SIZE)
        {
          currPkl.Add(pkl[end]);
          end++;
        }

        if (currPkl.Count <= maxCount)
        {
          if (end == pkl.Count)
          {
            break;
          }

          currPkl.RemoveAt(0);
        }
        else
        {
          List<T> tmp = new List<T>(currPkl);
          var lastPeak = currPkl[0];
          tmp.Sort((m1, m2) => -m1.Intensity.CompareTo(m2.Intensity));
          for (int i = maxCount; i < tmp.Count; i++)
          {
            deletePkl.Add(tmp[i]);
            currPkl.Remove(tmp[i]);
          }

          if (currPkl[0] == lastPeak)
          {
            currPkl.RemoveAt(0);
          }
        }
      }

      pkl.RemoveAll(m => deletePkl.Contains(m));
    }

    #endregion
  }
}
