using System;
using System.Collections.Generic;
using System.Text;
using RCPA.Utils;

namespace RCPA.Proteomics.Summary
{
  public interface IIdentifiedSpectrumBuilder
  {
    IProgressCallback Progress { get; set; }

    /// <summary>
    /// 功能：根据参数文件，构建鉴定质谱谱图列表。
    /// </summary>
    /// <param name="parameterFile">参数文件</param>
    /// <returns>鉴定质谱谱图列表</returns>
    List<IIdentifiedSpectrum> Build(string parameterFile);
  }
}
