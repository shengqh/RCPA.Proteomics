using System;
using System.Collections.Generic;
using System.Text;

namespace RCPA.Proteomics.Summary
{
  public interface IIdentifiedResultBuilder
  {
    /// <summary>
    /// 功能：根据非冗余的蛋白质群列表构建鉴定结果，解决protein inference问题。
    /// </summary>
    /// <param name="proteins">非冗余蛋白质群列表</param>
    /// <returns>最终鉴定结果</returns>
    /// <summary>
    IIdentifiedResult Build(List<IIdentifiedProteinGroup> groups);
  }
}
