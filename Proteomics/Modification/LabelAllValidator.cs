namespace RCPA.Proteomics.Modification
{
  /// <summary>
  /// 要求指定氨基酸全部被修饰或者全部没有被修饰
  /// </summary>
  public class LabelAllValidator : ILabelValidator
  {
    private string aminoAcids;

    public LabelAllValidator(string aminoAcids)
    {
      this.aminoAcids = aminoAcids;
    }

    #region ILabelValidator Members

    public bool Validate(string sequence)
    {
      if (sequence == null || sequence.Length == 0)
      {
        return false;
      }

      int specialAminoAcidCount = 0;
      int modifiedAminoAcidCount = 0;
      int i = 0;
      while (i < sequence.Length)
      {
        if (aminoAcids.IndexOf(sequence[i]) < 0)
        {
          i++;
          continue;
        }

        specialAminoAcidCount++;
        i++;

        if (i < sequence.Length)
        {
          if (ModificationUtils.IsModification(sequence[i]))
          {
            modifiedAminoAcidCount++;
            i++;
          }
        }
      }

      if (0 == specialAminoAcidCount)
      {
        return false;
      }

      return 0 == modifiedAminoAcidCount || modifiedAminoAcidCount == specialAminoAcidCount;
    }

    #endregion
  }
}
