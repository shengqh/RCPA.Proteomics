using RCPA.Proteomics.Mascot;

namespace RCPA.Proteomics.Summary.Uniform
{
  public class MascotDatasetBuilder : AbstractOneParserDatasetBuilder<MascotDatasetOptions>
  {
    public MascotDatasetBuilder(MascotDatasetOptions options) : base(options) { }

    protected override ISpectrumParser GetSpectrumParser()
    {
      return new MascotDatSpectrumParser(options.GetTitleParser())
      {
        Progress = this.Progress
      };
    }
  }
}