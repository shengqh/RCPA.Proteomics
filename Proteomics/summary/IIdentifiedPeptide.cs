using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace RCPA.Proteomics.Summary
{
  public interface IIdentifiedPeptide : IComparable<IIdentifiedPeptide>
  {
    /// <summary>
    /// Get or set corresponding spectrum base
    /// </summary>
    IIdentifiedSpectrumBase SpectrumBase { get; set; }

    /// <summary>
    /// Get or set corresponding spectrum
    /// </summary>
    IIdentifiedSpectrum Spectrum { get; set; }

    /// <summary>
    /// Get or set sequence
    /// </summary>
    string Sequence { get; set; }

    /// <summary>
    /// Get the pure sequence without modification and prior/next aminoacid
    /// </summary>
    string PureSequence { get; }

    /// <summary>
    /// Get readonly protein collection
    /// </summary>
    ReadOnlyCollection<string> Proteins { get; }

    /// <summary>
    /// Add protein
    /// </summary>
    /// <param name="protein"></param>
    void AddProtein(string protein);

    /// <summary>
    /// Assign proteins
    /// </summary>
    /// <param name="proteins"></param>
    void AssignProteins(IEnumerable<string> proteins);

    /// <summary>
    /// Set protein in special position
    /// </summary>
    /// <param name="index"></param>
    /// <param name="protein"></param>
    void SetProtein(int index, string protein);

    int ConfidenceLevel { get; set; }

    void RemoveProteinAt(int index);

    bool HasProtein(string protein);

    void ClearProteins();

    bool IsTopOne();
  }
}