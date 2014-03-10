using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using RCPA.Gui;
using RCPA.Proteomics.Raw;
using RCPA.Proteomics.Spectrum;

namespace RCPA.Proteomics.Quantification.ITraq
{
  /// <summary>
  /// 从Raw文件中读取iTraq对应的minMz-maxMz区间的Peak，要求这区间至少包含minPeakCount离子
  /// </summary>
  public abstract class AbstractITraqRawHCDMS3Reader : AbstractITraqRawReader
  {
    protected List<ScanLevel> list;
    protected Dictionary<int, ScanLevel> scanMap;

    public AbstractITraqRawHCDMS3Reader(string name)
      : base(3, name)
    { }

    protected override void DoAfterFileOpen()
    {
      base.DoAfterFileOpen();

      Progress.SetMessage("Reading scan relationship ...");

      list = GetScanLevelBuilder().GetScanLevels(RawReader);

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

    protected override int GetIsolationScan(int scan)
    {
      return this.GetIdentificationScan(scan);
    }

    protected abstract IScanLevelBuilder GetScanLevelBuilder();
  }
}
