using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using RCPA.Proteomics.Mascot;
using RCPA.Proteomics.Fragmentation;
using RCPA.Proteomics.Spectrum;
using RCPA.Proteomics.Summary;
using RCPA.Gui;
using ZedGraph;
using RCPA.Gui.Image;
using RCPA.Proteomics.Utils;
using RCPA.Gui.Command;
using RCPA.Gui.FileArgument;
using System.IO;
using System.Drawing.Imaging;

namespace RCPA.Proteomics.Image
{
  public class MatchImageBuilder : AbstractThreadFileProcessor
  {
    private Dictionary<string, PeakList<MatchedPeak>> mgfMap;

    private List<IIdentifiedSpectrum> peptides;

    private string targetDir;

    private Aminoacids aas = new Aminoacids();

    private IPeptideFragmentationBuilder<MatchedPeak> yBuilder, bBuilder, y2Builder, b2Builder;

    private bool isPPM = true;

    private double productIonPPM = 20;

    private ZedGraphControl zgcPeaks;

    public MatchImageBuilder(string mgfFile, string targetDir)
    {
      var mgfReader = new Mascot.MascotGenericFormatReader<MatchedPeak>();
      var spectra = mgfReader.ReadFromFile(mgfFile);
      spectra.ForEach(m => m.Experimental = m.Experimental.Substring(0, m.Experimental.Length - 4));
      mgfMap = spectra.ToDictionary(m => string.Format("{0}_{1}", m.Experimental, m.ScanTimes[0].Scan));

      var mods = new Dictionary<char, double>();
      mods['&'] = 7.017166;
      mods['#'] = 3.010071;
      mods['@'] = 6.013809;
      mods['*'] = 15.994919;
      mods['C'] = 57.021464 + aas['C'].MonoMass; // 160.16523;
      mods['K'] = 8.014206 + aas['K'].MonoMass;
      mods['R'] = 10.008270 + aas['R'].MonoMass;

      aas.SetModification(mods);

      yBuilder = new CIDPeptideYSeriesBuilder<MatchedPeak>()
      {
        CurAminoacids = aas
      };
      bBuilder = new CIDPeptideBSeriesBuilder<MatchedPeak>()
      {
        CurAminoacids = aas
      };
      y2Builder = new CIDPeptideY2SeriesBuilder<MatchedPeak>()
      {
        CurAminoacids = aas
      };
      b2Builder = new CIDPeptideB2SeriesBuilder<MatchedPeak>()
      {
        CurAminoacids = aas
      };

      zgcPeaks = new ZedGraphControl();
      zgcPeaks.Width = 1600;
      zgcPeaks.Height = 1200;
      zgcPeaks.MasterPane.Border.IsVisible = false;

      zgcPeaks.InitMasterPanel(Graphics.FromImage(zgcPeaks.GetImage()), 2, "");
      zgcPeaks.IsSynchronizeXAxes = true;

      if (!Directory.Exists(targetDir))
      {
        Directory.CreateDirectory(targetDir);
      }
      this.targetDir = targetDir;
    }

