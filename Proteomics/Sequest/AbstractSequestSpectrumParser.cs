using System.Collections.Generic;
using System.Linq;
using RCPA.Gui;
using RCPA.Proteomics.Summary;

namespace RCPA.Proteomics.Sequest
{
  public abstract class AbstractSequestSpectrumParser : ProgressClass, ISpectrumParser
  {
    public virtual SearchEngineType Engine { get { return SearchEngineType.SEQUEST; } }

    public abstract List<IIdentifiedSpectrum> ReadFromFile(string fileName);

    public ITitleParser TitleParser { get; set; }
  }
}