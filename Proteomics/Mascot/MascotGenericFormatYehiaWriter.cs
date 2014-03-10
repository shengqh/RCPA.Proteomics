using RCPA.Proteomics.Spectrum;
using System;
namespace RCPA.Proteomics.Mascot
{
  [Serializable]
  public class MascotGenericFormatYehiaWriter<T> : MascotGenericFormatWriter<T> where T : IPeak, new()
  {
    public MascotGenericFormatYehiaWriter(int[] defaultCharges)
      : base(defaultCharges)
    { }

    public MascotGenericFormatYehiaWriter()
    { }

    public override string GetFormatName()
    {
      return "MascotGenericYehiaFormat";
    }

    public override string GetTitle(PeakList<T> pl)
    {
      return MyConvert.Format("Cmpd {0}, +MSn({1:0.####}), {2:0.00} min",
                           pl.GetFirstScanTime().Scan,
                           pl.PrecursorMZ,
                           pl.GetFirstScanTime().RetentionTime);
    }
  }
}