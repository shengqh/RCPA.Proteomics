using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using RCPA.Format;
using System.IO;

namespace RCPA.Proteomics.Quantification.IsobaricLabelling
{
  /// <summary>
  /// Index the xml file for random access.
  /// </summary>
  public class IsobaricResultXmlIndexBuilder : AbstractThreadFileProcessor
  {
    private bool force;
    public IsobaricResultXmlIndexBuilder(bool force = false)
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

            if (line.Trim().Equals("<IsobaricScan>"))
            {
              string experimental = string.Empty;
              string scan = string.Empty;
              while (null != (line = sr.ReadLine()))
              {
                line = line.Trim();
                if (line.StartsWith("<Experimental>"))
                {
                  experimental = line.Substring(14, line.Length - 29);
                }
                else if (line.StartsWith("<Scan>"))
                {
                  scan = line.Substring(6, line.Length - 13);
                  break;
                }
              }

              while (null != (line = sr.ReadLine()))
              {
                if (line.Trim().StartsWith("</IsobaricScan>"))
                {
                  long endPos = sr.Position;
                  items.Add(new FileIndexItem(startPos, endPos - startPos, string.Format("{0},{1}", experimental, scan)));
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
