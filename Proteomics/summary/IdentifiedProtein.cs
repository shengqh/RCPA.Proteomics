using RCPA.Proteomics.Sequest;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace RCPA.Proteomics.Summary
{
  public class IdentifiedProtein : IIdentifiedProtein, IComparable<IdentifiedProtein>
  {
    private static Regex refReg = new Regex(@"(\S+)\s+(.+)");

    public IdentifiedProtein() { }

    public IdentifiedProtein(string name)
    {
      this.Name = name;
    }

    #region IIdentifiedProtein Members

    //public string AccessNumber { get; set; }

    public string Name { get; set; }

    public bool FromDecoy { get; set; }

    private string description = "";
    public string Description
    {
      get { return description; }
      set
      {
        if (null == value)
        {
          description = "";
        }
        else
        {
          description = value.Trim();
        }
      }
    }

    public string Reference
    {
      get
      {
        if (this.Description.Length > 0)
        {
          return this.Name + " " + this.Description;
        }
        else
        {
          return this.Name;
        }
      }
      set
      {
        Match m = refReg.Match(value);
        if (!m.Success)
        {
          this.Name = value;
          this.Description = "";
        }
        else
        {
          this.Name = m.Groups[1].Value.Trim();
          this.Description = m.Groups[2].Value.Trim();
        }
      }
    }

    public string Sequence { get; set; }

    public double MolecularWeight { get; set; }

    public double IsoelectricPoint { get; set; }

    public double Coverage { get; set; }

    private Dictionary<string, object> annotations;
    public Dictionary<string, object> Annotations
    {
      get
      {
        if (null == annotations)
        {
          annotations = new Dictionary<string, object>();
        }

        return annotations;
      }
    }

    public int GroupIndex { get; set; }

    public double Score { get; set; }

    public List<IIdentifiedSpectrum> GetSpectra()
    {
      var result = (from p in peptides select p.Spectrum).Distinct().ToList();
      result.TrimExcess();
      return result;
    }

    private List<IIdentifiedPeptide> peptides;
    public List<IIdentifiedPeptide> Peptides
    {
      get
      {
        if (null == peptides)
        {
          this.peptides = new List<IIdentifiedPeptide>();
        }
        return this.peptides;
      }
      set
      {
        if (null == value)
        {
          this.peptides = new List<IIdentifiedPeptide>();
        }
        else
        {
          this.peptides = value;
        }
      }
    }

    public class IIdentifiedSpectrumComparer : IEqualityComparer<IIdentifiedPeptide>
    {
      #region IEqualityComparer<IIdentifiedPeptide> Members

      public bool Equals(IIdentifiedPeptide x, IIdentifiedPeptide y)
      {
        return x.Spectrum == y.Spectrum;
      }

      public int GetHashCode(IIdentifiedPeptide obj)
      {
        return obj.Spectrum.GetHashCode();
      }
      #endregion
    }

    public IEnumerable<IIdentifiedPeptide> GetDistinctPeptides()
    {
      return new HashSet<IIdentifiedPeptide>(peptides, new IIdentifiedSpectrumComparer());
    }

    public IIdentifiedPeptide FindPeptideByQuery(int query)
    {
      return Peptides.Find(p => p.Spectrum.Query.QueryId == query);
    }

    public void InitUniquePeptideCount()
    {
      var peps = this.GetDistinctPeptides();

      DoInitUniquePeptideCount(peps);
    }

    public void InitUniquePeptideCount(Func<IIdentifiedPeptide, bool> func)
    {
      var peps = this.GetDistinctPeptides().Where(func).ToList();

      DoInitUniquePeptideCount(peps);
    }

    private void DoInitUniquePeptideCount(IEnumerable<IIdentifiedPeptide> peps)
    {
      this.uniquePeptideCount = IdentifiedPeptideUtils.GetUniquePeptideCount(peps);
      this.peptideCount = IdentifiedSpectrumUtils.GetSpectrumCount(from p in peps select p.Spectrum);
    }

    private int uniquePeptideCount;

    /// <summary>
    /// Gets and Sets Unique Peptide Count
    /// </summary>
    public int UniquePeptideCount
    {
      get
      {
        if (0 == uniquePeptideCount && this.peptides.Count > 0)
        {
          InitUniquePeptideCount();
        }
        return uniquePeptideCount;
      }
      set
      {
        uniquePeptideCount = value;
      }
    }

    private int peptideCount;
    public int PeptideCount
    {
      get
      {
        if (0 == uniquePeptideCount && this.peptides.Count > 0)
        {
          InitUniquePeptideCount();
        }
        return peptideCount;
      }
      set
      {
        peptideCount = value;
      }
    }

    public void SortPeptides()
    {
      this.Peptides.Sort();
    }

    #endregion

    public double GetScoreSum()
    {
      double result = 0;
      foreach (IIdentifiedSpectrum mphit in Peptides)
      {
        result += mphit.Score;
      }
      return result;
    }

    #region IComparable<IIdentifiedProtein> Members

    public int CompareTo(IIdentifiedProtein other)
    {
      int result = other.UniquePeptideCount.CompareTo(this.UniquePeptideCount);

      if (result != 0)
      {
        return result;
      }

      result = other.Peptides.Count.CompareTo(this.Peptides.Count);

      if (result != 0)
      {
        return result;
      }

      return this.Name.CompareTo(other.Name);
    }

    #endregion

    #region IComparable<IdentifiedProtein> Members

    public int CompareTo(IdentifiedProtein other)
    {
      return CompareTo(other as IIdentifiedProtein);
    }

    #endregion

    #region IIdentifiedProtein Members

    public void CalculateCoverage()
    {
      Coverage = 0.0;
      if (string.IsNullOrEmpty(Sequence))
      {
        return;
      }

      bool[] cov = new bool[Sequence.Length];
      for (int i = 0; i < cov.Length; i++)
      {
        cov[i] = false;
      }

      HashSet<string> seqs = new HashSet<string>();
      foreach (IIdentifiedPeptide pep in this.peptides)
      {
        seqs.Add(pep.PureSequence.ToUpper().Replace("I", "L"));
      }

      var seq = Sequence.Replace("I", "L");
      foreach (string pureSeq in seqs)
      {
        int ipos = seq.IndexOf(pureSeq);

        if (ipos == -1)
        {
          foreach (IIdentifiedPeptide pep in this.peptides)
          {
            if (pep.PureSequence.Replace("I", "L").Equals(pureSeq))
            {
              throw new Exception(MyConvert.Format("Cannot find peptide in protein {0} : {1}", this.Name, new SequestPeptideTextFormat().PeptideFormat.GetString(pep.Spectrum)));
            }
          }
        }

        for (int i = ipos; i < ipos + pureSeq.Length; i++)
        {
          cov[i] = true;
        }
      }

      int count = 0;
      for (int i = 0; i < cov.Length; i++)
      {
        if (cov[i])
        {
          count++;
        }
      }

      Coverage = (count * 100.0) / Sequence.Length;
    }

    #endregion

    #region ICloneable Members

    /// <summary>
    /// 浅复制，对于其包含的peptides不进行复制，对annotations进行复制。
    /// </summary>
    /// <returns></returns>
    public object Clone()
    {
      IdentifiedProtein result = (IdentifiedProtein)this.MemberwiseClone();

      result.peptides = new List<IIdentifiedPeptide>(this.peptides);

      if (this.annotations != null)
      {
        result.annotations = new Dictionary<string, object>(this.annotations);
      }

      return result;
    }

    #endregion

    public override string ToString()
    {
      return Name;
    }
  }
}
