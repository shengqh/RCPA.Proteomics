using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace RCPA.Proteomics.Summary
{
  public interface IIdentifiedSpectrumBase : IComparable
  {
    /// <summary>
    /// Get the sequence of first peptide
    /// </summary>
    string Sequence { get; }

    /// <summary>
    /// Get the readonly peptide collection
    /// </summary>
    ReadOnlyCollection<IIdentifiedPeptide> Peptides { get; }

    /// <summary>
    /// Add peptide
    /// </summary>
    /// <param name="peptide"></param>
    void AddPeptide(IIdentifiedPeptide peptide);

    /// <summary>
    /// Add peptides
    /// </summary>
    /// <param name="values"></param>
    void AddPeptides(IEnumerable<IIdentifiedPeptide> values);

    /// <summary>
    /// Assign peptides
    /// </summary>
    /// <param name="values"></param>
    void AssignPeptides(IEnumerable<IIdentifiedPeptide> values);

    /// <summary>
    /// Remove peptide at special position
    /// </summary>
    /// <param name="index"></param>
    void RemovePeptideAt(int index);

    /// <summary>
    /// Remove peptide
    /// </summary>
    /// <param name="peptide"></param>
    void RemovePeptide(IIdentifiedPeptide peptide);

    /// <summary>
    /// Clear peptides
    /// </summary>
    void ClearPeptides();

    /// <summary>
    /// Allocate new peptide instance
    /// </summary>
    /// <returns></returns>
    IIdentifiedPeptide NewPeptide();

    /// <summary>
    /// Get or set summary line
    /// </summary>
    string SummaryLine { get; set; }
  }
}