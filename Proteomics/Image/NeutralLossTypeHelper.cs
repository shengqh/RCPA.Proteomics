using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RCPA.Proteomics.Image
{
  public static class NeutralLossTypeHelper
  {
    public static bool IsPhosphoNeutralLossType(this INeutralLossType aType)
    {
      return aType.Name.Contains("PO") ;
    }
  }
}
