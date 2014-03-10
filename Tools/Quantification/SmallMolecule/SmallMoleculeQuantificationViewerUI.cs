using System;
using System.Drawing;
using System.Windows.Forms;
using System.Linq;
using RCPA.Gui;
using RCPA.Gui.Command;
using RCPA.Gui.FileArgument;
using RCPA.Gui.Image;
using RCPA.Proteomics;
using RCPA.Proteomics.Raw;
using ZedGraph;
using RCPA.Proteomics.Spectrum;
using System.Text;
using System.Collections.Generic;
using RCPA.Proteomics.Quantification.Srm;
using RCPA.Utils;
using System.IO;
using RCPA.Proteomics.Quantification.SmallMolecule;

namespace RCPA.Tools.Quantification.SmallMolecule
{
  public partial class SmallMoleculeQuantificationViewerUI : AbstractUI
  {
    private static readonly string title = "Small Molecule - Quantification Viewer";

    private static readonly string version = "1.0.0";

    private List<ZedGraphSmallMoleculeFilePeak> leftPeaks = new List<ZedGraphSmallMoleculeFilePeak>();
    private List<ZedGraphSmallMoleculeFilePeak> rightPeaks = new List<ZedGraphSmallMoleculeFilePeak>();
    private List<SmallMoleculePeakInfo> significantPeaks = new List<SmallMoleculePeakInfo>();
    private int sortColumn = 1;

    private RcpaFileField resultFile;

    public SmallMoleculeQuantificationViewerUI()
    {
      InitializeComponent();

      this.Text = Constants.GetSQHTitle(title, version);

      resultFile = new RcpaFileField(button1, textBox1, "ResultFile", new OpenFileArgument("Significance", "sig"), true);
      AddComponent(resultFile);
      sampleFiles.FileArgument = new OpenFileArgument("Sample", "txt");
      AddComponent(new RcpaMultipleFileComponent(sampleFiles.GetItemInfos(), "SampleFiles", "Sample", false, false));
      referenceFiles.FileArgument = new OpenFileArgument("Reference", "txt");
      AddComponent(new RcpaMultipleFileComponent(referenceFiles.GetItemInfos(), "ReferenceFiles", "Reference", false, false));

      InsertButton(2, btnExport);
    }

    protected void OnUpdateProductIon(UpdateSmallMoleculeFilePeakEventArgs e)
    {
      List<ZedGraphSmallMoleculeFilePeak> allPeaks = new List<ZedGraphSmallMoleculeFilePeak>();
      allPeaks.AddRange(leftPeaks);
      allPeaks.AddRange(rightPeaks);

      allPeaks.ForEach(m => m.Update(this, e));

      double maxIntensity = (from p in allPeaks select p.GetYScaleMax()).Max();

      allPeaks.ForEach(m => m.UpdateMaxIntensity(maxIntensity));
    }

    private void LoadPeaks(ZedGraphControl zgcScan, List<ZedGraphSmallMoleculeFilePeak> zgcPeaks, string[] fileNames, Color color)
    {
      var sigPeaks = from p in significantPeaks select p.Peak;
      List<FileData2> datas = FileData2.ReadFiles(fileNames, sigPeaks);

      SmallMoleculeZedGraphUtils.LoadPeaks(zgcScan, zgcPeaks, datas, color);

      zgcScan.MasterPane.SetLayout(zgcScan.CreateGraphics(), PaneLayout.SquareRowPreferred);

      zgcScan.AxisChange();
    }

    public class Command : IToolSecondLevelCommand
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
        new SmallMoleculeQuantificationViewerUI().MyShow();
      }

      #endregion

      #region IToolSecondLevelCommand Members

      public string GetSecondLevelCommandItem()
      {
        return "Small Molecule";
      }

