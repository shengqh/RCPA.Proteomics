using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RCPA.Proteomics.Summary.Uniform
{
  public class DecoySpectraFilter : IFilter<IIdentifiedSpectrum>
  {
    private int minDecoyScan;

    public DecoySpectraFilter(int minDecoyScan)
    {
      this.minDecoyScan = minDecoyScan;
    }

    public bool Accept(IIdentifiedSpectrum t)
    {
      return t.Query.FileScan.FirstScan >= minDecoyScan;
    }
  }
}
