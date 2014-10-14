using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RCPA.Proteomics.ProteomeDiscoverer
{
  public class ModificationEntry
  {
    public char SignChar { get; set; }
    public string SignStr { get; set; }
    public int PositionType { get; set; }
    public double DeltaMass { get; set; }
  }
}
