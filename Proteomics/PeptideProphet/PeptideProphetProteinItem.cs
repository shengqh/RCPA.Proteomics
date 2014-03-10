using System;
using System.Collections.Generic;

namespace RCPA.Proteomics.PeptideProphet
{
  public class SequestPeptideProphetProteinItem : IComparable<SequestPeptideProphetProteinItem>
  {
    private readonly List<SequestPeptideProphetItem> peptides = new List<SequestPeptideProphetItem>();
    public string Name { get; set; }

    public List<SequestPeptideProphetItem> Peptides
    {
      get { return this.peptides; }
    }

    #region IComparable<SequestPeptideProphetProteinItem> Members

    public int CompareTo(SequestPeptideProphetProteinItem other)
    {
      if (this.peptides.Count > other.peptides.Count)
      {
        return -1;
      }
      else if (this.peptides.Count < other.peptides.Count)
      {
        return 1;
      }
      else
      {
        return 0;
      }
    }

    #endregion
  }

  public class MascotPeptideProphetProteinItem : IComparable<MascotPeptideProphetProteinItem>
  {
    private readonly List<MascotPeptideProphetItem> peptides = new List<MascotPeptideProphetItem>();
    public string Name { get; set; }

    public List<MascotPeptideProphetItem> Peptides
    {
      get { return this.peptides; }
    }

    #region IComparable<MascotPeptideProphetProteinItem> Members

    public int CompareTo(MascotPeptideProphetProteinItem other)
    {
      if (this.peptides.Count > other.peptides.Count)
      {
        return -1;
      }
      else if (this.peptides.Count < other.peptides.Count)
      {
        return 1;
      }
      else
      {
        return 0;
      }
    }

    #endregion
  }
}