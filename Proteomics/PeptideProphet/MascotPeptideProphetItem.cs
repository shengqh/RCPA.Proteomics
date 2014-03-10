using System;
using RCPA.Proteomics.Summary;

namespace RCPA.Proteomics.PeptideProphet
{
  public class MascotPeptideProphetItem : AbstractPeptideProphetItem<IdentifiedSpectrum>,
                                          IComparable<MascotPeptideProphetItem>
  {
    private readonly IdentifiedSpectrum searchResult = new IdentifiedSpectrum();

    #region IComparable<MascotPeptideProphetItem> Members

    public int CompareTo(MascotPeptideProphetItem other)
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