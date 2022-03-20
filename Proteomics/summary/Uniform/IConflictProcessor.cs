using System;
using System.Collections.Generic;
using System.Linq;

namespace RCPA.Proteomics.Summary.Uniform
{
  public interface IConflictProcessor
  {
    IIdentifiedSpectrum[] Process(List<IIdentifiedSpectrum> source);
  }

  public class ConflictDiscardAllProcessor : IConflictProcessor
  {
    #region IConflictProcessor Members

    public IIdentifiedSpectrum[] Process(List<IIdentifiedSpectrum> source)
    {
      return null;
    }

    #endregion
  }

  public class ConflictPreferEngineProcessor : IConflictProcessor
  {
    private string[] perferEngines;

    public ConflictPreferEngineProcessor(SearchEngineType[] perferEngines)
    {
      this.perferEngines = (from set in perferEngines
                            select set.ToString()).ToArray();
    }

    #region IConflictProcessor Members

    public IIdentifiedSpectrum[] Process(List<IIdentifiedSpectrum> source)
    {
      foreach (var perferEngine in perferEngines)
      {
        foreach (var spectrum in source)
        {
          if (perferEngine == spectrum.Engine)
          {
            return new[] { spectrum };
          }
        }
      }

      throw new Exception(MyConvert.Format("No perfer engine [{0}] found in spectra {1}",
        StringUtils.Merge(perferEngines, ","),
        StringUtils.Merge((from s in source select s.Engine).Distinct(), ",")));
    }

    #endregion
  }

  public class ConflictQvalueProcessor : IConflictProcessor
  {
    public ConflictQvalueProcessor()
    { }

    #region IConflictProcessor Members

    public IIdentifiedSpectrum[] Process(List<IIdentifiedSpectrum> source)
    {
      if (null == source || source.Count == 0)
      {
        return null;
      }

      double minQValue = double.MaxValue;
      IIdentifiedSpectrum top1 = null;

      foreach (var spectrum in source)
      {
        if (spectrum.QValue < minQValue)
        {
          minQValue = spectrum.QValue;
          top1 = spectrum;
        }
      }

      List<IIdentifiedSpectrum> result = new List<IIdentifiedSpectrum>();
      result.Add(top1);

      foreach (var spectrum in source)
      {
        if (spectrum != top1 && spectrum.SequenceEquals(top1))
        {
          result.Add(spectrum);
        }
      }

      return result.ToArray();
    }

    #endregion
  }
}
