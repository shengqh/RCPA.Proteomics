using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using RCPA.Proteomics.Summary;

namespace RCPA.Proteomics.Quantification
{
  public interface IQuantificationSummaryOption
  {
    IProteinRatioCalculator GetProteinRatioCalculator();

    object ReadRatioFile(string file);

    /// <summary>
    /// 线性回归的RSquare阈值
    /// </summary>
    double MinimumRSquare { get; set; }

    /// <summary>
    /// 是否有ratio值
    /// </summary>
    /// <param name="ann"></param>
    /// <returns></returns>
    bool HasPeptideRatio(IIdentifiedSpectrum ann);

    /// <summary>
    /// 是否有ratio值
    /// </summary>
    /// <param name="ann"></param>
    /// <returns></returns>
    bool HasProteinRatio(IIdentifiedProtein ann);

    /// <summary>
    /// 是否有ratio值，有的话，是否通过了筛选
    /// </summary>
    /// <param name="ann"></param>
    /// <returns></returns>
    bool IsPeptideRatioValid(IIdentifiedSpectrum ann);

    /// <summary>
    /// 设置ratio是否enabled.
    /// </summary>
    /// <param name="ann"></param>
    /// <param name="value"></param>
    void SetPeptideRatioValid(IIdentifiedSpectrum ann, bool value);

    /// <summary>
    /// 是否有ratio值，有的话，是否通过了筛选
    /// </summary>
    /// <param name="ann"></param>
    /// <returns></returns>
    bool IsProteinRatioValid(IIdentifiedProtein ann);

    /// <summary>
    /// 设置ratio是否enabled.
    /// </summary>
    /// <param name="ann"></param>
    /// <param name="value"></param>
    void SetProteinRatioValid(IIdentifiedProtein ann, bool value);

    bool IsPeptideOutlier(IIdentifiedSpectrum ann);

    bool IsProteinOutlier(IIdentifiedProtein ann);

    double GetPeptideRatio(IIdentifiedSpectrum ann);

    double GetProteinRatio(IIdentifiedProtein ann);

    string GetProteinRatioDescription(IIdentifiedProtein ann);

    IQuantificationPeptideForm CreateForm();

    IGetRatioIntensity Func { get; }

    string RatioFileKey { get; }
  }

  public interface IExtendQuantificationSummaryOption : IQuantificationSummaryOption
  {
    string GetPeptideClassification(IIdentifiedSpectrum ann);

    double GetProteinRatio(IIdentifiedProtein ann, string key);

    string GetProteinRatioDescription(IIdentifiedProtein ann, string key);
  }
}
