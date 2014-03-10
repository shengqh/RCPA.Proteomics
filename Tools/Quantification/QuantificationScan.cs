using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using RCPA.Proteomics;
using RCPA.Proteomics.Spectrum;

namespace RCPA.Tools.Quantification
{
  public class QuantificationScan
  {
    public int Scan { get; set; }

    public bool IsIdentified { get; set; }

    public bool IsSelected { get; set; }

    public bool Enabled { get; set; }

    public List<QuantificationEnvelope> Envelopes { get; set; }

    public QuantificationScan()
    {
      this.Enabled = true;
      this.IsIdentified = false;
      this.IsSelected = false;
      this.Envelopes = new List<QuantificationEnvelope>();
    }
  }

  public static class QuantificationScanExntensiton
  {
    public static QuantificationScan GetQuantificationScan(this PeakList<Peak> observed, List<Envelope> theoretical, double mzTolerance)
    {
      QuantificationScan result = new QuantificationScan();
      result.Envelopes = new List<QuantificationEnvelope>();
      foreach (var mz in theoretical)
      {
        var pkl = observed.FindEnvelopeDirectly(mz, mzTolerance, () => new Peak());
        result.Envelopes.Add(new QuantificationEnvelope(mz.ToArray(), pkl.ToArray()));
      }
      return result;
    }

    public static QuantificationScan GetQuantificationScan(this PeakList<Peak> observed, List<double> theoretical, int charge, double mzTolerance, int profileLength)
    {
      var envs = theoretical.ConvertAll(m => new Envelope(m, charge, profileLength));
      return GetQuantificationScan(observed, envs, mzTolerance);
    }
  }
}
