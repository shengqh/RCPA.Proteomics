namespace RCPA.Proteomics.Summary
{
  public class IdentificationFactory : IIdentificationFactory
  {
    #region IIdentificationFactory Members

    public virtual IIdentifiedSpectrum AllocateSpectrum()
    {
      return new IdentifiedSpectrum();
    }

    public virtual IIdentifiedProtein AllocateProtein()
    {
      return new IdentifiedProtein();
    }

    public virtual IIdentifiedProteinGroup AllocateProteinGroup()
    {
      return new IdentifiedProteinGroup();
    }

    public virtual IIdentifiedResult AllocateResult()
    {
      return new IdentifiedResult();
    }

    #endregion
  }
}
