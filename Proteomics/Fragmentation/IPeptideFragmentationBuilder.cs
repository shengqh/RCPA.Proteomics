using System.Collections.Generic;
using RCPA.Proteomics.Image;
using RCPA.Proteomics.Spectrum;

namespace RCPA.Proteomics.Fragmentation
{
  public interface IPeptideFragmentationBuilder<T> where T : IIonTypePeak
  {
    List<T> Build(string sequence);

    IonType SeriesType { get; }
  }
}