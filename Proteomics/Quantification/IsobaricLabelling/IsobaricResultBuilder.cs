using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MathNet.Numerics.Statistics;
using RCPA.Proteomics.Spectrum;
using MathNet.Numerics.Distributions;

namespace RCPA.Proteomics.Quantification.IsobaricLabelling
{
  public class IsobaricResultBuilder
  {
    public List<UsedChannel> Channels { get; private set; }

    public IsobaricResultBuilder(List<UsedChannel> channels)
    {
      this.Channels = channels;
    }

    public virtual void BuildIsobaricResult(IsobaricResult result, int minPeakCount)
    {
    }
  }
}
