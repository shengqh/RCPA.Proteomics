using System.Collections.Generic;

namespace RCPA.Proteomics.Format
{
  public class TripleTOFTextToMGFMainProcessor : AbstractParallelMainFileProcessor
  {
    private string software;

    private double precursorTolerance;

    public TripleTOFTextToMGFMainProcessor(IEnumerable<string> ASourceFiles, string software, double precursorTolerance)
      : base(ASourceFiles)
    {
      this.software = software;
      this.precursorTolerance = precursorTolerance;
    }

    protected override IParallelTaskFileProcessor GetTaskProcessor(string targetDir, string fileName)
    {
      if (this.precursorTolerance != 0.0)
      {
        return new TripleTOFTextToMGFTaskResortProcessor(targetDir, software, precursorTolerance);
      }
      else
      {
        return new TripleTOFTextToMGFTaskProcessor(targetDir, software);
      }
    }
  }
}
