using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MathNet.Numerics.Statistics;

namespace RCPA.Proteomics.Quantification.Srm
{
  public class SrmPairedPeptideItem
  {
    /// <summary>
    /// 是否有效
    /// </summary>
    public bool Enabled { get; set; }

    /// <summary>
    /// 肽段轻重比（L/H）
    /// </summary>
    public double Ratio { get; set; }

    /// <summary>
    /// 肽段轻重比的标准差
    /// </summary>
    public double SD { get; set; }

    /// <summary>
    /// 最大的信噪比
    /// </summary>
    public double MaxSignalToNoise
    {
      get
      {
        return Math.Max(this.ProductIonPairs.Max(m => m.LightSignalToNoise), this.ProductIonPairs.Max(m => m.HeavySignalToNoise));
      }
    }

    /// <summary>
    /// 最大的回归相关性
    /// </summary>
    public double MaxRegressionCorrelation
    {
      get
      {
        return this.ProductIonPairs.Max(m => m.RegressionCorrelation);
      }
    }

    /// <summary>
    /// 成对的轻重标离子列表
    /// </summary>
    public List<SrmPairedProductIon> ProductIonPairs { get; set; }

    public SrmPairedPeptideItem()
    {
      Enabled = true;
      ProductIonPairs = new List<SrmPairedProductIon>();
    }

    public SrmPairedPeptideItem(SrmPeptideItem light)
    {
      Enabled = true;
      ProductIonPairs = (from p in light.ProductIons
                         select new SrmPairedProductIon(p)).ToList();
    }

    private SrmTransition FirstTransition
    {
      get
      {
        if (ProductIonPairs != null && ProductIonPairs.Count > 0)
        {
          if (ProductIonPairs[0].Light != null)
          {
            return ProductIonPairs[0].Light;
          }
          else
          {
            return ProductIonPairs[0].Heavy;
          }
        }
        return null;
      }
    }

    public string ObjectName
    {
      get
      {
        var trans = FirstTransition;
        return trans == null ? string.Empty : trans.ObjectName;
      }
    }

    public string PrecursorFormula
    {
      get
      {
        var trans = FirstTransition;
        return trans == null ? string.Empty : trans.PrecursorFormula;
      }
    }

    public int PrecursorCharge
    {
      get
      {
        var trans = FirstTransition;
        return trans == null ? 0 : trans.PrecursorCharge;
      }
    }

    /// <summary>
    /// 是否已配对
    /// </summary>
    public bool IsPaired
    {
      get
      {
        return ProductIonPairs != null && ProductIonPairs.Count > 0 &&
          ProductIonPairs.Any(m => m.IsPaired);
      }
    }

    public bool IsDecoy
    {
      get
      {
        if (ProductIonPairs == null || ProductIonPairs.Count == 0)
        {
          return false;
        }

        return ProductIonPairs[0].IsDecoy;
      }
    }

    /// <summary>
    /// 轻标母离子
    /// </summary>
    public double LightPrecursorMZ
    {
      get
      {
        if (ProductIonPairs != null && ProductIonPairs.Count > 0)
        {
          return ProductIonPairs[0].Light.PrecursorMZ;
        }
        else
        {
          return 0.0;
        }
      }
    }

    /// <summary>
    /// 重标母离子
    /// </summary>
    public double HeavyPrecursorMZ
    {
      get
      {
        if (IsPaired)
        {
          return ProductIonPairs[0].Heavy.PrecursorMZ;
        }
        else
        {
          return 0.0;
        }
      }
    }

    public bool PrecursorEquals(SrmPairedPeptideItem another, double mzTolerance)
    {
      return (Math.Abs(this.LightPrecursorMZ - another.LightPrecursorMZ) <= mzTolerance) &&
        (Math.Abs(this.HeavyPrecursorMZ - another.HeavyPrecursorMZ) <= mzTolerance);
    }

    /// <summary>
    /// 合并两个peptideitem。如果自动识别未能把来自同一个肽段的不同transaction合并，就需要手工合并。
    /// </summary>
    /// <param name="another">同一个肽段（对）的其他部分</param>
    public void MergePeptideItem(SrmPairedPeptideItem another)
    {
      this.ProductIonPairs.AddRange(another.ProductIonPairs);
    }

