using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using RCPA.Gui;
using RCPA.Proteomics.Raw;
using RCPA.Proteomics.Spectrum;

namespace RCPA.Proteomics.Quantification.ITraq
{
  public class ITraqRawHCDParallelMS3Reader : AbstractITraqRawHCDMS3Reader
  {
    public ITraqRawHCDParallelMS3Reader() : base("HCD-Parallel-MS3") { }

    protected override IScanLevelBuilder GetScanLevelBuilder()
    {
      return new ScanLevelParallelMS3Builder();
    }

    public override string ToString()
    {
      return Name + " : MS1->MS2->MS2->MS3->MS3";
    }

    public override bool IsTandemMS3
    {
      get
      {
        return true;
      }
    }
  }
}
