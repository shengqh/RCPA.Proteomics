using System;

namespace RCPA.Proteomics.Modification
{
  public interface IModificationCountCalculator
  {
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

    public ModificationCountCalculator(string modifiedAminoacids)
    {
      this._modifiedAminoacids = modifiedAminoacids;
      this.maxCount = int.MaxValue;
    }

    public ModificationCountCalculator(string modifiedAminoacids, int maxCount)
    {
      this._modifiedAminoacids = modifiedAminoacids;
      this.maxCount = maxCount;
    }

    #region IModificationCountCalculator Members

    public int Calculate(string sequence)
    {
      if (null == this._modifiedAminoacids || this._modifiedAminoacids.Length == 0)
      {
        return 0;
      }

      int result = 0;

      for (int i = 1; i < sequence.Length; i++)
      {
        if (Char.IsUpper(sequence[i]) || sequence[i] == '.')
        {
          continue;
        }

        if (this._modifiedAminoacids.IndexOf(sequence[i - 1]) >= 0)
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