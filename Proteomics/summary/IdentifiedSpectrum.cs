using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text.RegularExpressions;
using RCPA.Proteomics.Utils;
using RCPA.Utils;
using System.Text;

namespace RCPA.Proteomics.Summary
{
  public class IdentifiedSpectrum : IdentifiedSpectrumBase, IIdentifiedSpectrum, IComparable<IdentifiedSpectrum>
  {
    public static readonly string MODIFICATION_KEY = "MODIFICATION";
    private readonly SpectrumQuery query = new SpectrumQuery();
    private Dictionary<string, object> annotations;
    private List<FollowCandidate> diffModificationSiteCandidates;
    private double expectValue;
    private bool isPrecursorMonoisotopic = true;
    protected double IonHMass = Atom.H.MonoMass - Atom.ElectronMass;
    private double minusLogExpectValue;

    public IdentifiedSpectrum()
    {
      SpRank = 1;
      Id = string.Empty;
      NumProteaseTermini = 2;
      Tag = string.Empty;
    }

    //~IdentifiedSpectrum()
    //{
    //  Console.WriteLine("IdentifiedSpectrum destroyed");
    //}

    public string Engine { get; set; }

    public string Id { get; set; }

    public string Title
    {
      get { return Query.Title; }
      set { Query.Title = value; }
    }

    #region IIdentifiedSpectrum Members

    public int Charge
    {
      get { return Query.Charge; }
      set { Query.Charge = value; }
    }

    private string _tag;
    public string Tag
    {
      get
      {
        return _tag;
      }
      set
      {
        _tag = value;
        ClassificationTag = value;
      }
    }

    public string ClassificationTag { get; set; }

    public bool FromDecoy { get; set; }

    public bool IsContaminant { get; set; }

    public int SpRank { get; set; }

    public double SpScore { get; set; }

    public int DuplicatedCount { get; set; }

    public double IsoelectricPoint { get; set; }

    public double ObservedMz
    {
      get
      {
        if (this.query.ObservedMz == 0.0)
        {
          this.query.ObservedMz = GetPrecursorMz();
        }
        return this.query.ObservedMz;
      }
      set
      {
        this.query.ObservedMz = value;
      }
    }

    public IIdentifiedPeptide Peptide
    {
      get
      {
        if (0 == this.Peptides.Count)
        {
          return null;
        }
        else
        {
          return Peptides[0];
        }
      }
    }

    public ISpectrumQuery Query
    {
      get { return this.query; }
    }

    public bool IsPrecursorMonoisotopic
    {
      get
      {
        return this.isPrecursorMonoisotopic;
      }
      set
      {
        this.isPrecursorMonoisotopic = value;
        this.IonHMass = this.isPrecursorMonoisotopic ? Atom.H.MonoMass - Atom.ElectronMass : Atom.H.AverageMass - Atom.ElectronMass;
      }
    }

    public int NumMissedCleavages { get; set; }

    public int NumProteaseTermini { get; set; }

    public double TheoreticalMass { get; set; }

    private double _experimentalMass;
    public double ExperimentalMass
    {
      get
      {
        if (_experimentalMass == 0.0 && this.query.ObservedMz != 0.0 && this.query.Charge != 0.0)
        {
          _experimentalMass = this.query.ObservedMz * this.query.Charge - this.IonHMass * this.query.Charge;
        }

        return _experimentalMass;
      }
      set
      {
        _experimentalMass = value;
      }
    }

    public double TheoreticalMH
    {
      get { return TheoreticalMass + this.IonHMass; }
      set { TheoreticalMass = value - this.IonHMass; }
    }

    public double ExperimentalMH
    {
      get { return ExperimentalMass + this.IonHMass; }
      set { ExperimentalMass = value - this.IonHMass; }
    }

    public int Rank { get; set; }

    public double Score { get; set; }

    public double PValue { get; set; }

    public double QValue { get; set; }

    public double DeltaScore { get; set; }

    public Protease DigestProtease { get; set; }

    public int TheoreticalIonCount { get; set; }

    public int MatchedIonCount { get; set; }

    public double MatchedTIC { get; set; }

    public string Ions
    {
      get { return GetIons(); }
      set { SetIons(value); }
    }

    public Dictionary<string, object> Annotations
    {
      get
      {
        if (null == this.annotations)
        {
          this.annotations = new Dictionary<string, object>();
        }

        return this.annotations;
      }
    }