      #endregion
    }

    protected override void DoRealGo()
    {
      this.Cursor = Cursors.WaitCursor;
      try
      {
        bool isNegativeMode = sampleFiles.FileNames[0].Contains("neg");

        significantPeaks = new SmallMoleculePeakInfoListTextFormat().ReadFromFile(resultFile.FullName);
        if (isNegativeMode)
        {
          significantPeaks.RemoveAll(m => m.Peak[0] == 'P');
        }
        else
        {
          significantPeaks.RemoveAll(m => m.Peak[0] == 'N');
        }

        foreach (var s in significantPeaks)
        {
          s.Peak = s.Peak.Substring(1);
        }

        LoadPeaks(zgcScanLeft, leftPeaks, sampleFiles.FileNames, Color.Red);
        LoadPeaks(zgcScanRight, rightPeaks, referenceFiles.FileNames, Color.Green);

        UpdatePeaks();
      }
      finally
      {
        this.Cursor = Cursors.Default;
      }
    }

    private void UpdatePeaks()
    {
      //lvPeaks.Columns[sortColumn].ImageIndex
      significantPeaks.Sort((m1, m2) =>
      {
        switch (sortColumn)
        {
          case 0: return m1.Peak.CompareTo(m2.Peak);
          case 1: return m1.TwoTail.CompareTo(m2.TwoTail);
          case 2: return m1.LeftTail.CompareTo(m2.LeftTail);
          case 3: return m1.RightTail.CompareTo(m2.RightTail);
          default: return m1.TwoTail.CompareTo(m2.TwoTail);
        }
      });

      lvPeaks.BeginUpdate();
      try
      {
        lvPeaks.Items.Clear();
        significantPeaks.ForEach(info =>
        {
          var Item = lvPeaks.Items.Add(info.Peak);
          Item.SubItems.Add(MyConvert.Format("{0:E2}", info.TwoTail));
          Item.SubItems.Add(MyConvert.Format("{0:E2}", info.LeftTail));
          Item.SubItems.Add(MyConvert.Format("{0:E2}", info.RightTail));

          if (info.LeftTail < 0.01)
          {
            Item.ForeColor = Color.Green;
          }
          else if (info.RightTail < 0.01)
          {
            Item.ForeColor = Color.Red;
          }
        });
      }
      finally
      {
        lvPeaks.EndUpdate();
      }

      if (lvPeaks.Items.Count > 0)
      {
        lvPeaks.Items[0].Selected = true;
      }
    }

    private void SmallMoleculeQuantificationViewerUI_FormClosing(object sender, FormClosingEventArgs e)
    {
      SaveOption();
    }

    private void lvPeaks_SelectedIndexChanged(object sender, EventArgs e)
    {
      if (lvPeaks.SelectedItems.Count > 0)
      {
        string scan = lvPeaks.SelectedItems[0].Text;

        UpdateSmallMoleculeFilePeakEventArgs arg = new UpdateSmallMoleculeFilePeakEventArgs(scan);

        OnUpdateProductIon(arg);
      }
    }

    private void lvPeaks_ColumnClick(object sender, ColumnClickEventArgs e)
    {
      sortColumn = e.Column;
      UpdatePeaks();
    }

    private void btnExport_Click(object sender, EventArgs e)
    {
      if (saveFileDialog1.ShowDialog() == DialogResult.OK)
      {
        using (StreamWriter sw = new StreamWriter(saveFileDialog1.FileName))
        {
          sw.Write("Peak\tTwoTail\tLeftTail\tRightTail");
          leftPeaks.ForEach(m =>
          {
            var filename = new FileInfo(m.Data.FileName).Name;
            sw.Write("\t{0}", FileUtils.ChangeExtension(filename, ""));
          });
          rightPeaks.ForEach(m =>
          {
            var filename = new FileInfo(m.Data.FileName).Name;
            sw.Write("\t{0}", FileUtils.ChangeExtension(filename, ""));
          });
          sw.WriteLine();

          significantPeaks.ForEach(m =>
          {
            sw.Write("{0}\t{1:E2}\t{2:E2}\t{3:E2}", m.Peak, m.TwoTail, m.LeftTail, m.RightTail);
            foreach (var d in m.Samples)
            {
              sw.Write("\t{0:0.0}", d);
            }
            foreach (var d in m.References)
            {
              sw.Write("\t{0:0.0}", d);
            }
            sw.WriteLine();
          });
        }
      }
    }
  }
}
