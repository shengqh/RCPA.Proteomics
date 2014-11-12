using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections.Concurrent;
using System.IO;

namespace RCPA.Proteomics.Statistic
{
  public class ScanCountMainCalculator : AbstractParallelMainFileProcessor
  {
    private bool bMerge;
    public ScanCountMainCalculator(IEnumerable<string> ASourceFiles, bool bMerge)
      : base(ASourceFiles)
    {
      this.bMerge = bMerge;
    }

    protected override IParallelTaskFileProcessor GetTaskProcessor(string targetDir, string fileName)
    {
      return new ScanCountTaskCalculator(targetDir);
    }

    protected override void DoAfterProcessing(string aPath, ConcurrentBag<string> result)
    {
      if (bMerge)
      {
        var counts = new Dictionary<string, int>();
        foreach (var file in result)
        {
          var curCounts = ScanCountFormat.ReadFromFile(file);

          foreach (var entry in curCounts)
          {
            if (!counts.ContainsKey(entry.Key))
            {
              counts[entry.Key] = entry.Value;
            }
            else
            {
              counts[entry.Key] = counts[entry.Key] + entry.Value;
            }
          }
        }

        var combineFile = new FileInfo(aPath + "\\scancount.xls").FullName;
        ScanCountFormat.WriteToFile(combineFile, counts);

        result.Add(combineFile);
      }
    }
  }
}
