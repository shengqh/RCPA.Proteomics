using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using RCPA.Utils;

namespace RCPA.Proteomics.Quantification.Srm
{
  public class AgilentSrmTransition : SrmTransition
  {
    public AgilentSrmTransition(double precursorMZ, double productIon)
      : base(precursorMZ, productIon)
    { }

    public AgilentSrmTransition()
      : base()
    { }

    public bool IsQual { get; set; }

    public string Q1Resolution { get; set; }

    public string Q3Resolution { get; set; }

    public double Fragmentor { get; set; }

    public double DeltaRT { get; set; }

    public string MSpolarity { get; set; }

    public override string ToString()
    {
      return MyConvert.Format("{0} {1:0.00}-{2:0.00}", PrecursorFormula, PrecursorMZ, ProductIon);
    }

    public override double FirstRetentionTime
    {
      get
      {
        if (this.Intensities != null && this.Intensities.Count > 0)
        {
          return base.FirstRetentionTime;
        }

        return this.ExpectCenterRetentionTime - this.DeltaRT;
      }
    }

    public override double LastRetentionTime
    {
      get
      {
        if (this.Intensities != null && this.Intensities.Count > 0)
        {
          return base.LastRetentionTime;
        }

        return this.ExpectCenterRetentionTime + this.DeltaRT;
      }
    }
  }
}
