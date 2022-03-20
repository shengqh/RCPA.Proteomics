using RCPA.Proteomics.Raw;
using RCPA.Proteomics.Spectrum;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace RCPA.Proteomics.IO
{
  public class MultipleRaw2MgfProcessor2 : AbstractThreadFileProcessor
  {
    private readonly IProcessor<PeakList<Peak>> pklProcessor;
    private readonly double ppmPeakTolerance;
    private readonly double ppmPrecursorTolerance;
    private readonly double retentionTimeTolerance;
    private readonly IPeakListWriter<Peak> writer;
    private List<string> rawFiles;
    private bool groupByMode;

    public MultipleRaw2MgfProcessor2(IPeakListWriter<Peak> writer, double retentionTimeTolerance,
      double ppmPrecursorTolerance, double ppmPeakTolerance,
      IProcessor<PeakList<Peak>> pklProcessor, IEnumerable<string> rawFiles, bool groupByMode)
    {
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
        using (var rawFile = RawFileFactory.GetRawFileReader(rawFiles[i]))
        {
          var processor = new Raw2MgfProcessor(rawFile, this.writer, this.retentionTimeTolerance, this.ppmPrecursorTolerance,
                                               this.ppmPeakTolerance, this.pklProcessor, targetDir, groupByMode);
          processor.Progress = Progress;

          result.AddRange(processor.Process(rawFiles[i]));
        }
      }

      return result;
    }
  }
}