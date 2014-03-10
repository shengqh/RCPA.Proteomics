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
  public class ITraqRawPQDReader : AbstractITraqRawReader
  {
    public ITraqRawPQDReader() : base(2, "PQD") { }

    protected override string[] GetScanMode()
    {
      return new string[] { "PQD" };
    }

    public override string ToString()
    {
      return this.Name + " : MS1->PQD->PQD";
    }
  }
}
