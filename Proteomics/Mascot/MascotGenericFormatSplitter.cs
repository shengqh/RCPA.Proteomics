using System.Collections.Generic;
using System.IO;

namespace RCPA.Proteomics.Mascot
{
  public class MascotGenericFormatSplitter : AbstractThreadFileProcessor
  {
    public long fileSize;
    public MascotGenericFormatSplitter(double fileSizeInMB)
    {
      this.fileSize = (long)(fileSizeInMB * 1024 * 1024);
    }

    public override IEnumerable<string> Process(string fileName)
    {
      List<string> result = new List<string>();
      int surfix = 0;

      using (StreamReader sr = new StreamReader(fileName))
      {
        Progress.SetRange(0, sr.BaseStream.Length);

        Progress.SetPosition(0);

        var targetFilename = GetTargetFilename(fileName, surfix);
        result.Add(targetFilename);
        StreamWriter sw = new StreamWriter(targetFilename);

        string line;
        while ((line = sr.ReadLine()) != null)
        {
          sw.WriteLine(line);
          if (line.StartsWith("END"))
          {
            Progress.SetPosition(sr.BaseStream.Position);
            if (sw.BaseStream.Length >= fileSize)
            {
              sw.Close();
              surfix++;
              targetFilename = GetTargetFilename(fileName, surfix);
              result.Add(targetFilename);
              sw = new StreamWriter(targetFilename);
            }
          }
        }
        sw.Close();
      }

      return result;
    }

    private static string GetTargetFilename(string fileName, int surfix)
    {
      return FileUtils.ChangeExtension(fileName, "") + MyConvert.Format("_{0}{1}", surfix, new FileInfo(fileName).Extension);
    }
  }
}
