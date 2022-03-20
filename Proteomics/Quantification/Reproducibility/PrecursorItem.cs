using System;

namespace RCPA.Proteomics.Quantification.Reproducibility
{
  public class PrecursorItem : IComparable<PrecursorItem>
  {
    public double Mz { get; set; }
    public double Abundance { get; set; }
    public double RetentionTime { get; set; }
    public bool Matched { get; set; }

    public override string ToString()
    {
      return MyConvert.Format("mz={0:0.0000}, rt={1:0.00}, abundance={2:0.0}", Mz, RetentionTime, Abundance);
    }

    #region IComparable<PrecursorItem> Members

    public int CompareTo(PrecursorItem other)
    {
      return this.Mz.CompareTo(other.Mz);
    }

    #endregion
  }
}
