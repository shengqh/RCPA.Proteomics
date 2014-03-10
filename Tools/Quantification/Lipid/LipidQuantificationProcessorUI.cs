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
using RCPA.Proteomics.Quantification.Lipid;
using RCPA.Gui.Command;
using RCPA.Proteomics.Raw;
using RCPA.Gui.Image;
using ZedGraph;
using System.IO;
using RCPA.Utils;
using RCPA.Proteomics.Spectrum;
using RCPA.Proteomics;
using RCPA.Proteomics.Quantification;
using System.Drawing.Drawing2D;

namespace RCPA.Tools.Quantification.Lipid
{
  public partial class LipidQuantificationProcessorUI : AbstractFileProcessorUI
  {
    private static string title = "Liqid Quantification";

    private static string version = "1.0.2";

    private RcpaFileField rawFile;

    private RcpaDoubleField productIonPPM;

    private RcpaDoubleField precursorPPM;

    private string[] rawExtentions = new string[] { "raw", "mzData.xml", "mzData", "mzXML" };

    private string lastQueryFileName;
    private string lastRawFileName;
    private string lastProductIonMz;
    private string lastPrecursorMz;
    private double lastProductIonPPM;
    private double lastPrecursorIonPPM;

    private int sortIndex = 1;

    private bool lvScanUpdating = false;

    private static readonly Color PLOY_COLOR = Color.FromArgb(255, 255, 150);

    public LipidQuantificationProcessorUI()
    {
      InitializeComponent();

      SetFileArgument("QueryItem", new OpenFileArgument("Query Item", ".txt"));

      rawFile = new RcpaFileField(btnRawFile, txtRawFile, "RawFile", new OpenFileArgument("Raw", rawExtentions), true);
      AddComponent(rawFile);

      productIonPPM = new RcpaDoubleField(txtProductPPM, "ProductIonPPM", "Product Ion Tolerance (ppm)", 20, true);
      AddComponent(productIonPPM);

      precursorPPM = new RcpaDoubleField(txtPrecursorPPM, "PrecursorPPM", "Precursor Ion Tolerance (ppm)", 20, true);
      AddComponent(precursorPPM);

      this.Text = Constants.GetSQHTitle(title, version);

      AddButton(btnSmooth);
      AddButton(btnSave);
      AddButton(btnExport);
    }

    protected override IFileProcessor GetFileProcessor()
    {
      lastQueryFileName = GetOriginFile();
      lastRawFileName = rawFile.FullName;

      lastProductIonPPM = productIonPPM.Value;
      lastPrecursorIonPPM = precursorPPM.Value;

      return new LipidQuantificationProcessor(rawFile.FullName, lastProductIonPPM, lastPrecursorIonPPM);
    }

    protected override void ShowReturnInfo(IEnumerable<string> returnInfo)
    {
      base.ShowReturnInfo(returnInfo);

      QueryItemListFormat format = new QueryItemListFormat();
      var queryItems = format.ReadFromFile(lastQueryFileName);
      UpdateQueryItems(queryItems);
    }

    private void UpdateQueryItems(List<QueryItem> queryItems)
    {
      lvQuery.BeginUpdate();
      try
      {
        lvQuery.Items.Clear();
        foreach (var q in queryItems)
        {
          var item = lvQuery.Items.Add(q.ProductIonMz.ToString());
          item.SubItems.Add(q.MinRelativeIntensity.ToString());
        }
      }
      finally
      {
        lvQuery.EndUpdate();
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
        new LipidQuantificationProcessorUI().MyShow();
      }

      #endregion
    }

    private void lvQuery_SelectedIndexChanged(object sender, EventArgs e)
    {
      UpdatePrecursorMzs();
    }

