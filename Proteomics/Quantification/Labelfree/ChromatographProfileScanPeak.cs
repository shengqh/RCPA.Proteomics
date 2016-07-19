using RCPA.Proteomics.Spectrum;

namespace RCPA.Proteomics.Quantification.Labelfree
{
  public class ChromatographProfileScanPeak : Peak
  {
    public ChromatographProfileScanPeak() { }

    public int Isotopic { get; set; }

    public double PPMDistance { get; set; }
  }
}
