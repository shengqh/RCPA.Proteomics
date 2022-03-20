using System.Drawing;
using ZedGraph;

namespace RCPA.Proteomics.Quantification.ITraq
{
  public class ZedGraphScanIsolationWindow
  {
    private ZedGraphControl zgcGraph;
    private GraphPane panel;

    public ZedGraphScanIsolationWindow(ZedGraphControl zgcGraph, GraphPane panel)
    {
      this.zgcGraph = zgcGraph;

      this.panel = panel;

      panel.InitGraphPane("Isolation Window", "m/z", "Intensity", true, 0.0);

      ZedGraphicExtension.UpdateGraph(zgcGraph);
    }

    public void Update(object sender, UpdateQuantificationItemEventArgs e)
    {
      panel.ClearData();
      var summary = e.Item as IsobaricItem;
      if (null != summary)
      {
        if (summary.PeakInIsolationWindow.Count > 0)
        {
          var pplWinPrecursor = new PointPairList();
          foreach (var peak in summary.PeakInIsolationWindow)
          {
            if (peak.Tag > 0)
            {
              pplWinPrecursor.Add(new PointPair(peak.Mz, peak.Intensity, string.Format("{0:0.00000}", peak.Mz)));
            }
            else
            {
              pplWinPrecursor.Add(new PointPair(peak.Mz, peak.Intensity));
            }
          }

          panel.AddIndividualLine("", pplWinPrecursor, Color.Black, Color.Blue);

          panel.Title.Text = string.Format("Isolation Window (Specifity = {0:0.00}%)", summary.PrecursorPercentage * 100);
        }
      }

      ZedGraphicExtension.UpdateGraph(zgcGraph);
    }
  }
}
