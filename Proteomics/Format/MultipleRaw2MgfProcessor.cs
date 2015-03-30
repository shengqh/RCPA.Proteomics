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
  public class MultipleRaw2MgfProcessor : AbstractParallelMainFileProcessor
  {
    private MultipleRaw2MgfOptions options;

    public MultipleRaw2MgfProcessor(MultipleRaw2MgfOptions options)
      : base(options.RawFiles)
    {
      this.options = options;
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
      var taskProcessor = options.GetProcessor(fileName, Progress);

      if (options.OutputMzXmlFormat)
      {
        return new Raw2MzXMLProcessor()
        {
          PeakListProcessor = taskProcessor,
          TargetDirectory = targetDir,
          DataProcessingSoftware = options.ConverterName,
          DataProcessingSoftwareVersion = options.ConverterVersion,
          ExtractRawMS3 = options.ExtractRawMS3
        };
      }
      else
      {
        var newWriter = options.GetWriter();

        return new Raw2MgfProcessor()
        {
          Writer = newWriter,
          PeakListProcessor = taskProcessor,
          TargetDirectory = targetDir,
          GroupByScanMode = options.GroupByMode,
          GroupByMsLevel = options.GroupByMsLevel,
          ExtractRawMS3 = options.ExtractRawMS3
        };
      }
    }
  }
}