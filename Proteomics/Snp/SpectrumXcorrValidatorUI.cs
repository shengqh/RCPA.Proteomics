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
using RCPA.Proteomics.Spectrum;

namespace RCPA.Proteomics.Snp
{
  public partial class SpectrumXcorrValidatorUI : ComponentUI
  {
    private static readonly string title = "Spectrum Xcorr Validator";
    private static readonly string version = "1.0.0";

    private RcpaFileField ms3xcorrFile;
    private RcpaFileField rawFile;
    private IRawFile reader;
    private int lastScan;

    private List<MS3XcorrItem> items;

    public SpectrumXcorrValidatorUI()
    {
      InitializeComponent();
      ms3xcorrFile = new RcpaFileField(btnMatchFile, txtMatchFile, "PeptideFile", new OpenFileArgument("MS3 Xcorr", "ms3xcorr"), true);
      AddComponent(ms3xcorrFile);

      rawFile = new RcpaFileField(btnRawFile, txtRawFile, "RawFile", new OpenFileArgument("RAW", "raw"), true);
      AddComponent(rawFile);

      this.Text = Constants.GetSQHTitle(title, version);
      this.reader = null;
    }

    private void ShowImage()
    {
      if (reader != null && gvPeptides.SelectedRows.Count > 0)
      {
        var ms3 = gvPeptides.SelectedRows[0].DataBoundItem as MS3XcorrItem;
        AddPairedPeakList(ms3, zgcMS2, ms3.MS2Scan1, ms3.MS2Scan2);
        AddPairedPeakList(ms3, zgcMS3, ms3.MS3Scan1, ms3.MS3Scan2);
      }
    }

    private void AddPairedPeakList(MS3XcorrItem ms3, ZedGraphControl zgc, int scan1, int scan2)
    {
      zgc.GraphPane.ClearData();

      var s1 = GetPeakList(scan1);
      var s2 = GetPeakList(scan2);

      var ppl1 = new PointPairList();
      foreach (var m in s1)
      {
        ppl1.Add(new PointPair(m.Mz, m.Intensity));
      }
      zgc.GraphPane.AddIndividualLine(ms3.Sequence1 + ":" + scan1.ToString(), ppl1, Color.Red);

      var ppl2 = new PointPairList();
      foreach (var m in s2)
      {
        ppl2.Add(new PointPair(m.Mz, -m.Intensity));
      }
      zgc.GraphPane.AddIndividualLine(ms3.Sequence2 + ":" + scan2.ToString(), ppl2, Color.Blue);

      if (ms3.MS2Scan1 == scan1)
      {
        zgc.GraphPane.Title.Text = "MS2: " + ms3.Sequence1 + " ~ " + ms3.Sequence2;
      }
      else
      {
        zgc.GraphPane.Title.Text = string.Format("MS3: m/z = {0:0.####}, Xcorr = {1:0.####} ~ {2:0.####} = {3:0.####}", ms3.MS3Precursor1, SpectrumXcorr.CalculateUnnormalized(s1, s1), SpectrumXcorr.CalculateUnnormalized(s2, s2), ms3.Xcorr);
      }

      zgc.UpdateGraph();
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
        new SpectrumXcorrValidatorUI().MyShow();
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

      if (ms3xcorrFile.Exists)
      {
        items = (from line in File.ReadAllLines(ms3xcorrFile.FullName).Skip(1)
                 let parts = line.Split('\t')
                 select new MS3XcorrItem()
                 {
                   Category = parts[0],
                   Sequence1 = parts[1],
                   MS2Scan1 = int.Parse(parts[2]),
                   MS2Precursor1 = double.Parse(parts[3]),
                   MS3Scan1 = int.Parse(parts[4]),
                   MS3Precursor1 = double.Parse(parts[5]),
                   Sequence2 = parts[6],
                   MS2Scan2 = int.Parse(parts[7]),
                   MS2Precursor2 = double.Parse(parts[8]),
                   MS3Scan2 = int.Parse(parts[9]),
                   MS3Precursor2 = double.Parse(parts[10]),
                   Xcorr = double.Parse(parts[11])
                 }).ToList();

        items.RemoveAll(m => m.MS3Precursor1 < 250);
        items = items.OrderBy(m => m.Category).ThenBy(m => m.Sequence1).ThenBy(m => m.MS3Precursor1).ToList();
        gvPeptides.DataSource = items;
      }
    }

    private void gvPeptides_SelectionChanged(object sender, EventArgs e)
    {
      ShowImage();
    }
  }
}
