using RCPA.Proteomics.Summary.Uniform;

namespace RCPA.Proteomics.Mascot
{
  public class MascotDatasetBuilder : AbstractOneParserDatasetBuilder<MascotDatasetOptions>
  {
    public MascotDatasetBuilder(MascotDatasetOptions options) : base(options) { }
  }
}