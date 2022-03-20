using System;

namespace RCPA.Proteomics.Summary
{
  public interface IIdentifiedResultTextFormat : IFileFormat<IIdentifiedResult>
  {
    IIdentifiedProteinGroupWriter GroupWriter { get; set; }

    LineFormat<IIdentifiedProtein> ProteinFormat { get; set; }

    LineFormat<IIdentifiedSpectrum> PeptideFormat { get; set; }

    Func<IIdentifiedProteinGroup, bool> ValidGroup { get; set; }

    Func<IIdentifiedSpectrum, bool> ValidSpectrum { get; set; }

    void InitializeByResult(IIdentifiedResult result);
  }
}
