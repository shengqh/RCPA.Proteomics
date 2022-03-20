using RCPA.Proteomics.Spectrum;
using System.Collections.Generic;

namespace RCPA.Proteomics.Fragmentation
{
  public abstract class AbstractPeptideBSeriesBuilder<T> : AbstractPeptideFragmentationBuilder<T> where T : IIonTypePeak, new()
  {
    protected override int GetIonCount(int aminoacidCount)
    {
      return aminoacidCount - 1;
    }

    protected override List<AminoacidInfo> GetAminoacidInfos(string sequence)
    {
      return GetForwardAminoacidInfos(sequence);
    }
  }
}
