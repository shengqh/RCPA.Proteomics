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

    private double ppmTolerance;

    public IsobaricResultBuilder(List<UsedChannel> channels, double ppmTolerance)
    {
      this.Channels = channels;
      this.ppmTolerance = ppmTolerance;
    }

    public virtual IsobaricResult BuildIsobaricResult(IsobaricResult pkls, int minPeakCount)
    {
      IsobaricResult result = new IsobaricResult(pkls);
      result.Mode = pkls.Mode;
      result.PlexType = pkls.PlexType;
      result.UsedChannels = Channels;

      result.ForEach(m => m.DetectReporter(Channels));

      result.RemoveAll(m => m.PeakCount() < minPeakCount);

      return result;
    }
  }
}
