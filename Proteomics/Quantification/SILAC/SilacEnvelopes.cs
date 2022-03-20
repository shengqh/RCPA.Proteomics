using System.Collections.Generic;
using System.Text;

namespace RCPA.Proteomics.Quantification.SILAC
{
  public class SilacEnvelopes : List<SilacPeakListPair>
  {
    public bool ContainScan(int scan)
    {
      if (Count == 0)
      {
        return false;
      }

      if (this[0].Light.ScanTimes[0].Scan <= scan &&
          this[Count - 1].Light.ScanTimes[0].Scan >= scan)
      {
        return true;
      }

      return false;
    }

    public string GetScanString()
    {
      StringBuilder sb = new StringBuilder();
      foreach (SilacPeakListPair item in this)
      {
        sb.Append("_" + item.Light.ScanTimes[0].Scan);
      }
      return sb.ToString();
    }

    public SilacPeakListPair FindScan(int scan)
    {
      return this.Find(m => m.Scan == scan);
    }

    public void SetScanIdentified(int scan, bool isExtendedIdentification)
    {
      var pair = FindScan(scan);

      if (null != pair)
      {
        pair.IsIdentified = true;
        pair.IsExtendedIdentification = isExtendedIdentification;
      }
    }
  }
}