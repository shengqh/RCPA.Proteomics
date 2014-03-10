using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RCPA.Proteomics.Isotopic
{
  public static class IsotopicBuilderFactory
  {
    public static IIsotopicProfileBuilder2 GetBuilder()
    {
      return new EmassProfileBuilder();
    }
  }
}
