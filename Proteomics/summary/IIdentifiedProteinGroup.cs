using System;
using System.Collections.Generic;
using System.Text;
using System.Collections.ObjectModel;

namespace RCPA.Proteomics.Summary
{
  public interface IIdentifiedProteinGroup : ICloneable, IList<IIdentifiedProtein>, IComparable<IIdentifiedProteinGroup>
  {
    int Index { get; set; }

    bool Enabled { get; set; }

    bool Selected { get; set; }

    double Probability { get; set; }

    double TotalIonCount { get; set; }

    double QValue { get; set; }

    bool FromDecoy { get; set; }

    void AddIdentifiedSpectrum(IIdentifiedSpectrum peptide);

    void AddIdentifiedSpectra(IEnumerable<IIdentifiedSpectrum> peptides);

    ReadOnlyCollection<IIdentifiedSpectrum> GetPeptides();

    List<IIdentifiedSpectrum> GetSortedPeptides();

    void InitUniquePeptideCount();
  }
}
