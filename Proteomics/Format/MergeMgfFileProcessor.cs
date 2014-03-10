using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using RCPA.Proteomics.Mascot;

namespace RCPA.Proteomics.Format
{
  public class MergeMgfFileProcessor : AbstractThreadFileProcessor
  {
    List<string> sourceFiles;

    public MergeMgfFileProcessor(IEnumerable<string> sourceFiles)
    {
      this.sourceFiles = new List<string>(sourceFiles);
    }

    public override IEnumerable<string> Process(string fileName)
    {
      using (StreamWriter sw = new StreamWriter(fileName))
      {
        for (int i = 0; i < sourceFiles.Count; i++)
        {
          Progress.SetMessage("Reading/writing {0}/{1}: {2} ... ", i + 1, sourceFiles.Count, sourceFiles[i]);

          using (StreamReader sr = new StreamReader(sourceFiles[i]))
          {
            string line;
            if (i != 0)
            {
              while ((line = sr.ReadLine()) != null)
              {
                if (line.StartsWith(MascotGenericFormatConstants.BEGIN_PEAK_LIST_TAG))
                {
                  sw.WriteLine(line);
                  break;
                }
              }
            }

            sw.Write(sr.ReadToEnd());
          }

          sw.WriteLine();
        }
      }

      return new string[] { fileName };
    }
  }
}
