using RCPA.Proteomics.Spectrum;
using RCPA.Proteomics.Summary;
using System.Collections.Generic;

namespace RCPA.Tools.Quantification
{
  public interface ITheoreticalEnvelopesBuilder
  {
    List<Envelope> Build(IIdentifiedSpectrum spectrum);
  }
}
