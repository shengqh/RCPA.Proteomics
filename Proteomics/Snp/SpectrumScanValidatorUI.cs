using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using RCPA.Gui;
using RCPA.Gui.FileArgument;
using RCPA.Proteomics.Mascot;
using RCPA.Proteomics.Summary;
using System.IO;
using RCPA.Utils;
using RCPA.Gui.Command;
using System.Text.RegularExpressions;
using RCPA.Proteomics.Raw;
using ZedGraph;

namespace RCPA.Proteomics.Snp
{
  public partial class SpectrumScanValidatorUI : ComponentUI
  {
    private static readonly string title = "Spectrum SCAN Validator";
    private static readonly string version = "1.0.1";

    private RcpaFileField ms3MatchedFile;
    private RcpaFileField rawFile;
    private IRawFile reader;
    private int lastScan;

    private List<MS3MatchItem> items;

    public SpectrumScanValidatorUI()
    {
      InitializeComponent();
      ms3MatchedFile = new RcpaFileField(btnMatchFile, txtMatchFile, "PeptideFile", new OpenFileArgument("MS3 Match", "ms3match"), true);
      AddComponent(ms3MatchedFile);

      rawFile = new RcpaFileField(btnRawFile, txtRawFile, "RawFile", new OpenFileArgument("RAW", "raw"), true);
      AddComponent(rawFile);

      this.Text = Constants.GetSQHTitle(title, version);
      this.reader = null;
    }

    private void ShowImage()
    {
      if (reader != null && gvPeptides.SelectedRows.Count > 0)
      {
        zgcMS2.GraphPane.ClearData();

        var ms3 = gvPeptides.SelectedRows[0].DataBoundItem as MS3MatchItem;
        var s1 = GetPeakList(ms3.Scan1);
        var s2 = GetPeakList(ms3.Scan2);

        var ppl1 = new PointPairList();
        foreach (var m in s1)
        {
          ppl1.Add(new PointPair(m.Mz, m.Intensity));
        }
        zgcMS2.GraphPane.AddIndividualLine(ms3.Sequence1 + ":" + ms3.Scan1.ToString(), ppl1, Color.Red);

        var ppl2 = new PointPairList();
        foreach (var m in s2)
        {
          ppl2.Add(new PointPair(m.Mz, -m.Intensity));
        }
        zgcMS2.GraphPane.AddIndividualLine(ms3.Sequence2 + ":" + ms3.Scan2.ToString(), ppl2, Color.Blue);
        zgcMS2.UpdateGraph();

        zgcMS3.MasterPane.PaneList.Clear();
        var ppls1 = GetMS3Spectra(ms3.Scan1);
        var ppls2 = GetMS3Spectra(ms3.Scan2);

        var g = this.CreateGraphics();
        zgcMS3.InitMasterPanel(g, ppls1.Count + ppls2.Count, "");

        zgcMS3.MasterPane.SetLayout(g, 2, (ppls1.Count + ppls2.Count + 1) / 2);
        zgcMS3.AxisChange();

        zgcMS3.IsSynchronizeXAxes = false;

        for (int i = 0; i < ppls1.Count; i++)
        {
          zgcMS3.MasterPane.PaneList[i].Title.Text = string.Format("{0:0.0000}", ppls1[i].Item1);
          zgcMS3.MasterPane.PaneList[i].AddIndividualLine("", ppls1[i].Item2, Color.Red);
        }

        for (int i = 0; i < ppls2.Count; i++)
        {
          zgcMS3.MasterPane.PaneList[i + ppls1.Count].Title.Text = string.Format("{0:0.0000}", ppls2[i].Item1);
          zgcMS3.MasterPane.PaneList[i + ppls1.Count].AddIndividualLine("", ppls2[i].Item2, Color.Blue);
        }
        zgcMS3.UpdateGraph();
      }
    }

    private List<Tuple<double, PointPairList>> GetMS3Spectra(int scan)
    {
      var result = new List<Tuple<double, PointPairList>>();
      for (int i = scan + 1; i <= lastScan; i++)
      {
        var mslevel = reader.GetMsLevel(i);
        if (mslevel != 3)
        {
          break;
        }

        var pplms3 = new PointPairList();
        var ms3spectra = GetPeakList(i);
        var ms3precursor = reader.GetPrecursorPeak(i);
        foreach (var m in ms3spectra)
        {
          pplms3.Add(new PointPair(m.Mz, m.Intensity));
        }
        result.Add(new Tuple<double, PointPairList>(ms3precursor.Mz, pplms3));
      }

      result.Sort((m1, m2) => m1.Item1.CompareTo(m2.Item1));
      return result;
    }

    private Spectrum.PeakList<Spectrum.Peak> GetPeakList(int scan)
    {
      var s1 = reader.GetPeakList(scan);
      var s1max = s1.Max(l => l.Intensity);
      s1.ForEach(m => m.Intensity = m.Intensity * 100.0 / s1max);
      return s1;
    }

    private void SpectrumSnpValidatorUI_Load(object sender, EventArgs e)
    {
      LoadOption();
    }

    private void SpectrumSnpValidatorUI_FormClosing(object sender, FormClosingEventArgs e)
    {
      SaveOption();
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
        new SpectrumScanValidatorUI().MyShow();
      }

      #endregion
    }

    private void btnClose_Click(object sender, EventArgs e)
    {
      Close();
    }

    private void btnLoad_Click(object sender, EventArgs e)
    {
      if (rawFile.Exists)
      {
        if (reader != null)
        {
          reader.Close();
        }
        reader = RawFileFactory.GetRawFileReader(rawFile.FullName);
        lastScan = reader.GetLastSpectrumNumber();
      }

      if (ms3MatchedFile.Exists)
      {
        items = (from line in File.ReadAllLines(ms3MatchedFile.FullName).Skip(1)
                 let parts = line.Split('\t')
                 let pm = int.Parse(parts[6])
                 where pm > 0
                 select new MS3MatchItem()
                 {
                   Category = parts[0],
                   Sequence1 = parts[1],
                   Scan1 = int.Parse(parts[2]),
                   Sequence2 = parts[3],
                   Scan2 = int.Parse(parts[4]),
                   MinMz = parts[5].StringAfter(">=").Trim(),
                   PrecursorMatched = pm,
                   MS3Matched = int.Parse(parts[7])
                 }).ToList();

        gvPeptides.DataSource = items;
      }
    }

    private void gvPeptides_SelectionChanged(object sender, EventArgs e)
    {
      ShowImage();
    }
  }
}
