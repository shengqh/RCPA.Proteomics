using RCPA.Proteomics.Summary;
using RCPA.Utils;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

namespace RCPA.Proteomics.Mascot
{
  /// <summary>
  /// 20110127
  /// 将mgf中title格式改成sequest格式。TPP流程中需要用到。
  /// </summary>
  public class UpdateMascotMGFTitleFormatProcessor : AbstractThreadFileProcessor
  {
    private ITitleParser parser;
    private List<string> sourceFiles;

    public UpdateMascotMGFTitleFormatProcessor(ITitleParser parser, IEnumerable<string> sourceFiles)
    {
      this.parser = parser;
      this.sourceFiles = sourceFiles.ToList();
    }

    public override IEnumerable<string> Process(string targetDir)
    {
      var result = new List<string>();
      targetDir = new DirectoryInfo(targetDir).FullName;
      foreach (var sourceFile in sourceFiles)
      {
        var sourceDir = new FileInfo(sourceFile).DirectoryName;
        string targetFile;
        bool isSame = sourceDir.ToUpper() == targetDir.ToUpper();

        if (isSame)
        {
          targetFile = sourceFile + ".tmp";
        }
        else
        {
          targetFile = targetDir + "\\" + new FileInfo(sourceFile).Name;
        }

        var chargereg = new Regex(@"(\d+)");
        using (StreamReader sr = new StreamReader(sourceFile))
        {
          using (StreamWriter sw = new StreamWriter(targetFile))
          {
            string line;
            int charge = 0;
            while ((line = sr.ReadLine()) != null)
            {
              if (line.StartsWith("BEGIN IONS"))
              {
                charge = 0;
              }
              else if (line.StartsWith("CHARGE="))
              {
                charge = Convert.ToInt32(chargereg.Match(line).Groups[1].Value);
              }
              else if (line.StartsWith("TITLE="))
              {
                string title = line.Substring(6);
                SequestFilename sf = this.parser.GetValue(title);
                sf.Extension = "dta";
                sf.Charge = charge;
                line = "TITLE=" + sf.LongFileName;
              }

              sw.WriteLine(line);
            }
          }
        }

        if (isSame)
        {
          File.Delete(sourceFile);
          File.Move(targetFile, sourceFile);
          result.Add(sourceFile);
        }
        else
        {
          result.Add(targetFile);
        }
      }

      return result;
    }
  }
}
