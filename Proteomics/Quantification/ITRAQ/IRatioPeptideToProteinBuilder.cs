using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RCPA.Proteomics.Quantification.ITraq
{
  public interface IRatioPeptideToProteinBuilder
  {
    string Name { get; }

    ITraqProteinRatioItem Calculate(IEnumerable<ITraqPeptideRatioItem> items); 
  }
}
