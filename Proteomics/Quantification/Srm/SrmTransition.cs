using RCPA.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RCPA.Proteomics.Quantification.Srm
{
  public class SrmTransition
  {
    public SrmTransition()
      : this(0, 0)
    { }

    public SrmTransition(double precursorMZ, double productIon)
    {
      this._precursorMZ = precursorMZ;
      this._productIon = productIon;
      this.Intensities = new List<SrmScan>();
      this.IsDecoy = false;
      this.IsHeavy = false;
      this.ObjectName = string.Empty;
      this.PrecursorFormula = string.Empty;
      this.PrecursorCharge = 0;
      this.ProductIonCharge = 0;

      this._signalToNoise = -1;
      this._noise = -1;

      InitializeTransitionMZ();
    }

    private void InitializeTransitionMZ()
    {
      this._transitionMZ = MyConvert.Format("{0:0.0000}-{1:0.0000}", PrecursorMZ, ProductIon);
      this._transitionHashCode = this._transitionMZ.GetHashCode();
    }

    /// <summary>
    /// 是否诱饵transition
    /// </summary>
    public bool IsDecoy { get; set; }

    /// <summary>
    /// 组件名称，可以是蛋白质名称，也可以是其他。
    /// </summary>
    public string ObjectName { get; set; }

    /// <summary>
    /// 是否重标
    /// </summary>
    public bool IsHeavy { get; set; }

    /// <summary>
    /// 是否轻标
    /// </summary>
    public bool IsLight
    {
      get
      {
        return !IsHeavy;
      }
      set
      {
        IsHeavy = !value;
      }
    }

    /// <summary>
    /// 母离子名称，可以是肽段序列，也可以是其他。
    /// </summary>
    public string PrecursorFormula { get; set; }

    /// <summary>
    /// 母离子电荷
    /// </summary>
    public int PrecursorCharge { get; set; }

    private string _transitionMZ;
    private int _transitionHashCode;

    private double _precursorMZ;

    /// <summary>
    /// 母离子质荷比
    /// </summary>
    public double PrecursorMZ
    {
      get
      {
        return _precursorMZ;
      }
      set
      {
        _precursorMZ = value;
        InitializeTransitionMZ();
      }
    }

    /// <summary>
    /// 子离子电荷
    /// </summary>
    public int ProductIonCharge { get; set; }

    private double _productIon;

    /// <summary>
    /// 子离子质荷比
    /// </summary>
    public double ProductIon
    {
      get
      {
        return _productIon;
      }
      set
      {
        _productIon = value;
        InitializeTransitionMZ();
      }
    }

    public string Ion
    {
      get
      {
        if (string.IsNullOrEmpty(IonType))
        {
          return string.Empty;
        }

        return MyConvert.Format("{0}{1}", IonType, IonIndex);
      }
    }

    /// <summary>
    /// 离子类型，例如b/y
    /// </summary>
    public string IonType { get; set; }

    /// <summary>
    /// 离子下标，表示该离子在该离子系列中的位置，例如b系列的第7个离子（b7）
    /// </summary>
    public int IonIndex { get; set; }

    /// <summary>
    /// 实验设计的保留时间中间点
    /// </summary>
    public double ExpectCenterRetentionTime { get; set; }

    /// <summary>
    /// 碰撞能量
    /// </summary>
    public double CollisionEnergy { get; set; }

    /// <summary>
    /// 在采集的数据中有多少认为是正确的保留时间内
    /// </summary>
    public int EnabledCount
    {
      get
      {
        return this.Intensities.Count(m => m.Enabled);
      }
    }

    /// <summary>
    /// 检测结果的保留时间的中间点
    /// </summary>
    public virtual double ActualCenterRetentionTime
    {
      get
      {
        return (this.FirstRetentionTime + this.LastRetentionTime) / 2;
      }
    }

    /// <summary>
    /// 计算平均的噪音水平。
    /// </summary>
    public void CalculateNoise()
    {
      if (this.Intensities == null || this.Intensities.Count == 0)
      {
        _noise = 1.0;
        return;
      }

      var intensities = (from m in this.Intensities
                         orderby m.Intensity
                         select m.Intensity).ToList();

      var count = (int)(intensities.Count * SrmOptions.NoisePercentage);
      var min5 = intensities.Take(count).ToList();

      if (min5.Count() == 0)
      {
        _noise = intensities.Average();
        return;
      }

      _noise = Math.Max(min5.Average(), 1.0);
    }

    /// <summary>
    /// 读取/设置噪音水平。读取时，如果之前未设置，则根据检测到的离子强度进行估算。
    /// </summary>
    private double _noise;
    public double Noise
    {
      get
      {
        if (_noise == -1)
        {
          CalculateNoise();
        }
        return _noise;
      }
      set
      {
        _noise = value;
      }
    }

    /// <summary>
    /// 读取/设置信噪比。读取时，如果之前未设置，则根据检测到的离子强度进行估算。
    /// </summary>
    private Double _signalToNoise;
    public double SignalToNoise
    {
      get
      {
        if (_signalToNoise == -1)
        {
          CalculateSignalToNoise();
        }
        return _signalToNoise;
      }
      set
      {
        _signalToNoise = value;
      }
    }

    /// <summary>
    /// 计算选择区域的信噪比。即只有设置为Enabled的离子会用于信噪比计算。
    /// </summary>
    public void CalculateSignalToNoise()
    {
      if (Intensities == null || !Intensities.Any(m => m.Enabled))
      {
        this._signalToNoise = 0;
      }
      else
      {
        this._signalToNoise = (from m in Intensities
                               where m.Enabled
                               select m.Intensity).Max() / Noise;
      }
    }

    /// <summary>
    /// 该transaction包含的检测到的离子。
    /// </summary>
    public List<SrmScan> Intensities { get; set; }

    /// <summary>
    /// 判断两个transaction是否来自同一个precursor。判断依据是母离子质量差在一定范围，同时保留时间差也在一定范围。
    /// </summary>
    /// <param name="other">另一个transaction</param>
    /// <param name="rtToleranceInSecond">保留时间阈值</param>
    /// <param name="precursorMzTolerance">母离子质荷比阈值</param>
    /// <returns>true/false</returns>
    public bool IsBrother(SrmTransition other, double rtToleranceInSecond, double precursorMzTolerance)
    {
      return Math.Abs(PrecursorMZ - other.PrecursorMZ) < precursorMzTolerance && RetentionTimeEqualsTo(other, rtToleranceInSecond);
    }

    /// <summary>
    /// 起始保留时间。
    /// </summary>
    public virtual double FirstRetentionTime
    {
      get
      {
        return this.Intensities[0].RetentionTime;
      }
    }

    /// <summary>
    /// 终止保留时间。
    /// </summary>
    public virtual double LastRetentionTime
    {
      get
      {
        return this.Intensities.Last().RetentionTime;
      }
    }

    /// <summary>
    /// 本transaction与另一个transaction的保留时间是否一致。标准是起始和终止时间的差值都在给定阈值范围内。
    /// </summary>
    /// <param name="other">另一个transaction</param>
    /// <param name="rtToleranceInSecond">保留时间阈值</param>
    /// <returns>true/false</returns>
    public bool RetentionTimeEqualsTo(SrmTransition other, double rtToleranceInSecond)
    {
      var startGap = Math.Abs(this.Intensities[0].RetentionTime - other.Intensities[0].RetentionTime);
      var endGap = Math.Abs(this.Intensities.Last().RetentionTime - other.Intensities.Last().RetentionTime);

      return (startGap <= rtToleranceInSecond) && (endGap <= rtToleranceInSecond);
    }

    /// <summary>
    /// 用savitzky golay 5点平滑方法进行处理。
    /// </summary>
    public void Smoothing()
    {
      var intensities = (from s in Intensities
                         select s.Intensity).ToArray();

      var smoothed = MathUtils.SavitzkyGolay5Point(intensities);
      for (int i = 0; i < Intensities.Count; i++)
      {
        Intensities[i].Intensity = smoothed[i];
      }
    }

    public override bool Equals(object obj)
    {
      if (!(obj is SrmTransition))
      {
        return false;
      }

      var other = obj as SrmTransition;

      return this._transitionMZ.Equals(other._transitionMZ);
    }

    public override int GetHashCode()
    {
      return this._transitionHashCode;
    }

    /// <summary>
    /// 获取设置为Enabled的离子的保留时间列表。
    /// </summary>
    /// <returns>保留时间列表</returns>
    public List<double> GetEnabledRetentionTime()
    {
      return (from scan in this.Intensities
              where scan.Enabled
              orderby scan.RetentionTime
              select scan.RetentionTime).ToList();
    }

    /// <summary>
    /// 获取设置为Enabled的离子的丰度列表
    /// </summary>
    /// <param name="deductValue">去除值，一般为noise，每个peak的丰度减去该值后作为返回值进行后继分析。</param>
    /// <returns>离子的丰度列表</returns>
    public List<double> GetEnabledIntensities(double deductValue)
    {
      return (from scan in this.Intensities
              where scan.Enabled
              orderby scan.RetentionTime
              select scan.Intensity - deductValue).ToList();
    }

    /// <summary>
    /// 保留包含最高丰度离子的孤岛。
    /// </summary>
    public void KeepHighestContig()
    {
      //从最高峰开始判断区间。
      var maxIntensity = this.Intensities.Max(m => m.Intensity);
      var maxIntensityIndex = this.Intensities.FindIndex(m => m.Intensity == maxIntensity);

      bool bEnabled = true;
      for (int iScan = maxIntensityIndex - 1; iScan >= 0; iScan--)
      {
        if (!bEnabled)
        {
          this.Intensities[iScan].Enabled = false;
        }
        else
        {
          if (!this.Intensities[iScan].Enabled)
          {
            bEnabled = false;
          }
        }
      }

      bEnabled = true;
      for (int iScan = maxIntensityIndex + 1; iScan < this.Intensities.Count; iScan++)
      {
        if (!bEnabled)
        {
          this.Intensities[iScan].Enabled = false;
        }
        else
        {
          if (!this.Intensities[iScan].Enabled)
          {
            bEnabled = false;
          }
        }
      }
    }

    public string GetInformation()
    {
      var sb = new StringBuilder();
      if (!string.IsNullOrEmpty(this.ObjectName))
      {
        sb.Append(this.ObjectName).Append("-");
      }

      if (!string.IsNullOrEmpty(this.PrecursorFormula) && !this.PrecursorFormula.Equals(this.ObjectName))
      {
        sb.Append(this.PrecursorFormula).Append("-");
      }

      sb.AppendFormat("{0:0.0000}-{1:0.0000}", PrecursorMZ, ProductIon);

      return sb.ToString();
    }

    public override string ToString()
    {
      return this._transitionMZ;
    }

    public bool MzMatch(SrmTransition mrm, double mzTolerance)
    {
      return Math.Abs(this.PrecursorMZ - mrm.PrecursorMZ) < mzTolerance
        && Math.Abs(this.ProductIon - mrm.ProductIon) < mzTolerance;
    }

    public bool ActualRtMatch(SrmTransition mrm, double rtTolerance)
    {
      return Math.Abs(this.ActualCenterRetentionTime - mrm.ActualCenterRetentionTime) < rtTolerance;
    }

    public void CopyData(SrmTransition mrm)
    {
      if (this == mrm)
      {
        return;
      }

      this.Intensities.Clear();
      this.Intensities.AddRange(mrm.Intensities);
      this.Noise = mrm.Noise;
      this.SignalToNoise = mrm.SignalToNoise;
    }

    public double EnabledRetentionWindow
    {
      get
      {
        var times = GetEnabledRetentionTime();
        if (times.Count == 0)
        {
          return 0;
        }
        return times.Last() - times.First();
      }
    }
  }

  public static class SrmTransitionExtension
  {
    public static SrmPairedResult ToPairedResult(this List<SrmTransition> transList)
    {
      SrmPairedResult result = new SrmPairedResult();

      //所有母离子有相同名称、相同质荷比认为是来自相同precursor，合并为一个peptide。
      var pepgroup = transList.GroupBy(m => m.PrecursorFormula + MyConvert.Format("{0:0.0000}", m.PrecursorMZ));
      var peptides = new List<SrmPeptideItem>();
      foreach (var g in pepgroup)
      {
        var pep = new SrmPeptideItem();
        pep.PrecursorMZ = g.First().PrecursorMZ;
        pep.ProductIons = new List<SrmTransition>(g);
        peptides.Add(pep);
      }

      //具有相同母离子名称和电荷的transition认为是轻重标。
      var lhgroup = peptides.GroupBy(m => m.ProductIons[0].PrecursorFormula + "_" + m.ProductIons[0].PrecursorCharge.ToString()).ToList();
      //      var lhgroup = peptides.GroupBy(m => m.ProductIons[0].PrecursorFormula + "_" + m.ProductIons[0].PrecursorCharge.ToString()).ToList();
      foreach (var g in lhgroup)
      {
        if (g.Count() > 2)
        {
          throw new Exception(string.Format("There are {0} transition with same precursor formula and precursor charge: {1}, {2}", g.Count(), g.First().ProductIons[0].PrecursorFormula, g.First().ProductIons[0].PrecursorCharge));
        }
        else if (g.Count() > 1)
        {

          var items = g.ToList();
          items.Sort((m1, m2) => m1.PrecursorMZ.CompareTo(m2.PrecursorMZ));
          SrmPairedPeptideItem light = new SrmPairedPeptideItem(items[0]);
          SrmPairedPeptideItem heavy = new SrmPairedPeptideItem(items[1]);

          if (!string.IsNullOrEmpty(light.ProductIonPairs[0].Light.Ion))
          {
            light.SetHeavyPeptideItemByIon(heavy);
          }
          else
          {
            light.SetHeavyPeptideItem(heavy);
          }
          result.Add(light);
        }
        else
        {
          SrmPairedPeptideItem light = new SrmPairedPeptideItem(g.First());
          result.Add(light);
        }
      }

      return result;
    }
  }
}
