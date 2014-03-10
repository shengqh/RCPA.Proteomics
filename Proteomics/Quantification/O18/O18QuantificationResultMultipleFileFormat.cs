using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RCPA.Proteomics.Quantification.O18
{
  public class O18QuantificationResultMultipleFileFormat : QuantificationResultMultipleFileFormat
  {
    public O18QuantificationResultMultipleFileFormat()
      : base(".PepO18Quant")
    { }
  }
}
