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

      if (!options.Overwrite && (options.OutputMzXmlFormat || (!options.GroupByMode && !options.GroupByMsLevel)))
      {
        AbstractRawConverter proc;
        if (options.OutputMzXmlFormat)
        {
          proc = new Raw2MzXMLProcessor(options);
        }
        else
        {
          proc = new Raw2MgfProcessor(options);
        }

        sourceFiles = (from f in sourceFiles
                       let ret = proc.GetResultFile(RawFileFactory.GetRawFileReaderWithoutOpen(f), f)
                       where !File.Exists(ret)
                       select f).ToList();
      }
    }

    protected override IParallelTaskFileProcessor GetTaskProcessor(string targetDir, string fileName)
    {
      var taskProcessor = options.GetProcessor(fileName, Progress);

      if (options.OutputMzXmlFormat)
      {
        return new Raw2MzXMLProcessor(options)
        {
          PeakListProcessor = taskProcessor,
          DataProcessingSoftware = options.ConverterName,
          DataProcessingSoftwareVersion = options.ConverterVersion,
        };
      }
      else
      {
        var newWriter = options.GetMGFWriter();

        return new Raw2MgfProcessor(options)
        {
          Writer = newWriter,
          PeakListProcessor = taskProcessor,
        };
      }
    }
  }
}