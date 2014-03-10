using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RCPA.Proteomics.Raw
{
  public interface IMasterScanFinder
  {
    int Find(IRawFile reader, int scan);
  }
}
