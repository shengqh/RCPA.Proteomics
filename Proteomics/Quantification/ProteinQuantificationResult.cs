using System.Collections.Generic;

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
