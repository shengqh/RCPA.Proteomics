using System.IO;

namespace RCPA.Proteomics.Quantification.O18
{
  public class O18QuantificationSummaryItemUtils
  {
    public static bool IsXmlFormat(string filename)
    {
      using (var sr = new StreamReader(filename))
      {
        string line;
        while ((line = sr.ReadLine()) != null)
        {
          if (line.Trim().Length == 0)
          {
            continue;
          }

          if (line.StartsWith("<?xml"))
          {
            return true;
          }
        }
      }

      return false;
    }
  }
}