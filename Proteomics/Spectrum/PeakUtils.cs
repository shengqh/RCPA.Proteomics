using System;
using System.Collections.Generic;

namespace RCPA.Proteomics.Spectrum
{
  public static class PeakUtils
  {
    public static int FindMaxIndex<T>(this List<T> pkls) where T : Peak
    {
      if (pkls == null || pkls.Count == 0)
      {
        throw new ArgumentException("pkls can not be null or empty");
      }
      int result = 0;
      double intensity = pkls[0].Intensity;
      for (int i = 1; i < pkls.Count; i++)
      {
        if (pkls[i].Intensity > intensity)
        {
          result = i;
          intensity = pkls[i].Intensity;
        }
      }
      return result;
    }
  }
}
