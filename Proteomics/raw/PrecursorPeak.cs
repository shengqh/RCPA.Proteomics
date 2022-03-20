using RCPA.Proteomics.Spectrum;

namespace RCPA.Proteomics.Raw
{
  public class PrecursorPeak
  {
    public int MasterScan { get; set; }

    public double MonoIsotopicMass { get; set; }

    public double IsolationWidth { get; set; }

    public double IsolationMass { get; set; }

    public int Charge { get; set; }

    public double Intensity { get; set; }

    public PrecursorPeak()
    { }

    public PrecursorPeak(Peak source)
    {
      MonoIsotopicMass = source.Mz;
      IsolationMass = source.Mz;
      Charge = source.Charge;
      Intensity = source.Intensity;
    }

    public PrecursorPeak(PrecursorPeak source)
    {
      MonoIsotopicMass = source.MonoIsotopicMass;
      IsolationMass = source.IsolationMass;
      IsolationWidth = source.IsolationWidth;
      Charge = source.Charge;
      Intensity = source.Intensity;
      MasterScan = source.MasterScan;
    }
  }
}
