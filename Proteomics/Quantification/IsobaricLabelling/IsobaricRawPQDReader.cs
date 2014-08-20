using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using RCPA.Gui;
using RCPA.Proteomics.Raw;
using RCPA.Proteomics.Spectrum;

namespace RCPA.Proteomics.Quantification.IsobaricLabelling
{
  public class IsobaricRawPQDReader : AbstractIsobaricRawReader
  {
    public IsobaricRawPQDReader() : base(2, "PQD") { }

    protected override string[] GetScanMode()
    {
      return new string[] { "PQD" };
    }

    public override string ToString()
    {
      return this.Name + " : MS1->PQD->PQD";
    }
  }
}
