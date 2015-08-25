using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RCPA.Proteomics.Snp
{
  public class SapMatchedCount
  {
    public MS2Item Item1 { get; set; }
    public MS2Item Item2 { get; set; }
    public List<double> PrecursorMatched { get; set; }
    public List<int> MS3Matched { get; set; }
  }
}
