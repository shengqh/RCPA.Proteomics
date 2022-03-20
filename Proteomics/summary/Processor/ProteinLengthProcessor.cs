using System;

namespace RCPA.Proteomics.Summary.Processor
{
  /// <summary>
  /// 保留蛋白质群中最长或者最短的蛋白质
  /// </summary>
  public class ProteinLengthProcessor : IProcessor<IIdentifiedProteinGroup>
  {
    private Func<IIdentifiedProteinGroup, IIdentifiedProtein> findProtein;

    public ProteinLengthProcessor(bool keptLonger)
    {
      if (keptLonger)
      {
        findProtein = FindLonger;
      }
      else
      {
        findProtein = FindShorter;
      }
    }

    #region IProcessor<IIdentifiedProteinGroup> Members

    public IIdentifiedProteinGroup Process(IIdentifiedProteinGroup group)
    {
      if (group.Count == 1)
      {
        return group;
      }

      IIdentifiedProtein protein = findProtein(group);

      for (int i = group.Count - 1; i >= 0; i--)
      {
        if (group[i] != protein)
        {
          group.RemoveAt(i);
        }
      }

      return group;
    }

    #endregion

    private IIdentifiedProtein FindLonger(IIdentifiedProteinGroup group)
    {
      IIdentifiedProtein result = null;

      foreach (IIdentifiedProtein protein in group)
      {
        if (result == null)
        {
          result = protein;
          continue;
        }

        if (null == protein.Sequence)
        {
          continue;
        }

        if (null == result.Sequence)
        {
          result = protein;
          continue;
        }

        if (result.Sequence.Length < protein.Sequence.Length)
        {
          result = protein;
        }
      }

      return result;
    }

    private IIdentifiedProtein FindShorter(IIdentifiedProteinGroup group)
    {
      IIdentifiedProtein result = null;

      foreach (IIdentifiedProtein protein in group)
      {
        if (result == null)
        {
          result = protein;
          continue;
        }

        if (null == protein.Sequence)
        {
          continue;
        }

        if (null == result.Sequence)
        {
          result = protein;
          continue;
        }

        if (result.Sequence.Length > protein.Sequence.Length)
        {
          result = protein;
        }
      }

      return result;
    }
  }
}