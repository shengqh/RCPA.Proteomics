﻿using RCPA.Gui;
using System.Collections.Generic;

namespace RCPA.Proteomics.Summary.Uniform
{
  public interface IDatasetBuilder : IProgress
  {
    /// <summary>
    /// 对应的数据集定义
    /// </summary>
    IDatasetOptions Options { get; }

    /// <summary>
    /// 针对指定的数据集，根据其参数构建通过固定阈值筛选、去除冗余后的肽段列表
    /// </summary>
    /// <returns>肽段列表</returns>
    List<IIdentifiedSpectrum> ParseFromSearchResult();

    /// <summary>
    /// 对spectrua计算QValue。
    /// </summary>
    /// <param name="spectra"></param>
    void InitializeQValue(List<IIdentifiedSpectrum> spectra);
  }
}
