using System;

namespace RCPA.Proteomics.Modification
{
  public interface IModificationCountCalculator
  {
    /// <summary>
    /// Get modification count from matched sequence
    /// </summary>
    /// <param name="sequence">Matched sequence</param>
    /// <returns>Modification count</returns>
    int Calculate(string sequence);
  }

  public class ModificationCountForwardCalculator : IModificationCountCalculator
  {
    private readonly int defaultCount;

    public ModificationCountForwardCalculator(int defaultCount)
    {
      this.defaultCount = defaultCount;
    }

    #region IModificationCountCalculator Members

    public int Calculate(string sequence)
    {
      return this.defaultCount;
    }

    #endregion
  }

  public class ModificationCountCalculator : IModificationCountCalculator
  {
    private readonly string _modifiedAminoacids;

    private readonly int maxCount;

    private bool hasNterminal;

    public ModificationCountCalculator(string modifiedAminoacids)
      : this(modifiedAminoacids, int.MaxValue) { }

    public ModificationCountCalculator(string modifiedAminoacids, int maxCount)
    {
      this._modifiedAminoacids = modifiedAminoacids;
      this.maxCount = maxCount;
      this.hasNterminal = modifiedAminoacids.Contains("(") || modifiedAminoacids.Contains("[");
    }

    #region IModificationCountCalculator Members

    public int Calculate(string sequence)
    {
      if (null == this._modifiedAminoacids || this._modifiedAminoacids.Length == 0)
      {
        return 0;
      }

      int result = 0;

      for (int i = 0; i < sequence.Length; i++)
      {
        if (Char.IsUpper(sequence[i]))
        {
          continue;
        }

        if (i == 0)
        {
          if (hasNterminal)
          {
            result++;
          }
        }
        else if (this._modifiedAminoacids.IndexOf(sequence[i - 1]) >= 0)
        {
          result++;
        }
      }

      if (result > this.maxCount)
      {
        return this.maxCount;
      }
      else
      {
        return result;
      }
    }

    #endregion
  }
}