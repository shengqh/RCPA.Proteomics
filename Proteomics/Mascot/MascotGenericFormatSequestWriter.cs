using RCPA.Proteomics.Spectrum;
using System;
namespace RCPA.Proteomics.Mascot
{
  [Serializable]
  public class MascotGenericFormatSequestWriter<T> : MascotGenericFormatWriter<T> where T : IPeak
  {
    public MascotGenericFormatSequestWriter(int[] defaultCharges)
      : base(defaultCharges)
    {
    }

    public MascotGenericFormatSequestWriter()
    {
    }

    public override string GetFormatName()
    {
      return "MascotGenericSequestFormat";
    }

    public override string GetTitle(PeakList<T> pl)
    {
      if (pl.Annotations.ContainsKey(MascotGenericFormatConstants.TITLE_TAG))
      {
        return pl.Annotations[MascotGenericFormatConstants.TITLE_TAG].ToString();
      }
      else
      {
        return MyConvert.Format("{0}.{1}.{2}.{3}.dta",
                             pl.Experimental,
                             pl.GetFirstScanTime().Scan,
                             pl.GetLastScanTime().Scan,
                             pl.PrecursorCharge);
      }
    }
  }
}