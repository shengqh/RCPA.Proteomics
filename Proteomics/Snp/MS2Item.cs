﻿using RCPA.Proteomics.Spectrum;
using RCPA.Proteomics.Utils;
using System;
using System.Collections.Generic;
using System.Linq;

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
      FileScans = new List<SequestFilename>();
      MS3Spectra = new List<MS3Item>();
      TerminalLoss = new List<TerminalLossItem>();
      Proteins = string.Empty;
    }

    public string Peptide { get; set; }
    public string Modification { get; set; }
    public double Precursor { get; set; }
    public double Score { get; set; }
    public double ExpectValue { get; set; }
    public int Charge { get; set; }
    public string ModifiedPeptide { get; set; }
    public Aminoacids Aminoacids { get; set; }
    public Dictionary<IonType, List<IonTypePeak>> Spectra { get; set; }
    public Dictionary<char, string> ModificationNameMap { get; set; }

    public string Proteins { get; set; }
    public List<MS3Item> MS3Spectra { get; private set; }
    public List<TerminalLossItem> TerminalLoss { get; private set; }

    /// <summary>
    /// Minimim matched precursor mz when ppm tolerance fixed
    /// </summary>
    public double MinPrecursorMz { get; set; }

    /// <summary>
    /// Maximim matched precursor mz when ppm tolerance fixed
    /// </summary>
    public double MaxPrecursorMz { get; set; }

    public void InitTerminalLoss(Aminoacids aa, int maxTerminalLossLength, int minSequenceLength)
    {
      this.TerminalLoss = new List<TerminalLossItem>();

      var seq = PeptideUtils.GetMatchedSequence(this.Peptide);
      var pureseq = PeptideUtils.GetPureSequence(this.Peptide);

      var pos = 0;
      var index = 0;
      var maxIndex = Math.Min(maxTerminalLossLength, pureseq.Length - minSequenceLength);
      var deltaMass = 0.0;
      while (index < maxIndex)
      {
        deltaMass += aa[seq[pos]].MonoMass;
        if (!char.IsUpper(seq[pos + 1]))
        {
          pos++;
          continue;
        }

        index++;
        pos++;

        var precursorLoss = this.Precursor - deltaMass / this.Charge;
        this.TerminalLoss.Add(new TerminalLossItem(true, pureseq.Substring(index), precursorLoss));
      }

      index = 0;
      pos = seq.Length - 1;
      deltaMass = 0.0;
      while (index < maxIndex)
      {
        deltaMass += aa[seq[pos]].MonoMass;
        if (!char.IsUpper(seq[pos]))
        {
          pos--;
          continue;
        }

        index++;
        pos--;

        var precursorLoss = this.Precursor - deltaMass / this.Charge;
        this.TerminalLoss.Add(new TerminalLossItem(false, pureseq.Substring(0, pureseq.Length - index), precursorLoss));
      }
    }

    public void CombineMS3Spectra(IBestSpectrumBuilder builder, double precursorPPMTolerance)
    {
      List<List<MS3Item>> groups = GroupMS3SpectraByPrecursor(precursorPPMTolerance);

      this.MS3Spectra.Clear();

      foreach (var g in groups)
      {
        var pkl = builder.Build(g);
        this.MS3Spectra.Add(pkl);
      }
    }

    public List<List<MS3Item>> GroupMS3SpectraByPrecursor(double precursorPPMTolerance)
    {
      List<List<MS3Item>> result = new List<List<MS3Item>>();

      this.MS3Spectra.ForEach(m =>
      {
        m.Tag = m.CombinedCount == 0 ? 1 : m.CombinedCount;
        m.ForEach(p => p.CombinedCount = p.CombinedCount == 0 ? 1 : p.CombinedCount);
      });

      this.MS3Spectra.Sort((m1, m2) => m1.PrecursorMZ.CompareTo(m2.PrecursorMZ));

      //group ms3spectra by precursor m/z
      List<MS3Item> current = new List<MS3Item>();
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
          current = new List<MS3Item>();
          current.Add(this.MS3Spectra[i]);
        }
      }
      result.Add(current);
      result.Reverse();

      return result;
    }

    public int CombinedCount { get; set; }

    public char[] AminoacidCompsition { get; set; }

    public List<SequestFilename> FileScans { get; set; }

    public string GetFileScans()
    {
      return (from fs in FileScans
              select fs.ShortFileName).Merge(";");
    }
  }
}
