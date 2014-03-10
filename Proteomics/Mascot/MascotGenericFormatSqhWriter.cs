using RCPA.Proteomics.Spectrum;
using System;
namespace RCPA.Proteomics.Mascot
{
  [Serializable]
  public class MascotGenericFormatSqhWriter<T> : MascotGenericFormatWriter<T> where T : IPeak
  {
    public MascotGenericFormatSqhWriter(int[] defaultCharges)
      : base(defaultCharges)
    {
    }

    public MascotGenericFormatSqhWriter()
    {
    }

    public override string GetFormatName()
    {
      return "MascotGenericSqhFormat";
    }

    public override string GetTitle(PeakList<T> pl)
    {
      if (pl.Annotations.ContainsKey(MascotGenericFormatConstants.TITLE_TAG))
      {
        return pl.Annotations[MascotGenericFormatConstants.TITLE_TAG].ToString();
      }
      else
      {
        return MyConvert.Format("{0}, Cmpd {1}, +MSn({2:0.####}), {3:0.00} min",
                             pl.Experimental,
                             pl.GetFirstScanTime().Scan,
                             pl.PrecursorMZ,
                             pl.GetFirstScanTime().RetentionTime);
      }
    }
  }
}