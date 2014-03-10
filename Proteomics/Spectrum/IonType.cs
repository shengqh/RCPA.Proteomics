using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RCPA.Proteomics.Spectrum
{
  public enum IonType { UNKNOWN, B, Y, B2, Y2, C, Z, PRECURSOR, PRECURSOR_NEUTRAL_LOSS_PHOSPHO, PRECURSOR_NEUTRAL_LOSS, B_NEUTRAL_LOSS_PHOSPHO, B_NEUTRAL_LOSS, Y_NEUTRAL_LOSS_PHOSPHO, Y_NEUTRAL_LOSS };

  public static class IonTypeUtils
  {
    public static string GetDisplayName(this IonType ionType)
    {
      if (ionType == IonType.B2 || ionType == IonType.Y2)
      {
        return ionType.ToString().Substring(0, 1).ToLower();
      }
      else
      {
        return ionType.ToString().ToLower();
      }
    }
  }
}
