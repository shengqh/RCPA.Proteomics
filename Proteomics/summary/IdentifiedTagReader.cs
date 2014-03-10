using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace RCPA.Proteomics.Summary
{
  public static class IdentifiedTagReader
  {
    public static HashSet<string> GetTags(string fileName, bool isProteinFile)
    {
      HashSet<string> result = new HashSet<string>();

      using (StreamReader sr = new StreamReader(fileName))
      {
        if (isProteinFile)
        {
          sr.ReadLine();
        }
        var line = sr.ReadLine();
        var parts = line.Split('\t');
        var tagIndex = Array.IndexOf(parts, "Tag");
        if (tagIndex >= 0)
        {
          while ((line = sr.ReadLine()) != null)
          {
            if (isProteinFile && IdentifiedResultUtils.IsProteinLine(line))
            {
              continue;
            }

            parts = line.Split('\t');
            if (parts.Length <= tagIndex)
            {
              break;
            }

            result.Add(parts[tagIndex]);
          }
        }
      }

      return result;
    }
  }
}
