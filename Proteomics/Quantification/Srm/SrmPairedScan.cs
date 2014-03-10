using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RCPA.Proteomics.Quantification.Srm
{
  public class SrmPairedScan
  {
    public double RetentionTime { get { return Light.RetentionTime; } }

    public SrmScan Light { get; set; }

    public SrmScan Heavy { get; set; }

    public override string ToString()
    {
      if (Heavy != null)
      {
        return MyConvert.Format("L={0} : H={1}", Light, Heavy);
      }

      return MyConvert.Format("L={0}", Light);
    }
  }
}
