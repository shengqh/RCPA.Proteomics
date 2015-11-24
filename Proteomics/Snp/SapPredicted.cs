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
    public TargetVariant Target { get; set; }
    public TargetVariant Expect { get; set; }
    public bool IsExpect
    {
      get
      {
        if (this.Expect == null)
        {
          return false;
        }
        
        if (!this.Expect.Source.Equals(Target.Source))
        {
          return false;
        }

        foreach (var expect in Expect.Target)
        {
          if (!Target.Target.Contains(expect))
          {
            return false;
          }
        }

        return true;
      }
    }
  }
}
