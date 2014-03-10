using System.Collections.Generic;
using System.Text;
using System.Xml;
using System.Linq;
using RCPA.Utils;
using RCPA.Proteomics.Spectrum;
using System.Xml.Linq;
using System.IO;
using System;

namespace RCPA.Proteomics.Quantification.Lipid
{
  public class PrecursorAreaListTextFormat : IFileFormat<List<PrecursorArea>>
  {
    #region IFileFormat<List<PrecursorArea>> Members

    public void WriteToFile(string fileName, List<PrecursorArea> items)
    {
      using (StreamWriter sw = new StreamWriter(fileName))
      {
        sw.WriteLine("PrecursorMz\tScanCount\tArea\tEnabled");
        items.ForEach(m => sw.WriteLine("{0:0.0000}\t{1}\t{2:0.0}\t{3}", m.PrecursorMz, m.ScanCount, m.Area, m.Enabled.ToString()));
      }
    }

    public List<PrecursorArea> ReadFromFile(string fileName)
    {
      if (!File.Exists(fileName))
      {
        throw new FileNotFoundException(fileName);
      }

      List<PrecursorArea> result = new List<PrecursorArea>();
      using (StreamReader sr = new StreamReader(fileName))
      {
        sr.ReadLine();
        string line;
        while ((line = sr.ReadLine()) != null)
        {
          if (line.Trim().Length == 0)
          {
            break;
          }

          string[] parts = line.Split('\t');
          PrecursorArea pa = new PrecursorArea()
          {
            PrecursorMz = MyConvert.ToDouble(parts[0]),
            ScanCount = Convert.ToInt32(parts[1]),
            Area = MyConvert.ToDouble(parts[2]),
            Enabled = Convert.ToBoolean(parts[3])
          };

          result.Add(pa);
        }
      }
      return result;
    }

    #endregion
  }
}