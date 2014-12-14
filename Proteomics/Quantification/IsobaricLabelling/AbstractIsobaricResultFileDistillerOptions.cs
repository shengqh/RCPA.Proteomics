using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RCPA.Proteomics.Quantification.IsobaricLabelling
{
  public class AbstractIsobaricResultFileDistillerOptions
  {
    public AbstractIsobaricResultFileDistillerOptions()
    {
      this.PerformPurityCorrection = true;
    }

    public IIsobaricRawReader Reader { get; set; }

    public int MinPeakCount { get; set; }

    public IsobaricType PlexType
    {
      get
      {
        return Reader.PlexType;
      }
    }

    public double PrecursorPPMTolerance { get; set; }

    public double ProductPPMTolerance { get; set; }

    public List<IsobaricIndex> RequiredChannels { get; set; }

    public List<IsobaricIndex> UsedChannels { get; set; }

    public bool PerformPurityCorrection { get; set; }
  }
}
