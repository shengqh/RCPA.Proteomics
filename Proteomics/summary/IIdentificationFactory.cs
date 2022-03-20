namespace RCPA.Proteomics.Summary
{
  public interface IIdentificationFactory
  {
    IIdentifiedSpectrum AllocateSpectrum();

    IIdentifiedProtein AllocateProtein();

    IIdentifiedProteinGroup AllocateProteinGroup();

    IIdentifiedResult AllocateResult();
  }
}
