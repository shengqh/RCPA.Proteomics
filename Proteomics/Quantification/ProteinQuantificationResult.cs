using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RCPA.Proteomics.Quantification
{
  public class ProteinQuantificationResult
  {
    public ProteinQuantificationResult()
    {
      Items = new Dictionary<string, QuantificationItem>();
    }

    public Dictionary<string, QuantificationItem> Items { get; set; }
  }
}
