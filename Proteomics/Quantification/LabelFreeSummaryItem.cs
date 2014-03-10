using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RCPA.Proteomics.Quantification
{
  public class LabelFreeSummaryItem : List<LabelFreeItem>
  {
    public string RawFilename { get; set; }

    public string Sequence { get; set; }

    public void CalculatePPM(double referencePrecursorMz)
    {
      this.ForEach(m => m.DeltaMzPPM = PrecursorUtils.mz2ppm(referencePrecursorMz, m.Mz - referencePrecursorMz));
    }

    public double GetArea()
    {
      return (from c in this
              where c.Enabled
              select c.AdjustIntensity).Sum();
    }
  }
}
