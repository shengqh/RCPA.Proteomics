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
  public class ITraqHcdRawMS3Reader : AbstractITraqRawReader
  {
    public ITraqHcdRawMS3Reader()
    {
      this.MsLevel = 3;
    }

    protected override string[] GetScanMode()
    {
      return new string[] { "HCD" };
    }

    protected override int GetMS2Scan(int scan)
    {
      var result = scan;
      while (true)
      {
        var msLevel = RawReader.GetMsLevel(result);
        if (msLevel == 2)
        {
          break;
        }

        result--;
        if (result < FirstScan)
        {
          result = scan;
          break;
        }
      }

      return result;
    }

    public override string ToString()
    {
      return "HCD-MS3";
    }
  }
}
