namespace RCPA.Proteomics.Quantification.Srm
{
  public class SrmScan
  {
    public double PrecursorMz { get; set; }

    public double ProductMz { get; set; }

    public double RetentionTime { get; set; }

    public double Intensity { get; set; }

    public bool Enabled { get; set; }

    public SrmScan(double mz, double ion, double rt, double intensity, bool enabled)
    {
      this.PrecursorMz = mz;
      this.ProductMz = ion;
      this.RetentionTime = rt;
      this.Intensity = intensity;
      this.Enabled = enabled;
    }

    public override string ToString()
    {
      return MyConvert.Format("{0:0.0000}-{1:0.0000} : {2:0.0000}, {3:0.0}, {4}", PrecursorMz, ProductMz, RetentionTime, Intensity, Enabled);
    }
  }
}
