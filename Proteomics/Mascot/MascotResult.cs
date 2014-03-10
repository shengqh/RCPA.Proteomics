using RCPA.Proteomics.Isotopic;
using RCPA.Proteomics.Summary;

namespace RCPA.Proteomics.Mascot
{
  public class MascotResult : IdentifiedResult
  {
    private IsotopicType peakIsotopicType = IsotopicType.Monoisotopic;
    public int PValueScore { get; set; }

    public double PValue { get; set; }

    public IsotopicType PeakIsotopicType
    {
      get { return this.peakIsotopicType; }
      set { this.peakIsotopicType = value; }
    }

    public double PeakTolerance { get; set; }
  }
}