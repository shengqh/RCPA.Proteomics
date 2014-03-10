using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace RCPA.Tools.Quantification.SmallMolecule
{
  public class SmallMoleculePeakInfoListTextFormat : IFileFormat<List<SmallMoleculePeakInfo>>
  {
    #region IFileReader<List<SmallMoleculePeakInfo>> Members

    public List<SmallMoleculePeakInfo> ReadFromFile(string fileName)
    {
      List<SmallMoleculePeakInfo> result = new List<SmallMoleculePeakInfo>();

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
          result.Add(new SmallMoleculePeakInfo(null, null, parts[0])
          {
            TwoTail = MyConvert.ToDouble(parts[1]),
            LeftTail = MyConvert.ToDouble(parts[2]),
            RightTail = MyConvert.ToDouble(parts[3])
          });
        }
      }

      return result;
    }

    #endregion

    #region IFileWriter<List<SmallMoleculePeakInfo>> Members

    public void WriteToFile(string fileName, List<SmallMoleculePeakInfo> t)
    {
      using (StreamWriter sw = new StreamWriter(fileName))
      {
        sw.WriteLine("Peak\tTwoTailed\tLeftTailed\tRightTailed");
        foreach (var p in t)
        {
          sw.WriteLine("{0}\t{1:E2}\t{2:E2}\t{3:E2}", p.Peak, p.TwoTail, p.LeftTail, p.RightTail);
        }

        sw.WriteLine();
        sw.WriteLine("TwoTailed : the probability that the peak intensity in samples are not lower/higher than in references");
        sw.WriteLine("LeftTailed : the probability that the peak intensity in samples are not lower than in references");
        sw.WriteLine("RightTailed : the probability that the peak intensity in samples are not higher than in references");
      }
    }

    #endregion
  }
}
