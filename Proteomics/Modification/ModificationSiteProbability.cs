using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RCPA.Proteomics.Modification
{
  public class ModificationSiteProbability
  {
    public string Aminoacid { get; set; }

    public int Site { get; set; }

    public double Probability { get; set; }

    public override string ToString()
    {
      return string.Format("{0}({1}): {2}", Aminoacid, Site, Probability);
    }
  }
}
