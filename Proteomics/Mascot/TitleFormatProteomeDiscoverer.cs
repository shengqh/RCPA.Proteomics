namespace RCPA.Proteomics.Mascot
{
  public class TitleFormatProteomeDiscoverer : AbstractTitleFormat
  {
    public override string Build<T>(Spectrum.PeakList<T> pkl)
    {
      return MyConvert.Format("{0} Spectrum{1} scans: {1}",
                           pkl.Experimental,
                           pkl.GetFirstScanTime().Scan);
    }

    public override string FormatName
    {
      get { return "ProteomeDiscoverer"; }
    }

    public override string Example
    {
      get { return "{Raw File Name} Spectrum{First Scan Number} scans: {First Scan Number}"; }
    }
  }
}
