using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RCPA.Proteomics.Raw
{
  public class RawReadException:Exception
  {
    public RawReadException(int scan)
    {
      this.Scan = scan;
    }

    public int Scan { get; private set; }
  }
}
