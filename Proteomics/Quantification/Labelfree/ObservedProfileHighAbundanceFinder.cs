using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RCPA.Proteomics.Spectrum;

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
