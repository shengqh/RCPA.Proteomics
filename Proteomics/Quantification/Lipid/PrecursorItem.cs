namespace RCPA.Proteomics.Quantification.Lipid
{
  public class PrecursorItem
  {
    public PrecursorItem()
    {
      this.Enabled = true;
      this.IsIsotopic = false;
    }

    public int Scan { get; set; }

    public double PrecursorMZ { get; set; }

    public double PrecursorIntensity { get; set; }

    public double ProductIonRelativeIntensity { get; set; }

    public bool Enabled { get; set; }

    public bool IsIsotopic { get; set; }
  }
}
