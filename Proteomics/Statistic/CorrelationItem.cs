using System.Collections.Generic;

namespace RCPA.Proteomics.Statistic
{
  public class CorrelationItem
  {
    public string Name { get; set; }

    public double[] Values { get; set; }

    public double Correlation { get; set; }
  }

  public class ProteinCorrelationItem
  {
    public int Index { get; set; }

    public CorrelationItem Protein { get; set; }

    public List<CorrelationItem> Peptides { get; private set; }

    public ProteinCorrelationItem()
    {
      this.Peptides = new List<CorrelationItem>();
    }
  }

  public class ResultCorrelationItem : List<ProteinCorrelationItem>
  {
    public string[] ClassificationTitles { get; set; }
  }
}
