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
  public class ITraqRawHCDMS2Reader : AbstractITraqRawReader
  {
    public ITraqRawHCDMS2Reader() : base(2, "HCD-MS2") { }

    protected override string[] GetScanMode()
    {
      return new string[] { "HCD" };
    }

    public override string ToString()
    {
      return Name + " : MS1->MS2->MS2";
    }
  }
}
