using RCPA.Proteomics.Mascot;
using RCPA.Proteomics.Summary;
using RCPA.Proteomics.Summary.Uniform;

namespace RCPA.Proteomics.Percolator
{
  public class PercolatorDatasetBuilder : AbstractOneParserDatasetBuilder<PercolatorDatasetOptions>
  {
    public PercolatorDatasetBuilder(PercolatorDatasetOptions options) : base(options) { }

    protected override IIdentifiedPeptideTextFormat GetPeptideFormat()
    {
      return new MascotPeptideTextFormat(MascotHeader.MASCOT_PEPTIDE_HEADER + "\tSVMScore");
    }

    protected override ISpectrumParser GetParser(string dataFile)
    {
      var baseParser = base.GetParser(dataFile);

      return new PercolatorFileParser(baseParser);
    }

    protected override string GetPeptideFile(string dataFile)
    {
      return dataFile + ".percolator.peptides";
    }
  }
}