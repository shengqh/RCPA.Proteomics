using System.Collections.Generic;
using RCPA.Seq;

namespace RCPA.Proteomics
{
  public interface IRangeLocationFilter
  {
    void SetSequence(Sequence sequence);
    bool Accept(RangeLocation rl);
  }

  public class NGlycanFilter : IRangeLocationFilter
  {
    private bool[] isNglycan;

    public bool[] IsNglycan
    {
      get { return this.isNglycan; }
    }

    #region IRangeLocationFilter Members

    public void SetSequence(Sequence sequence)
    {
      this.isNglycan = GetNglycanLocation(sequence.SeqString);
    }

    public bool Accept(RangeLocation rl)
    {
      for (int index = rl.Min - 1; index <= rl.Max - 1; index++)
      {
        if (this.isNglycan[index])
        {
          return true;
        }
      }
      return false;
    }

    #endregion

    public static bool[] GetNglycanLocation(string sequence)
    {
      var result = new bool[sequence.Length];
      for (int i = 0; i < result.Length; i++)
      {
        result[i] = false;
      }

      for (int i = 0; i < sequence.Length - 2; i++)
      {
        if (sequence[i] == 'N')
        {
          if ('P' == sequence[i + 1] ||
              'D' == sequence[i + 1])
          {
            continue;
          }

          if ('S' == sequence[i + 2] ||
              'T' == sequence[i + 2])
          {
            result[i] = true;
          }
        }
      }

      return result;
    }
  }

  public class PeptideWeightFilter : IRangeLocationFilter
  {
    private readonly Aminoacids aminoacids;
    private readonly double maxWeight;
    private readonly double minWeight;
    private string sequence;

    public PeptideWeightFilter(Aminoacids aminoacids, double minWeight, double maxWeight)
    {
      this.aminoacids = aminoacids;
      this.minWeight = minWeight;
      this.maxWeight = maxWeight;
    }

    #region IRangeLocationFilter Members

    public void SetSequence(Sequence sequence)
    {
      this.sequence = sequence.SeqString;
    }

    public bool Accept(RangeLocation rl)
    {
      string peptide = this.sequence.Substring(rl.Min - 1, rl.Max - rl.Min + 1);

      double mass = this.aminoacids.AveragePeptideMass(peptide);

      return mass >= this.minWeight && mass <= this.maxWeight;
    }

    #endregion
  }

  public class PeptideLengthFilter : IRangeLocationFilter
  {
    private readonly int minLength;

    public PeptideLengthFilter(int minLength)
    {
      this.minLength = minLength;
    }

    #region IRangeLocationFilter Members

    public void SetSequence(Sequence sequence)
    {
    }

    public bool Accept(RangeLocation rl)
    {
      return rl.Max - rl.Min + 1 >= this.minLength;
    }

    #endregion
  }

  public class AndRangeLocationFilter : IRangeLocationFilter
  {
    private readonly List<IRangeLocationFilter> filters = new List<IRangeLocationFilter>();

    #region IRangeLocationFilter Members

    public void SetSequence(Sequence sequence)
    {
      foreach (IRangeLocationFilter filter in this.filters)
      {
        filter.SetSequence(sequence);
      }
    }

    public bool Accept(RangeLocation rl)
    {
      foreach (IRangeLocationFilter filter in this.filters)
      {
        if (!filter.Accept(rl))
        {
          return false;
        }
      }

      return true;
    }

    #endregion

    public void AddFilter(IRangeLocationFilter filter)
    {
      this.filters.Add(filter);
    }
  } ;

  public class ExceptAminoacidFilter : IRangeLocationFilter
  {
    private readonly char[] exceptAminoacids;
    private string sequence;

    public ExceptAminoacidFilter(char[] exceptAminoacids)
    {
      this.exceptAminoacids = exceptAminoacids;
    }

    #region IRangeLocationFilter Members

    public void SetSequence(Sequence sequence)
    {
      this.sequence = sequence.SeqString;
    }

    public bool Accept(RangeLocation rl)
    {
      string peptide = this.sequence.Substring(rl.Min - 1, rl.Max - rl.Min + 1);
      if (0 <= peptide.IndexOfAny(this.exceptAminoacids))
      {
        return false;
      }
      return true;
    }

    #endregion
  }
}