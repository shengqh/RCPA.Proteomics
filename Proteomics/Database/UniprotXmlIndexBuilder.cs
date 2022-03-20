using RCPA.Format;
using System.Collections.Generic;
using System.IO;

namespace RCPA.Proteomics.Database
{
  public class UniprotXmlIndexBuilder : AbstractThreadFileProcessor
  {
    private bool force;
    public UniprotXmlIndexBuilder(bool force = false)
    {
      this.force = force;
    }

    public static string GetTargetFile(string sourceFile)
    {
      return sourceFile + ".index";
    }

    public override IEnumerable<string> Process(string fileName)
    {
      string indexFile = GetTargetFile(fileName);
      if (!File.Exists(indexFile) || force)
      {
        Progress.SetMessage("Building index of " + fileName + " ...");
        List<FileIndexItem> items = new List<FileIndexItem>();
        using (FileStream sr = new FileStream(fileName, FileMode.Open))
        {
          string line;

          Progress.SetRange(1, sr.Length);
          while (true)
          {
            long startPos = sr.Position;
            line = sr.ReadLine();

            if (null == line)
            {
              break;
            }

            if (line.Trim().StartsWith("<entry"))
            {
              string name = string.Empty;
              while (null != (line = sr.ReadLine()))
              {
                line = line.Trim();
                if (line.StartsWith("<name>"))
                {
                  var pos = line.IndexOf('<', 6);
                  name = line.Substring(6, pos - 6);
                  break;
                }
              }

              while (null != (line = sr.ReadLine()))
              {
                if (line.Trim().StartsWith("</entry>"))
                {
                  long endPos = sr.Position;
                  items.Add(new FileIndexItem(startPos, endPos - startPos, name));
                  break;
                }
              }

              Progress.SetPosition(sr.Position);
            }
          }
          Progress.SetMessage("Building index of " + fileName + " finished.");
        }

        new FileIndexFormat().WriteToFile(indexFile, items);
      }

      return new string[] { indexFile };
    }
  }
}
