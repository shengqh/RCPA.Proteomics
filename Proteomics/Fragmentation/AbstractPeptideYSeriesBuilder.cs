using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using RCPA.Proteomics.Spectrum;

namespace RCPA.Proteomics.Fragmentation
{
  public abstract class AbstractPeptideYSeriesBuilder<T> : AbstractPeptideFragmentationBuilder<T> where T : IIonTypePeak, new()
  {
    protected override int GetIonCount(int aminoacidCount)
    {
      return aminoacidCount - 1;
    }

    protected override List<AminoacidInfo> GetAminoacidInfos(string sequence)
    {
      return GetReverseAminoacidInfos(sequence);
    }
  }
}
