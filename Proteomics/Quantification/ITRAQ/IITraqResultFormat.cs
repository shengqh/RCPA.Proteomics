using RCPA.Gui;
using System;

namespace RCPA.Proteomics.Quantification.ITraq
{
  public interface IITraqResultFileFormat : IProgress, IFileFormat<IsobaricResult>
  {
    /// <summary>
    /// 是否读取Peak信息，包括报告离子区间以及对应Full Scan中母离子区间的离子。
    /// </summary>
    bool ReadPeaks { get; set; }

    /// <summary>
    /// 根据ITraqItem的基本信息进行筛选，包括PlexType，Experimental，ScanMode，Scan，Scan.IonInjectionTime，PrecursorPercentage。
    ///例如可以根据鉴定结果，读取这些结果相应的scan的信息。
    /// </summary>
    Predicate<IsobaricItem> Accept { get; set; }
  }
}
