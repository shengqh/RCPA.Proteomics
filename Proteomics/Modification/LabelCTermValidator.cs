using System;
using System.Collections.Generic;
using System.Text;

namespace RCPA.Proteomics.Modification
{
  public class LabelCTermValidator : ILabelValidator
  {
    private string aminoAcids;

    public LabelCTermValidator(string aminoAcids)
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

      char ctermChar = sequence[sequence.Length - 1];

      if (Char.IsLetter(ctermChar))
      {
        return aminoAcids.IndexOf(ctermChar) >= 0;
      }

      if (1 == sequence.Length)
      {
        return false;
      }

      return aminoAcids.IndexOf(sequence[sequence.Length - 2]) >= 0;
    }

    #endregion
  }
}
