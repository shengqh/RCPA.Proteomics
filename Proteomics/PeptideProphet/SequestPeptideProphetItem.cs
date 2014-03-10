using System;
using RCPA.Proteomics.Summary;

namespace RCPA.Proteomics.PeptideProphet
{
  public class SequestPeptideProphetItem : AbstractPeptideProphetItem<IdentifiedSpectrum>,
                                           IComparable<SequestPeptideProphetItem>
  {
    private readonly IdentifiedSpectrum searchResult = new IdentifiedSpectrum();

    #region IComparable<SequestPeptideProphetItem> Members

    public int CompareTo(SequestPeptideProphetItem other)
    {
      return base.CompareTo(other);
    }

    #endregion

    protected override IdentifiedSpectrum DoGetSearchResult()
    {
      return this.searchResult;
    }
  }
}