    /// <summary>
    /// 根据Ion来合并轻重标结果。如果自动识别未能把成对的肽段合并，就需要手工合并。
    /// </summary>
    /// <param name="heavy">重标肽段</param>
    public void SetHeavyPeptideItemByIon(SrmPairedPeptideItem heavy)
    {
      foreach (var light in this.ProductIonPairs)
      {
        var ion = light.Light.Ion;
        foreach (var h in heavy.ProductIonPairs)
        {
          if (ion.Equals(h.Light.Ion))
          {
            light.SetHeavyIon(h);
            heavy.ProductIonPairs.Remove(h);
            break;
          }
        }
      }

      this.ProductIonPairs.RemoveAll(m => m.Heavy == null);
    }

    /// <summary>
    /// 合并轻重标结果。如果自动识别未能把成对的肽段合并，就需要手工合并。
    /// </summary>
    /// <param name="heavy">重标肽段</param>
    public void SetHeavyPeptideItem(SrmPairedPeptideItem heavy)
    {
      var sorted = (from h in heavy.ProductIonPairs
                    orderby h.Light.ProductIon
                    select h).ToList();

      var sortedThis = (from h in this.ProductIonPairs
                        orderby h.Light.ProductIon
                        select h).ToList();

      //首先去除完全一样的production
      for (int i = sorted.Count - 1; i >= 0; i--)
      {
        var index = sortedThis.FindIndex(m => m.Light.ProductIon == sorted[i].Light.ProductIon);
        if (index != -1)
        {
          sortedThis[index].SetHeavyIon(sorted[i]);
          sorted.RemoveAt(i);
          sortedThis.RemoveAt(index);
        }
      }

      for (int i = 0; i < sorted.Count; i++)
      {
        sortedThis[i].SetHeavyIon(sorted[i]);
      }
    }

    /// <summary>
    /// production的差距是否在容许的差距集合中
    /// </summary>
    /// <param name="gap">轻重production的质荷比的差距</param>
    /// <param name="allowedGaps">容许的差距集合（例如4,5,8,10是K8R10的二价/一价离子差距值）</param>
    /// <param name="mzTolerance">误差</param>
    /// <returns>是否容许</returns>
    private bool InAllowedGap(double gap, double[] allowedGaps, double mzTolerance)
    {
      foreach (var allowedGap in allowedGaps)
      {
        if (Math.Abs(gap - allowedGap) < mzTolerance)
        {
          return true;
        }
      }

      return false;
    }

    /// <summary>
    /// 判断另外一个肽段是否是这个肽段的配对肽段，要求该肽段与本肽段的所有子离子质荷比完全相同，或者差值在给定允许集合内。
    /// 其中一个肽段的所有离子必须全部包含或者差值在给定集合内。
    /// </summary>
    /// <param name="another">另一个肽段</param>
    /// <param name="allowedGaps">子离子质荷比之差的容许值集合</param>
    /// <param name="mzTolerance">质荷比阈值</param>
    /// <returns>配对是否成功</returns>
    public bool AddPerfectPairedPeptideItem(SrmPairedPeptideItem another, double[] allowedGaps, double mzTolerance, double rtTolerance)
    {
      if (another == null)
      {
        return false;
      }

      if (!RetentionTimeEqualsTo(another, rtTolerance))
      {
        return false;
      }

      List<double> thisions = (from p in this.ProductIonPairs
                               orderby p.Light.ProductIon
                               select p.Light.ProductIon).ToList();

      List<double> anotherions = (from p in another.ProductIonPairs
                                  select p.Light.ProductIon).ToList();

      List<Pair<double, double>> pairs = new List<Pair<double, double>>();

      for (int i = thisions.Count - 1; i >= 0; i--)
      {
        for (int j = anotherions.Count - 1; j >= 0; j--)
        {
          if (thisions[i] == anotherions[j])
          {
            pairs.Add(new Pair<double, double>(thisions[i], anotherions[j]));
            thisions.RemoveAt(i);
            anotherions.RemoveAt(j);
            break;
          }
        }
      }

      for (int i = thisions.Count - 1; i >= 0; i--)
      {
        for (int j = anotherions.Count - 1; j >= 0; j--)
        {
          var gap = Math.Abs(thisions[i] - anotherions[j]);
          if (InAllowedGap(gap, allowedGaps, mzTolerance))
          {
            pairs.Add(new Pair<double, double>(thisions[i], anotherions[j]));
            thisions.RemoveAt(i);
            anotherions.RemoveAt(j);
            break;
          }
        }
      }

      if (thisions.Count != 0 && anotherions.Count != 0)
      {
        return false;
      }

      if (thisions.Count > 0)
      {
        this.ProductIonPairs.RemoveAll(m => thisions.Contains(m.LightProductIon));
      }

      if (anotherions.Count > 0)
      {
        another.ProductIonPairs.RemoveAll(m => anotherions.Contains(m.LightProductIon));
      }

      SetHeavyPeptideItem(another);

      return true;
    }

