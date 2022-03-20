using System.IO;

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