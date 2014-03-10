using System;
using System.Collections.Generic;
using System.Text;

namespace RCPA.Proteomics.Summary
{
  public interface IIdentifiedProteinBuilder
  {
    /// <summary>
    /// 功能：从鉴定质谱列表构建蛋白质列表。
    /// </summary>
    /// <param name="proteins">鉴定质谱列表</param>
    /// <returns>蛋白质列表</returns>
    List<IIdentifiedProtein> Build<T>(IEnumerable<T> spectra) where T : IIdentifiedSpectrumBase;
  }
}
