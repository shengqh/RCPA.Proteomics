namespace RCPA.Proteomics.Quantification.ITraq
{
  public interface IITraqNormalizationBuilder
  {
    bool ByIonInjectionTime { get; set; }

    void Normalize(IsobaricResult result);
  }
}
