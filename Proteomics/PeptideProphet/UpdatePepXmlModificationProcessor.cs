using RCPA.Proteomics.Summary;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

namespace RCPA.Proteomics.PeptideProphet
{
  /// <summary>
  /// 20150729
  /// 去除modificationinfo中添加modified_peptide，用于iProphet
  /// </summary>
  public class UpdatePepXmlModificationProcessor : AbstractThreadFileProcessor
  {
    private ITitleParser parser;
    private List<string> sourceFiles;

    public UpdatePepXmlModificationProcessor(ITitleParser parser, IEnumerable<string> sourceFiles)
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
        var mparser = new MascotPepXmlParser() { TitleParser = parser };
        var spectra = mparser.ReadFromFile(sourceFile).ToDictionary(m => m.Query.FileScan.ShortFileName, m => m.GetMatchSequence());

        string sequence = string.Empty;

        using (StreamReader sr = new StreamReader(sourceFile))
        {
          using (StreamWriter sw = new StreamWriter(targetFile))
          {
            string line;
            while ((line = sr.ReadLine()) != null)
            {
              if (line.Contains("<spectrum_query"))
              {
                var query = line.StringAfter("spectrum=\"").StringBefore("\"");
                var sf = parser.GetValue(query);
                sequence = spectra[sf.ShortFileName];
                sw.WriteLine(line);
              }
              else if (line.Contains("<modification_info"))
              {
                if (!line.Contains("modified_peptide"))
                {
                  line = line.StringBefore(">") + " modified_peptide=\"" + sequence + "\">" + line.StringAfter(">");
                }
                sw.WriteLine(line);
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
