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

      var builder = new UniformIdentifiedResultBuilder();
      builder.Progress = Progress;

      foreach (var paramFile in paramFiles)
      {
        builder.Process(paramFile);
      }

      return null;
    }
  }
}
