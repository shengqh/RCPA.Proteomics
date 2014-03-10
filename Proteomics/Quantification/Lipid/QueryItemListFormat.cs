using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Text.RegularExpressions;

namespace RCPA.Proteomics.Quantification.Lipid
{
  public class QueryItemListFormat : IFileFormat<List<QueryItem>>
  {
    #region IFileReader<List<QueryItem>> Members

    public List<QueryItem> ReadFromFile(string fileName)
    {
      List<QueryItem> result = new List<QueryItem>();

      using (var sr = new StreamReader(fileName))
      {
        string line;
        Regex reg = new Regex(@"([\d.]+)\s+([\d.]+)");
        while ((line = sr.ReadLine()) != null)
        {
          line = line.Trim();
          if (line.Length == 0)
          {
            break;
          }

          var m = reg.Match(line);
          if (m.Success)
          {
            result.Add(new QueryItem()
            {
              ProductIonMz = MyConvert.ToDouble(m.Groups[1].Value),
              MinRelativeIntensity = MyConvert.ToDouble(m.Groups[2].Value)
            });
          }
          else
          {
            throw new ArgumentException(MyConvert.Format("Line {0} is not valid.\nIt should be like: 186.0247 0.3", line));
          }
        }
      }

      return result;
    }

    #endregion

    #region IFileWriter<List<QueryItem>> Members

    public void WriteToFile(string fileName, List<QueryItem> t)
    {
      using (var sw = new StreamWriter(fileName))
      {
        sw.WriteLine("ProductIonMz\tMinRelativeIntensity");
        t.ForEach(m => sw.WriteLine(m.ProductIonMz + "\t" + m.MinRelativeIntensity));
      }
    }

    #endregion
  }
}
