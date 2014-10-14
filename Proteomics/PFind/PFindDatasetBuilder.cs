using RCPA.Proteomics.PFind;
using RCPA.Proteomics.Summary;
using RCPA.Proteomics.Summary.Uniform;

namespace RCPA.Proteomics.PFind
{
  public class PFindDatasetBuilder : AbstractOneParserDatasetBuilder<PFindDatasetOptions>
  {
    public PFindDatasetBuilder(PFindDatasetOptions options) : base(options) { }
  }
}