using System.Collections.Generic;
using System.Text;

namespace RCPA.Proteomics.Spectrum
{
  public class Peak : IPeak
  {
    private Dictionary<string, object> annotations;

    public Peak()
      : this(0.0, 0.0, 0)
    { }

    public Peak(Peak p)
    {
      this.Mz = p.Mz;
      this.Intensity = p.Intensity;
      this.Charge = p.Charge;
      if (null != p.annotations)
      {
        this.annotations = new Dictionary<string, object>(p.annotations);
      }
    }

    public Peak(double aMz, double aIntensity)
      : this(aMz, aIntensity, 0)
    { }

    public Peak(double aMz, double aIntensity, int aCharge)
    {
      this.Mz = aMz;
      this.Intensity = aIntensity;
      this.Charge = aCharge;
    }

    public double Mz { get; set; }

    public double Intensity { get; set; }

    public double Noise { get; set; }

    public int Charge { get; set; }

    public int Tag { get; set; }

    public int CombinedCount { get; set; }

    public double GetSignalToNose()
    {
      if (Noise == 0.0)
      {
        return 1000;
      }
      else
      {
        return Intensity / Noise;
      }
    }

    public Dictionary<string, object> Annotations
    {
      get
      {
        if (null == this.annotations)
        {
          this.annotations = new Dictionary<string, object>();
        }
        return this.annotations;
      }
    }

    public override string ToString()
    {
      var sb = new StringBuilder();
      sb.Append(MyConvert.Format("[{0:0.0000}, {1:0.0000}, {2}]", this.Mz, this.Intensity, this.Charge));
      if (null != this.annotations)
      {
        foreach (string annotation in this.annotations.Keys)
        {
          sb.Append("\t[" + annotation + "=" + this.annotations[annotation] + "]");
        }
      }
      return sb.ToString();
    }
  }

  public class PeakMzAscendingComparer<T> : IComparer<T> where T : IPeak
  {
    #region IComparer<T> Members

    public int Compare(T x, T y)
    {
      return x.Mz.CompareTo(y.Mz);
    }

    #endregion
  }

  public class PeakIntensityDescendingComparer<T> : IComparer<T> where T : IPeak
  {
    #region IComparer<T> Members

    public int Compare(T x, T y)
    {
      return -x.Intensity.CompareTo(y.Intensity);
    }

    #endregion
  }
}