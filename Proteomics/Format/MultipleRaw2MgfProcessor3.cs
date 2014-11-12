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
  public class MultipleRaw2MgfProcessor3 : AbstractParallelMainFileProcessor
  {
    private Raw2MgfOption option;

    public MultipleRaw2MgfProcessor3(Raw2MgfOption option)
      : base(option.RawFiles)
    {
      this.option = option;
    }

    protected override void PrepareBeforeProcessing(string aPath)
    {
      var targetDir = new DirectoryInfo(aPath);
      if (!targetDir.Exists)
      {
        targetDir.Create();
      }
    }

    protected override IParallelTaskFileProcessor GetTaskProcessor(string targetDir, string fileName)
    {
      var taskProcessor = option.GetProcessor(fileName, Progress);

      var newWriter = option.GetWriter();

      return new Raw2MgfProcessor2()
      {
        Writer = newWriter,
        PeakListProcessor = taskProcessor,
        TargetDirectory = targetDir,
        GroupByScanMode = option.GroupByMode,
        GroupByMsLevel = option.GroupByMsLevel
      };
    }
  }
}