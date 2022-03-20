using RCPA.Proteomics.Summary;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RCPA.Tools.Quantification
{
  public class QuantificationChromotograph : List<QuantificationScan>
  {
    private List<IIdentifiedSpectrum> spectra = new List<IIdentifiedSpectrum>();

    public List<IIdentifiedSpectrum> IdentifiedSpectra { get { return spectra; } }

    public bool ContainScan(int scan)
    {
      if (this.Count == 0)
      {
        return false;
      }

      if (this[0].Scan > scan)
      {
        return false;
      }

      if (this[this.Count - 1].Scan < scan)
      {
        return false;
      }

      return true;
    }

    public void SetScanIdentified(int startScan)
    {
      for (int i = 1; i < this.Count; i++)
      {
        if (this[i].Scan > startScan)
        {
          this[i - 1].IsIdentified = true;
          return;
        }
      }

      this.Last().IsIdentified = true;
    }

    public string GetScanString()
    {
      StringBuilder result = new StringBuilder();
      foreach (var qScan in this)
      {
        result.Append("_" + qScan.Scan);
      }
      return result.ToString();
    }
  }
}
