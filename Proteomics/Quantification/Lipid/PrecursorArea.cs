namespace RCPA.Proteomics.Quantification.Lipid
{
  public class PrecursorArea
  {
    public PrecursorArea()
    {
      this.Enabled = true;
    }

    public double PrecursorMz { get; set; }

    public int ScanCount { get; set; }

    public double Area { get; set; }

    public bool Enabled { get; set; }
  }
}
