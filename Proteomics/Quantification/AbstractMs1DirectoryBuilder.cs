using System;
using System.Collections.Generic;
using System.IO;

namespace RCPA.Proteomics.Quantification
{
  public abstract class AbstractMs1DirectoryBuilder : AbstractThreadFileProcessor
  {
    private readonly string targetDir;

    private string program;

    private string version;

    public AbstractMs1DirectoryBuilder(string targetDir, string program, string version)
    {
      this.targetDir = targetDir;
      this.program = program;
      this.version = version;
    }

    public override IEnumerable<string> Process(string rawDirectory)
    {
      var result = new List<string>();

      var builder = GetBuilder(this.targetDir, program, version);
      builder.Progress = Progress;

      List<string> rawFiles = GetFiles(rawDirectory);

      var waitingFiles = new List<string>();
      foreach (string rawFile in rawFiles)
      {
        string resultFilename = builder.GetResultFilename(rawFile);

        if (!new FileInfo(resultFilename).Exists)
        {
          waitingFiles.Add(rawFile);
        }
      }

      waitingFiles.Sort();

      int count = 0;
      foreach (string rawFile in waitingFiles)
      {
        Console.WriteLine(rawFile);
        count++;
        Progress.SetMessage(1, MyConvert.Format("{0}/{1}, Processing {2}", count, waitingFiles.Count, rawFile));

        result.AddRange(builder.Process(rawFile));
      }

      return result;
    }

    protected abstract List<string> GetFiles(string rawDirectory);

    protected abstract AbstractMs1FileBuilder GetBuilder(string targetDir, string program, string version);
  }
}