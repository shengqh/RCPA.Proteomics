using RCPA.Gui;
using RCPA.Gui.Command;
using RCPA.Gui.FileArgument;
using RCPA.Proteomics.Mascot;
using RCPA.Proteomics.Raw;
using RCPA.Proteomics.Summary;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using ZedGraph;

namespace RCPA.Proteomics.Quantification.Labelfree
{
  public partial class ProteinChromatographProcessorUI : AbstractFileProcessorUI
  {
    private static string title = "Identified Peptide Chromatograph Extractor & Viewer";

    private static string version = "1.0.0";

    private Aminoacids aas = new Aminoacids();

    private RcpaFileField rawFile;

    private RcpaDoubleField ppmTolerance;

    private RcpaDoubleField window;

    private ListViewColumnField lvcPeptides;

    private RcpaCheckBox rawonly;

    private bool bFirstLoad;

    private List<string> peptideIgnoreKeys = new List<string>();

    private List<int> peptideIgnoreKeyIndecies = new List<int>();

    private List<IIdentifiedSpectrum> peptides;

    private MascotPeptideTextFormat format;

    public ProteinChromatographProcessorUI()
    {
      InitializeComponent();

      ProteaseManager.Load();

      SetFileArgument("PeptideFile", new OpenFileArgument("peptides", "peptides"));

      rawFile = new RcpaFileField(btnRawDirectory, txtRawDirectory, "RawFile", new OpenFileArgument("Thermo Raw", "raw"), true);
      AddComponent(rawFile);

      ppmTolerance = new RcpaDoubleField(txtPPMTolerance, "PPMTolerance", "Precursor PPM Tolerance", 10, true);
      AddComponent(ppmTolerance);

      window = new RcpaDoubleField(txtWindow, "Window", "Elution Window", 1, true);
      AddComponent(window);

      lvcPeptides = new ListViewColumnField(lvPeptides, "LvPeptides");
      AddComponent(lvcPeptides);

      rawonly = new RcpaCheckBox(cbShowInRawFileOnly, "ShowRawFileOnly", true);
      AddComponent(rawonly);

      bFirstLoad = true;
    }

    private void ProteinChromotographViewer_Shown(object sender, EventArgs e)
    {
      LoadOption();
    }

    protected override string GetOriginFile()
    {
      string peptides = base.GetOriginFile();

      DirectoryInfo dir = new DirectoryInfo(peptides + ".labelfree");
      if (!dir.Exists)
      {
        dir.Create();
      }

      return dir.FullName;
    }

    protected override IFileProcessor GetFileProcessor()
    {
      format = new MascotPeptideTextFormat();
      peptides = format.ReadFromFile(base.GetOriginFile());

      if (bFirstLoad)
      {
        var allColumns = format.PeptideFormat.GetHeader().Split('\t').ToList();
        var lvColumns = lvPeptides.GetColumnList().ConvertAll(m => m.Text);
        if (lvColumns.Count > 0)
        {
          this.peptideIgnoreKeys = allColumns.Except(lvColumns).ToList();
        }

        bFirstLoad = false;
      }

      FillListViewColumns(this.lvPeptides, format.PeptideFormat.GetHeader(), this.peptideIgnoreKeys, this.peptideIgnoreKeyIndecies);

      UpdatePeptides();

      var chros = (from p in peptides
                   select SpectrumToChro(p)).ToList();

      for (int i = chros.Count - 1; i >= 0; i--)
      {
        for (int j = i - 1; j >= 0; j--)
        {
          if ((chros[i].Sequence == chros[j].Sequence) && (chros[i].Charge == chros[j].Charge) && (Math.Abs(chros[i].Mz - chros[j].Mz) < 0.0001))
          {
            chros.RemoveAt(j);
            break;
          }
        }
      }

      lvPeptides.SelectedIndexChanged -= lvPeptides_SelectedIndexChanged;

      return new ProteinChromatographProcessor(chros, new string[] { rawFile.FullName }.ToList(), new RawFileImpl(), ppmTolerance.Value, window.Value, false);
    }

    private void UpdatePeptides()
    {
      var exp = Path.GetFileNameWithoutExtension(rawFile.FullName);

      this.lvPeptides.BeginUpdate();
      try
      {
        this.lvPeptides.Items.Clear();
        foreach (IIdentifiedSpectrum mph in peptides)
        {
          if ((!cbShowInRawFileOnly.Checked) || (mph.Query.FileScan.Experimental == exp))
          {
            ListViewItem item = this.lvPeptides.Items.Add("");
            UpdatePeptideHit(mph, item);
          }
        }
      }
      finally
      {
        this.lvPeptides.EndUpdate();
      }
    }

    private void UpdatePeptideHit(IIdentifiedSpectrum mph, ListViewItem item)
    {
      item.SubItems.Clear();
      var parts = new List<string>(this.format.PeptideFormat.GetString(mph).Split(new[] { '\t' }));
      for (int i = this.peptideIgnoreKeyIndecies.Count - 1; i >= 0; i--)
      {
        parts.RemoveAt(this.peptideIgnoreKeyIndecies[i]);
      }

      for (int i = 1; i < parts.Count; i++)
      {
        item.SubItems.Add(parts[i]);
      }
      item.Tag = mph;
    }

