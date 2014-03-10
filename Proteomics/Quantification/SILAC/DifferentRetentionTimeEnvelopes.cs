using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using RCPA.Proteomics.Spectrum;

namespace RCPA.Proteomics.Quantification.SILAC
{
  public class DifferentRetentionTimeEnvelopes : List<SilacEnvelopes>
  {
    public List<Peak> LightProfile { get; set; }

    public List<Peak> HeavyProfile { get; set; }

    public SilacEnvelopes FindSilacEnvelope(int scan)
    {
      return this.Find(m => m.ContainScan(scan));
    }
  }
}
