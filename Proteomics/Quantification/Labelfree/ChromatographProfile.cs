using RCPA.Proteomics.Isotopic;
using RCPA.Proteomics.Spectrum;
using System.Collections.Generic;
using System.Linq;

namespace RCPA.Proteomics.Quantification.Labelfree
{
  public class IsotopicIon : Peak
  {
    public double MinimumMzWithinTolerance { get; set; }

    public double MaximumMzWithinTolerance { get; set; }
  }

  public class ChromatographProfile
  {
    private static IIsotopicProfileBuilder2 profileBuilder = new EmassProfileBuilder();

    private static Aminoacids aas = new Aminoacids();

    public string Experimental { get; set; }

    public int IdentifiedScan { get; set; }

    public double IdentifiedRetentionTime { get; set; }

    public string Sequence { get; set; }

    public double ObservedMz { get; set; }

    public double TheoreticalMz { get; set; }

    public int Charge { get; set; }

    public string FileName { get; set; }

    public string SubFileName { get; set; }

    public IsotopicIon[] IsotopicIons { get; set; }

    public double[] IsotopicIntensities { get; private set; }

    public List<ChromatographProfileScan> Profiles { get; private set; }

    public ChromatographProfile()
    {
      this.Profiles = new List<ChromatographProfileScan>();
    }

    public void InitializeIsotopicIons(double ppmTolerance, double minimumPercentage = 0.05, int maxProfileLength = int.MaxValue)
    {
      var atomComposition = aas.GetPeptideAtomComposition(this.Sequence);
      var profiles = profileBuilder.GetProfile(atomComposition, this.Charge, minimumPercentage).Take(maxProfileLength);
      this.IsotopicIons = (from peak in profiles
                           select new IsotopicIon()
                           {
                             Mz = peak.Mz,
                             Intensity = peak.Intensity,
                           }).ToArray();
      var diff = this.TheoreticalMz - this.IsotopicIons[0].Mz;
      //Replace the ion m/z to real ion m/z but keep the distance between ions in case of the peptide contains modification
      foreach (var ion in this.IsotopicIons)
      {
        ion.Mz += diff;
        var mzdiff = PrecursorUtils.ppm2mz(ion.Mz, ppmTolerance);
        ion.MinimumMzWithinTolerance = ion.Mz - mzdiff;
        ion.MaximumMzWithinTolerance = ion.Mz + mzdiff;
      }

      this.IsotopicIntensities = (from ion in this.IsotopicIons
                                  select ion.Intensity).ToArray();
    }

    public void InitalizePPMTolerance()
    {
      foreach (var peaks in Profiles)
      {
        for (int i = 0; i < peaks.Count; i++)
        {
          peaks[i].PPMDistance = PrecursorUtils.mz2ppm(peaks[i].Mz, IsotopicIons[i].Mz - peaks[i].Mz);
        }
      }
    }

    public override string ToString()
    {
      return MyConvert.Format("[{0}-{1} : {2:0.0000}, {3}]", this.Experimental, this.Sequence, this.TheoreticalMz, this.Charge);
    }

    public string GetSourceFileScan()
    {
      return new SequestFilename(this.Experimental, this.IdentifiedScan, this.IdentifiedScan, this.Charge, "").ShortFileName;
    }

    public string GetPeptideId()
    {
      return string.Format("{0}_{1:0.0000}", this.Sequence, this.TheoreticalMz);
    }

  }
}
