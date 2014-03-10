using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ZedGraph;
using RCPA.Gui.Image;
using System.Drawing;

namespace RCPA.Proteomics.Quantification.SmallMolecule
{
  public class ZedGraphSmallMoleculeFilePeak
  {
    private ZedGraphControl zgcGraph;

    private GraphPane panel;

    private FileData2 data;

    private Color color;

    public FileData2 Data
    {
      get
      {
        return data;
      }
      set
      {
        data = value;
        this.LastScan = data.Values.FirstOrDefault().Max(m => m.Scan);
      }
    }

    public int LastScan { get; set; }

    public ZedGraphSmallMoleculeFilePeak(ZedGraphControl zgcGraph, GraphPane panel, string title, Color color)
    {
      this.zgcGraph = zgcGraph;

      this.panel = panel;

      this.color = color;

      panel.InitGraphPane(title, "Scan", "Intensity", true, 0.0);

      ZedGraphicExtension.UpdateGraph(zgcGraph);
    }

    public void UpdateMaxIntensity(double maxIntensity)
    {
      panel.YAxis.Scale.Max = maxIntensity;

      ZedGraphicExtension.UpdateGraph(zgcGraph);
    }

    public void Update(object sender, UpdateSmallMoleculeFilePeakEventArgs e)
    {
      var peak = e.Peak;

      panel.ClearData();

      var pplReal = new PointPairList();
      var pplSmoothed = new PointPairList();

      var map = data[peak];

      foreach (var scan in map)
      {
        pplReal.Add(scan.Scan, scan.Intensity);
        pplSmoothed.Add(scan.Scan, scan.SmoothedIntensity);
      }

      if (map.Last().Scan != LastScan)
      {
        pplReal.Add(LastScan, 0.0);
      }

      panel.YAxis.Scale.MaxAuto = true;

      bool bSmoothed = false;
      foreach (var m in map)
      {
        if (m.Intensity != m.SmoothedIntensity)
        {
          bSmoothed = true;
          break;
        }
      }

      if (bSmoothed)
      {
        var line = panel.AddCurve("", pplSmoothed, Color.Blue, SymbolType.None);
      }

      panel.AddCurve("", pplReal, this.color, SymbolType.None);

      ZedGraphicExtension.UpdateGraph(zgcGraph);
    }

    public double GetYScaleMax()
    {
      return panel.YAxis.Scale.Max;
    }
  }
}
