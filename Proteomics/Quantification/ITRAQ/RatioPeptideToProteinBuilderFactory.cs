using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RCPA.Proteomics.Quantification.ITraq
{
  public static class RatioPeptideToProteinBuilderFactory
  {
    private static IRatioPeptideToProteinBuilder[] builders;

    private static Object obj = new Object();

    public static IRatioPeptideToProteinBuilder[] GetBuilders()
    {
      lock (obj)
      {
        if (null == builders)
        {
          builders = new IRatioPeptideToProteinBuilder[] { new RatioPeptideToProteinMedianBuilder(), new RatioPeptideToProteinSumBuilder() };
        }
      }
      return builders;
    }

    public static IRatioPeptideToProteinBuilder FindBuilder(string builderName)
    {
      var builders = GetBuilders();

      var result = builders.FirstOrDefault(m => m.Name.Equals(builderName));

      if (null == result)
      {
        return builders[0];
      }
      else
      {
        return result;
      }
    }
  }
}
