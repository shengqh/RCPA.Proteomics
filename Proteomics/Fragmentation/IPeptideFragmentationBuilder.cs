using RCPA.Proteomics.Spectrum;
using System.Collections.Generic;

namespace RCPA.Proteomics.Fragmentation
{
  public interface IPeptideFragmentationBuilder<T> where T : IIonTypePeak
  {
    List<T> Build(string sequence);

    IonType SeriesType { get; }
  }
}