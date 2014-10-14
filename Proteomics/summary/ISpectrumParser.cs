using RCPA.Gui;
using RCPA.Proteomics.Summary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RCPA.Proteomics.Summary
{
  public interface ISpectrumParser : IFileReader<List<IIdentifiedSpectrum>>, IProgress
  {
    SearchEngineType Engine { get; }

    ITitleParser TitleParser { get; set; }
  }
}
