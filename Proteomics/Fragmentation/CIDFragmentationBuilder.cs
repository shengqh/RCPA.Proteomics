using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using RCPA.Proteomics.Spectrum;

namespace RCPA.Proteomics.Fragmentation
{
  public class CIDFragmentationBuilder<T> where T : IIonTypePeak, new()
  {
    private List<IPeptideFragmentationBuilder<T>> builders = new List<IPeptideFragmentationBuilder<T>>();

    public CIDFragmentationBuilder(int charge, Aminoacids aas)
    {
      builders.Add(new CIDPeptideBSeriesBuilder<T>()
      {
        CurAminoacids = aas
      });

      builders.Add(new CIDPeptideYSeriesBuilder<T>()
      {
        CurAminoacids = aas
      });

      if (charge >= 2)
      {
        builders.Add(new CIDPeptideB2SeriesBuilder<T>()
        {
          CurAminoacids = aas
        });

        builders.Add(new CIDPeptideY2SeriesBuilder<T>()
        {
          CurAminoacids = aas
        });
      }
    }

    public Dictionary<IonType, List<T>> GetIonSeries(string sequence)
    {
      Dictionary<IonType, List<T>> result = new Dictionary<IonType, List<T>>();

      builders.ForEach(builder => result[builder.SeriesType] = builder.Build(sequence));

      return result;
    }
  }
}
