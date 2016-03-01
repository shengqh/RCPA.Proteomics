using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using RCPA.Seq;
using RCPA.Gui;
using RCPA.Proteomics.Spectrum;
using RCPA.Gui.FileArgument;
using RCPA.Proteomics.Raw;
using ZedGraph;
using RCPA.Gui.Image;
using System.IO;
using RCPA.Gui.Command;
using RCPA.Proteomics.Summary;
using RCPA.Proteomics.Mascot;

namespace RCPA.Proteomics.Quantification.Labelfree
{
  public partial class ProteinChromatographViewer : AbstractFileProcessorUI
  {
    private static string title = "Protein Chromatograph Extractor & Viewer";

    private static string version = "1.0.0";

    private Aminoacids aas = new Aminoacids();

    private static string CHRO_KEY = "CHRO_KEY";
    private RcpaComboBox<string> proteases;

    private RcpaFileField rawFile;
    //private RcpaDirectoryField rawDirectory;

    private RcpaFileField noredundantFile;

    private RcpaDoubleField ppmTolerance;

    private RcpaCheckBox rebuildAll;

    public ProteinChromatographViewer()
    {
      InitializeComponent();

      ProteaseManager.Load();

      proteases = new RcpaComboBox<string>(cbProtease, "ProteaseIndex", ProteaseManager.GetNames().ToArray(), 3);
      AddComponent(proteases);

      SetFileArgument("FastaFile", new OpenFileArgument("Fasta", "fasta"));

      rawFile = new RcpaFileField(btnRawDirectory, txtRawDirectory, "RawFile", new OpenFileArgument("Thermo Raw", "raw"), true);
      AddComponent(rawFile);

      ppmTolerance = new RcpaDoubleField(txtPPMTolerance, "PPMTolerance", "Precursor PPM Tolerance", 10, true);
      AddComponent(ppmTolerance);

      rebuildAll = new RcpaCheckBox(cbRebuildAll, "RebuildAll", false);
      AddComponent(rebuildAll);

      noredundantFile = new RcpaFileField(btnNoredundant, txtNoredundant, "NoredundantFile", new OpenFileArgument("noredundant", "noredundant"), false);
      AddComponent(noredundantFile);
      //rawDirectory = new RcpaDirectoryField(btnRawDirectory, txtRawDirectory, "RawDirectory", "Thermo Fisher Raw", true);
      //AddComponent(rawDirectory);
    }

    private List<Sequence> proteins = new List<Sequence>();

    private void ProteinChromotographViewer_Shown(object sender, EventArgs e)
    {
      LoadOption();
    }

    protected override string GetOriginFile()
    {
      string raw = rawFile.FullName;

      DirectoryInfo dir = new DirectoryInfo(raw + ".labelfree");
      if (!dir.Exists)
      {
        dir.Create();
      }

      return dir.FullName;
    }

    protected override IFileProcessor GetFileProcessor()
    {
      proteins = SequenceUtils.Read(new FastaFormat(), base.GetOriginFile());
      Protease protease = ProteaseManager.GetProteaseByName(proteases.SelectedItem);
      Digest digest = new Digest()
      {
        DigestProtease = protease,
        MaxMissedCleavages = 2
      };

      List<SimplePeakChro> totalPeaks = new List<SimplePeakChro>();
      foreach (var seq in proteins)
      {
        digest.ProteinSequence = seq;
        digest.AddDigestFeatures();

        List<DigestPeptideInfo> peptides = seq.GetDigestPeptideInfo();
        peptides.RemoveAll(m => m.PeptideSeq.Length < 6);
        foreach (var dpi in peptides)
        {
          double mass = aas.MonoPeptideMass(dpi.PeptideSeq);
          List<SimplePeakChro> curPeaks = new List<SimplePeakChro>();
          for (int charge = 2; charge <= 3; charge++)
          {
            double precursor = (mass + Atom.H.MonoMass * charge) / charge;
            if (precursor < 300 || precursor > 2000)
            {
              continue;
            }

            curPeaks.Add(new SimplePeakChro()
            {
              Mz = precursor,
              Sequence = dpi.PeptideSeq,
              Charge = charge
            });
          }

          if (curPeaks.Count > 0)
          {
            dpi.Annotations[CHRO_KEY] = curPeaks;
            totalPeaks.AddRange(curPeaks);
          }
        }

        peptides.RemoveAll(m => !m.Annotations.ContainsKey(CHRO_KEY));
      }

      return new ProteinChromatographProcessor(totalPeaks, new string[] { rawFile.FullName }.ToList(), new RawFileImpl(), ppmTolerance.Value, 2.0, rebuildAll.Checked);
    }

    private void UpdateProteins()
    {
      lbProteins.BeginUpdate();
      try
      {
        lbProteins.Items.Clear();
        foreach (var protein in proteins)
        {
          lbProteins.Items.Add(protein);
        }
      }
      finally
      {
        lbProteins.EndUpdate();
      }
    }

