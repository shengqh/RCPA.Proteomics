using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RCPA.Proteomics.Summary
{
  public static class FalseDiscoveryRateUtils
  {
    public static double CalculateStepFdr(double initFdr)
    {
      double stepFdr = 0.1;

      while (initFdr <= stepFdr || Math.Abs(initFdr - stepFdr) <= 0.00001)
      {
        stepFdr /= 10;
      }

      return stepFdr;
    }
  }
}
