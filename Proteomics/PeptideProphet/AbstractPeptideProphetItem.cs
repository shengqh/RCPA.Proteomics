using RCPA.Proteomics.Summary;
using System;

namespace RCPA.Proteomics.PeptideProphet
{
  public abstract class AbstractPeptideProphetItem<T> : IComparable<AbstractPeptideProphetItem<T>>,
                                                        IPeptideProphetItem<T> where T : IIdentifiedSpectrum
  {
    private double peptideProphetProbability;

    #region IComparable<AbstractPeptideProphetItem<T>> Members

    public int CompareTo(AbstractPeptideProphetItem<T> other)
    {
      if (this.peptideProphetProbability > other.peptideProphetProbability)
      {
        return -1;
      }

      if (this.peptideProphetProbability < other.peptideProphetProbability)
      {
        return 1;
      }

      return 0;
    }

    #endregion

    #region IPeptideProphetItem<T> Members

    public double PeptideProphetProbability
    {
      get { return this.peptideProphetProbability; }
      set { this.peptideProphetProbability = value; }
    }

    public int NumTotalProteins { get; set; }

    public T SearchResult
    {
      get { return DoGetSearchResult(); }
    }

    #endregion

    protected abstract T DoGetSearchResult();
  }
}