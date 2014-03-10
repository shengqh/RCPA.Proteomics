using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using RCPA.Proteomics.Summary;
using System.IO;
using System.Text.RegularExpressions;

namespace RCPA.Proteomics.Mascot
{
  /// <summary>
  /// 20110126
  /// 将dat中title格式改成sequest格式。TPP流程中需要用到。
  /// </summary>
  public class UpdateMascotDatTitleFormatProcessor : AbstractThreadFileProcessor
  {
    private ITitleParser parser;
    private List<string> sourceFiles;

    public UpdateMascotDatTitleFormatProcessor(ITitleParser parser, IEnumerable<string> sourceFiles)
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
        if(isSame)
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
            while ((line = sr.ReadLine()) != null)
            {
              if (line.StartsWith("title="))
              {
                string title = Uri.UnescapeDataString(line.Substring(6));
                SequestFilename sf = this.parser.GetValue(title);

                var chargeLine = sr.ReadLine();
                sf.Charge = Convert.ToInt32(chargereg.Match(chargeLine).Groups[1].Value);

                line = "title=" + Uri.EscapeDataString(sf.LongFileName);

                sw.WriteLine(line);
                sw.WriteLine(chargeLine);
              }
              else
              {
                sw.WriteLine(line);
              }
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
