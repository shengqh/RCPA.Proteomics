using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace RCPA.Proteomics.Raw
{
  public abstract class AbstractScanLevelBuilder : IScanLevelBuilder
  {
    protected List<ScanLevel> GetRawScanLevels(IRawFile rawFile)
    {
      var result = new List<ScanLevel>();
      var firstScan = rawFile.GetFirstSpectrumNumber();
      var lastScan = rawFile.GetLastSpectrumNumber();
      for (var scan = firstScan; scan <= lastScan; scan++)
      {
        var mslevel = rawFile.GetMsLevel(scan);
        result.Add(new ScanLevel()
        {
          Scan = scan,
          Level = mslevel
        });
      }
      return result;
    }

    public abstract List<ScanLevel> GetScanLevels(IRawFile rawFile);
  }
}
