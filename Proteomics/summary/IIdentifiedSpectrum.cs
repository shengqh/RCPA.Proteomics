using System;
using System.Linq;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using RCPA.Proteomics.Utils;

namespace RCPA.Proteomics.Summary
{
  public interface IIdentifiedSpectrum : IIdentifiedSpectrumBase, IAnnotation, IComparable<IIdentifiedSpectrum>
  {
    string Id { get; set; }

    ISpectrumQuery Query { get; }

    string Engine { get; set; }

    int Charge { get; set; }

    double ObservedMz { get; set; }

    string Tag { get; set; }

    string ClassificationTag { get; set; }

    bool IsPrecursorMonoisotopic { get; set; }

    bool FromDecoy { get; set; }

    bool AssignedDecoy { get; set; }

    bool IsContaminant { get; set; }

    ReadOnlyCollection<string> Proteins { get; }

    double IsoelectricPoint { get; set; }

    int NumMissedCleavages { get; set; }

    int NumProteaseTermini { get; set; }

    double ExperimentalMass { get; set; }

    double TheoreticalMass { get; set; }

    double TheoreticalMH { get; set; }

    double ExperimentalMH { get; set; }

    double TheoreticalMinusExperimentalMass { get; set; }

    int SpRank { get; set; }

    double SpScore { get; set; }

    int Rank { get; set; }

    double Score { get; set; }

    double Probability { get; set; }

    double QValue { get; set; }

    double DeltaScore { get; set; }

    int DuplicatedCount { get; set; }

    double ExpectValue { get; set; }

    double MinusLogExpectValue { get; }

    Protease DigestProtease { get; set; }

    int TheoreticalIonCount { get; set; }

    int MatchedIonCount { get; set; }

    double MatchedTIC { get; set; }

    string Ions { get; set; }

    string Modifications { get; set; }

    IIdentifiedPeptide Peptide { get; }

    bool Selected { get; set; }

    int GroupCount { get; set; }

    List<FollowCandidate> DiffModificationSiteCandidates { get; set; }

    double GetPrecursorMz();

    double GetTheoreticalMz();

    string GetMatchSequence();

    string GetSequences(string delimiter);

    string GetPureSequences(string delimiter);

    string GetProteins(string delimiter);

    void ClearProteins();
  }

  public static class IIdentifiedSpectrumExtension
  {
    public static HashSet<string> GetSequencesSet(this IIdentifiedSpectrum spectrum)
    {
      HashSet<string> result = new HashSet<string>();
      foreach (var p in spectrum.Peptides)
      {
        result.Add(p.Sequence);
      }
      return result;

    }
  }
}