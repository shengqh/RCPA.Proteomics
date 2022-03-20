using RCPA.Proteomics.Spectrum;
using System;

namespace RCPA.Proteomics.Snp
{
  public class MS3Peak : Peak
  {
    public MS3Peak() { }

    public MS3Peak(Peak source) : base(source) { }

    /// <summary>
    /// Minimim matched ion mz when ppm tolerance fixed
    /// </summary>
    public double MinMatchMz { get; set; }

    /// <summary>
    /// Maximim matched ion mz when ppm tolerance fixed
    /// </summary>
    public double MaxMatchMz { get; set; }
  }

  public class MS3Item : PeakList<MS3Peak>
  {
    public MS3Item() { }

    public MS3Item(PeakList<MS3Peak> source) : base(source) { }

    public MS3Item(PeakList<Peak> source)
    {
      AssignInforamtion(source);
      foreach (var p in source)
      {
        Add(new MS3Peak(p));
      }
      foreach (String key in source.Annotations.Keys)
      {
        this.Annotations.Add(key, source.Annotations[key]);
      }
    }

    /// <summary>
    /// Minimim matched precursor mz when ppm tolerance fixed
    /// </summary>
    public double MinPrecursorMz { get; set; }

    /// <summary>
    /// Maximim matched precursor mz when ppm tolerance fixed
    /// </summary>
    public double MaxPrecursorMz { get; set; }
  }
}
