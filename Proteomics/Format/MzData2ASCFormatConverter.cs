using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using RCPA.Proteomics.Raw;
using RCPA.Utils;
using System.IO;
using RCPA.Proteomics.Spectrum;

namespace RCPA.Proteomics.Format
{
  public class MzData2ASCFormatConverter : AbstractThreadFileProcessor
  {
    private string targetDir;
    private IRawFile reader;
    public MzData2ASCFormatConverter(IRawFile reader, string targetDir)
    {
      this.reader = reader;
      this.targetDir = targetDir;
    }

    public override IEnumerable<string> Process(string fileName)
    {
      string resultFile = new FileInfo(targetDir + "\\" + FileUtils.ChangeExtension(new FileInfo(fileName).Name, ".txt")).FullName;
      reader.Open(fileName);
      try
      {
        using (StreamWriter sw = new StreamWriter(resultFile))
        {
          sw.NewLine = "\n";
          sw.WriteLine();
          sw.WriteLine("FUNCTION 1");

          int firstScan = reader.GetFirstSpectrumNumber();
          int lastScan = reader.GetLastSpectrumNumber();

          for (int i = firstScan; i <= lastScan; i++)
          {
            var pkl = reader.GetPeakList(i);
            sw.WriteLine("");
            sw.WriteLine("Scan\t\t{0}", i);
            sw.WriteLine("Retention Time\t{0:0.000}", reader.ScanToRetentionTime(i));
            sw.WriteLine("");
            foreach (var peak in pkl)
            {
              sw.WriteLine("{0:0.0000}\t{1:0}", peak.Mz, peak.Intensity);
            }
          }
        }
      }
      finally
      {
        reader.Close();
      }
      return new string[] { resultFile };
    }
  }
}
