using System;
using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;
using RCPA.Proteomics.Mascot;
using RCPA.Utils;
using RCPA.Proteomics.Summary;
using RCPA.Proteomics.Spectrum;
using RCPA.Proteomics.IO;

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