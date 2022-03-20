using System.Collections.Generic;

namespace RCPA.Proteomics.IO
{
  public class MsLevelMultipleDistiller : AbstractThreadFileProcessor
  {
    private readonly MsLevelSingleDistiller distiller = new MsLevelSingleDistiller();

    public override IEnumerable<string> Process(string filename)
    {
      this.distiller.Progress = Progress;

      var result = new List<string>();
      List<string> rawFiles = FileUtils.GetFiles(filename, "*.raw");
      for (int i = 0; i < rawFiles.Count; i++)
      {
        if (Progress.IsCancellationPending())
        {
          throw new UserTerminatedException();
        }
        Progress.SetMessage(1, MyConvert.Format("{0}/{1} -- {2}", i + 1, rawFiles.Count, rawFiles[i]));
        result.AddRange(this.distiller.Process(rawFiles[i]));
      }

      return result;
    }
  }
}