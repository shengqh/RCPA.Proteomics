using System.Collections.Generic;

namespace RCPA.Proteomics.Summary
{
  public interface ISummaryBuilderBaseFactory
  {
    bool SavePeptidesFile { get; }

    IFileFormat<List<IIdentifiedSpectrum>> GetIdentifiedSpectrumFormat();

    IFileFormat<IIdentifiedResult> GetIdetifiedResultFormat();
  }
}
