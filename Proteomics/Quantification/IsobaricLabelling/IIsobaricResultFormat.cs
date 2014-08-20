using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using RCPA.Gui;

namespace RCPA.Proteomics.Quantification.IsobaricLabelling
{
  public interface IIsobaricResultFormat : IProgress, IFileFormat<IsobaricResult>
  {
    /// <summary>
    /// 是否读写报告离子信息。对于原始数据，没有报告离子信息。报告离子需要利用子离子阈值进行筛选得到！
    /// </summary>
    bool HasReporters { get; set; }

    /// <summary>
    /// 是否读取Peak信息，包括报告离子区间以及对应Full Scan中母离子区间的离子。
    /// </summary>
    bool ReadPeaks { get; set; }

    /// <summary>
    /// 根据IsobaricItem的基本信息进行筛选，包括PlexType，Experimental，ScanMode，Scan，Scan.IonInjectionTime，PrecursorPercentage。
    /// 例如可以根据鉴定结果，读取这些结果相应的scan的信息。
    /// </summary>
    Predicate<IsobaricItem> Accept { get; set; }
  }
}