    private void lbPeptides_SelectedIndexChanged(object sender, EventArgs e)
    {
      if (lbPeptides.SelectedItem != null)
      {
        DigestPeptideInfo dpi = lbPeptides.SelectedItem as DigestPeptideInfo;

        lbPrecursor.BeginUpdate();
        try
        {
          lbPrecursor.Items.Clear();
          List<SimplePeakChro> peaks = (List<SimplePeakChro>)dpi.Annotations[CHRO_KEY];

          var validPeaks = (from p in peaks
                            where p.Peaks.Count > 0
                            select p).ToList();

          try
          {
            if (validPeaks.Count == 0)
            {
              ZedGraphicExtension.InitMasterPanel(zgcScans, CreateGraphics(), 1, "Chromotograph");
              zgcScans.MasterPane.PaneList[0].ClearData();
              return;
            }

            var mainPane = ZedGraphicExtension.InitMasterPanel(zgcScans, CreateGraphics(), validPeaks.Count, "Chromotograph");
            int index = 0;
            foreach (var chro in peaks)
            {
              lbPrecursor.Items.Add(chro);
              if (chro.Peaks.Count > 0)
              {
                var pplSample = new PointPairList();
                foreach (ScanPeak p in chro.Peaks)
                {
                  pplSample.Add(p.Scan, p.Intensity);
                }

                mainPane.PaneList[index].AddCurve(MyConvert.Format("{0:0.0000},{1}", chro.Mz, chro.Charge), pplSample, Color.Red, SymbolType.None);
                //mainPane.PaneList[index].XAxis.Scale.Min = 0;
                //mainPane.PaneList[index].XAxis.Scale.Max = chro.MaxRetentionTime;
                
                index++;
              }
            }
          }
          finally
          {
            ZedGraphicExtension.UpdateGraph(zgcScans);
          }
        }
        finally
        {
          lbPrecursor.EndUpdate();
        }

        lbIdentified.Items.Clear();
        if (ir != null)
        {
          if (pepMap.ContainsKey(dpi.PeptideSeq))
          {
            lbIdentified.BeginUpdate();
            try
            {
              var ids = pepMap[dpi.PeptideSeq];
              foreach (var id in ids)
              {
                lbIdentified.Items.Add(MyConvert.Format("[{0}; {1}]", id.Query.FileScan.FirstScan, id.Charge));
              }
            }
            finally
            {
              lbIdentified.EndUpdate();
            }
          }
        }

      }
    }

    private IIdentifiedResult ir;
    private Dictionary<string, List<IIdentifiedSpectrum>> pepMap;

    protected override void ShowReturnInfo(IEnumerable<string> returnInfo)
    {
      if (File.Exists(noredundantFile.FullName))
      {
        ir = new MascotResultTextFormat().ReadFromFile(noredundantFile.FullName);
        var peps = ir.GetSpectra();
        pepMap = new Dictionary<string, List<IIdentifiedSpectrum>>();
        foreach (var pep in peps)
        {
          if (!pepMap.ContainsKey(pep.Peptide.PureSequence))
          {
            pepMap[pep.Peptide.PureSequence] = new List<IIdentifiedSpectrum>();
          }
          pepMap[pep.Peptide.PureSequence].Add(pep);
        }

        foreach (var lst in pepMap.Values)
        {
          lst.Sort((m1, m2) =>
          {
            var r = m1.Charge.CompareTo(m2.Charge);
            if (r == 0)
            {
              r = m1.Query.FileScan.FirstScan.CompareTo(m2.Query.FileScan.FirstScan);
            }
            return r;
          });
        }
      }
      else
      {
        ir = null;
      }

      UpdateProteins();

      if (lbProteins.Items.Count > 0)
      {
        lbProteins.SelectedIndex = 0;
      }

      if (lbPeptides.Items.Count > 0)
      {
        lbPeptides.SelectedIndex = 0;
      }
    }

    private void lbProteins_SelectedIndexChanged(object sender, EventArgs e)
    {
      if (lbProteins.SelectedItem != null)
      {
        Sequence seq = lbProteins.SelectedItem as Sequence;

        lbPeptides.BeginUpdate();
        try
        {
          lbPeptides.Items.Clear();
          List<DigestPeptideInfo> peptides = seq.GetDigestPeptideInfo();
          peptides.Sort((m1, m2) => m1.PeptideSeq.CompareTo(m2.PeptideSeq));

          foreach (var dp in peptides)
          {
            if (dp.Annotations.ContainsKey(CHRO_KEY))
            {
              List<SimplePeakChro> peaks = (List<SimplePeakChro>)dp.Annotations[CHRO_KEY];

              if (peaks.Find(m => m.Peaks.Count > 0) != null)
              {
                lbPeptides.Items.Add(dp);
              }
            }
          }
        }
        finally
        {
          lbPeptides.EndUpdate();
        }
      }
    }

    public class Command : IToolCommand
    {
      #region IToolCommand Members

      public string GetClassification()
      {
        return MenuCommandType.Quantification;
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
        new ProteinChromatographViewer().MyShow();
      }

      #endregion
    }
  }
}
