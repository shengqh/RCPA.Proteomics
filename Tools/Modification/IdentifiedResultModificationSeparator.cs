using RCPA.Proteomics.Modification;
using RCPA.Proteomics.Summary;
using RCPA.Tools.Summary;
using System.Collections.Generic;

namespace RCPA.Tools.Modification
{
  public class IdentifiedResultModificationSeparator : AbstractThreadFileProcessor
  {
    private IdentifiedResultSeparatorBySpectrumFilter processor;

    public IdentifiedResultModificationSeparator(string modifiedAminoacids)
    {
      Dictionary<IFilter<IIdentifiedSpectrum>, string> map = new Dictionary<IFilter<IIdentifiedSpectrum>, string>();

      ModificationSpectrumFilter filter = new ModificationSpectrumFilter(modifiedAminoacids);
      map[filter] = "Modified_" + modifiedAminoacids;
      map[new NotFilter<IIdentifiedSpectrum>(filter)] = "not_Modified_" + modifiedAminoacids;

      processor = new IdentifiedResultSeparatorBySpectrumFilter(map);
    }

    public override IEnumerable<string> Process(string filename)
    {
      processor.Progress = this.Progress;

      return processor.Process(filename);
    }
  }
}
