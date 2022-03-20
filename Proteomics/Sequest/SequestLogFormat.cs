using System;
using System.Collections.Generic;
using System.IO;

namespace RCPA.Proteomics.Sequest
{
  public class SequestLogFormat : IFileFormat<Dictionary<string, int>>
  {
    #region IFileReader<Dictionary<string,int>> Members

    public Dictionary<string, int> ReadFromFile(string fileName)
    {
      Dictionary<string, int> result = new Dictionary<string, int>();
      using (StreamReader sr = new StreamReader(fileName))
      {
        string line;
        char[] splitChars = new char[] { '\t' };
        while ((line = sr.ReadLine()) != null)
        {
          if (line.Trim().Length == 0)
          {
            break;
          }

          string[] parts = line.Split(splitChars);
          result[parts[0]] = Convert.ToInt32(parts[1]);
        }
      }
      return result;
    }

    #endregion

    #region IFileWriter<Dictionary<string,int>> Members

    public void WriteToFile(string fileName, Dictionary<string, int> t)
    {
      using (StreamWriter sw = new StreamWriter(fileName))
      {
        foreach (var entry in t)
        {
          sw.WriteLine(entry.Key + "\t" + entry.Value);
        }
      }
    }

    #endregion
  }
}
