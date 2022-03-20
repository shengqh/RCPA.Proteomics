using System.Collections.Generic;

namespace RCPA.Proteomics.Image
{
  public class CombinedMatcher : IMatcher
  {
    private IEnumerable<IMatcher> matchers;

    public CombinedMatcher(IEnumerable<IMatcher> matchers)
    {
      this.matchers = matchers;
    }

    #region IMatcher Members

    public void Match(IIdentifiedPeptideResult sr)
    {
      foreach (IMatcher matcher in matchers)
      {
        matcher.Match(sr);
      }
    }

    #endregion
  }
}
