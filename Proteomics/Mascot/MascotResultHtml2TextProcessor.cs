using RCPA.Proteomics.Summary;
using System;
using System.Collections.Generic;
using System.IO;

namespace RCPA.Proteomics.Mascot
{
  public class MascotResultHtml2TextProcessor : IFileProcessor
  {
    #region IFileProcessor Members

    public IEnumerable<string> Process(string htmlFilename)
    {
      MascotResult mr = new MascotResultHtmlStandaloneParser(true).
        ParseFile(new FileInfo(htmlFilename));

      mr.Sort();

      mr.BuildGroupIndex();

      foreach (IdentifiedProteinGroup mpg in mr)
      {
        if (mpg.Count > 0)
        {
          mpg[0].SortPeptides();
        }
      }

      String resultFile = FileUtils.ChangeExtension(htmlFilename, ".txt");

      new MascotResultTextFormat().WriteToFile(resultFile, mr);

      return new[] { resultFile };
    }

    #endregion
  }
}