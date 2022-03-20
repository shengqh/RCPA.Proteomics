using RCPA.Utils;
using System;

namespace RCPA.Proteomics.Utils
{
  public class IsoelectricPointCalculator
  {
    // Table of pk values :
    // Note: the current algorithm does not use the last two columns. Each
    // row corresponds to an amino acid starting with Ala. J, O and U are
    // inexistant, but here only in order to have the complete alphabet.
    //
    // Ct Nt Sm Sc Sn
    //

    private static readonly double[,] cPk = new[,]
                                              {
                                                {3.55, 7.59, 0, 0, 0,}, // A
                                                {
                                                  3.55, 7.50, 0, 0, 0,
                                                }
                                                , {
                                                    // B
                                                    3.55, 7.50, 9.00, 9.00, 9.00,
                                                  }
                                                , {
                                                    // C
                                                    4.55, 7.50, 4.05, 4.05, 4.05,
                                                  }
                                                , {
                                                    // D
                                                    4.75, 7.70, 4.45, 4.45, 4.45,
                                                  }
                                                , {
                                                    // E
                                                    3.55, 7.50, 0, 0, 0,
                                                  }
                                                , {
                                                    // F
                                                    3.55, 7.50, 0, 0, 0,
                                                  }
                                                , {
                                                    // G
                                                    3.55, 7.50, 5.98, 5.98, 5.98,
                                                  }
                                                , {
                                                    // H
                                                    3.55, 7.50, 0, 0, 0,
                                                  }
                                                , {
                                                    // I
                                                    0.00, 0.00, 0, 0, 0,
                                                  }
                                                , {
                                                    // J
                                                    3.55, 7.50, 10.00, 10.00, 10.00,
                                                  }
                                                , {
                                                    // K
                                                    3.55, 7.50, 0, 0, 0,
                                                  }
                                                , {
                                                    // L
                                                    3.55, 7.00, 0, 0, 0,
                                                  }
                                                , {
                                                    // M
                                                    3.55, 7.50, 0, 0, 0,
                                                  }
                                                , {
                                                    // N
                                                    0.00, 0.00, 0, 0, 0,
                                                  }
                                                , {
                                                    // O
                                                    3.55, 8.36, 0, 0, 0,
                                                  }
                                                , {
                                                    // P
                                                    3.55, 7.50, 0, 0, 0,
                                                  }
                                                , {
                                                    // Q
                                                    3.55, 7.50, 12.0, 12.0, 12.0,
                                                  }
                                                , {
                                                    // R
                                                    3.55, 6.93, 0, 0, 0,
                                                  }
                                                , {
                                                    // S
                                                    3.55, 6.82, 0, 0, 0,
                                                  }
                                                , {
                                                    // T
                                                    0.00, 0.00, 0, 0, 0,
                                                  }
                                                , {
                                                    // U
                                                    3.55, 7.44, 0, 0, 0,
                                                  }
                                                , {
                                                    // V
                                                    3.55, 7.50, 0, 0, 0,
                                                  }
                                                , {
                                                    // W
                                                    3.55, 7.50, 0, 0, 0,
                                                  }
                                                , {
                                                    // X
                                                    3.55, 7.50, 10.00, 10.00, 10.00,
                                                  }
                                                , {
                                                    // Y
                                                    3.55, 7.50, 0, 0, 0
                                                  } // Z
                                              };

    private static int C = 'C' - 'A';
    private static int D = 'D' - 'A';
    private static int E = 'E' - 'A';

    private static double EPSI = 0.0001; /* desired precision */
    private static int H = 'H' - 'A';
    private static int K = 'K' - 'A';
    private static int MAXLOOP = 2000; /* maximum number of iterations */
    private static double PH_MAX = 14; /* maximum pH value */
    private static double PH_MIN = 0; /* minimum pH value */
    private static int R = 'R' - 'A';
    private static int Y = 'Y' - 'A';

    private IsoelectricPointCalculator()
    {
    }

