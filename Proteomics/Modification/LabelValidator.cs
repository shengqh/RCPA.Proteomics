using System;
using System.Collections.Generic;
using System.Text;

namespace RCPA.Proteomics.Modification
{
  public enum LabelPosition { ALL, NTERM, CTERM };

  public class LabelValidator : ILabelValidator
  {
    private ILabelValidator validator;

    public LabelValidator(string aminoAcids, LabelPosition position)
    {
      switch (position)
      {
        case LabelPosition.NTERM:
          validator = new LabelNTermValidator(aminoAcids);
          break;
        case LabelPosition.CTERM:
          validator = new LabelCTermValidator(aminoAcids);
          break;
        default:
          validator = new LabelAllValidator(aminoAcids);
          break;
      }
    }

    #region ILabelValidator Members

    public bool Validate(string sequence)
    {
      return validator.Validate(sequence);
    }

    #endregion
  }
}
