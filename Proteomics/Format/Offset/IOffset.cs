using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using RCPA.Gui;

namespace RCPA.Proteomics.Format.Offset
{
  public interface IOffset : IProgress
  {
    double GetPrecursorOffset(string fileName, int scan);

    double GetProductIonOffset(string fileName, int scan);
  }
}
