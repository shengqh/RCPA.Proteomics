using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;

namespace RCPA.Proteomics.Sequest
{
  public class SequestLogReader : IFileReader<Dictionary<string, int>>
  {
    #region IFileReader<Dictionary<string,int>> Members

    public Dictionary<string, int> ReadFromFile(string fileName)
    {
      Dictionary<string, int> result = new Dictionary<string, int>();

      Regex reg = new Regex(@"Searched dta file .+ on (\S+)");
      using (StreamReader sr = new StreamReader(fileName))
      {
        string line;
        while ((line = sr.ReadLine()) != null)
        {
          Match m = reg.Match(line);
          if (m.Success)
          {
            string machine = m.Groups[1].Value;
            if (!result.ContainsKey(machine))
            {
              result[machine] = 1;
            }
            else
            {
              result[machine] = result[machine] + 1;
            }
          }
        }
      }

      return result;
    }

    #endregion
  }
}