    private void UpdatePrecursorMzs()
    {
      if (lvQuery.SelectedItems.Count > 0)
      {
        lastProductIonMz = lvQuery.SelectedItems[0].Text;

        string savedQueryFile = GetAreaFileName();

        var format = new PrecursorAreaListTextFormat();

        var precursorMzs = format.ReadFromFile(savedQueryFile);

        if (sortIndex == 0)
        {
          precursorMzs = precursorMzs.OrderBy(m => m.PrecursorMz).ToList();
        }
        else if (sortIndex == 1)
        {
          precursorMzs = precursorMzs.OrderByDescending(m => m.ScanCount).ToList();
        }
        else if (sortIndex == 2)
        {
          precursorMzs = precursorMzs.OrderByDescending(m => m.Area).ToList();
        }

        lvPrecursor.Tag = precursorMzs;
        lvPrecursor.BeginUpdate();
        try
        {
          lvPrecursor.Items.Clear();
          foreach (var d in precursorMzs)
          {
            var item = lvPrecursor.Items.Add(d.PrecursorMz.ToString());
            item.SubItems.Add(d.ScanCount.ToString());
            item.SubItems.Add(MyConvert.Format("{0:0.0}", d.Area));
            item.Checked = d.Enabled;
            item.Tag = d;
          }
        }
        finally
        {
          lvPrecursor.EndUpdate();
        }
      }
    }

    private void lvPrecursor_SelectedIndexChanged(object sender, EventArgs e)
    {
      if (lvPrecursor.SelectedItems.Count > 0)
      {
        if (lastPrecursorMz == lvPrecursor.SelectedItems[0].Text)
          return;

        lastPrecursorMz = lvPrecursor.SelectedItems[0].Text;
      }

      UpdateScans();
      UpdateCurve();
    }

    private void UpdateScans()
    {
      lvScans.BeginUpdate();
      try
      {
        lvScanUpdating = true;
        lvScans.Items.Clear();

        if (lvPrecursor.SelectedItems.Count == 0)
        {
          return;
        }

        LabelFreeSummaryItem chro = GetSelectLabelFreeSummaryItem();

        chro.ForEach(m =>
        {
          var item = lvScans.Items.Add(m.Scan.ToString());
          item.SubItems.Add(MyConvert.Format("{0:0.0000}", m.Intensity));
          item.Checked = m.Enabled;
          if (m.Identified)
          {
            item.ForeColor = Color.Red;
          }
          item.Tag = m;
        });
      }
      finally
      {
        lvScanUpdating = false;
        lvScans.EndUpdate();
      }
    }

    private void UpdatePrecursorItem()
    {
      LabelFreeSummaryItem item = lvScans.Tag as LabelFreeSummaryItem;
      if (null == item)
      {
        return;
      }

      for (int i = 0; i < item.Count; i++)
      {
        item[i].Enabled = lvScans.Items[i].Checked;
      }

      new LabelFreeSummaryItemXmlFormat().WriteToFile(GetDetailFileName(), item);

      PrecursorArea pa = (PrecursorArea)lvPrecursor.SelectedItems[0].Tag;
      pa.Area = item.GetArea();

      List<PrecursorArea> pas = (List<PrecursorArea>)lvPrecursor.Tag;
      new PrecursorAreaListTextFormat().WriteToFile(GetAreaFileName(), pas);

      lvPrecursor.SelectedItems[0].SubItems[2].Text = MyConvert.Format("{0:0.0}", pa.Area);
      lvPrecursor.Invalidate();
    }

    private LabelFreeSummaryItem GetSelectLabelFreeSummaryItem()
    {
      LabelFreeSummaryItem result = new LabelFreeSummaryItemXmlFormat().ReadFromFile(GetDetailFileName());

      lvScans.Tag = result;

      return result;
    }

