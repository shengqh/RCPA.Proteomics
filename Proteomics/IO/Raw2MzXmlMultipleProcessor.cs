using RCPA.Proteomics.Raw;
using System.Collections.Generic;

namespace RCPA.Proteomics.IO
{
  public class Raw2MzXmlMultipleProcessor : AbstractThreadFileProcessor
  {
    private readonly Raw2MzXmlProcessor processor;

    public Raw2MzXmlMultipleProcessor(RawFileImpl rawFile, bool fullMsOnly, bool doCentroid, string targetDirectoryName,
                                      string dataProcessingSoftware, string dataProcessingSoftwareVersion,
                                      Dictionary<string, string> dataProcessingOperations)
    {
      this.processor = new Raw2MzXmlProcessor(rawFile, fullMsOnly, doCentroid, targetDirectoryName,
                                              dataProcessingSoftware, dataProcessingSoftwareVersion,
                                              dataProcessingOperations);
    }

    public override IEnumerable<string> Process(string rawDirectory)
    {
      this.processor.Progress = Progress;

      var result = new List<string>();
      List<string> rawFiles = FileUtils.GetFiles(rawDirectory, "*.raw");
      for (int i = 0; i < rawFiles.Count; i++)
      {
        if (Progress.IsCancellationPending())
        {
          throw new UserTerminatedException();
        }
        Progress.SetMessage(1, MyConvert.Format("{0}/{1} -- {2}", i + 1, rawFiles.Count, rawFiles[i]));
        result.AddRange(this.processor.Process(rawFiles[i]));
      }

      return result;
    }
  }
}