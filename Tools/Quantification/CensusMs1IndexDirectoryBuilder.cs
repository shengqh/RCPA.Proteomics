using System.Collections.Generic;
using System.IO;

namespace RCPA.Proteomics.Quantification
{
  public class CensusMs1IndexDirectoryBuilder : AbstractThreadFileProcessor
  {
    public override IEnumerable<string> Process(string filename)
    {
      var result = new List<string>();

      var builder = new CensusMs1IndexFileBuilder() { Progress = this.Progress };

      List<string> ms1Files = FileUtils.GetFiles(filename, "*.ms1");

      var waitingFiles = new List<string>();
      foreach (string ms1File in ms1Files)
      {
        string resultFilename = builder.GetResultFilename(ms1File);

        if (!new FileInfo(resultFilename).Exists)
        {
          waitingFiles.Add(ms1File);
        }
      }

      waitingFiles.Sort();

      int count = 0;
      foreach (string ms1File in waitingFiles)
      {
        count++;
        Progress.SetMessage(MyConvert.Format("{0}/{1}, Processing {2}", count, waitingFiles.Count, ms1File));

        result.AddRange(builder.Process(ms1File));

        Progress.SetPosition(1, count);
      }

      return result;
    }
  }
}