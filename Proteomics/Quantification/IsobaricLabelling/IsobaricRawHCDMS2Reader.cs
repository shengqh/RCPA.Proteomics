using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using RCPA.Gui;
using RCPA.Proteomics.Raw;
using RCPA.Proteomics.Spectrum;

namespace RCPA.Proteomics.Quantification.IsobaricLabelling
{
  public class IsobaricRawHCDMS2Reader : AbstractIsobaricRawReader
  {
    public IsobaricRawHCDMS2Reader() : base(2, "HCD-MS2") { }

    protected override string[] GetScanMode()
    {
      return new string[] { "HCD" };
    }

    public override string ToString()
    {
      return Name + " : MS1->MS2->MS2";
    }
  }
}
