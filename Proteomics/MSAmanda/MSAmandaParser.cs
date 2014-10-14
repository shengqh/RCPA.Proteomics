using RCPA.Gui;
using RCPA.Proteomics.MzIdent;
using RCPA.Proteomics.Summary;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace RCPA.Proteomics.MSAmanda
{
  public class MSAmandaParser : ProgressClass, ISpectrumParser
  {
    public MSAmandaParser() { }

    public SearchEngineType Engine
    {
      get { return SearchEngineType.MSAmanda; }
    }

    public ITitleParser TitleParser { get; set; }

    public List<IIdentifiedSpectrum> ReadFromFile(string fileName)
    {
      using (var sr = new StreamReader(fileName))
      {
      }
      throw new NotImplementedException();
    }
  }
}
