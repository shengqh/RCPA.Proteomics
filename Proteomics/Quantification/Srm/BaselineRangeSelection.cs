using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RCPA.Proteomics.Quantification.Srm
{
  /// <summary>
  /// 根据Baseline的倍数（例如3倍）进行区间选择
  /// </summary>
  public class BaselineRangeSelection : IRangeSelection
  {
    private double lowestPercentageForBaseline;

    private double minFactor;

    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="lowestPercentageForBaseline">取百分之几的最低丰度进行基线计算</param>
    /// <param name="minFactor">峰的丰度超过几倍基线作为有效峰</param>
    public BaselineRangeSelection(double lowestPercentageForBaseline, double minFactor)
    {
      this.lowestPercentageForBaseline = lowestPercentageForBaseline;
      this.minFactor = minFactor;
    }

    #region IRangeSelection Members

    public void Select(SrmTransition trans)
    {
      var intensities = (from s in trans.Intensities
                         where s.Intensity > 0
                         orderby s.Intensity
                         select s.Intensity).ToList();

      if (intensities.Count > 0)
      {
        int selectedCount = (int)(intensities.Count * this.lowestPercentageForBaseline);
        if (selectedCount > 0)
        {
          trans.Noise = intensities.Take(selectedCount).Average();

          var minIntensity = trans.Noise * minFactor;

          trans.Intensities.ForEach(m => m.Enabled = m.Intensity >= minIntensity);

          trans.KeepHighestContig();

          return;
        }
        else
        {
          trans.Noise = intensities.Average();
        }
      }
      else
      {
        trans.Noise = 1.0;
      }

      trans.Intensities.ForEach(m => m.Enabled = false);
    }

    #endregion
  }
}
