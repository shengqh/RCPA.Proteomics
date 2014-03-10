using System;
using System.Collections.Generic;
using System.Text;

namespace RCPA.Proteomics.Summary
{
  public interface IIdentifiedResult : System.Collections.Generic.IList<IIdentifiedProteinGroup>, IAnnotation
  {
    List<IIdentifiedProtein> GetProteins();

    List<IIdentifiedSpectrum> GetSpectra();

    void InitUniquePeptideCount();

    void BuildGroupIndex();

    Dictionary<IIdentifiedSpectrum, List<IIdentifiedProteinGroup>> GetPeptideProteinGroupMap();

    Dictionary<string, HashSet<IIdentifiedSpectrum>> GetExperimentalPeptideMap();

    void Filter(Predicate<IIdentifiedPeptide> match);

    HashSet<string> GetExperimentals();

    void Sort();
  }
}
