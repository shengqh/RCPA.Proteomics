using System;
using System.Drawing;
using ZedGraph;

namespace RCPA.Proteomics.Quantification.Srm
{
  public class ZedGraphMRMRegression
  {
    private ZedGraphControl zgcGraph;
    private GraphPane panel;

    public ZedGraphMRMRegression(ZedGraphControl zgcGraph, GraphPane panel)
    {
      this.zgcGraph = zgcGraph;
      this.panel = panel;

      panel.InitGraphPane(null, "Heavy intensity", "Light intensity", true, 0.0);

      ZedGraphicExtension.UpdateGraph(zgcGraph);
    }

    public void Update(object sender, UpdateMRMPairedProductIonEventArgs e)
    {
      var summary = e.Item;

      if (summary == null)
      {
        throw new ArgumentNullException("UpdateMRMItemEventArgs.Item cannot be null");
      }

      panel.Tag = e.Item;

      panel.ClearData();
      try
      {
        if (summary.Heavy != null)
        {
          var pplRed = new PointPairList();
          var noise = summary.DeductBaseLine ? summary.Light.Noise : 0.0;
          for (int i = 0; i < summary.Light.Intensities.Count; i++)
          {
            if (summary.Light.Intensities[i].Enabled)
            {
              pplRed.Add(new PointPair(summary.Heavy.Intensities[i].Intensity - noise, summary.Light.Intensities[i].Intensity - noise, 0.0, i));
            }
          };

          if (pplRed.Count > 0)
          {
            panel.AddPoints(pplRed, Color.Red);
            PointPairList line = ZedGraphicExtension.GetRegressionLine(pplRed, summary.Ratio, summary.Distance);
            panel.AddCurve(null, line, Color.Green, SymbolType.None);
            panel.Title.Text = summary.GetFormula();
            panel.Title.IsVisible = true;
          }
          else
          {
            panel.Title.IsVisible = false;
          }
        }
      }
      finally
      {
        ZedGraphicExtension.UpdateGraph(this.zgcGraph);
      }
    }
  }
}
