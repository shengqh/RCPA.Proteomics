using System;
using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;
using RCPA.Proteomics.Mascot;
using RCPA.Utils;
using RCPA.Proteomics.Summary;
using RCPA.Proteomics.Spectrum;
using RCPA.Proteomics.IO;

namespace RCPA.Proteomics.Format
{
  public class MultipleMgf2Ms2Processor : AbstractParallelMainFileProcessor
  {
    private MultipleMgf2Ms2ProcessorOptions options;

    public MultipleMgf2Ms2Processor(MultipleMgf2Ms2ProcessorOptions options) :
      base(options.InputFiles)
    {
      this.options = options;
      this.Option.MaxDegreeOfParallelism = options.ThreadCount;
    }

    protected override IParallelTaskFileProcessor GetTaskProcessor(string targetDir, string mgfFile)
    {
      var targetFile = Path.Combine(targetDir, Path.GetFileNameWithoutExtension(mgfFile) + ".ms2");
      return new Mgf2Ms2Processor(targetFile, options.Parser);
    }
  }
}