    /// <summary>
    /// 判断另外一个肽段是否是这个肽段的配对肽段，要求该肽段与本肽段的所有子离子质荷比完全相同，或者差值在给定允许集合内。
    /// 允许两个肽段只有部分离子相等或者差值在给定集合内。
    /// </summary>
    /// <param name="another">另一个肽段</param>
    /// <param name="allowedGaps">子离子质荷比之差的容许值集合</param>
    /// <param name="mzTolerance">质荷比阈值</param>
    /// <returns>配对是否成功</returns>
    public bool AddErrorPairedPeptideItem(SrmPairedPeptideItem another, double[] allowedGaps, double mzTolerance, double rtTolerance)
    {
      if (another == null)
      {
        return false;
      }

      if (!RetentionTimeEqualsTo(another, rtTolerance))
      {
        return false;
      }

      List<double> thisions = (from p in this.ProductIonPairs
                               orderby p.Light.ProductIon
                               select p.Light.ProductIon).ToList();

      List<double> anotherions = (from p in another.ProductIonPairs
                                  select p.Light.ProductIon).ToList();

      List<Pair<double, double>> pairs = new List<Pair<double, double>>();

      for (int i = thisions.Count - 1; i >= 0; i--)
      {
        for (int j = anotherions.Count - 1; j >= 0; j--)
        {
          if (thisions[i] == anotherions[j])
          {
            pairs.Add(new Pair<double, double>(thisions[i], anotherions[j]));
            thisions.RemoveAt(i);
            anotherions.RemoveAt(j);
            break;
          }
        }
      }

      for (int i = thisions.Count - 1; i >= 0; i--)
      {
        for (int j = anotherions.Count - 1; j >= 0; j--)
        {
          var gap = Math.Abs(thisions[i] - anotherions[j]);
          if (InAllowedGap(gap, allowedGaps, mzTolerance))
          {
            pairs.Add(new Pair<double, double>(thisions[i], anotherions[j]));
            thisions.RemoveAt(i);
            anotherions.RemoveAt(j);
            break;
          }
        }
      }

      if (pairs.Count == 0)
      {
        return false;
      }

      if (thisions.Count > 0)
      {
        this.ProductIonPairs.RemoveAll(m => thisions.Contains(m.LightProductIon));
      }

      if (anotherions.Count > 0)
      {
        another.ProductIonPairs.RemoveAll(m => anotherions.Contains(m.LightProductIon));
      }

      SetHeavyPeptideItem(another);

      return true;
    }

    /// <summary>
    /// 根据给定保留时间区间设置所有子离子对的有效区域。
    /// </summary>
    /// <param name="minRT">最小保留时间</param>
    /// <param name="maxRT">最大保留时间</param>
    public void SetEnabledRetentionTimeRange(double minRT, double maxRT)
    {
      this.ProductIonPairs.ForEach(m => m.SetEnabledRetentionTimeRange(minRT, maxRT));
    }

    /// <summary>
    /// 计算每个离子对的轻重比及面积
    /// </summary>
    /// <param name="deductBaseLine">是否去除基线。当去除基线时，结果为Light = a * Heavy，过原点。否则为Light = a * Heavy + b，b为截距。</param>
    public void CalculateTransactionRatio(SrmOptions options)
    {
      this.ProductIonPairs.ForEach(m => m.CalculateRatio(options));
    }

    /// <summary>
    /// 计算肽段的轻重比/标准差。为各个有效的子离子的比值的算术平均。
    /// </summary>
    public void CalculatePeptideRatio()
    {
      if (this.ProductIonPairs.Any(m => m.Enabled))
      {
        var ratios = (from m in ProductIonPairs
                      where m.Enabled
                      select m.Ratio).ToArray();
        this.Ratio = Statistics.Mean(ratios);
        this.SD = Statistics.StandardDeviation(ratios);
      }
      else
      {
        this.Ratio = -1;
        this.SD = -1;
      }
    }

