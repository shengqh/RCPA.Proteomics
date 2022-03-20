using System.Drawing;
using ZedGraph;

namespace RCPA.Proteomics.Quantification.ITraq
{
  public class ZedGraphITraqSpectrum
  {
    private ZedGraphControl zgcGraph;

    private ZedGraphScanITraq scanItraq;
    private ZedGraphScanIsolationWindow isolationWindow;

    public ZedGraphITraqSpectrum(ZedGraphControl zgcGraph, Graphics g, string title)
    {
      this.zgcGraph = zgcGraph;

      zgcGraph.InitMasterPanel(g, 2, title, PaneLayout.SingleColumn);

      scanItraq = new ZedGraphScanITraq(zgcGraph, zgcGraph.MasterPane[0], false);
      isolationWindow = new ZedGraphScanIsolationWindow(zgcGraph, zgcGraph.MasterPane[1]);

      ZedGraphicExtension.UpdateGraph(zgcGraph);
    }

    public void Update(object sender, UpdateQuantificationItemEventArgs e)
    {
      scanItraq.Update(sender, e);
      isolationWindow.Update(sender, e);
    }
  }
}
