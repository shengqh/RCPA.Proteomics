using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RCPA.Proteomics.Mascot
{
  public class TitleFormatRawCmpd : AbstractTitleFormat
  {
    public override string Build<T>(Spectrum.PeakList<T> pl)
    {
      return MyConvert.Format("{0}, Cmpd {1}, +MSn({2:0.####}), {3:0.00} min",
                           pl.Experimental,
                           pl.GetFirstScanTime().Scan,
                           pl.PrecursorMZ,
                           pl.GetFirstScanTime().RetentionTime);
    }

    public override string FormatName
    {
      get { return "RAWCMPD"; }
    }

    public override string Example
    {
      get { return "{Raw File Name}, Cmpd {Scan Number}, +MSn({Precursor Mz}), {Retention Time} min"; }
    }
  }
}
