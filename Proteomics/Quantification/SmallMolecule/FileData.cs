using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace RCPA.Proteomics.Quantification.SmallMolecule
{
  public class FileData : Dictionary<int, Dictionary<string, double>>
  {
    public string FileName { get; set; }

    public static FileData ReadFromFile(string fileName)
    {
      FileData result = new FileData();
      result.Read(fileName);
      return result;
    }

    public double MaxIntensity(string peak)
    {
      return (from p in Values
              from p2 in p
              where p2.Key.Equals(peak)
              select p2.Value).Max();
    }

    public void Read(string fileName)
    {
      Clear();

      this.FileName = new FileInfo(fileName).Name;

      using (StreamReader sr = new StreamReader(fileName))
      {
        string line;
        line = sr.ReadLine();
        string[] mzStr = line.Split('\t');

        while ((line = sr.ReadLine()) != null)
        {
          if (line.Trim().Length == 0)
          {
            break;
          }

          Dictionary<string, double> scanMap = new Dictionary<string, double>();

          string[] parts = line.Split('\t');
          for (int i = 1; i < parts.Length; i++)
          {
            scanMap[mzStr[i]] = MyConvert.ToDouble(parts[i]);
          }

          this[int.Parse(parts[0])] = scanMap;
        }
      }
    }
  }
}
