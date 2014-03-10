using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using RCPA.Gui;
using RCPA.Proteomics.Raw;
using RCPA.Proteomics.Spectrum;

namespace RCPA.Proteomics.Quantification.ITraq
{
  /// <summary>
  /// MS1->MS2->MS3->MS2->MS3
  /// </summary>
  public class ITraqRawHCDMS3Reader : AbstractITraqRawHCDMS3Reader
  {
    public ITraqRawHCDMS3Reader() : base("HCD-MS3") { }

    protected override IScanLevelBuilder GetScanLevelBuilder()
    {
      return new ScanLevelBuilder();
    }

    public override string ToString()
    {
      return Name + " : MS1->MS2->MS3->MS2->MS3";
    }
  }
}