    /// <summary>
    /// 判断目标肽段与本肽段的保留时间是否一致
    /// </summary>
    /// <param name="another">目标肽段</param>
    /// <param name="rtToleranceMilliseconds">保留时间阈值</param>
    /// <returns></returns>
    public bool RetentionTimeEqualsTo(SrmPairedPeptideItem another, double rtToleranceMilliseconds)
    {
      return this.ProductIonPairs[0].Light.RetentionTimeEqualsTo(another.ProductIonPairs[0].Light, rtToleranceMilliseconds);
    }

    public override string ToString()
    {
      StringBuilder sb = new StringBuilder();

      if (this.ProductIonPairs.Count > 0)
      {
        if (this.ProductIonPairs[0].Light != null)
        {
          sb.AppendFormat("[{0}-{1}] - ", this.ProductIonPairs[0].Light.ObjectName, this.ProductIonPairs[0].Light.PrecursorFormula);
        }
        else if (this.ProductIonPairs[0].Heavy != null)
        {
          sb.AppendFormat("[{0}-{1}] - ", this.ProductIonPairs[0].Heavy.ObjectName, this.ProductIonPairs[0].Heavy.PrecursorFormula);
        }
      }

      sb.AppendFormat("{0:0.0000} : {1:0.0000}", this.LightPrecursorMZ, this.HeavyPrecursorMZ);

      return sb.ToString();
    }

    /// <summary>
    /// 根据给定算法进行区间选择
    /// </summary>
    /// <param name="peakPicking">区间选择算法</param>
    /// <param name="deductBaseLine">是否需要去除基线</param>
    public void PeakPicking(IRangeSelection peakPicking, SrmOptions options)
    {
      //首先，每个独立进行peakpicking。
      foreach (var product in ProductIonPairs)
      {
        product.PeakPicking(peakPicking);
        product.CalculateRatio(options);
        product.Enabled = false;
      }

      if (options.RefineData && ProductIonPairs.Count > 1)
      {
        //如果所有scan都是false，直接返回。
        if (!ProductIonPairs.Any(m => m.EnabledScanCount > 0))
        {
          return;
        }

        int maxIndex;
        if (IsPaired)
        {
          //选择correlation最高的一个作为标准
          var maxCorr = double.MinValue;
          maxIndex = -1;
          for (int i = 0; i < ProductIonPairs.Count; i++)
          {
            if (ProductIonPairs[i].EnabledScanCount == 0)
            {
              continue;
            }

            if (maxCorr < ProductIonPairs[i].RegressionCorrelation)
            {
              maxCorr = ProductIonPairs[i].RegressionCorrelation;
              maxIndex = i;
            }
          }
        }
        else//只有轻标结果
        {
          var maxIntensity = 0.0;
          maxIndex = 0;

          for (int i = 0; i < ProductIonPairs.Count; i++)
          {
            var intensities = (from s in ProductIonPairs[i].Light.Intensities
                               where s.Enabled
                               select s.Intensity).ToList();
            if (intensities.Count > 0)
            {
              var maxInt = intensities.Max();
              if (maxIntensity < maxInt)
              {
                maxIntensity = maxInt;
                maxIndex = i;
              }
            }
          }
        }

        var rts = (from m in ProductIonPairs[maxIndex].Light.Intensities
                   where m.Enabled
                   orderby m.RetentionTime
                   select m.RetentionTime).ToList();

        this.SetEnabledRetentionTimeRange(rts.First(), rts.Last());
      }

      this.CalculateTransactionRatio(options);
    }


    public void CheckEnabled(double outlierEvalue, int minValidTransitionPair)
    {
      var pips = (from p in this.ProductIonPairs
                  where p.Enabled && p.IsPaired
                  select p).ToList();

      if (pips.Count > 0)
      {
        var values = (from p in pips
                      select Math.Log(p.Ratio)).ToList();

        var outlier = OutlierDetector.Detect(values, outlierEvalue);
        if (outlier != -1)
        {
          pips[outlier].Enabled = false;
        }
      }

      this.Enabled = this.ProductIonPairs.Count(n => n.Enabled) >= minValidTransitionPair;
    }
  }
}
