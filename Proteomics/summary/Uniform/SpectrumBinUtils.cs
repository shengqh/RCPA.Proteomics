using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RCPA.Proteomics.Summary.Uniform
{
  public static class SpectrumBinUtils
  {
    /// <summary>
    /// 对一次实验后的谱图字典进行fdr筛选，要求该spectra按照计算QValue的score从小到大排序。
    /// </summary>
    /// <param name="source">谱图字典</param>
    /// <param name="maxFdr">筛选fdr</param>
    public static void FilterByFdr(this Dictionary<OptimalResultCondition, List<IIdentifiedSpectrum>> source, double maxFdr)
    {
      foreach (var spectra in source.Values)
      {
        if (spectra.Count == 0)
        {
          continue;
        }

        if (spectra[0].QValue > maxFdr)
        {
          spectra.Clear();
          continue;
        }

        for (int i = spectra.Count - 1; i >= 0; i--)
        {
          if (spectra[i].QValue <= maxFdr)
          {
            if (i != spectra.Count - 1)
            {
              spectra.RemoveRange(i + 1, spectra.Count - i - 1);
            }

            break;
          }
        }
      }
    }

    /// <summary>
    /// 复制谱图字典，key用潜度复制的，但是List<IIdentifiedSpectrum>是深度复制的，这样，对该List的筛选不会影响原来的字典。
    /// </summary>
    /// <param name="source">谱图字典</param>
    /// <param name="maxFdr">筛选fdr</param>
    public static Dictionary<OptimalResultCondition, List<IIdentifiedSpectrum>> Copy(this Dictionary<OptimalResultCondition, List<IIdentifiedSpectrum>> source)
    {
      Dictionary<OptimalResultCondition, List<IIdentifiedSpectrum>> result = new Dictionary<OptimalResultCondition, List<IIdentifiedSpectrum>>();

      foreach (var keyvalue in source)
      {
        result[keyvalue.Key] = new List<IIdentifiedSpectrum>(keyvalue.Value);
      }

      return result;
    }

    /// <summary>
    /// 对谱图字典中谱图进行QValue计算
    /// </summary>
    /// <param name="source">谱图字典</param>
    /// <param name="maxFdr">筛选fdr</param>
    public static void CalculateQValue(this Dictionary<OptimalResultCondition, List<IIdentifiedSpectrum>> source, IDatasetBuilder builder)
    {
      //把谱图计算QValue，以便计算fdr。
      foreach (var spectra in source.Values)
      {
        builder.InitializeQValue(spectra);
      }
    }
  }
}
