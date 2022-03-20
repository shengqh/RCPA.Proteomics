using RCPA.Proteomics.Modification;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace RCPA.Proteomics.Summary
{
  public class IdentifiedPeptide : IIdentifiedPeptide, IComparable<IdentifiedPeptide>
  {
    public IdentifiedPeptide(IIdentifiedSpectrumBase spectrum)
    {
      DoSetSpectrum(spectrum);

      this.SiteProbability = string.Empty;
    }

    public int ConfidenceLevel { get; set; }

    private void DoSetSpectrum(IIdentifiedSpectrumBase spectrum)
    {
      if (this.spectrum == spectrum)
      {
        return;
      }

      if (null != this.spectrum)
      {
        this.spectrum.RemovePeptide(this);
      }

      this.spectrum = spectrum;
      if (spectrum != null)
      {
        spectrum.AddPeptide(this);
      }
    }

    private string RemoveModificationChar(string oldseq, int ibegin, int istop)
    {
      if (ibegin < 0 || istop > oldseq.Length)
      {
        throw new ArgumentOutOfRangeException(
          MyConvert.Format("Argument ibegin ({0}) and istop ({1}) should be in range [{2},{3}]", ibegin, istop, 0, oldseq.Length));
      }

      StringBuilder result = new StringBuilder();

      for (int i = ibegin; i < istop; i++)
      {
        if (oldseq[i] < 'A' || oldseq[i] > 'Z')
          continue;
        result.Append(oldseq[i]);
      }
      return result.ToString();
    }

    private void SetSequence(string value)
    {
      sequence = value;
      pureSequence = null;
      pureILReplacedSequence = null;
    }

    #region IIdentifiedPeptide Members

    private IIdentifiedSpectrumBase spectrum;

    public IIdentifiedSpectrumBase SpectrumBase
    {
      get { return spectrum; }
      set { DoSetSpectrum(value); }
    }

    public IIdentifiedSpectrum Spectrum
    {
      get { return spectrum as IIdentifiedSpectrum; }
      set { DoSetSpectrum(value); }
    }

    private string sequence;

    public string Sequence
    {
      get { return sequence; }
      set { SetSequence(value); }
    }

    private string pureSequence;

    public string PureSequence
    {
      get
      {
        if (pureSequence == null && sequence != null)
        {
          var seq = sequence.ToUpper();
          int ifirstpos = seq.IndexOf('.');
          if (-1 == ifirstpos)
          {
            pureSequence = RemoveModificationChar(seq, 0, seq.Length);
          }
          else
          {

            int ilastpos = seq.LastIndexOf('.');
            if (-1 == ilastpos)
            {
              ilastpos = seq.Length;
            }

            pureSequence = RemoveModificationChar(seq, ifirstpos, ilastpos);
          }
        }
        return pureSequence;
      }
    }

    private string pureILReplacedSequence;

    public string PureILReplacedSequence
    {
      get
      {
        if (pureILReplacedSequence == null && sequence != null)
        {
          pureILReplacedSequence = PureSequence.Replace("I", "L");
        }
        return pureILReplacedSequence;
      }
    }

    private List<string> proteins = new List<string>();

    public void ClearProteins()
    {
      proteins.Clear();
    }

    public void AddProtein(string protein)
    {
      string proteinName = GetValidProteinName(protein);
      if (!proteins.Contains(proteinName))
      {
        proteins.Add(proteinName);
      }
    }

    private static string GetValidProteinName(string protein)
    {
      return protein.Replace('\t', ' ');
    }

    public void AssignProteins(IEnumerable<string> proteins)
    {
      this.proteins.Clear();
      foreach (string protein in proteins)
      {
        AddProtein(protein);
      }
    }

    public void SetProtein(int index, string protein)
    {
      if (index < 0 || index >= proteins.Count)
      {
        throw new ArgumentOutOfRangeException(MyConvert.Format("Argument index should be in range [{0},{1}], actually it is {3}",
          0, proteins.Count - 1, index));
      }
      proteins[index] = GetValidProteinName(protein);
    }

    public void RemoveProteinAt(int index)
    {
      proteins.RemoveAt(index);
    }

    public ReadOnlyCollection<string> Proteins
    {
      get { return proteins.AsReadOnly(); }
    }

    public bool HasProtein(string protein)
    {
      return this.proteins.Contains(protein);
    }

    #endregion

    #region IComparable<IIdentifiedPeptide> Members

    public int CompareTo(IIdentifiedPeptide other)
    {
      if (this.spectrum == null)
      {
        return -1;
      }

      if (other == null || other.Spectrum == null)
      {
        return 1;
      }

      return this.spectrum.CompareTo(other.Spectrum);
    }

    #endregion

    #region IComparable<IdentifiedPeptide> Members

    public int CompareTo(IdentifiedPeptide other)
    {
      return CompareTo(other as IIdentifiedPeptide);
    }

    #endregion

    public override string ToString()
    {
      return this.sequence;
    }

    public bool IsTopOne()
    {
      if (Spectrum == null)
      {
        return true;
      }

      return Spectrum.Peptides.IndexOf(this) == 0;
    }

    public string SiteProbability { get; set; }


    public List<ModificationSiteProbability> GetSiteProbabilities()
    {
      return ModificationUtils.ParseProbability(this.SiteProbability);
    }
  }
}
