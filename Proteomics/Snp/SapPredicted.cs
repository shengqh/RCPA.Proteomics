using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RCPA.Proteomics.Snp
{
  public class SapPredicted
  {
    public MS2Item Ms2 { get; set; }
    public MS2Item LibMs2 { get; set; }
    public SapMatchedCount Matched { get; set; }
    public TargetSAP Target { get; set; }
    public TargetSAP Expect { get; set; }
    public bool IsExpect
    {
      get
      {
        if (this.Expect == null)
        {
          return false;
        }
        return Expect.Source == Target.Source && Expect.Target == Target.Target;
      }
    }
  }
}
