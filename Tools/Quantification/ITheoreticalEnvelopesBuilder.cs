using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using RCPA.Proteomics.Spectrum;
using RCPA.Proteomics.Summary;

namespace RCPA.Tools.Quantification
{
  public interface ITheoreticalEnvelopesBuilder
  {
    List<Envelope> Build(IIdentifiedSpectrum spectrum);
  }
}
