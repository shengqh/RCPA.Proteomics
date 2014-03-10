using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using RCPA.Proteomics.Summary;

namespace RCPA.Proteomics.Sequest
{
  public interface ISequestOutParser
  {
    List<IIdentifiedSpectrum> ParsePeptides(string fileOrDirectory);
  }
}
