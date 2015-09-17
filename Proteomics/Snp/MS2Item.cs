using RCPA.Proteomics.Spectrum;
using RCPA.Proteomics.Utils;
using RCPA.Seq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RCPA.Proteomics.Snp
{
  public class TerminalLossItem
  {
    public bool IsNterminal { get; set; }
    public string Sequence { get; set; }
    public double Precursor { get; set; }
    public TerminalLossItem(bool isNterminal, string seq, double precursor)
    {
      this.IsNterminal = isNterminal;
      this.Sequence = seq;
      this.Precursor = precursor;
    }
  }
  public class MS2Item
  {
    public MS2Item()
    {
      Peptide = string.Empty;
      Modification = string.Empty;
      ModifiedPeptide = string.Empty;
      CombinedCount = 1;
      FileScan = string.Empty;
      MS3Spectra = new List<PeakList<Peak>>();
      TerminalLoss = new List<TerminalLossItem>();
    }

    public string Peptide { get; set; }
    public string Modification { get; set; }
    public double Precursor { get; set; }
    public int Charge { get; set; }
    public string ModifiedPeptide { get; set; }
    public Aminoacids Aminoacids { get; set; }
    public Dictionary<IonType, List<IonTypePeak>> Spectra { get; set; }
    public Dictionary<char, string> ModificationNameMap { get; set; }
    public List<PeakList<Peak>> MS3Spectra { get; private set; }

    public List<TerminalLossItem> TerminalLoss { get; private set; }
    public void InitNterminalLoss(Aminoacids aa)
    {
      this.TerminalLoss = new List<TerminalLossItem>();

      var seq = PeptideUtils.GetMatchedSequence(this.Peptide);
      var pureseq = PeptideUtils.GetMatchedSequence(this.Peptide);

      var pos = 0;
      if (!char.IsUpper(seq[pos])) // ignore the Nterminal modification
      {
        pos++;
      }

      if (char.IsUpper(seq[pos + 1])) //if the first base is not modified
      {
        var precursorLoss1 = this.Precursor - aa[seq[pos]].MonoMass / this.Charge;
        this.TerminalLoss.Add(new TerminalLossItem(true, pureseq.Substring(1), precursorLoss1));
        if (char.IsUpper(seq[pos + 2])) //if the second base is not modified
        {
          var precursorLoss2 = precursorLoss1 - aa[seq[pos + 1]].MonoMass / this.Charge;
          this.TerminalLoss.Add(new TerminalLossItem(true, pureseq.Substring(2), precursorLoss2));
        }
      }

      pos = seq.Length - 1;
      if (char.IsUpper(seq[pos])) // if the last base is not modified
      {
        var precursorLoss1 = this.Precursor - aa[seq[pos]].MonoMass / this.Charge;
        this.TerminalLoss.Add(new TerminalLossItem(false, pureseq.Substring(0, pureseq.Length - 1), precursorLoss1));
        if (char.IsUpper(seq[pos - 1])) //if the second last base is not modified
        {
          var precursorLoss2 = precursorLoss1 - aa[seq[pos - 1]].MonoMass / this.Charge;
          this.TerminalLoss.Add(new TerminalLossItem(false, pureseq.Substring(0, pureseq.Length - 2), precursorLoss2));
        }
      }
    }

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

    public string FileScan { get; set; }

    public int GetFirstScan()
    {
      return new SequestFilename(this.FileScan).FirstScan;
    }
  }
}
