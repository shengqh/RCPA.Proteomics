using System;
using System.Collections.Generic;
using System.Text;
using RCPA.Utils;

namespace RCPA.Proteomics.Summary
{
  public interface ISummaryBuilderBaseFactory
  {
    bool SavePeptidesFile { get; }

    IFileFormat<List<IIdentifiedSpectrum>> GetIdentifiedSpectrumFormat();

    IFileFormat<IIdentifiedResult> GetIdetifiedResultFormat();
  }
}
