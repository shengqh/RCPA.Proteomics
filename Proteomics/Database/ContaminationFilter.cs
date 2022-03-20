using System.Text.RegularExpressions;

namespace RCPA.Proteomics.Database
{
  public class ContaminationFilter : IFilter<string>
  {
    private Regex reg;
    public ContaminationFilter(string contaminationPattern)
    {
      this.reg = new Regex(contaminationPattern);
    }

    #region IFilter<string> Members

    public bool Accept(string t)
    {
      return reg.Match(t).Success;
    }

    #endregion
  }
}
