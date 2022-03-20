using RCPA.Proteomics.Spectrum;
using System;
using System.Collections.Generic;

namespace RCPA.Proteomics.Quantification.O18
{
  public class O18QuanEnvelope : QuanEnvelope
  {
    public O18QuanEnvelope()
    { }

    public O18QuanEnvelope(PeakList<Peak> source)
      : base(source)
    { }

    public SpeciesAbundanceInfo SpeciesAbundance { get; set; }

    public SampleAbundanceInfo SampleAbundance { get; set; }

    public void CalculateFromProfile(List<Peak> profile, ISampleAbundanceCalculator calc)
    {
      var top2percentage = profile[0].Intensity + profile[1].Intensity;

      this.SpeciesAbundance = new SpeciesAbundanceInfo();
      this.SpeciesAbundance.O16 = (this[0].Intensity + this[1].Intensity) / top2percentage;

      var intensity3 = Math.Max(0, this[2].Intensity - this.SpeciesAbundance.O16 * profile[2].Intensity);
      var intensity4 = Math.Max(0, this[3].Intensity - this.SpeciesAbundance.O16 * profile[3].Intensity);

      this.SpeciesAbundance.O181 = (intensity3 + intensity4) / top2percentage;

      var intensity5 = Math.Max(0, this[4].Intensity - this.SpeciesAbundance.O16 * profile[4].Intensity - this.SpeciesAbundance.O181 * profile[2].Intensity);
      var intensity6 = Math.Max(0, this[5].Intensity - this.SpeciesAbundance.O16 * profile[5].Intensity - this.SpeciesAbundance.O181 * profile[3].Intensity);

      this.SpeciesAbundance.O182 = (intensity5 + intensity6) / top2percentage;

      this.SampleAbundance = calc.Calculate(this.SpeciesAbundance);
    }
  }
}
