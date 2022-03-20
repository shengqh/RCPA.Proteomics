using RCPA.Proteomics.Spectrum;
using System.Collections.Generic;
using System.Linq;

namespace RCPA.Proteomics.Quantification.Labelfree
{
  public class ObservedProfileHighAbundanceFinder : AbstractObservedProfileOptimizationFinder
  {
    protected override List<Peak> Resolve(ChromatographProfile chro, List<List<Peak>> envelope)
    {
      return (from peaks in envelope
              let peakindex = peaks.FindMaxIndex()
              select peaks[peakindex]).ToList();
    }
  }
}