    private void UpdateCurve()
    {
      foreach (GraphPane pane in zgcCurve.MasterPane.PaneList)
      {
        pane.ClearData();
      }

      try
      {
        if (lvPrecursor.SelectedItems.Count == 0)
        {
          return;
        }

        GraphPane graphScans = zgcCurve.MasterPane.PaneList[0];
        GraphPane graphPPM = zgcCurve.MasterPane.PaneList[1];

        var pplScan = new PointPairList();
        var pplPPM = new PointPairList();

        var pplScanCurve = new PointPairList();
        bool bChecked = false;

        double precursorMz = MyConvert.ToDouble(lvPrecursor.SelectedItems[0].Text);
        var identified = new PointPairList();

        foreach (ListViewItem item in this.lvScans.Items)
        {
          if (item == null)
          {
            return;
          }

          if (item.Checked != bChecked)
          {
            if (pplScanCurve.Count > 0 && bChecked)
            {
              graphScans.AddPoly("", pplScanCurve, Color.Blue, new[] { PLOY_COLOR });
            }
            pplScanCurve = new PointPairList();
            bChecked = item.Checked;
          }

          LabelFreeItem c = (LabelFreeItem)item.Tag;

          pplScan.Add(new PointPair(c.Scan, c.AdjustIntensity));
          pplScanCurve.Add(new PointPair(c.Scan, c.AdjustIntensity));
          pplPPM.Add(new PointPair(c.Scan, c.DeltaMzPPM));
          if (c.Identified)
          {
            identified.Add(new PointPair(c.Scan, c.AdjustIntensity));
          }
        }

        if (pplScanCurve.Count > 0 && bChecked)
        {
          graphScans.AddPoly("", pplScanCurve, Color.Blue, new[] { PLOY_COLOR });
        }

        graphScans.AddCurve(precursorMz.ToString(), pplScan, Color.Blue, SymbolType.None);
        graphPPM.AddCurve(precursorMz.ToString(), pplPPM, Color.Blue, SymbolType.None);
        graphScans.AddIndividualLine("Identified", identified, Color.Red);
      }
      finally
      {
        ZedGraphicExtension.UpdateGraph(this.zgcCurve);
      }
    }

    private void LipidQuantificationProcessorUI_Load(object sender, EventArgs e)
    {
      MasterPane myMaster = zgcCurve.MasterPane;
      myMaster.PaneList.Clear();

      myMaster.Margin.All = 10;
      myMaster.InnerPaneGap = 10;

      // Set the master pane title
      myMaster.Title.Text = "Experimental Envelopes";
      myMaster.Title.IsVisible = true;
      myMaster.Legend.IsVisible = false;

      GraphPane curve = new GraphPane();
      curve.XAxis.Title.Text = "Scan";
      curve.YAxis.Title.Text = "Intensity";
      myMaster.Add(curve);

      GraphPane ppm = new GraphPane();
      ppm.XAxis.Title.Text = "Scan";
      ppm.YAxis.Title.Text = "Monoisotopic PPM";
      myMaster.Add(ppm);

      myMaster.SetLayout(CreateGraphics(), PaneLayout.SingleColumn);
      zgcCurve.AxisChange();
    }

    private string GetAreaFileName()
    {
      return LipidQuantificationProcessor.GetAreaFileName(lastRawFileName, MyConvert.ToDouble(lastProductIonMz), lastProductIonPPM, lastPrecursorIonPPM);
    }

    private string GetDetailFileName()
    {
      string savedAreaFile = GetAreaFileName();
      string savedDetailDir = FileUtils.ChangeExtension(savedAreaFile, "");

      double precursorMz = MyConvert.ToDouble(lvPrecursor.SelectedItems[0].Text);
      var fName = LipidQuantificationProcessor.GetDetailFileName(lastRawFileName, precursorMz);

      return savedDetailDir + "\\" + fName;
    }

    private void lvPrecursor_ColumnClick(object sender, ColumnClickEventArgs e)
    {
      if (e.Column < 2)
      {
        sortIndex = e.Column;
        UpdatePrecursorMzs();
      }
    }

    private void btnSmooth_Click(object sender, EventArgs e)
    {
      mnuSmoothing.PerformClick();
    }

