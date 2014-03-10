using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RCPA.Proteomics.Image
{
  public static class NeutralLossUtils
  {
    public static bool CanLossWater(char aminoacid)
    {
      return aminoacid == 'S' || aminoacid == 'T' || aminoacid == 'E' || aminoacid == 'D';
    }

    public static bool CanLossWater(string peptide)
    {
      return true;
    }

    public static bool CanLossAmmonia(char aminoacid)
    {
      return aminoacid == 'R' || aminoacid == 'K' || aminoacid == 'N' || aminoacid == 'Q';
    }

    public static bool CanLossAmmonia(string peptide)
    {
      foreach (char c in peptide)
      {
        if (CanLossAmmonia(c))
        {
          return true;
        }
      }

      return false;
    }
  }
}
