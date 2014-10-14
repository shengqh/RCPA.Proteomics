using RCPA.Proteomics.Summary;
using RCPA.Proteomics.Summary.Uniform;

namespace RCPA.Proteomics.PeptideProphet
{
  public class PeptideProphetDatasetBuilder : AbstractOneParserDatasetBuilder<PeptideProphetDatasetOptions>
  {
    public PeptideProphetDatasetBuilder(PeptideProphetDatasetOptions options) : base(options) { }
  }
}