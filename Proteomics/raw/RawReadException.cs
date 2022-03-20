using System;

namespace RCPA.Proteomics.Raw
{
  public class RawReadException : Exception
  {
    public RawReadException(int scan)
    {
      this.Scan = scan;
    }

    public int Scan { get; private set; }
  }
}
