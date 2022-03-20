using System.Collections.Generic;

namespace RCPA.Proteomics.Quantification.ITraq
{
  public interface IRatioPeptideToProteinBuilder
  {
    string Name { get; }

    ITraqProteinRatioItem Calculate(IEnumerable<ITraqPeptideRatioItem> items);
  }
}
