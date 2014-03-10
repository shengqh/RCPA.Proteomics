using RCPA.Proteomics.Summary;
using RCPA.Proteomics.Summary.Uniform;

namespace RCPA.Proteomics.Mascot
{
  public class MascotDatSummaryConfiguration : AbstractExpectValueSummaryConfiguration
  {
    public MascotDatSummaryConfiguration(string applicationTitle)
      : base(applicationTitle, "MASCOT", "Score")
    { }
  }
}