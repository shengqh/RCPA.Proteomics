using System;
using System.Collections.Generic;
using System.Text;
using RCPA.Proteomics.Summary;
using System.Collections.ObjectModel;

namespace RCPA.Proteomics.Summary
{
  public class IdentifiedProteinGroup : List<IIdentifiedProtein>, IIdentifiedProteinGroup, IComparable<IdentifiedProteinGroup>
  {
    private int _index = 0;
    public int Index
    {
      get
      {
        if (0 == Count)
        {
          return 0;
        }

        if (_index != 0)
        {
          return _index;
        }

        return this[0].GroupIndex;
      }
      set
      {
        _index = value;
        foreach (IIdentifiedProtein mp in this)
        {
          mp.GroupIndex = value;
        }
      }
    }

    public bool Enabled { get; set; }

    public bool Selected { get; set; }

    public double Probability { get; set; }

    public double TotalIonCount { get; set; }

    public double QValue { get; set; }

    public bool FromDecoy { get; set; }

    public void AddIdentifiedSpectrum(IIdentifiedSpectrum pephit)
    {
      foreach (IIdentifiedProtein prohit in this)
      {
        bool bFound = false;
        foreach (IIdentifiedPeptide peptide in pephit.Peptides)
        {
          foreach (string proteinName in peptide.Proteins)
          {
            if (prohit.Name.Contains(proteinName))
            {
              prohit.Peptides.Add(peptide);
              bFound = true;
              break;
            }
          }
        }
        if (bFound)
        {
          continue;
        }

        foreach (IIdentifiedPeptide peptide in pephit.Peptides)
        {
          foreach (string proteinName in peptide.Proteins)
          {
            if (proteinName.Contains(prohit.Name))
            {
              prohit.Peptides.Add(peptide);
              bFound = true;
              break;
            }
          }
        }
        if (bFound)
        {
          continue;
        }

        foreach (var pro in this)
        {
          pro.Peptides.Add(pephit.Peptide);
        }

        //throw new ArgumentException(MyConvert.Format("Cannot find protein {0} in spectrum {1}", prohit.Name, pephit.Query.FileScan.LongFileName));
      }
    }

    public void AddIdentifiedSpectra(IEnumerable<IIdentifiedSpectrum> peptides)
    {
      foreach (IIdentifiedSpectrum peptide in peptides)
      {
        AddIdentifiedSpectrum(peptide);
      }
    }

    public ReadOnlyCollection<IIdentifiedSpectrum> GetPeptides()
    {
      if (0 == Count)
      {
        return new List<IIdentifiedSpectrum>().AsReadOnly();
      }
      else
      {
        return this[0].GetSpectra().AsReadOnly();
      }
    }

    public List<IIdentifiedSpectrum> GetSortedPeptides()
    {
      List<IIdentifiedSpectrum> result = new List<IIdentifiedSpectrum>(GetPeptides());
      result.Sort();
      return result;
    }

    public void InitUniquePeptideCount()
    {
      if (0 == this.Count)
      {
        return;
      }

      this[0].InitUniquePeptideCount();

      for (int i = 1; i < this.Count; i++)
      {
        this[i].UniquePeptideCount = this[0].UniquePeptideCount;
        this[i].PeptideCount = this[0].PeptideCount;
      }
    }

    #region IComparable<IIdentifiedProteinGroup> Members

    public int CompareTo(IIdentifiedProteinGroup other)
    {
      if (this.Count == 0)
      {
        return 1;
      }

      if (other.Count == 0)
      {
        return -1;
      }
      return this[0].CompareTo(other[0]);
    }

    #endregion

    #region IComparable<IdentifiedProteinGroup> Members

    public int CompareTo(IdentifiedProteinGroup other)
    {
      return CompareTo(other as IIdentifiedProteinGroup);
    }

    #endregion


    #region ICloneable Members

    public object Clone()
    {
      IdentifiedProteinGroup result = new IdentifiedProteinGroup();

      this.ForEach(p => result.Add((IIdentifiedProtein)p.Clone()));

      result.Index = this.Index;
      result.Enabled = this.Enabled;

      return result;
    }

    #endregion
  }
}
