using RCPA.Proteomics.Spectrum;
using System.Collections.Generic;

namespace RCPA.Proteomics.Quantification.Labelfree
{
  public class ObservedProfileOptimizationFinder : AbstractObservedProfileOptimizationFinder
  {
    protected override List<Peak> Resolve(ChromatographProfile chro, List<List<Peak>> envelope)
    {
      //return Isotopic.InterfernceOptimization.ResolveByPearsonCorrelation(chro.IsotopicIntensities, envelope);
      return Isotopic.InterfernceOptimization.ResolveByKullbackLeiblerDistance(chro.IsotopicIntensities, envelope);
    }
  }
}