    private void setEnabledToolStripMenuItem_Click(object sender, EventArgs e)
    {
      GraphPane graphScans = zgcCurve.MasterPane.PaneList[0];
      int min = (int)graphScans.XAxis.Scale.Min;
      int max = (int)graphScans.XAxis.Scale.Max + 1;

      lvScanUpdating = true;
      try
      {
        foreach (ListViewItem item in lvScans.Items)
        {
          LabelFreeItem c = (LabelFreeItem)item.Tag;

          if (c.Scan >= min && c.Scan <= max)
          {
            item.Checked = true;
          }
        }
      }
      finally
      {
        lvScanUpdating = false;
      }

      UpdatePrecursorItem();
      UpdateCurve();
    }

    private void setEnabledOnlyToolStripMenuItem_Click(object sender, EventArgs e)
    {
      GraphPane graphScans = zgcCurve.MasterPane.PaneList[0];
      int min = (int)graphScans.XAxis.Scale.Min;
      int max = (int)graphScans.XAxis.Scale.Max + 1;

      lvScanUpdating = true;
      try
      {
        foreach (ListViewItem item in lvScans.Items)
        {
          LabelFreeItem c = (LabelFreeItem)item.Tag;

          item.Checked = c.Scan >= min && c.Scan <= max;
        }
      }
      finally
      {
        lvScanUpdating = false;
      }

      UpdatePrecursorItem();
      UpdateCurve();
    }

    private void zgcCurve_ContextMenuBuilder(ZedGraphControl sender, ContextMenuStrip menuStrip, Point mousePt, ZedGraphControl.ContextMenuObjectState objState)
    {
      menuStrip.Items.Add(toolStripMenuItem1);
      menuStrip.Items.Add(mnuSetEnabled);
      menuStrip.Items.Add(mnuSetEnabledOnly);
      menuStrip.Items.Add(mnuSmoothing);
    }

    private void mnuSavitzkyGolaySmoothing_Click(object sender, EventArgs e)
    {
      List<double> source = new List<double>();
      foreach (ListViewItem item in lvScans.Items)
      {
        LabelFreeItem c = (LabelFreeItem)item.Tag;
        source.Add(c.AdjustIntensity);
      }

      var result = source.ToArray();
      for (int repeat = 0; repeat < 10; repeat++)
      {
        result = MathUtils.SavitzkyGolay7Point(result);
        for (int i = 0; i < result.Length; i++)
        {
          result[i] = Math.Max(0, result[i]);
        }
      }

      int index = 0;
      foreach (ListViewItem item in lvScans.Items)
      {
        LabelFreeItem c = (LabelFreeItem)item.Tag;
        c.AdjustIntensity = result[index++];
      }

      if (lvPrecursor.SelectedItems.Count > 0)
      {
        double area = 0.0;
        foreach (ListViewItem item in lvScans.Items)
        {
          if (item.Checked)
          {
            LabelFreeItem c = (LabelFreeItem)item.Tag;
            area += c.AdjustIntensity;
          }
        }
        lvPrecursor.SelectedItems[0].SubItems[2].Text = area.ToString("0.0");
      }

      UpdateCurve();
    }

    private void btnExport_Click(object sender, EventArgs e)
    {
      if (saveFileDialog1.ShowDialog() == DialogResult.OK)
      {
        List<PrecursorArea> enabled = new List<PrecursorArea> ();
        foreach (ListViewItem item in lvPrecursor.Items)
        {
          if (item.Checked)
          {
            enabled.Add(item.Tag as PrecursorArea);
          }
        }

        new PrecursorAreaListTextFormat().WriteToFile(saveFileDialog1.FileName, enabled);

        MessageBox.Show(this, "Save Finished!", "Success");
      }
    }

    private void lvScans_ItemChecked(object sender, ItemCheckedEventArgs e)
    {
      if (!lvScanUpdating)
      {
        UpdatePrecursorItem();
      }
    }

    private void btnSave_Click(object sender, EventArgs e)
    {
      List<PrecursorArea> all = new List<PrecursorArea>();
      foreach (ListViewItem item in lvPrecursor.Items)
      {
        PrecursorArea pa = item.Tag as PrecursorArea;
        pa.Enabled = item.Checked;
        all.Add(pa);
      }

      new PrecursorAreaListTextFormat().WriteToFile(GetAreaFileName(), all);
    }
  }
}
