using RCPA.Proteomics.Omssa;
using RCPA.Proteomics.PeptideProphet;

namespace RCPA.Proteomics.Summary.Uniform
{
  public class OmssaDatasetBuilder : AbstractOneParserDatasetBuilder<OmssaDatasetOptions>
  {
    public OmssaDatasetBuilder(OmssaDatasetOptions options) : base(options) { }

    protected override ISpectrumParser GetSpectrumParser()
    {
      var result = new OmssaOmxParser(options.GetTitleParser());
      result.Progress = Progress;
      return result;
    }
  }
}