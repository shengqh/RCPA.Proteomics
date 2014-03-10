namespace RCPA.Proteomics
{
  public interface IAtomCompositionCalculator
  {
    AtomComposition GetAtomComposition(IPeptideInfo compoundInfo);
  }

  public class PeptideAtomCompositionCalculator : IAtomCompositionCalculator
  {
    private readonly Aminoacids aas;
    private readonly AtomComposition cTerm;
    private readonly AtomComposition nTerm;

    public PeptideAtomCompositionCalculator(AtomComposition nTerm, AtomComposition cTerm, Aminoacids aas)
    {
      this.nTerm = nTerm;
      this.cTerm = cTerm;
      this.aas = aas;
    }

    #region IAtomCompositionCalculator Members

    public AtomComposition GetAtomComposition(IPeptideInfo compoundInfo)
    {
      var result = new AtomComposition("");
      result.Add(this.aas.GetPeptideAtomComposition(compoundInfo.Sequence, ""));
      result.Add(this.nTerm);
      result.Add(this.cTerm);
      return result;
    }

    #endregion
  }
}