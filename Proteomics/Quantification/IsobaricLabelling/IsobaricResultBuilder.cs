using System.Collections.Generic;

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
