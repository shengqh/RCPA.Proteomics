using System.Collections.Generic;
using System.IO;

namespace RCPA.Proteomics.Quantification.Census
{
  public static class CensusUtils
  {
    public static List<string> ReadHeaders(string fileName)
    {
      var result = new List<string>();
      using (var sr = new StreamReader(fileName))
      {
        string lastLine;
        while ((lastLine = sr.ReadLine()) != null)
        {
          if (lastLine.StartsWith("H"))
          {
            result.Add(lastLine);
          }
          else
          {
            break;
          }
        }
      }
      return result;
    }
  }
}
