using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using RCPA.Proteomics.Raw;
using MathNet.Numerics.Statistics;
using System.IO;
using MathNet.Numerics.LinearAlgebra.Double;
using RCPA.Proteomics.Spectrum;

namespace RCPA.Proteomics.Statistic
{
  public static class ScanCountFormat
  {
    public static Dictionary<string, int> ReadFromFile(string fileName)
    {
      var result =new Dictionary<string, int>();
      var lines = File.ReadAllLines(fileName).Skip(1);
      foreach (var line in lines)
      {
        var parts = line.Split('\t');
        if (parts.Length > 1)
        {
          result[parts[0]] = int.Parse(parts[1]);
        }
      }

      return result;
    }

    public static void WriteToFile(string fileName, Dictionary<string, int> counts)
    {
      using (StreamWriter sw = new StreamWriter(fileName))
      {
        sw.WriteLine("ScanType\tCount");

        var keys = (from key in counts.Keys
                    orderby key
                    select key).ToList();

        foreach (var key in keys)
        {
          sw.WriteLine("{0}\t{1}", key, counts[key]);
        }
      }
    }
  }

  public class ScanCountTaskCalculator : AbstractParallelTaskProcessor
  {
    private string targetDir;

    public ScanCountTaskCalculator(string targetDir)
    {
      this.targetDir = targetDir;
    }

    public override IEnumerable<string> Process(string fileName)
    {
      var result = new FileInfo(targetDir + "\\" + new FileInfo(fileName).Name + ".scancount").FullName;

      Dictionary<string, int> scanTypeCount = new Dictionary<string, int>();
      using (var reader = RawFileFactory.GetRawFileReader(fileName))
      {
        var firstScan = reader.GetFirstSpectrumNumber();
        var lastScan = reader.GetLastSpectrumNumber();
        for (int scan = firstScan; scan <= lastScan; scan++)
        {
          var msLevel = reader.GetMsLevel(scan);
          var scanMode = reader.GetScanMode(scan);
          var key = string.Format("MS{0}_{1}", msLevel, scanMode);
          if (!scanTypeCount.ContainsKey(key))
          {
            scanTypeCount[key] = 1;
          }
          else
          {
            scanTypeCount[key] = scanTypeCount[key] + 1;
          }
        }
      }

      ScanCountFormat.WriteToFile(result, scanTypeCount);

      return new string[] { result };
    }
  }
}
