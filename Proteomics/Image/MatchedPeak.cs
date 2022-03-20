using RCPA.Proteomics.Spectrum;
using System;

namespace RCPA.Proteomics.Image
{
  public class MatchedPeak : IonTypePeak, IComparable<MatchedPeak>
  {
    public MatchedPeak()
      : base()
    {
      PeakType = IonType.UNKNOWN;
    }

    public MatchedPeak(double mz, double intensity, int charge)
      : base(mz, intensity, charge)
    {
      PeakType = IonType.UNKNOWN;
    }

    public string DisplayName { get; set; }

    public bool Matched { get; set; }

    public double MatchedMZ { get; set; }

    public override string ToString()
    {
      return Information;
    }

    public string Information
    {
      get
      {
        string chargeString = "";
        if (Charge > 1)
        {
          chargeString = StringUtils.RepeatChar('+', Charge);
        }

        if (null != DisplayName && 0 != DisplayName.Length)
        {
          return MyConvert.Format("{0}{1}   {2:0.0000}", DisplayName, chargeString, Mz);
        }

        if (IonType.PRECURSOR == this.PeakType)
        {
          return MyConvert.Format("[MH{0}]{1}   {2:0.0000}", Charge > 1 ? Charge.ToString() : "", chargeString, Mz);
        }

        string name = this.PeakType.ToString().Substring(0, 1).ToLower();
        return MyConvert.Format("{0}{1}{2}   {3:0.0000}", name, PeakIndex, chargeString, Mz);
      }
    }

    #region IComparable<MatchedPeak> Members

    public int CompareTo(MatchedPeak other)
    {
      return this.Mz.CompareTo(other.Mz);
    }

    #endregion
  }
}
