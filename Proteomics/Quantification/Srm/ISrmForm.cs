using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RCPA.Proteomics.Quantification.Srm
{
  public interface ISrmForm
  {
    object GetCurrentFocusedObject();
    void SetCurrentFocusedObject(object obj);
  }
}
