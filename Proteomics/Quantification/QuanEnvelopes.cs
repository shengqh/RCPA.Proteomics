using RCPA.Proteomics.Quantification.O18;
using RCPA.Proteomics.Spectrum;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RCPA.Proteomics.Quantification
{
  public class QuanEnvelopes<T> : List<T> where T : QuanEnvelope
  {
    public string GetScanRange()
    {
      var scanList = from pkl in this
                     let s = pkl.Scan
                     orderby s
                     select s;

      return MyConvert.Format("{0}-{1}", scanList.Min(), scanList.Max());
    }

    public string GetScanString()
    {
      StringBuilder sbCurr = new StringBuilder();
      foreach (PeakList<Peak> pkl in this)
      {
        sbCurr.Append("_" + pkl.ScanTimes[0].Scan);
      }
      return sbCurr.ToString();
    }
  }

  public class O18QuanEnvelopes : QuanEnvelopes<O18QuanEnvelope>
  { }
}
