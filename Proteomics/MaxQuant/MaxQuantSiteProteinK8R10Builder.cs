namespace RCPA.Proteomics.MaxQuant
{
  public class MaxQuantSiteProteinK8R10Builder : AbstractMaxQuantSiteProteinBuilder
  {
    public MaxQuantSiteProteinK8R10Builder(MaxQuantSiteProteinBuilderOption option) : base(option)
    { }

    protected override void InitializeAminoacids(out Aminoacids samAminoacids, out Aminoacids refAminoacids)
    {
      samAminoacids = new Aminoacids();
      refAminoacids = new Aminoacids();
      refAminoacids['R'].CompositionStr = "Cx6H12ONx4";
      refAminoacids['K'].CompositionStr = "Cx6H12ONx2";
    }
  }
}
