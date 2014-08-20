using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using RCPA.Gui;
using RCPA.Proteomics.Spectrum;
using RCPA.Proteomics.Raw;

namespace RCPA.Proteomics.Quantification.IsobaricLabelling
{
  public interface IIsobaricRawReader : IProgress, IFileReader<List<IsobaricItem>>
  {
    IsobaricType PlexType { get; set; }

    string Name { get; }

    bool IsTandemMS3 { get; }
  }
}
