using System.Collections.Generic;

namespace RCPA.Proteomics.Isotopic
{
  public interface IIsotopicProfileBuilder
  {
    List<double> GetProfile(AtomComposition ac, int profileLength);

    List<double> GetProfile(AtomComposition ac);
  }

  public abstract class AbstractIsotopicProfileBuilder : IIsotopicProfileBuilder
  {
    public const double PERCENT_TOLERANCE = 0.0001;

    #region IIsotopicProfileBuilder Members

    public abstract List<double> GetProfile(AtomComposition ac, int profileLength);

    public List<double> GetProfile(AtomComposition ac)
    {
      List<double> result;
      for (int iLength = 10; ; iLength *= 2)
      {
        result = GetProfile(ac, iLength);
        if (result[result.Count - 1] < result[result.Count - 2]
            && result[result.Count - 1] < PERCENT_TOLERANCE)
        {
          break;
        }
      }

      while (result.Count > 1 && result[result.Count - 2] < PERCENT_TOLERANCE)
      {
        result.RemoveAt(result.Count - 1);
      }

      return result;
    }

    #endregion
  }
}