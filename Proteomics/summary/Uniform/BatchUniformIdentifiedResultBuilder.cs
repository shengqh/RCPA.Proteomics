using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace RCPA.Proteomics.Summary.Uniform
{
  public class BatchUniformIdentifiedResultBuilder : AbstractThreadFileProcessor
  {
    public override IEnumerable<string> Process(string dir)
    {
      var paramFiles = Directory.GetFiles(dir, "*.param").OrderBy(m => m).ToList();

      foreach (var paramFile in paramFiles)
      {
        new UniformSummaryBuilder(new UniformSummaryBuilderOptions()
        {
          InputFile = paramFile
        }) { Progress = this.Progress }.Process();
      }

      return null;
    }
  }
}
