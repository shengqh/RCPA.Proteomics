using System.Drawing;
using ZedGraph;

namespace RCPA.Proteomics.Quantification.O18
{
  public class ZedGraphO18Experimental_ScanPPM : IQuantificationItemUpdate
  {
    private CompositeQuantificationItemUpdate updates = new CompositeQuantificationItemUpdate();

    public ZedGraphO18Experimental_ScanPPM(ZedGraphControl zgcGraph, Graphics g)
    {
      MasterPane myMaster = ZedGraphicExtension.InitMasterPanel(zgcGraph, g, 2, "Experimental Envelopes");

      updates.Updates.Add(new ZedGraphO18ExperimentalScan(zgcGraph, myMaster.PaneList[0], ""));

      updates.Updates.Add(new ZedGraphO18ExperimentalPPM(zgcGraph, myMaster.PaneList[1], ""));
    }

    public void Update(object sender, UpdateQuantificationItemEventArgs e)
    {
      updates.Update(sender, e);
    }
  }
}