    private void DrawSpectrum(IIdentifiedSpectrum spectrum)
    {
      if (spectrum.Annotations.ContainsKey("PepMutation"))
      {
        zgcPeaks.MasterPane.PaneList[0].Title.Text = string.Format("MH={0:0.00000},PPM={1:0.0},SEQ={2},MUT={3},COUNT={4}",
          spectrum.TheoreticalMH,
          PrecursorUtils.mz2ppm(spectrum.TheoreticalMH, spectrum.TheoreticalMinusExperimentalMass),
          spectrum.Sequence,
          spectrum.Annotations["PepMutation"],
          (from a in spectrum.Annotations
           where a.Key.EndsWith("_PepCount")
           select Convert.ToInt32(a.Value)).Sum());
      }
      else
      {
        zgcPeaks.MasterPane.PaneList[0].Title.Text = string.Format("MH={0:0.00000},PPM={1:0.0},SEQ={2}",
          spectrum.TheoreticalMH,
          PrecursorUtils.mz2ppm(spectrum.TheoreticalMH, spectrum.TheoreticalMinusExperimentalMass),
          spectrum.Sequence);
      }

      var mass = aas.MonoPeptideMass(PeptideUtils.GetMatchedSequence(spectrum.Sequence));

      if (Math.Abs(mass - spectrum.TheoreticalMass) > 0.1)
      {
        MessageBox.Show(string.Format("Error : {0} - {1}", mass, spectrum.TheoreticalMass));
        return;
      }

      var name = string.Format("{0}_{1}", spectrum.Query.FileScan.Experimental, spectrum.Query.FileScan.FirstScan);

      var peakPane = zgcPeaks.MasterPane.PaneList[0];
      peakPane.ClearData();

      var ppmPane = zgcPeaks.MasterPane.PaneList[1];
      ppmPane.ClearData();

      if (!mgfMap.ContainsKey(name))
      {
        MessageBox.Show("Cannot find peak list {0}", name);
        return;
      }

      var mgf = mgfMap[name];

      var maxIntensity = mgf.Max(m => m.Intensity);
      var maxMz = Math.Min(2000, mgf.Max(m => m.Mz));

      var productTolerance = productIonPPM;
      var minIntensity = maxIntensity * 0.05;

      AddIonSeries(peakPane, ppmPane, mgf, productTolerance, minIntensity, yBuilder, spectrum.Sequence, Color.Red);
      AddIonSeries(peakPane, ppmPane, mgf, productTolerance, minIntensity, bBuilder, spectrum.Sequence, Color.Blue);

      if (spectrum.Charge > 2)
      {
        AddIonSeries(peakPane, ppmPane, mgf, productTolerance, minIntensity, y2Builder, spectrum.Sequence, Color.Brown);
        AddIonSeries(peakPane, ppmPane, mgf, productTolerance, minIntensity, b2Builder, spectrum.Sequence, Color.GreenYellow);
      }

      AddUnmatchedIons(peakPane, mgf);

      foreach (var pane in zgcPeaks.MasterPane.PaneList)
      {
        pane.XAxis.Scale.Min = 0;
        pane.XAxis.Scale.Max = maxMz;
      }
      zgcPeaks.UpdateGraph();

      var targetpngfile = GetTargetFile(targetDir, spectrum);

      zgcPeaks.GetImage().Save(targetpngfile, ImageFormat.Png);
    }

    public static string GetTargetFile(string targetDir, IIdentifiedSpectrum peak)
    {
      var result = MyConvert.Format(@"{0}\{1}.{2}.match.png", targetDir, peak.Query.FileScan.Experimental, peak.Query.FileScan.FirstScan);
      return new FileInfo(result).FullName;
    }

    private void AddIonSeries(GraphPane peakPane, GraphPane ppmPane, PeakList<MatchedPeak> mgf, double productTolerance, double minIntensity, IPeptideFragmentationBuilder<MatchedPeak> builder, string sequence, Color tagColor)
    {
      MatchedPeakUtils.MatchPPM(mgf, builder.Build(sequence), productTolerance, minIntensity);

      var ionType = builder.SeriesType.ToString();
      var matchedIons = (from m in mgf
                         where m.Matched && m.PeakType == builder.SeriesType
                         select m).ToList();

      var ppl = new PointPairList();
      foreach (var m in matchedIons)
      {
        ppl.Add(new PointPair(m.Mz, m.Intensity, m.Information));
      }

      peakPane.AddIndividualLine("", ppl, Color.Black, tagColor);

      if (ppmPane != null)
      {
        var diff = new PointPairList();
        foreach (var m in matchedIons)
        {
          if (isPPM)
          {
            diff.Add(new PointPair(m.Mz, PrecursorUtils.mz2ppm(m.Mz, m.Mz - m.MatchedMZ)));
          }
          else
          {
            diff.Add(new PointPair(m.Mz, m.Mz - m.MatchedMZ));
          }
        }
        ppmPane.AddPoints(diff, tagColor);
      }
    }

    private void AddUnmatchedIons(GraphPane peakPane, PeakList<MatchedPeak> mgf)
    {
      var matchedIons = (from m in mgf
                         where !m.Matched
                         select m).ToList();

      var ppl = new PointPairList();
      foreach (var m in matchedIons)
      {
        ppl.Add(new PointPair(m.Mz, m.Intensity));
      }

      peakPane.AddIndividualLine("", ppl, Color.Black);
    }

    public override IEnumerable<string> Process(string fileName)
    {
      peptides = new MascotPeptideTextFormat().ReadFromFile(fileName);

      foreach (var peptide in peptides)
      {
        DrawSpectrum(peptide);
      }

      return new string[] { targetDir };
    }
  }
}
