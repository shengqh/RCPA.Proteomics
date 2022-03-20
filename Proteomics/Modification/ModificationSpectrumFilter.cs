using RCPA.Proteomics.Summary;
using System.Text.RegularExpressions;

namespace RCPA.Proteomics.Modification
{
  public class ModificationSpectrumFilter : IFilter<IIdentifiedSpectrum>
  {
    private readonly Regex reg;

    public ModificationSpectrumFilter(string modifiedAminoacids)
    {
      this.reg = new Regex("[" + modifiedAminoacids + "][^\\w.]");
    }

    #region IFilter<IIdentifiedSpectrum> Members

    public bool Accept(IIdentifiedSpectrum t)
    {
      return this.reg.Match(t.Sequence).Success;
    }

    #endregion
  }
}