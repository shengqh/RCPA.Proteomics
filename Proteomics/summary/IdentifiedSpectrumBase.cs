using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text.RegularExpressions;
using RCPA.Proteomics.Utils;
using RCPA.Utils;
using System.Text;
using RCPA.Seq;

namespace RCPA.Proteomics.Summary
{
  public class IdentifiedSpectrumBase : IIdentifiedSpectrumBase
  {
    private readonly List<IIdentifiedPeptide> peptides = new List<IIdentifiedPeptide>();

    public IdentifiedSpectrumBase()
    { }

    #region IIdentifiedSpectrum Members

    public ReadOnlyCollection<IIdentifiedPeptide> Peptides
    {
      get { return this.peptides.AsReadOnly(); }
    }

    public void AddPeptide(IIdentifiedPeptide peptide)
    {
      if (!this.peptides.Contains(peptide))
      {
        this.peptides.Add(peptide);
        peptide.SpectrumBase = this;
      }
    }

    public void AddPeptides(IEnumerable<IIdentifiedPeptide> values)
    {
      List<IIdentifiedPeptide> peps = new List<IIdentifiedPeptide>(values);
      foreach (IIdentifiedPeptide peptide in peps)
      {
        AddPeptide(peptide);
      }
    }

    public void AssignPeptides(IEnumerable<IIdentifiedPeptide> values)
    {
      ClearPeptides();

      AddPeptides(values);
    }

    public void RemovePeptideAt(int index)
    {
      IIdentifiedPeptide peptide = this.peptides[index];

      this.peptides.RemoveAt(index);

      peptide.Spectrum = null;
    }

    public void RemovePeptide(IIdentifiedPeptide peptide)
    {
      int index = this.peptides.IndexOf(peptide);
      if (index >= 0)
      {
        RemovePeptideAt(index);
      }
    }

    public void ClearPeptides()
    {
      for (int i = this.peptides.Count - 1; i >= 0; i--)
      {
        RemovePeptideAt(i);
      }
    }

    public string Sequence
    {
      get
      {
        if (peptides.Count > 0) { return this.peptides[0].Sequence; }
        else { return string.Empty; }
      }
    }

    public string SummaryLine { get; set; }

    public virtual IIdentifiedPeptide NewPeptide()
    {
      return new IdentifiedPeptide(this);
    }

    #endregion

    #region IComparable Members

    public int CompareTo(object obj)
    {
      if (null == obj)
      {
        return 1;
      }

      if (!(obj is IIdentifiedSpectrumBase))
      {
        throw new ArgumentException(MyConvert.Format("Parameter obj is not IIdentifiedSpectrumBase, it's {0}.", obj.GetType()));
      }

      IIdentifiedSpectrumBase other = obj as IIdentifiedSpectrumBase;
      return Sequence.CompareTo(other.Sequence);
    }

    #endregion

    public void SortPeptides()
    {
      this.peptides.Sort((m1, m2) => PeptideUtils.GetMatchedSequence(m1.Sequence).CompareTo(PeptideUtils.GetMatchedSequence(m2.Sequence)));
    }
  }
}