using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Text.RegularExpressions;
using RCPA.Proteomics.Spectrum;

namespace RCPA.Proteomics.Format
{
  public class TripleTOFTextToMGFTaskProcessor : AbstractParallelTaskFileProcessor
  {
    private string software;

    private string targetDir;

    public TripleTOFTextToMGFTaskProcessor(string targetDir, string software)
    {
      this.software = software;
      this.targetDir = targetDir;
    }

    public override IEnumerable<string> Process(string fileName)
    {
      var result = new FileInfo(targetDir + "\\" + Path.ChangeExtension(new FileInfo(fileName).Name, ".mgf")).FullName;

      using (StreamWriter sw = new StreamWriter(result))
      {
        sw.WriteLine("COM=" + software);
        using (StreamReader sr = new StreamReader(fileName))
        {
          string line;
          bool bWrite = false;
          while ((line = sr.ReadLine()) != null)
          {
            if (line.StartsWith("BEGIN IONS"))
            {
              bWrite = true;
            }

            if (!bWrite)
            {
              sw.WriteLine("###" + line);
            }
            else
            {
              sw.WriteLine(line);
            }
          }
        }
      }

      return new string[] { result };
    }
  }
}
