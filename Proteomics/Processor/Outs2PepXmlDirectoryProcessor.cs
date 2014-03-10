using System.Collections.Generic;
using System.IO;

namespace RCPA.Proteomics.Processor
{
  public class Outs2PepXmlDirectoryProcessor : AbstractThreadFileProcessor
  {
    public static string version = "1.0.0";

    private readonly string targetDirectory;

    public Outs2PepXmlDirectoryProcessor(string targetDirectory)
    {
      this.targetDirectory = targetDirectory;
    }

    public override IEnumerable<string> Process(string outsRootDirectory)
    {
      var sequestRoot = new DirectoryInfo(outsRootDirectory);

      var pepXmlRoot = new DirectoryInfo(this.targetDirectory);
      if (!pepXmlRoot.Exists)
      {
        pepXmlRoot.Create();
      }

      var result = new List<string>();

      DirectoryInfo[] subSequestDirs = sequestRoot.GetDirectories();
      for (int i = 0; i < subSequestDirs.Length; i++)
      {
        DirectoryInfo di = subSequestDirs[i];

        string rootMsg = MyConvert.Format("{0} / {1} : {2}", i + 1, subSequestDirs.Length,
                                       di.FullName);

        DirectoryInfo[] experimentDirs = di.GetDirectories();

        var targetRoot = new DirectoryInfo(pepXmlRoot.FullName + "\\" + di.Name);
        if (!targetRoot.Exists)
        {
          targetRoot.Create();
        }

        foreach (DirectoryInfo experimentDir in experimentDirs)
        {
          FileInfo[] fis = experimentDir.GetFiles("*.outs");
          if (fis.Length == 0)
          {
            continue;
          }

          if (Progress.IsCancellationPending())
          {
            throw new UserTerminatedException();
          }

          Progress.SetMessage(rootMsg + " - " + fis[0].Name);

          var processor = new Outs2PepXmlProcessor(targetRoot.FullName) { Progress = this.Progress };

          result.AddRange(processor.Process(fis[0].FullName));
        }
      }

      return result;
    }
  }
}