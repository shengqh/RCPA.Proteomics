using System;
using System.IO;
using System.Threading;
using RCPA.Utils;
using RCPA.Proteomics.Spectrum;
using RCPA.Proteomics.Raw;

namespace RCPA.Proteomics.IO
{
  public class MultipleRaw2OneMgfThreadProcessor
  {
    private readonly IProcessor<PeakList<Peak>> pklProcessor;
    private readonly double ppmPeakTolerance;
    private readonly double ppmPrecursorTolerance;
    private readonly DirectoryInfo rawDir;
    private readonly double retentionTimeTolerance;
    private readonly FileInfo saveToFile;
    private IRawFile2 rawFile;
    private readonly IPeakListWriter<Peak> writer;
    private IProgressCallback eachProcessorProgress;

    public MultipleRaw2OneMgfThreadProcessor(DirectoryInfo rawDir, FileInfo saveToFile,
      IRawFile2 rawFile, IPeakListWriter<Peak> writer,
                                             double retentionTimeTolerance, double ppmPrecursorTolerance,
                                             double ppmPeakTolerance, IProcessor<PeakList<Peak>> pklProcessor,
                                             IProgressCallback eachProcessorProgress)
    {
      this.rawDir = rawDir;
      this.saveToFile = saveToFile;
      this.rawFile = rawFile;
      this.writer = writer;
      this.retentionTimeTolerance = retentionTimeTolerance;
      this.ppmPrecursorTolerance = ppmPrecursorTolerance;
      this.ppmPeakTolerance = ppmPeakTolerance;
      this.pklProcessor = pklProcessor;
      this.eachProcessorProgress = eachProcessorProgress;
    }

    public void DoSomeWork(object status)
    {
      var callback = status as IProgressCallback;
      try
      {
        FileInfo[] rawFiles = this.rawDir.GetFiles("*.raw");
        callback.SetRange(0, rawFiles.Length);

        var processor = new MultipleRaw2OneMgfProcessor(this.rawFile, this.writer, this.retentionTimeTolerance,
                                                        this.ppmPrecursorTolerance, this.ppmPeakTolerance,
                                                        this.pklProcessor, this.saveToFile);
        //        processor.Progress = eachProcessorProgress;

        for (int i = 0; i < rawFiles.Length; ++i)
        {
          callback.SetMessage(MyConvert.Format("Performing raw file {0}/{1}: {2}", i + 1, rawFiles.Length, rawFiles[i].Name));
          callback.SetPosition(i);
          if (callback.IsCancellationPending())
          {
            callback.SetMessage(MyConvert.Format("User terminated when performing raw file: {0}", rawFiles[i].Name));
            return;
          }

          processor.Process(rawFiles[i].FullName);
        }
        callback.SetMessage("Finished!");
        callback.SetPosition(rawFiles.Length);
      }
      catch (ThreadAbortException)
      {
        // We want to exit gracefully here (if we're lucky)
      }
      catch (ThreadInterruptedException)
      {
        // And here, if we can
      }
      catch (UserTerminatedException ex)
      {
        if (callback != null)
        {
          callback.SetMessage("User terminated : " + ex.Message);
        }
      }
      catch (Exception ex)
      {
        if (callback != null)
        {
          callback.SetMessage("Exception : " + ex.Message + " from\n" + ex.StackTrace);
        }
        else
        {
          throw ex;
        }
      }
    }
  }
}