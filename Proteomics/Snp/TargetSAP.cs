using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RCPA.Proteomics.Snp
{
  public class TargetSAP
  {
    public TargetSAP()
    {
      IsNterminalLoss = false;
      Source = string.Empty;
      Target = string.Empty;
      DeltaMass = 0.0;
    }
    public bool IsNterminalLoss { get; set; }
    public string Source { get; set; }
    public string Target { get; set; }
    public double DeltaMass { get; set; }
    public bool IsTerminalLoss
    {
      get { return this.Source.Length > 1; }
    }
  }
}
