namespace RCPA.Proteomics.Quantification.ITraq
{
  public class ITraqPeptideRatioItem
  {
    public double Sample { get; set; }

    public double Reference { get; set; }

    public double Ratio { get; set; }

    public double IonInjectTime { get; set; }

    public bool IsOutlier { get; set; }

    public ITraqPeptideRatioItem()
    {
      this.IsOutlier = false;
    }
  }
}
