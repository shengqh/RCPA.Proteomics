using System.Collections.Generic;

namespace RCPA.Proteomics.Processor
{
  public class PeakListRemovePrecursorProcessorOptions
  {
    public bool RemovePrecursor { get; set; }
    public double PPMTolerance { get; set; }
    public bool RemoveNeutralLoss { get; set; }
    public string NeutralLoss { get; set; }
    public bool RemoveChargeMinus1Precursor { get; set; }
    public bool RemoveIsotopicIons { get; set; }
    public bool RemoveIonLargerThanPrecursor { get; set; }

    public PeakListRemovePrecursorProcessorOptions()
    {
      RemovePrecursor = false;
      RemoveNeutralLoss = true;
      RemoveChargeMinus1Precursor = true;
      RemoveIsotopicIons = true;
      RemoveIonLargerThanPrecursor = true;
      NeutralLoss = "NH3,H2O,";
      PPMTolerance = 10;
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
      return string.Format("RemovePrecursor={0},RemoveNeutralLoss={1},RemoveChargeMinus1Precursor={2},RemoveIsotopicIons={3},RemoveIonLargerThanPrecursor={4},PPMTolerance={5},NeutralLoss={6}",
        RemovePrecursor,
        RemoveNeutralLoss,
        RemoveChargeMinus1Precursor,
        RemoveIsotopicIons,
        RemoveIonLargerThanPrecursor,
        PPMTolerance,
        NeutralLoss);
    }
  }
}
