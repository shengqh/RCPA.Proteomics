using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RCPA.Proteomics.Quantification.IsobaricLabelling
{
  public class IsobaricResultMultipleFileDistillerOptions : AbstractIsobaricResultFileDistillerOptions
  {
    public string[] RawFiles { get; set; }

    public bool Individual { get; set; }
  }
}
