using RCPA.Gui;
using RCPA.Proteomics.Raw;
using System.Collections.Generic;

namespace RCPA.Proteomics.Quantification.ITraq
{
  public interface IITraqRawReader : IProgress, IFileReader<List<IsobaricItem>>
  {
    IRawFile2 RawReader { get; set; }

    IsobaricType PlexType { get; set; }

    string Name { get; }

    bool IsTandemMS3 { get; }
  }
}
