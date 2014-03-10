using System;
using System.Collections.Generic;
using System.Text;

namespace RCPA.Proteomics.Summary
{
  public interface IIdentifiedProteinGroupBuilder
  {
    /// <summary>
    /// 功能：从蛋白质列表构建蛋白质群列表。
    /// 首先，那些包含相同鉴定肽段的蛋白质被合并为一个蛋白质群。
    /// 然后，如果蛋白质群A的鉴定肽段被包含在另一个蛋白质群B的鉴定肽段中，蛋白质群A为冗余数据，被去除。
    /// </summary>
    /// <param name="proteins">蛋白质列表</param>
    /// <returns>非冗余的蛋白质群列表</returns>
    List<IIdentifiedProteinGroup> Build(List<IIdentifiedProtein> proteins);
  }
}
