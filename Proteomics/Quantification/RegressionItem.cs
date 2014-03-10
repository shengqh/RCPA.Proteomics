namespace RCPA.Proteomics.Quantification
{
  public class SpeciesRegressionItem
  {
    public SpeciesRegressionItem()
    {
    }

    public SpeciesRegressionItem(double mz, double observedIntensity, double regressionIntensity)
    {
      Mz = mz;
      ObservedIntensity = observedIntensity;
      RegressionIntensity = regressionIntensity;
    }

    public double Mz { get; set; }

    public double ObservedIntensity { get; set; }

    public double RegressionIntensity { get; set; }
  }

  public class SilacRegressionItem
  {
    public SilacRegressionItem()
    {
    }

    public SilacRegressionItem(int scan, double sampleIntensity, double referenceIntensity)
    {
      Scan = scan;
      SampleIntensity = sampleIntensity;
      ReferenceIntensity = referenceIntensity;
    }

    public int Scan { get; set; }

    public double SampleIntensity { get; set; }

    public double ReferenceIntensity { get; set; }
  }
}