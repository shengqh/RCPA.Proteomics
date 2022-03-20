using System;
using System.Text;

namespace RCPA.Proteomics.Quantification.Srm
{
  public class MzItem
  {
    public string ObjectName { get; set; }

    public string PrecursurFormula { get; set; }

    public int PrecursurCharge { get; set; }

    public double LightMz { get; set; }

    public double HeavyMz { get; set; }

    public bool HasName()
    {
      return !string.IsNullOrEmpty(this.ObjectName) && !string.IsNullOrEmpty(this.PrecursurFormula);
    }

    public bool MzEquals(MzItem another, double mzTolerance)
    {
      return (Math.Abs(this.LightMz - another.LightMz) <= mzTolerance) && (Math.Abs(this.HeavyMz - another.HeavyMz) <= mzTolerance);
    }

    public bool MzEquals(Pair<double, double> lh, double mzTolerance)
    {
      return (Math.Abs(this.LightMz - lh.First) <= mzTolerance) && (Math.Abs(this.HeavyMz - lh.Second) <= mzTolerance);
    }

    public bool NameChargeEquals(MzItem another)
    {
      return string.Equals(this.ObjectName, another.ObjectName) && string.Equals(this.PrecursurFormula, another.PrecursurFormula) && this.PrecursurCharge == another.PrecursurCharge;
    }

    public bool NameChargeEquals(SrmFileItem another)
    {
      if (string.IsNullOrEmpty(this.ObjectName) || string.IsNullOrEmpty(this.PrecursurFormula))
      {
        return false;
      }

      return string.Equals(this.ObjectName, another.PairedProductIon.ObjectName) && string.Equals(this.PrecursurFormula, another.PairedPeptide.PrecursorFormula) && (this.PrecursurCharge == another.PairedPeptide.PrecursorCharge);
    }

    public override string ToString()
    {
      StringBuilder sb = new StringBuilder();

      if (!string.IsNullOrEmpty(this.PrecursurFormula))
      {
        sb.Append('[');
        if (!string.IsNullOrEmpty(this.ObjectName))
        {
          sb.AppendFormat("{0}-", this.ObjectName);
        }

        sb.AppendFormat("{0}] - ", this.PrecursurFormula);
      }

      if (this.PrecursurCharge != 0)
      {
        sb.AppendFormat("[{0}] - ", this.PrecursurCharge);
      }

      sb.AppendFormat("{0:0.0000} : {1:0.0000}", this.LightMz, this.HeavyMz);

      return sb.ToString();
    }
  }
}
