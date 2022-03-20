﻿using System.Collections.Generic;

namespace RCPA.Proteomics.Quantification.IsobaricLabelling
{
  public class AbstractIsobaricResultFileDistillerOptions
  {
    public AbstractIsobaricResultFileDistillerOptions()
    {
      this.PerformPurityCorrection = true;
      this.PerformMassCalibration = false;
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

    public bool PerformMassCalibration { get; set; }
  }
}
