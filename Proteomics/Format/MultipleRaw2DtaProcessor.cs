using System.Collections.Generic;
using System.IO;
using System.Linq;
using RCPA.Utils;
using RCPA.Proteomics.Spectrum;
using System.Threading.Tasks;
using System;
using System.Collections.Concurrent;
using System.Text;

namespace RCPA.Proteomics.Format
{
  public class MultipleRaw2DtaProcessor : AbstractParallelMainProcessor
  {
    private IProcessor<PeakList<Peak>> pklProcessor;

    private bool splitMsLevel;

    public MultipleRaw2DtaProcessor(IEnumerable<string> rawFiles, IProcessor<PeakList<Peak>> pklProcessor, bool splitMsLevel)
      : base(rawFiles)
    {
      this.pklProcessor = pklProcessor;
      this.splitMsLevel = splitMsLevel;
    }

    protected override IParallelTaskFileProcessor GetTaskProcessor(string targetDir, string fileName)
    {
      return new Raw2DtaProcessor(this.pklProcessor, targetDir, this.splitMsLevel);
    }
  }
}