namespace RCPA.Proteomics.IO
{
  //public class Raw2MgfDirectoryProcessor : AbstractThreadFileProcessor
  //{
  //  private readonly IProcessor<PeakList<Peak>> pklProcessor;
  //  private readonly double ppmPeakTolerance;
  //  private readonly double ppmPrecursorTolerance;
  //  private readonly double retentionTimeTolerance;
  //  private readonly DirectoryInfo targetDir;
  //  private readonly IPeakListWriter<Peak> writer;
  //  private IRawFile rawFile;

  //  public Raw2MgfDirectoryProcessor(IRawFile rawFile, IPeakListWriter<Peak> writer, double retentionTimeTolerance,
  //                                   double ppmPrecursorTolerance, double ppmPeakTolerance,
  //                                   IProcessor<PeakList<Peak>> pklProcessor, DirectoryInfo targetDir)
  //  {
  //    this.rawFile = rawFile;
  //    this.writer = writer;
  //    this.retentionTimeTolerance = retentionTimeTolerance;
  //    this.ppmPrecursorTolerance = ppmPrecursorTolerance;
  //    this.ppmPeakTolerance = ppmPeakTolerance;
  //    this.pklProcessor = pklProcessor;
  //    this.targetDir = targetDir;
  //  }

  //  public override IEnumerable<string> Process(string rawDirectory)
  //  {
  //    var rawDir = new DirectoryInfo(rawDirectory);
  //    FileInfo[] rawFiles = rawDir.GetFiles("*.raw");

  //    if (!this.targetDir.Exists)
  //    {
  //      this.targetDir.Create();
  //    }

  //    var result = new List<string>();

  //    for (int i = 0; i < rawFiles.Length; i++)
  //    {
  //      if (Progress.IsCancellationPending())
  //      {
  //        throw new UserTerminatedException();
  //      }

  //      string rootMsg = MyConvert.Format("{0} / {1} : {2}", i + 1, rawFiles.Length,
  //                                     rawFiles[i].FullName);

  //      Progress.SetMessage(1, rootMsg);

  //      var processor = new Raw2MgfProcessor(this.rawFile, this.writer, this.retentionTimeTolerance, this.ppmPrecursorTolerance,
  //                                           this.ppmPeakTolerance, this.pklProcessor, this.targetDir);
  //      processor.Progress = Progress;

  //      result.AddRange(processor.Process(rawFiles[i].FullName));
  //    }

  //    return result;
  //  }
  //}
}