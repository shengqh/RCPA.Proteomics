using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using RCPA.Proteomics.Spectrum;

namespace RCPA.Proteomics.Snp
{
  public interface IBestSpectrumBuilder
  {
    PeakList<Peak> Build(List<PeakList<Peak>> pkls);
  }
}
