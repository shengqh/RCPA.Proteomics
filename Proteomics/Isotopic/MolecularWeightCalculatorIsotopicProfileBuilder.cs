using System;
using System.Collections.Generic;
using MwtWinDll;

namespace RCPA.Proteomics.Isotopic
{
  public class MolecularWeightCalculatorIsotopicProfileBuilder2 : AbstractIsotopicProfileBuilder
  {
    private readonly MolecularWeightCalculator calculator;

    public MolecularWeightCalculatorIsotopicProfileBuilder2()
    {
      this.calculator = new MolecularWeightCalculator();
      this.calculator.SetElementMode(MwtWinDll.MWElementAndMassRoutines.emElementModeConstants.emIsotopicMass);
    }

    public override List<double> GetProfile(AtomComposition ac, int profileLength)
    {
      String formula = ac.GetCHNOSFormula();

      var result = new List<double>();
      string resultStr = "";
      double[,] d = null;
      int count = 0;
      if (0 ==
          this.calculator.ComputeIsotopicAbundances(ref formula, 1, ref resultStr, ref d, ref count, "Formula", "Mass",
                                                    "Fraction", "Intensity"))
      {
        if (resultStr.StartsWith("Formula"))
        {
          double totalIntensity = 0.0;
          for (int i = 1; i < d.GetLength(0); i++)
          {
            totalIntensity += d[i, 1];
          }

          for (int i = 1; i < d.GetLength(0); i++)
          {
            result.Add(d[i, 1]/totalIntensity);
          }
        }
        else
        {
          throw new Exception(MyConvert.Format("Calculate profile of {0} error : {1}", formula, resultStr));
        }
      }

      return result;
    }
  }
}