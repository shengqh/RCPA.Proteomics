
namespace RCPA.Proteomics.Summary.Uniform
{
  public class MzIdentDatasetBuilder : AbstractOneParserDatasetBuilder<MzIdentDatasetOptions>
  {
    public MzIdentDatasetBuilder(MzIdentDatasetOptions options) : base(options) { }

    private MzIdentDatasetOptions mzIdentOptions { get { return Options as MzIdentDatasetOptions; } }

    protected override ISpectrumParser GetSpectrumParser()
    {
      var datParser = mzIdentOptions.GetParser();
      return datParser;
    }
  }
}