    private void FillListViewColumns(ListView listView, string line, List<string> ignoreKeys,
                                     List<int> ignoreKeyIndecies)
    {
      var columns = new List<string>(line.Split(new[] { '\t' }));
      ignoreKeyIndecies.Clear();

      foreach (string key in ignoreKeys)
      {
        int index = columns.IndexOf(key);
        if (index >= 0)
        {
          ignoreKeyIndecies.Add(index);
        }
      }
      ignoreKeyIndecies.Sort();

      for (int i = ignoreKeyIndecies.Count - 1; i >= 0; i--)
      {
        columns.RemoveAt(ignoreKeyIndecies[i]);
      }

      for (int i = 0; i < listView.Columns.Count & i < columns.Count; i++)
      {
        listView.Columns[i].Text = columns[i];
      }

      for (int i = listView.Columns.Count; i < columns.Count; i++)
      {
        listView.Columns.Add(columns[i]);
      }

      while (listView.Columns.Count > columns.Count)
      {
        listView.Columns.RemoveAt(listView.Columns.Count - 1);
      }
    }

    protected override void ShowReturnInfo(IEnumerable<string> returnInfo)
    {
      lvPeptides.SelectedIndexChanged += lvPeptides_SelectedIndexChanged;
      lvPeptides_SelectedIndexChanged(null, null);
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
        new ProteinChromatographProcessorUI().MyShow();
      }

      #endregion
    }

    private void lvPeptides_SelectedIndexChanged(object sender, EventArgs e)
    {
      if (lvPeptides.SelectedItems.Count > 0)
      {
        var spectrum = lvPeptides.SelectedItems[0].Tag as IIdentifiedSpectrum;
        var file = ProteinChromatographProcessor.GetTargetFile(GetOriginFile(), rawFile.FullName, SpectrumToChro(spectrum));
        var chro = new SimplePeakChroXmlFormat().ReadFromFile(file);

        try
        {
          zgcScans.ClearData(false);

          var mainPane = ZedGraphicExtension.InitMasterPanel(zgcScans, CreateGraphics(), 2, "Chromotograph");

          if (chro.Peaks.Count > 0)
          {
            var pplSample = new PointPairList();
            var pplppm = new PointPairList();
            var bFirst = true;
            foreach (ScanPeak p in chro.Peaks)
            {
              pplSample.Add(p.RetentionTime, p.Intensity);
              pplppm.Add(p.RetentionTime, p.PPMDistance);
              //if (p.Intensity > 0)
              //{
              //  pplSample.Add(p.RetentionTime, p.Intensity);
              //  pplppm.Add(p.RetentionTime, p.PPMDistance);
              //}
              //else if (pplSample.Count > 0)
              //{
              //  if (bFirst)
              //  {
              //    mainPane.PaneList[0].AddCurve(MyConvert.Format("{0:0.0000},{1}", chro.Mz, chro.Charge), pplSample, Color.Red, SymbolType.None);
              //    mainPane.PaneList[1].AddCurve(MyConvert.Format("{0:0.0000},{1}", chro.Mz, chro.Charge), pplppm, Color.Red, SymbolType.None);
              //  }
              //  else
              //  {
              //    mainPane.PaneList[0].AddCurve("a", pplSample, Color.Red, SymbolType.None);
              //    mainPane.PaneList[1].AddCurve("a", pplppm, Color.Red, SymbolType.None);
              //  }

              //  pplSample.Clear();
              //  pplppm.Clear();
              //  bFirst = false;
              //}
            }

            if (pplSample.Count > 0)
            {
              if (bFirst)
              {
                mainPane.PaneList[0].AddCurve(MyConvert.Format("{0:0.0000},{1}", chro.Mz, chro.Charge), pplSample, Color.Red, SymbolType.None);
                mainPane.PaneList[1].AddCurve(MyConvert.Format("{0:0.0000},{1}", chro.Mz, chro.Charge), pplppm, Color.Red, SymbolType.None);
              }
              else
              {
                mainPane.PaneList[0].AddCurve("a", pplSample, Color.Red, SymbolType.None);
                mainPane.PaneList[1].AddCurve("a", pplppm, Color.Red, SymbolType.None);
              }
            }
          }
        }
        finally
        {
          ZedGraphicExtension.UpdateGraph(zgcScans);
        }
      }
    }

    private SimplePeakChro SpectrumToChro(IIdentifiedSpectrum spectrum)
    {
      return new SimplePeakChro()
      {
        Mz = spectrum.GetPrecursorMz(),
        Sequence = spectrum.Peptide.PureSequence,
        Charge = spectrum.Charge
      };
    }

    private void cbShowInRawFileOnly_CheckedChanged(object sender, EventArgs e)
    {
      if (null != peptides)
      {
        UpdatePeptides();
      }
    }
  }

}
