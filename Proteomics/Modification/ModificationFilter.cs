using System;
using System.Text.RegularExpressions;

namespace RCPA.Proteomics.Modification
{
  public class ModificationFilter : IFilter<String>
  {
    private readonly Regex reg;

    public ModificationFilter(string modifiedAminoacids)
    {
      this.reg = new Regex("[" + modifiedAminoacids + "][^\\w.]");
    }

    #region IFilter<string> Members

    public bool Accept(string t)
    {
      return this.reg.Match(t).Success;
    }

    #endregion
  }
}