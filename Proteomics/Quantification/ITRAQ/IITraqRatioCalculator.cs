namespace RCPA.Proteomics.Quantification.ITraq
{
  public interface IITraqRatioCalculator
  {
    string GetRatioHeader();

    string GetRatioValue(IsobaricItem item);

    bool Valid(IsobaricItem item);
  }
}
