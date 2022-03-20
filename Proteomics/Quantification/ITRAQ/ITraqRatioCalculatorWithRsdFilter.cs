using MathNet.Numerics.Distributions;
using System;

namespace RCPA.Proteomics.Quantification.ITraq
{
  public class ITraqRatioCalculatorWithRsdFilter : IITraqRatioCalculator
  {
    protected int sample1, sample2, reference1, reference2;

    private double maxRsdInGroup;

    protected Func<IsobaricItem, bool> Validator { get; set; }

    public ITraqRatioCalculatorWithRsdFilter(int sample1, int sample2, int reference1, int reference2, double maxRsdInGroup)
    {
      this.sample1 = sample1;
      this.sample2 = sample2;
      this.reference1 = reference1;
      this.reference2 = reference2;
      this.maxRsdInGroup = maxRsdInGroup;

      this.Validator = (item =>
      {
        if (item[sample1] == ITraqConsts.NULL_INTENSITY || item[sample2] == ITraqConsts.NULL_INTENSITY || item[reference1] == ITraqConsts.NULL_INTENSITY || item[reference2] == ITraqConsts.NULL_INTENSITY)
        {
          return false;
        }

        return GetRsd(item, sample1, sample2) <= maxRsdInGroup && GetRsd(item, reference1, reference2) <= maxRsdInGroup;
      });
    }

    private double GetRsd(IsobaricItem item, int index1, int index2)
    {
      var acc = new MeanStandardDeviation(new double[] { item[index1], item[index2] });
      return acc.StdDev / acc.Mean;
    }

    #region IITraqRatioCalculator Members

    public string GetRatioHeader()
    {
      return MyConvert.Format("({0}+{1})/({2}+{3})", sample1, sample2, reference1, reference2);
      throw new NotImplementedException();
    }

    public string GetRatioValue(IsobaricItem item)
    {
      return MyConvert.Format("{0:0.00}", (item[sample1] + item[sample2]) / (item[reference1] + item[reference2]));
    }

    public bool Valid(IsobaricItem item)
    {
      if (null != Validator)
      {
        return Validator(item);
      }

      return true;
    }

    #endregion
  }
}
