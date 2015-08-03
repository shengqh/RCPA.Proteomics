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

    public void CombineMS3Spectra(double precursorPPMTolerance, double fragmentPPMTolerance)
    {
      this.MS3Spectra.Sort((m1, m2) => m1.PrecursorMZ.CompareTo(m2.PrecursorMZ));

      //group ms3spectra by precursor m/z
      List<List<PeakList<Peak>>> groups = new List<List<PeakList<Peak>>>();
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

        if (!current.Contains(this.MS3Spectra[i]))
        {
          groups.Add(current);
          current = new List<PeakList<Peak>>();
          current.Add(this.MS3Spectra[i]);
        }
      }
      groups.Add(current);
      groups.Reverse();

      this.MS3Spectra.Clear();
      foreach (var g in groups)
      {
        var pkl = g.First();
        pkl.PrecursorMZ = g.Sum(m => m.PrecursorMZ * m.Sum(p => p.Intensity)) / g.Sum(m => m.Sum(p => p.Intensity));

        for(int i = 0;i < g.Count;i++){
          g[i].FirstScan = i;
        }

        var peaks = (from p in g
                     from peak in p
                     select new Peak() { Mz = peak.Mz, Intensity = peak.Intensity, Tag = p.FirstScan }).OrderBy(m => m.Mz).ToArray();

        //group peak by m/z
        List<List<Peak>> pgroups = new List<List<Peak>>();
        List<Peak> currentPkl = new List<Peak>();
        currentPkl.Add(peaks.First());
        for (int i = 1; i <peaks.Length;i++)
        {
          foreach (var peak in currentPkl)
          {
            var diff = Math.Abs(peaks[i].Mz - peak.Mz);
            var ppmdiff = PrecursorUtils.mz2ppm(peak.Mz, diff);
            if (ppmdiff <= fragmentPPMTolerance)
            {
              currentPkl.Add(peaks[i]);
              break;
            }
          }

          if (!currentPkl.Contains(peaks[i]))
          {
            pgroups.Add(currentPkl);
            currentPkl = new List<Peak>();
            currentPkl.Add(peaks[i]);
          }
        }
        pgroups.Add(currentPkl);

        pkl.Clear();
        foreach (var pg in pgroups)
        {
          if (pg.Count == 1)
          {
            pkl.Add(pg.First());
          }
          else
          {
            //for each spectrum, only the peak with most intensity will be used for merge
            var waiting = (from t in pg.GroupBy(m => m.Tag)
                           select (from tt in t
                                   orderby tt.Intensity descending
                                   select tt).First()).ToArray();

            var combinedpeak = new Peak()
            {
              Mz = waiting.Sum(m => m.Mz * m.Intensity) / waiting.Sum(m => m.Intensity),
              Intensity = waiting.Sum(m => m.Intensity)
            };

            pkl.Add(combinedpeak);
          }
        }

        this.MS3Spectra.Add(pkl);
      }
    }
  }
}
