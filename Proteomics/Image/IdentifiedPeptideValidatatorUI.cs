using RCPA.Gui;
using RCPA.Gui.Command;
using RCPA.Gui.FileArgument;
using RCPA.Proteomics.Fragmentation;
using RCPA.Proteomics.Mascot;
using RCPA.Proteomics.Spectrum;
using RCPA.Proteomics.Summary;
using RCPA.Proteomics.Utils;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using ZedGraph;

namespace RCPA.Proteomics.Image
{
  public partial class IdentifiedPeptideValidatatorUI : ComponentUI
  {
    private static readonly string title = "Identified Peptide Validator";

    private static readonly string version = "2.0.1";

    private Dictionary<string, PeakList<MatchedPeak>> mgfMap;

    private List<IIdentifiedSpectrum> peptides;

    private Aminoacids aas = new Aminoacids();

    private IPeptideFragmentationBuilder<MatchedPeak> yBuilder, bBuilder, y2Builder, b2Builder;

    private OpenFileArgument mgfFile;

    private OpenFileArgument pepFile;

    public IdentifiedPeptideValidatatorUI()
    {
      InitializeComponent();

      mgfFile = new OpenFileArgument("MASCOT Generic Format", new string[] { "mgf", "msm" });

      pepFile = new OpenFileArgument("Identified Peptide", new string[] { "peptides" });

      this.Text = Constants.GetSQHTitle(title, version);
    }

    private bool isPPM = true;
    private void iIdentifiedSpectrumDataGridView_SelectionChanged(object sender, EventArgs e)
    {
      CurrencyManager cm = (CurrencyManager)this.BindingContext[peptides];
      var spectrum = cm.Current as IIdentifiedSpectrum;

      if (spectrum.Annotations.ContainsKey("PepMutation"))
      {
        zgcPeaks.MasterPane.Title.Text = string.Format("MH={0:0.00000},PPM={1:0.0},SEQ={2},MUT={3},COUNT={4}",
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
        zgcPeaks.MasterPane.Title.Text = string.Format("MH={0:0.00000},PPM={1:0.0},SEQ={2}",
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

      var mzTolerance = 0.02;
      var minIntensity = maxIntensity * 0.05;

      AddIonSeries(peakPane, ppmPane, mgf, mzTolerance, minIntensity, yBuilder, spectrum.Sequence, Color.Red);
      AddIonSeries(peakPane, ppmPane, mgf, mzTolerance, minIntensity, bBuilder, spectrum.Sequence, Color.Blue);

      if (spectrum.Charge > 2)
      {
        AddIonSeries(peakPane, ppmPane, mgf, mzTolerance, minIntensity, y2Builder, spectrum.Sequence, Color.Brown);
        AddIonSeries(peakPane, ppmPane, mgf, mzTolerance, minIntensity, b2Builder, spectrum.Sequence, Color.GreenYellow);
      }

      AddUnmatchedIons(peakPane, mgf);

      foreach (var pane in zgcPeaks.MasterPane.PaneList)
      {
        pane.XAxis.Scale.Min = 0;
        pane.XAxis.Scale.Max = maxMz;
      }
      zgcPeaks.UpdateGraph();
    }

    private void AddIonSeries(GraphPane peakPane, GraphPane ppmPane, PeakList<MatchedPeak> mgf, double mzTolerance, double minIntensity, IPeptideFragmentationBuilder<MatchedPeak> builder, string sequence, Color tagColor)
    {
      MatchedPeakUtils.Match(mgf, builder.Build(sequence), mzTolerance, minIntensity);

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

    private void iIdentifiedSpectrumDataGridView_CellValueNeeded(object sender, DataGridViewCellValueEventArgs e)
    {
      var spectrum = peptides[e.RowIndex];
      if (0 == e.ColumnIndex)
      {
        e.Value = spectrum.Query.FileScan.ShortFileName;
      }
      else if (1 == e.ColumnIndex)
      {
        e.Value = spectrum.Sequence;
      }
      else if ((int)gvPeptides.Columns[e.ColumnIndex].Tag == 1)
      {
        e.Value = spectrum.Annotations[gvPeptides.Columns[e.ColumnIndex].HeaderText];
      }
    }

    public class Command : IToolCommand
    {
      #region IToolCommand Members

      public string GetClassification()
      {
        return MenuCommandType.Misc;
      }

      public string GetCaption()
      {
        return title;
      }

      public string GetVersion()
      {
        return version;
      }

      public void Run()
      {
        new IdentifiedPeptideValidatatorUI().MyShow();
      }

      #endregion
    }

    private void btnOpen_Click(object sender, EventArgs e)
    {
      //var mgf = @"E:\backup\data\SAP\20111116_ZDSu_v_SAP_26_JPT_HCD.raw.mgf";
      if (mgfFile.GetFileDialog().ShowDialog(this) == System.Windows.Forms.DialogResult.OK)
      {
        var mgf = mgfFile.GetFileDialog().FileName;
        var mgfReader = new Mascot.MascotGenericFormatReader<MatchedPeak>();
        var spectra = mgfReader.ReadFromFile(mgf);
        spectra.ForEach(m => m.Experimental = m.Experimental.Substring(0, m.Experimental.Length - 4));
        mgfMap = spectra.ToDictionary(m => string.Format("{0}_{1}", m.Experimental, m.ScanTimes[0].Scan));
      }
      else
      {
        return;
      }

      //var peptideFile = @"E:\backup\data\SAP\final.peptides.type1.paired.one2one.mut";
      if (pepFile.GetFileDialog().ShowDialog(this) == System.Windows.Forms.DialogResult.OK)
      {
        var peptideFile = pepFile.GetFileDialog().FileName;
        peptides = new MascotPeptideTextFormat().ReadFromFile(peptideFile);
      }
      else
      {
        return;
      }

      var annKeys = new string[] { "OriginalSequence", "PepMutation", "_PepCount", "_OriginalCount" };

      foreach (var key in peptides[0].Annotations.Keys)
      {
        if (annKeys.Any(m => key.Contains(m)))
        {
          gvPeptides.Columns.Add(new DataGridViewTextBoxColumn()
          {
            HeaderText = key,
            Tag = 1
          });
        }
      }

      var mods = new Dictionary<char, double>();
      mods['&'] = 7.017166;
      mods['#'] = 3.010071;
      mods['@'] = 6.013809;
      mods['*'] = 15.994919;
      mods['C'] = 57.021464 + aas['C'].MonoMass; // 160.16523;
      mods['K'] = 8.01 + aas['K'].MonoMass;
      mods['R'] = 10.01 + aas['R'].MonoMass;

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

      zgcPeaks.InitMasterPanel(this.CreateGraphics(), 2, "");
      zgcPeaks.IsSynchronizeXAxes = true;

      gvPeptides.DataSource = peptides;
    }
  }
}
