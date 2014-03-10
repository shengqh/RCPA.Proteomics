using System.Collections.Generic;
using RCPA.Utils;

namespace RCPA.Proteomics.Mascot
{
  public class MascotGenericFormatShiftPrecursorMultipleFileProcessor : AbstractThreadFileProcessor
  {
    private readonly MascotGenericFormatShiftPrecursorProcessor processor;

    public MascotGenericFormatShiftPrecursorMultipleFileProcessor()
    {
      this.processor = new MascotGenericFormatShiftPrecursorProcessor();
    }

    public override IEnumerable<string> Process(string mgfDirectory)
    {
      this.processor.Progress = Progress;

      var result = new List<string>();
      List<string> mgfFiles = FileUtils.GetFiles(mgfDirectory, "*.mgf");
      for (int i = 0; i < mgfFiles.Count; i++)
      {
        if (Progress.IsCancellationPending())
        {
          throw new UserTerminatedException();
        }
        Progress.SetMessage(1, MyConvert.Format("{0}/{1} -- {2}", i + 1, mgfFiles.Count, mgfFiles[i]));
        result.AddRange(this.processor.Process(mgfFiles[i]));
      }

      return result;
    }
  }
}