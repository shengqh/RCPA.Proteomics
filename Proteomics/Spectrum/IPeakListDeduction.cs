namespace RCPA.Proteomics.Spectrum
{
  public interface IPeakListDeduction
  {
    void Deduct<T>(PeakList<T> pkl) where T : Peak;
  }
}
