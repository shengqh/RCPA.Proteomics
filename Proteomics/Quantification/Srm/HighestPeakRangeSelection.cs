using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RCPA.Proteomics.Quantification.Srm
{
  /// <summary>
  /// 根据最高峰的百分比（例如5%）进行区间选择
  /// </summary>
  public class HighestPeakRangeSelection : IRangeSelection
  {
    private double percentage;

    public HighestPeakRangeSelection(double percentage)
    {
      this.percentage = percentage;
    }

    #region IRangeSelection Members

    public void Select(SrmTransition trans)
    {
      var maxIntensity = trans.Intensities.Max(m => m.Intensity);
      if (maxIntensity > 0)
      {
        double minIntensity = maxIntensity * percentage;

        trans.Intensities.ForEach(m => m.Enabled = m.Intensity >= minIntensity);

        trans.KeepHighestContig();
      }
      else
      {
        trans.Intensities.ForEach(m => m.Enabled = false);
      }
    }

    #endregion
  }
}
