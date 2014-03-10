using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RCPA.Proteomics.Quantification.Srm
{
  public class SrmPeptideItem
  {
    public SrmPeptideItem()
    {
      ProductIons = new List<SrmTransition>();
    }

    public double PrecursorMZ { get; set; }

    public List<SrmTransition> ProductIons { get; set; }

    public override string ToString()
    {
      StringBuilder sb = new StringBuilder();
      sb.AppendFormat("{0:0.00} [", PrecursorMZ);
      sb.Append((from ion in ProductIons
                 select MyConvert.Format("{0:0.00}", ion.ProductIon)).Merge(",") + "]");
      return sb.ToString();
    }
  }
}
