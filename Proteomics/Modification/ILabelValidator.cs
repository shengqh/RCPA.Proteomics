using System;
using System.Collections.Generic;
using System.Text;

namespace RCPA.Proteomics.Modification
{
  public interface ILabelValidator
  {
    bool Validate(string sequence);
  }
}
