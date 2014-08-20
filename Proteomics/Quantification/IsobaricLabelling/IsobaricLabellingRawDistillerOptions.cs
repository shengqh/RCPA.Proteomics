using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RCPA.Proteomics.Quantification.IsobaricLabelling
{
  public class IsobaricLabellingRawDistillerOptions
  {
    public string InputFile { get; set; }
    public string OutputFile { get; set; }
    public int MinPeakCount { get; set; }
    public IsobaricType PlexType { get; set; }
    public double PPMTolearnce { get; set; }
  }
}
