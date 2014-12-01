using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RCPA.Proteomics.Mascot
{
  public class TitleFormatSequest : AbstractTitleFormat
  {
    public override string Build<T>(Spectrum.PeakList<T> pkl)
    {
      return MyConvert.Format("{0}.{1}.{2}.{3}.dta",
                           pkl.Experimental,
                           pkl.GetFirstScanTime().Scan,
                           pkl.GetLastScanTime().Scan,
                           pkl.PrecursorCharge);
    }

    public override string FormatName
    {
      get { return "SEQUEST"; }
    }

    public override string Example
    {
      get { return "{Raw File Name}.{First Scan Number}.{Last Scan Number}.{Precursor Charge}.dta"; }
    }
  }
}
