using System;
using System.Collections.Generic;
using System.Text;

namespace RCPA.Proteomics.Modification
{
  public class LabelNTermValidator : ILabelValidator
  {
    private string aminoAcids;

    public LabelNTermValidator(string aminoAcids)
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

      char ntermChar = sequence[0];

      return aminoAcids.IndexOf(ntermChar) >= 0;
    }

    #endregion
  }
}