    public double TheoreticalMinusExperimentalMass
    {
      get
      {
        if (0.0 == TheoreticalMass || 0.0 == ExperimentalMass)
        {
          throw new Exception("Set TheoreticalMass and ExperimentalMass first!");
        }
        return TheoreticalMass - ExperimentalMass;
      }
      set
      {
        if (0.0 == TheoreticalMass)
        {
          this.TheoreticalMass = this.ExperimentalMass + value;
        }
        else
        {
          this.ExperimentalMass = this.TheoreticalMass - value;
        }
      }
    }

    public ReadOnlyCollection<string> Proteins
    {
      get
      {
        var result = new HashSet<string>();

        foreach (var pep in this.Peptides)
        {
          result.UnionWith(pep.Proteins);
        }

        return new List<string>(result).AsReadOnly();
      }
    }

    public double GetPrecursorMz()
    {
      return PrecursorUtils.MHToMz(ExperimentalMH, Query.Charge, IsPrecursorMonoisotopic);
    }

    public double GetTheoreticalMz()
    {
      return PrecursorUtils.MHToMz(TheoreticalMH, Query.Charge, IsPrecursorMonoisotopic);
    }

    public string GetMatchSequence()
    {
      if (this.Peptides.Count == 0)
      {
        return "";
      }

      return PeptideUtils.GetMatchedSequence(this.Peptides[0].Sequence);
    }

    public string GetSequences(string delimiter)
    {
      var result = new List<string>();
      foreach (IIdentifiedPeptide peptide in this.Peptides)
      {
        result.Add(peptide.Sequence);
      }

      return StringUtils.Merge(result, delimiter);
    }

    public string GetPureSequences(string delimiter)
    {
      var result = new List<string>();
      foreach (IIdentifiedPeptide peptide in this.Peptides)
      {
        result.Add(peptide.PureSequence);
      }

      return StringUtils.Merge(result, delimiter);
    }

    public string GetProteins(string delimiter)
    {
      var result = new List<string>();
      foreach (IIdentifiedPeptide peptide in this.Peptides)
      {
        result.Add(StringUtils.Merge(peptide.Proteins, "/"));
      }

      return StringUtils.Merge(result, delimiter);
    }

    public void ClearProteins()
    {
      foreach (IIdentifiedPeptide peptide in Peptides)
      {
        peptide.ClearProteins();
      }
    }

    public double ExpectValue
    {
      get { return this.expectValue; }
      set
      {
        this.expectValue = value;
        this.minusLogExpectValue = -Math.Log(value);
      }
    }

    public double MinusLogExpectValue
    {
      get { return this.minusLogExpectValue; }
    }

    public string Modifications { get; set; }

    public bool Selected { get; set; }

    public int GroupCount { get; set; }

    public List<FollowCandidate> DiffModificationSiteCandidates
    {
      get
      {
        if (null == this.diffModificationSiteCandidates)
        {
          this.diffModificationSiteCandidates = new List<FollowCandidate>();
        }
        return this.diffModificationSiteCandidates;
      }
      set { this.diffModificationSiteCandidates = value; }
    }

    #endregion

    private String GetIons()
    {
      return MyConvert.Format("{0}/{1}", MatchedIonCount, TheoreticalIonCount);
    }

    private void SetIons(string ions)
    {
      string pattern = @"(\d+)[/|](\d+)";
      Match match = Regex.Match(ions, pattern);
      if (!match.Success)
      {
        throw new Exception(ions + " is not a valid ions line (for example 10/22 or 10|22)");
      }

      MatchedIonCount = int.Parse(match.Groups[1].Value);
      TheoreticalIonCount = int.Parse(match.Groups[2].Value);
    }

    public string[] GetModifications()
    {
      if (null == Modifications || 0 == Modifications.Length)
      {
        return new string[0];
      }
      else
      {
        return Regex.Split(Modifications, @";\s*");
      }
    }

    public override string ToString()
    {
      StringBuilder sb = new StringBuilder(Sequence);

      sb.Append(",").Append(Query.FileScan.ShortFileName);

      return sb.ToString();
    }

    public int CompareTo(IIdentifiedSpectrum other)
    {
      if (null == other)
      {
        return 1;
      }

      int result = Sequence.CompareTo(other.Sequence);
      if (result == 0)
      {
        result = Query.FileScan.Experimental.CompareTo(other.Query.FileScan.Experimental);
      }

      if (result == 0)
      {
        result = Query.FileScan.FirstScan.CompareTo(other.Query.FileScan.FirstScan);
      }

      return result;
    }

    #region IComparable<IdentifiedSpectrum> Members

    public int CompareTo(IdentifiedSpectrum other)
    {
      return CompareTo((IIdentifiedSpectrum)other);
    }

    #endregion
  }
}