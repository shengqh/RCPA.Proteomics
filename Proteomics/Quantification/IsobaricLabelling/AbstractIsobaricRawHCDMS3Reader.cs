using RCPA.Proteomics.Raw;
using System.Collections.Generic;
using System.Linq;

namespace RCPA.Proteomics.Quantification.IsobaricLabelling
{
  /// <summary>
  /// 从Raw文件中读取iTraq对应的minMz-maxMz区间的Peak，要求这区间至少包含minPeakCount离子
  /// </summary>
  public abstract class AbstractIsobaricRawHCDMS3Reader : AbstractIsobaricRawReader
  {
    protected List<ScanLevel> list;
    protected Dictionary<int, ScanLevel> scanMap;

    public AbstractIsobaricRawHCDMS3Reader(string name)
      : base(3, name)
    { }

    protected override void DoAfterFileOpen(IRawFile2 rawReader)
    {
      base.DoAfterFileOpen(rawReader);

      Progress.SetMessage("Reading scan relationship ...");

      list = GetScanLevelBuilder().GetScanLevels(rawReader);

      scanMap = list.ToDictionary(m => m.Scan);
    }

    protected override string[] GetScanMode()
    {
      return new string[] { "HCD" };
    }

    protected override int GetIdentificationScan(int scan)
    {
      if (scanMap.ContainsKey(scan))
      {
        return scanMap[scan].Parent.Scan;
      }
      return scan;
    }

    protected override int GetIsolationScan(IRawFile2 rawReader, int scan)
    {
      return this.GetIdentificationScan(scan);
    }

    protected abstract IScanLevelBuilder GetScanLevelBuilder();
  }
}
