using System;
using System.Collections.Generic;
using RCPA.Utils;

namespace RCPA.Proteomics.Isotopic
{
  /**
   * This implementation of IIsotopicProfile only considers 
   * C/H/O/N/S, not all the atoms
   */

  public class CHONSIsotopicProfileBuilder : AbstractIsotopicProfileBuilder
  {
    public const double C12 = 0.988922;
    public const double C13 = 0.011078;
    public const double H = 0.99984426;
    public const double H2 = 0.00015574;
    public const double N14 = 0.996337;
    public const double N15 = 0.003663;
    public const double O16 = 0.997628;
    public const double O18 = 0.002;
    public const double RATIO_C = C13/C12;
    public const double RATIO_H = H2/H;
    public const double RATIO_N = N15/N14;
    public const double RATIO_O = O18/O16;
    public const double RATIO_S = S34/S32;
    public const double S32 = 0.95018;
    public const double S34 = 0.04215;

    public override List<double> GetProfile(AtomComposition ac, int profileLength)
    {
      int countC = ac.GetAtomCount(Atom.C);
      int countH = ac.GetAtomCount(Atom.H);
      int countO = ac.GetAtomCount(Atom.O);
      int countN = ac.GetAtomCount(Atom.N);
      int countS = ac.GetAtomCount(Atom.S);
      int maxC13 = Math.Min(profileLength, countC);
      int maxH2 = Math.Min(profileLength, countH);
      int maxO18 = Math.Min(profileLength/2, countO);
      int maxN15 = Math.Min(profileLength, countN);
      int maxS34 = Math.Min(profileLength/2, countS);

      var profile = new double[profileLength];
      for (int countC13 = 0; countC13 <= maxC13; countC13++)
      {
        int pcLength = countC13;
        double combC = StatisticsUtils.GetCombinationProbability(countC13, countC, C13, C12);
        for (int countH2 = 0; countH2 <= maxH2; countH2++)
        {
          int phLength = pcLength + countH2;
          if (phLength >= profileLength)
          {
            break;
          }
          double combH = StatisticsUtils.GetCombinationProbability(countH2, countH, H2, H);
          for (int countN15 = 0; countN15 <= maxN15; countN15++)
          {
            int pnLength = phLength + countN15;
            if (pnLength >= profileLength)
            {
              break;
            }
            double combN = StatisticsUtils.GetCombinationProbability(countN15, countN, N15, N14);
            for (int countO18 = 0; countO18 <= maxO18; countO18++)
            {
              int poLength = pnLength + countO18*2;
              if (poLength >= profileLength)
              {
                break;
              }
              double combO = StatisticsUtils.GetCombinationProbability(countO18, countO, O18, O16);

              for (int countS34 = 0; countS34 <= maxS34; countS34++)
              {
                int psLength = poLength + countS34*2;
                if (psLength >= profileLength)
                {
                  break;
                }
                double combS = StatisticsUtils.GetCombinationProbability(countS34, countS, S34, S32);
                double totalProbability = combC*combH*combN*combO*combS;
                profile[psLength] += totalProbability;
              }
            }
          }
        }
      }
      var result = new List<double>();
      foreach (double value in profile)
      {
        result.Add(value);
      }

      return result;
    }
  }
}