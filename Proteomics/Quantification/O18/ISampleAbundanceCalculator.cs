namespace RCPA.Proteomics.Quantification.O18
{
  public interface ISampleAbundanceCalculator
  {
    SampleAbundanceInfo Calculate(SpeciesAbundanceInfo speciesAbundance);
  }
}