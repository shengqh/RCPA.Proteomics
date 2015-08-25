using RCPA.Proteomics.Spectrum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RCPA.Proteomics.Snp
{
  public class MS2Item
  {
    public MS2Item()
    {
      MS3Spectra = new List<PeakList<Peak>>();
      Peptide = string.Empty;
      Modification = string.Empty;
      ModifiedPeptide = string.Empty;
      CombinedCount = 1;
    }

    public string Peptide { get; set; }
    public SequestFilename FileScan { get; set; }
    public string Modification { get; set; }
    public double Precursor { get; set; }
    public int Charge { get; set; }
    public string ModifiedPeptide { get; set; }
    public Aminoacids Aminoacids { get; set; }
    public Dictionary<IonType, List<IonTypePeak>> Spectra { get; set; }
    public Dictionary<char, string> ModificationNameMap { get; set; }
    public List<PeakList<Peak>> MS3Spectra { get; private set; }

    public void CombineMS3Spectra(IBestSpectrumBuilder builder, double precursorPPMTolerance)
    {
      List<List<PeakList<Peak>>> groups = GroupMS3SpectraByPrecursor(precursorPPMTolerance);

      this.MS3Spectra.Clear();

      foreach (var g in groups)
      {
        var pkl = builder.Build(g);
        this.MS3Spectra.Add(pkl);
      }
    }

    public List<List<PeakList<Peak>>> GroupMS3SpectraByPrecursor(double precursorPPMTolerance)
    {
      List<List<PeakList<Peak>>> result = new List<List<PeakList<Peak>>>();

      this.MS3Spectra.ForEach(m =>
      {
        m.Tag = m.CombinedCount == 0 ? 1 : m.CombinedCount;
        m.ForEach(p => p.CombinedCount = p.CombinedCount == 0 ? 1 : p.CombinedCount);
      });

      this.MS3Spectra.Sort((m1, m2) => m1.PrecursorMZ.CompareTo(m2.PrecursorMZ));

      //group ms3spectra by precursor m/z
      List<PeakList<Peak>> current = new List<PeakList<Peak>>();
      current.Add(this.MS3Spectra.Last());
      for (int i = this.MS3Spectra.Count - 2; i >= 0; i--)
      {
        foreach (var spectra in current)
        {
          var diff = Math.Abs(this.MS3Spectra[i].PrecursorMZ - spectra.PrecursorMZ);
          var ppmdiff = PrecursorUtils.mz2ppm(spectra.PrecursorMZ, diff);
          if (ppmdiff <= precursorPPMTolerance)
          {
            current.Add(this.MS3Spectra[i]);
            break;
          }
        }

        if (this.MS3Spectra[i] != current.Last())
        {
          result.Add(current);
          current = new List<PeakList<Peak>>();
          current.Add(this.MS3Spectra[i]);
        }
      }
      result.Add(current);
      result.Reverse();

      return result;
    }

    public int CombinedCount { get; set; }

    public char[] AminoacidCompsition { get; set; }
  }
}
