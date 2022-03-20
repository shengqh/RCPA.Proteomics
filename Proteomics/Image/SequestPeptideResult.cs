using RCPA.Proteomics.Fragmentation;
using RCPA.Proteomics.IO;
using RCPA.Proteomics.Sequest;
using RCPA.Proteomics.Spectrum;
using RCPA.Proteomics.Summary;
using RCPA.Proteomics.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace RCPA.Proteomics.Image
{
  public class SequestPeptideResult : IIdentifiedPeptideResult
  {
    public static Regex peakPattern = new Regex("(\\d+\\.{0,1}\\d*)\\s+(\\d+\\.{0,1}\\d*)");

    public static Regex massTypePattern = new Regex("fragment\\stol.+?(\\S+)/(\\S+)");

    public static Regex dynamicModificationPattern = new Regex("\\(\\S+(\\S)\\s([\\+|-]\\d+\\.{0,1}\\d+)\\)");

    public static Regex staticModificationPattern = new Regex("([A-Z])=(\\d+\\.{0,1}\\d+)");

    public bool PrecursorMonoMass { get; set; }

    public bool PeakMonoMass { get; set; }

    public Dictionary<char, double> StaticModification { get; set; }

    #region IIdentifiedPeptideResult Members

    public string Peptide { get; set; }

    public PeakList<MatchedPeak> ExperimentalPeakList { get; set; }

    private Dictionary<IonType, List<MatchedPeak>> ionSeries;
    public Dictionary<IonType, List<MatchedPeak>> GetIonSeries()
    {
      return ionSeries;
    }

    public bool IsBy2Matched()
    {
      return ExperimentalPeakList.Any(m => m.Matched && (m.PeakType == IonType.B2 || m.PeakType == IonType.Y2));
    }

    public Dictionary<string, string> ScoreMap { get; set; }

    public Dictionary<char, double> DynamicModification { get; set; }

    #endregion

    public void LoadData(string dtaFile, string outFile)
    {
      ExperimentalPeakList = DtaIO.ReadFromDta<MatchedPeak>(dtaFile);

      SequestOutHeaderParser parser = new SequestOutHeaderParser();

      string[] lines = FileUtils.ReadFile(outFile).ToArray();

      int index = ParseMassType(lines, 0, parser);
      index = ParseModification(lines, index, parser);

      IIdentifiedSpectrum spectrum = new OutParser().ParseFromFile(outFile);

      ScoreMap = new Dictionary<string, string>();
      if (spectrum != null)
      {
        Peptide = spectrum.Sequence;

        ScoreMap["Xcorr"] = MyConvert.Format("{0:0.0000}", spectrum.Score);
        ScoreMap["DeltaCn"] = MyConvert.Format("{0:0.0000}", spectrum.DeltaScore);

        InitTheoreticalPeaks();
      }
    }

    private void InitTheoreticalPeaks()
    {
      Aminoacids aas = new Aminoacids();

      aas.SetModification(StaticModification);
      aas.SetModification(DynamicModification);

      CIDFragmentationBuilder<MatchedPeak> builder = new CIDFragmentationBuilder<MatchedPeak>(ExperimentalPeakList.PrecursorCharge, aas);

      ionSeries = builder.GetIonSeries(PeptideUtils.GetMatchedSequence(Peptide));
    }

    private int ParseMassType(string[] lines, int index, SequestOutHeaderParser parser)
    {
      while (index < lines.Length)
      {
        bool precursorIsMono, peakIsMono;
        if (parser.ParseMassType(lines[index], out precursorIsMono, out peakIsMono))
        {
          return index + 1;
        }
        index++;
      }

      throw new ArgumentException("Cannot find mass type information");
    }

    private int ParseModification(string[] lines, int index, SequestOutHeaderParser parser)
    {
      while (index < lines.Length)
      {
        if (lines[index].IndexOf("Enzyme:") != -1)
        {
          DynamicModification = parser.ParseDynamicModification(lines[index]);
          StaticModification = parser.ParseStaticModification(lines[index]);
          return index + 1;
        }
        index++;
      }

      throw new ArgumentException("Cannot find modification information");
    }
  }
}
