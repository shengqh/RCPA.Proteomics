namespace RCPA.Proteomics.Summary
{
  public class IdentifiedProteinGroupSingleWonderPeptideCountFilter : IIdentifiedProteinGroupFilter
  {
    private int peptideCount;

    public IdentifiedProteinGroupSingleWonderPeptideCountFilter(int peptideCount)
    {
      this.peptideCount = peptideCount;
    }

    #region IFilter<IIdentifiedProteinGroup> Members

    public bool Accept(IIdentifiedProteinGroup t)
    {
      return t[0].UniquePeptideCount > 1 || t[0].Peptides.Count >= peptideCount;
    }

    #endregion

    #region IIdentifiedProteinGroupFilter Members

    public string FilterCondition
    {
      get { return "UniquePeptideCount > 1 || PeptideCount >= " + peptideCount; }
    }

    #endregion
  }

  public class IdentifiedProteinSingleWonderPeptideCountFilter : IFilter<IIdentifiedProtein>
  {
    private int peptideCount;

    public IdentifiedProteinSingleWonderPeptideCountFilter(int peptideCount)
    {
      this.peptideCount = peptideCount;
    }

    #region IFilter<IIdentifiedProtein> Members

    public bool Accept(IIdentifiedProtein t)
    {
      return t.UniquePeptideCount > 1 || t.Peptides.Count >= peptideCount;
    }

    #endregion
  }
}
