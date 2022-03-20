namespace RCPA.Proteomics.Mascot
{
  public class TitleFormatCmpd : AbstractTitleFormat
  {
    public override string Build<T>(Spectrum.PeakList<T> pl)
    {
      return MyConvert.Format("Cmpd {0}, +MSn({1:0.####}), {2:0.00} min",
                           pl.GetFirstScanTime().Scan,
                           pl.PrecursorMZ,
                           pl.GetFirstScanTime().RetentionTime);
    }

    public override string FormatName
    {
      get { return "CMPD"; }
    }

    public override string Example
    {
      get { return "Cmpd {Scan Number}, +MSn({Precursor Mz}), {Retention Time} min"; }
    }
  }
}
