using System;
using System.Collections.Generic;
using System.IO;

namespace RCPA.Proteomics.Summary
{
  public class IdentifiedResultExperimentalReader
  {
    private char[] chars = new char[] { '\t' };

    public HashSet<string> ReadFromFile(string fileName)
    {
      HashSet<string> result = new HashSet<string>();

      SequestFilename sf = new SequestFilename();
      using (StreamReader sr = new StreamReader(fileName))
      {
        //ignore header lines
        string line;
        while ((line = sr.ReadLine()) != null)
        {
          string[] parts = line.Trim().Split(chars);
          try
          {
            sf.ShortFileName = parts[0];
            break;
          }
          catch (Exception)
          { }
        }

        if (line != null)
        {
          result.Add(sf.Experimental);
        }

        while ((line = sr.ReadLine()) != null)
        {
          line = line.Trim();

          if (line.Length == 0)
          {
            break;
          }

          if (IdentifiedResultUtils.IsProteinLine(line))
          {
            continue;
          }

          string[] parts = line.Split(chars);
          sf.ShortFileName = parts[0];

          result.Add(sf.Experimental);
        }
      }

      return result;
    }
  }
}
