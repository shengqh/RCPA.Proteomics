using RCPA.Proteomics.PFind;

namespace RCPA.Proteomics.Summary.Uniform
{
  public class PFindDatasetBuilder : AbstractOneParserDatasetBuilder<PFindDatasetOptions>
  {
    public PFindDatasetBuilder(PFindDatasetOptions options) : base(options) { }

    protected override ISpectrumParser GetSpectrumParser()
    {
      var result = new PFindSpectrumParser(options.GetTitleParser());
      result.Progress = Progress;
      return result;
    }
  }
}