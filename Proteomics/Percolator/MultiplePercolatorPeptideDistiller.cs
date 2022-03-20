using System.Collections.Generic;

namespace RCPA.Proteomics.Percolator
{
  public class MultiplePercolatorPeptideDistiller : AbstractParallelMainProcessor
  {
    private MultiplePercolatorPeptideDistillerOptions _options;

    public MultiplePercolatorPeptideDistiller(MultiplePercolatorPeptideDistillerOptions options)
    {
      this._options = options;
      this.Option.MaxDegreeOfParallelism = options.ThreadCount;
    }

    protected override List<IParallelTaskProcessor> GetTaskProcessors()
    {
      var result = new List<IParallelTaskProcessor>();
      foreach (var file in _options.PercolatorOutputFiles)
      {
        var taskoptions = new PercolatorPeptideDistillerOptions()
        {
          PercolatorOutputFile = file
        };

        result.Add(new PercolatorPeptideDistiller(taskoptions));

      }
      return result;
    }
  }
}