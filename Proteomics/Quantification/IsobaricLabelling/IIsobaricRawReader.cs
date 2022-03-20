using RCPA.Gui;
using System.Collections.Generic;

namespace RCPA.Proteomics.Quantification.IsobaricLabelling
{
  public interface IIsobaricRawReader : IProgress, IFileReader<List<IsobaricScan>>
  {
    IsobaricType PlexType { get; set; }

    string Name { get; }

    bool IsTandemMS3 { get; }
  }
}
