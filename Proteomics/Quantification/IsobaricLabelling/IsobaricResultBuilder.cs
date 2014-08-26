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
    public IsobaricType Definition { get; private set; }

    private double ppmTolerance;

    public IsobaricResultBuilder(IsobaricType definition, double ppmTolerance)
    {
      this.Definition = definition;
      this.ppmTolerance = ppmTolerance;
    }

    public virtual IsobaricResult BuildIsobaricResult(IsobaricResult pkls, int minPeakCount)
    {
      IsobaricResult result = new IsobaricResult(pkls);
      result.Mode = pkls.Mode;
      result.PlexType = pkls.PlexType;

      result.ForEach(m => m.DetectReporter(result.PlexType, this.ppmTolerance));

      result.RemoveAll(m => m.PeakCount() < minPeakCount);

      return result;
    }
  }
}
