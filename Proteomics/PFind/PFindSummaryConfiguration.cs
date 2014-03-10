using RCPA.Proteomics.Summary;

namespace RCPA.Proteomics.PFind
{
  public class PFindSummaryConfiguration : AbstractExpectValueSummaryConfiguration
  {
    public PFindSummaryConfiguration(string applicationTitle)
      : base(applicationTitle, "PFind", "ExpectValue")
    { }
  }
}