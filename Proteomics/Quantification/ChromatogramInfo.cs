using System.Collections.Generic;

namespace RCPA.Proteomics.Quantification
{
  public class ChromatogramInfoComparer : IComparer<ChromatogramInfo>
  {
    #region IComparer<ChromatogramInfo> Members

    public int Compare(ChromatogramInfo ciX, ChromatogramInfo ciY)
    {
      if (ciX == null && ciY == null)
      {
        return 0;
      }
      else if (ciX == null && ciY != null)
      {
        return -1;
      }
      else if (ciX != null && ciY == null)
      {
        return 1;
      }
      else
      {
        return ciX.Scan.CompareTo(ciY.Scan);
      }
    }

    #endregion
  }

  public class ChromatogramInfo
  {
    public int Scan { get; set; }

    public double RetationTime { get; set; }

    public double SampleIntensity { get; set; }

    public double ReferenceIntensity { get; set; }
  }
}