using RCPA.Proteomics.PeptideProphet;

namespace RCPA.Proteomics.Summary.Uniform
{
  public class PeptideProphetDatasetBuilder : AbstractOneParserDatasetBuilder<PeptideProphetDatasetOptions>
  {
    public PeptideProphetDatasetBuilder(PeptideProphetDatasetOptions options) : base(options) { }

    protected override ISpectrumParser GetSpectrumParser()
    {
      var result = new PeptideProphetXmlReader(options.GetTitleParser());
      result.Progress = Progress;
      return result;
    }
  }
}