    /**
     *
     * @param proteinSequence UPPERCASED protein sequence, characters not in
     *   ('A'..'Z') will be ignored.
     * @return Isoelectric point of protein sequence
     */

    public static double GetIsoelectricPoint(String proteinSequence)
    {
      int[] comp = GetAminoacidComposition(proteinSequence);
      if (comp == null)
      {
        return 7.0;
      }

      int nTermResidue = GetNTermResidue(proteinSequence);
      int cTermResidue = GetCTermResidue(proteinSequence);

      double phMin = PH_MIN;
      double phMax = PH_MAX;

      for (int i = 0; i < MAXLOOP && (phMax - phMin) > EPSI; i++)
      {
        double phMid = phMin + (phMax - phMin) / 2;

        double cter =
          MathUtils.Exp10(-cPk[cTermResidue, 0])
          / (MathUtils.Exp10(-cPk[cTermResidue, 0]) + MathUtils.Exp10(-phMid));
        double nter =
          MathUtils.Exp10(-phMid) /
          (MathUtils.Exp10(-cPk[nTermResidue, 1]) + MathUtils.Exp10(-phMid));

        double carg =
          comp[R] * MathUtils.Exp10(-phMid) /
          (MathUtils.Exp10(-cPk[R, 2]) + MathUtils.Exp10(-phMid));
        double chis =
          comp[H] * MathUtils.Exp10(-phMid) /
          (MathUtils.Exp10(-cPk[H, 2]) + MathUtils.Exp10(-phMid));
        double clys =
          comp[K] * MathUtils.Exp10(-phMid) /
          (MathUtils.Exp10(-cPk[K, 2]) + MathUtils.Exp10(-phMid));

        double casp =
          comp[D] * MathUtils.Exp10(-cPk[D, 2]) /
          (MathUtils.Exp10(-cPk[D, 2]) + MathUtils.Exp10(-phMid));
        double cglu =
          comp[E] * MathUtils.Exp10(-cPk[E, 2]) /
          (MathUtils.Exp10(-cPk[E, 2]) + MathUtils.Exp10(-phMid));

        double ccys =
          comp[C] * MathUtils.Exp10(-cPk[C, 2]) /
          (MathUtils.Exp10(-cPk[C, 2]) + MathUtils.Exp10(-phMid));
        double ctyr =
          comp[Y] * MathUtils.Exp10(-cPk[Y, 2]) /
          (MathUtils.Exp10(-cPk[Y, 2]) + MathUtils.Exp10(-phMid));

        double charge =
          carg + clys + chis + nter - (casp + cglu + ctyr + ccys + cter);

        if (charge > 0.0)
        {
          phMin = phMid;
        }
        else
        {
          phMax = phMid;
        }
      }

      return phMax;
    }

    private static int[] GetAminoacidComposition(String aSequence)
    {
      aSequence = aSequence.ToUpper();

      var result = new int[26];
      for (int i = 0; i < 26; i++)
      {
        result[i] = 0;
      }

      bool validResidueExisted = false;
      for (int i = 0; i < aSequence.Length; i++)
      {
        if (aSequence[i] < 'A' ||
            aSequence[i] > 'Z')
        {
          continue;
        }

        result[aSequence[i] - 'A']++;
        validResidueExisted = true;
      }

      if (validResidueExisted)
      {
        return result;
      }
      else
      {
        return null;
      }
    }

    private static int GetNTermResidue(String aSequence)
    {
      int result = 0;
      for (int i = 0; i < aSequence.Length; i++)
      {
        if (aSequence[i] >= 'A' &&
            aSequence[i] <= 'Z')
        {
          result = aSequence[i] - 'A';
          break;
        }
      }
      return result;
    }

    private static int GetCTermResidue(String aSequence)
    {
      int result = 0;
      for (int i = aSequence.Length - 1; i >= 0; i--)
      {
        if (aSequence[i] >= 'A' &&
            aSequence[i] <= 'Z')
        {
          result = aSequence[i] - 'A';
          break;
        }
      }
      return result;
    }
  }
}