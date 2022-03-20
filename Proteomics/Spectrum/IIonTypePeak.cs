namespace RCPA.Proteomics.Spectrum
{
  public interface IIonTypePeak : IPeak
  {
    IonType PeakType { get; set; }

    int PeakIndex { get; set; }
  }
}
