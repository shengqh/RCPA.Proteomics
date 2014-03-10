using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RCPA.Proteomics.Quantification.SILAC
{
  public class SilacQuantificationResultMultipleFileFormat : QuantificationResultMultipleFileFormat
  {
    public SilacQuantificationResultMultipleFileFormat()
      : base(".PepSILACQuant")
    { }
  }
}
