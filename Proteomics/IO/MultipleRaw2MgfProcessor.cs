using System.Collections.Generic;
using System.IO;
using System.Linq;
using RCPA.Proteomics.Spectrum;
using RCPA.Proteomics.Raw;

namespace RCPA.Proteomics.IO
{
  public class MultipleRaw2MgfProcessor : AbstractThreadFileProcessor
  {
    private readonly IProcessor<PeakList<Peak>> pklProcessor;
    private readonly double ppmPeakTolerance;
    private readonly double ppmPrecursorTolerance;
    private readonly double retentionTimeTolerance;
    private readonly IPeakListWriter<Peak> writer;
    private List<string> rawFiles;
    private IRawFile2 rawFile;
    private bool groupByMode;

    public MultipleRaw2MgfProcessor(IRawFile2 rawFile, IPeakListWriter<Peak> writer, double retentionTimeTolerance,
      double ppmPrecursorTolerance, double ppmPeakTolerance,
      IProcessor<PeakList<Peak>> pklProcessor, IEnumerable<string> rawFiles, bool groupByMode)
    {
      this.rawFile = rawFile;
      this.writer = writer;
      this.retentionTimeTolerance = retentionTimeTolerance;
      this.ppmPrecursorTolerance = ppmPrecursorTolerance;
      this.ppmPeakTolerance = ppmPeakTolerance;
      this.pklProcessor = pklProcessor;
      this.rawFiles = rawFiles.ToList();
      this.groupByMode = groupByMode;
    }

    public override IEnumerable<string> Process(string targetDirectory)
    {
      var targetDir = new DirectoryInfo(targetDirectory);
      if (!targetDir.Exists)
      {
        targetDir.Create();
      }

      var result = new List<string>();

      for (int i = 0; i < rawFiles.Count; i++)
      {
        if (Progress.IsCancellationPending())
        {
          throw new UserTerminatedException();
        }

        string rootMsg = MyConvert.Format("{0} / {1} : {2}", i + 1, rawFiles.Count,
                                       rawFiles[i]);

        Progress.SetMessage(1, rootMsg);

        var processor = new Raw2MgfProcessor(this.rawFile, this.writer, this.retentionTimeTolerance, this.ppmPrecursorTolerance,
                                             this.ppmPeakTolerance, this.pklProcessor, targetDir, groupByMode);
        processor.Progress = Progress;

        result.AddRange(processor.Process(rawFiles[i]));
      }

      return result;
    }
  }
}