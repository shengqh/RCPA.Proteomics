using System.Collections.Generic;
using System.IO;
using System.Linq;
using RCPA.Proteomics.Spectrum;
using RCPA.Proteomics.Raw;
using RCPA.Proteomics.Mascot;
using System.Threading.Tasks;
using System.Collections.Concurrent;
using System;
using System.Text;
using System.Threading;
using RCPA.Proteomics.Statistic;
using RCPA.Proteomics.Processor;

namespace RCPA.Proteomics.Format
{
  public class MultipleRaw2MSnProcessor : AbstractParallelMainFileProcessor
  {
    private MultipleRaw2MSnProcessorOptions options;

    public MultipleRaw2MSnProcessor(MultipleRaw2MSnProcessorOptions options)
      : base(options.RawFiles)
    {
      this.options = options;
      this.ParallelMode = options.ParallelMode;
    }

    protected override void PrepareBeforeProcessing(string directory)
    {
      if (!Directory.Exists(directory))
      {
        Directory.CreateDirectory(directory);
      }
    }

    protected override IParallelTaskFileProcessor GetTaskProcessor(string targetDir, string fileName)
    {
      return new Raw2MSnProcessor(options);
    }
  }
}