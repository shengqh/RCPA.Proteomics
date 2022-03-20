using RCPA.Proteomics.Summary;

namespace RCPA.Proteomics.Mascot
{
  public class MascotDatSummaryConfiguration : AbstractExpectValueSummaryConfiguration
  {
    public MascotDatSummaryConfiguration(string applicationTitle)
      : base(applicationTitle, "MASCOT", "Score")
    { }
  }
}