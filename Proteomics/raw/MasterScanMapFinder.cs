using System.Collections.Generic;
using System.Linq;

namespace RCPA.Proteomics.Raw
{
  public class MasterScanMapFinder : IMasterScanFinder
  {
    private IScanLevelBuilder builder;
    public MasterScanMapFinder(IScanLevelBuilder builder)
    {
      this.builder = builder;
    }

    private class PairResult
    {
      public string FileName { get; set; }
      public Dictionary<int, ScanLevel> ScanMap { get; set; }
    }

    private Dictionary<IRawFile, PairResult> map = new Dictionary<IRawFile, PairResult>();
    public int Find(IRawFile reader, int scan)
    {
      PairResult pr;
      if (!map.TryGetValue(reader, out pr) || !reader.FileName.Equals(pr.FileName))
      {
        pr = new PairResult()
        {
          FileName = reader.FileName,
          ScanMap = builder.GetScanLevels(reader).ToDictionary(m => m.Scan)
        };
        map[reader] = pr;
      }

      ScanLevel level;
      if (pr.ScanMap.TryGetValue(scan, out level))
      {
        if (level.Parent != null)
        {
          return level.Parent.Scan;
        }
      }

      return 0;
    }
  }

  public class MasterScanParallelMS3Finder : MasterScanMapFinder
  {
    public MasterScanParallelMS3Finder() : base(new ScanLevelParallelMS3Builder()) { }
  }
}
