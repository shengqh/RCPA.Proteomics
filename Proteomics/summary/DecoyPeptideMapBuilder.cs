using RCPA.Proteomics.Summary.Uniform;
using System.Collections.Generic;

namespace RCPA.Proteomics.Summary
{
  public class DecoyPeptideBuilder
  {
    private DecoyPeptideBuilder() { }

    public static void AssignDecoy<T>(List<T> peptides, IFilter<IIdentifiedSpectrum> decoyFilter) where T : IIdentifiedSpectrum
    {
      foreach (T phit in peptides)
      {
        phit.FromDecoy = decoyFilter.Accept(phit);
      }
    }

    public static void AssignDecoy<T>(List<T> peptides, string decoyPattern, bool conflictAsDecoy) where T : IIdentifiedSpectrum
    {
      IFilter<IIdentifiedSpectrum> decoyFilter;
      if (conflictAsDecoy)
      {
        decoyFilter = ResolveTargetDecoyConflictTypeFactory.Decoy.GetSpectrumFilter(decoyPattern);
      }
      else
      {
        decoyFilter = ResolveTargetDecoyConflictTypeFactory.Target.GetSpectrumFilter(decoyPattern);
      }

      AssignDecoy(peptides, decoyFilter);
    }
  }
}
