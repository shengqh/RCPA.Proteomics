namespace RCPA.Proteomics.Quantification.ITraq
{
  public class ITraqPhosphoRatioCalculator : IITraqRatioCalculator
  {
    protected int phospho1, phospho2, normal1, normal2;

    public ITraqPhosphoRatioCalculator(int phospho1, int phospho2, int normal1, int normal2)
    {
      this.phospho1 = phospho1;
      this.phospho2 = phospho2;
      this.normal1 = normal1;
      this.normal2 = normal2;
    }

    #region IITraqRatioCalculator Members

    public virtual string GetRatioHeader()
    {
      return MyConvert.Format("PhosphoRatio");
    }

    public virtual string GetRatioValue(IsobaricItem item)
    {
      return MyConvert.Format("{0:0.0000}", 1 - (item[phospho1] + item[phospho2]) / (item[normal1] + item[normal2]));
    }

    public bool Valid(IsobaricItem m)
    {
      return m[phospho1] != ITraqConsts.NULL_INTENSITY && m[phospho2] != ITraqConsts.NULL_INTENSITY;
    }

    #endregion
  }
}
