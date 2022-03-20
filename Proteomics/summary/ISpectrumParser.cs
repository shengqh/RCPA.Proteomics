using RCPA.Gui;
using System.Collections.Generic;

namespace RCPA.Proteomics.Summary
{
  public interface ISpectrumParser : IFileReader<List<IIdentifiedSpectrum>>, IProgress
  {
    SearchEngineType Engine { get; }

    ITitleParser TitleParser { get; set; }
  }
}
