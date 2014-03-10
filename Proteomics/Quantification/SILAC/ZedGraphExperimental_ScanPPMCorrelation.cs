using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ZedGraph;
using RCPA.Gui.Image;
using System.Drawing;

namespace RCPA.Proteomics.Quantification.SILAC
{
  public class ZedGraphExperimental_ScanPPMCorrelation
  {
    private ZedGraphExperimentalScan zgcScan;
    private ZedGraphExperimentalPPM zgcPPM;
    private ZedGraphExperimentalCorrelation zgcCorrelation;

    public ZedGraphExperimental_ScanPPMCorrelation(ZedGraphControl zgcGraph, Graphics g)
    {
      MasterPane myMaster = zgcGraph.MasterPane;
      myMaster.PaneList.Clear();

      myMaster.Margin.All = 10;
      myMaster.InnerPaneGap = 10;

      // Set the master pane title
      myMaster.Title.Text = "Experimental Envelopes";
      myMaster.Title.IsVisible = true;
      myMaster.Legend.IsVisible = false;

      myMaster.Add(new GraphPane());
      myMaster.Add(new GraphPane());
      myMaster.Add(new GraphPane());
      myMaster.SetLayout(g, PaneLayout.SingleColumn);
      zgcGraph.AxisChange();

      zgcScan = new ZedGraphExperimentalScan(zgcGraph, myMaster.PaneList[0], "");
      zgcPPM = new ZedGraphExperimentalPPM(zgcGraph, myMaster.PaneList[1], "");
      zgcCorrelation = new ZedGraphExperimentalCorrelation(zgcGraph, myMaster.PaneList[2], "");
    }

    public void Update(object sender, UpdateQuantificationItemEventArgs e)
    {
      zgcScan.Update(sender, e);
      zgcPPM.Update(sender, e);
      zgcCorrelation.Update(sender, e);
    }
  }
}
