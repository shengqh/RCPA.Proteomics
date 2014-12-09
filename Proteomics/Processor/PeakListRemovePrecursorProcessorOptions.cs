using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RCPA.Proteomics.Processor
{
  public class PeakListRemovePrecursorProcessorOptions
  {
    public string NeutralLoss { get; set; }
    public double PPMTolerance { get; set; }
    public bool RemoveChargeMinus1Precursor { get; set; }
    public bool RemoveIsotopicIons { get; set; }
    public bool RemoveIonLargerThanPrecursor { get; set; }

    public PeakListRemovePrecursorProcessorOptions()
    {
      NeutralLoss = string.Empty;
      PPMTolerance = 10;
      RemoveChargeMinus1Precursor = true;
      RemoveIsotopicIons = true;
      RemoveIonLargerThanPrecursor = true;
    }

    public double[] ParseOffsets()
    {
      var acs = this.NeutralLoss.Split(new char[] { ',', ';', ' ', '\t' }, System.StringSplitOptions.RemoveEmptyEntries);
      var masses = new List<double>();
      foreach (var acStr in acs)
      {
        double mass;
        if (double.TryParse(acStr, out mass))
        {
          masses.Add(mass);
        }
        else
        {
          var ac = new AtomComposition(acStr);
          masses.Add(-Atom.GetMonoMass(ac));
        }
      }

      return masses.ToArray();
    }

    public override string ToString()
    {
      return string.Format("RemoveChargeMinus1Precursor={0},RemoveIsotopicIons={1},RemoveIonLargerThanPrecursor={2},PPMTolerance={3},NeutralLoss={4}",
        RemoveChargeMinus1Precursor,
        RemoveIsotopicIons,
        RemoveIonLargerThanPrecursor,
        PPMTolerance,
        NeutralLoss);
    }
  }
}
