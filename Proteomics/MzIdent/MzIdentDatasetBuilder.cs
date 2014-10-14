
using RCPA.Proteomics.Summary;
using RCPA.Proteomics.Summary.Uniform;
namespace RCPA.Proteomics.MzIdent
{
  public class MzIdentDatasetBuilder : AbstractOneParserDatasetBuilder<AbstractMzIdentDatasetOptions>
  {
    public MzIdentDatasetBuilder(AbstractMzIdentDatasetOptions options) : base(options) { }
  }
}