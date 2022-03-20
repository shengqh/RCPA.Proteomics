using System;
using System.Drawing;
using ZedGraph;

namespace RCPA.Proteomics.Quantification.Srm
{
  public class ZedGraphMRMProductIonScansAndRegression
  {
    private ZedGraphControl zgcGraph;
    private Graphics g;

    public ZedGraphMRMProductIonScansAndRegression(ZedGraphControl zgcGraph, Graphics g)
    {
      this.zgcGraph = zgcGraph;
      this.g = g;
    }

    public void Update(object sender, UpdateMRMPairedProductIonEventArgs e)
    {
      var summary = e.Item;
      if (summary == null)
      {
        throw new ArgumentNullException("UpdateMRMPairedProductIonEventArgs.Item cannot be null");
      }

      MasterPane myMaster = zgcGraph.MasterPane;
      myMaster.Border.IsVisible = false;
      myMaster.PaneList.Clear();
      try
      {
        myMaster.Margin.All = 10;
        myMaster.InnerPaneGap = 10;

        // Set the master pane title
        myMaster.Title.Text = MyConvert.Format("{0:0.0000} - {1:0.0000}", summary.Light == null ? 0.0 : summary.Light.PrecursorMZ, summary.Heavy == null ? 0.0 : summary.Heavy.PrecursorMZ);
        myMaster.Title.IsVisible = true;
        myMaster.Legend.IsVisible = false;

        myMaster.Add(new GraphPane());
        myMaster.Add(new GraphPane());

        new ZedGraphMRMProductIonScans(zgcGraph, myMaster.PaneList[0]).Update(null, e);
        new ZedGraphMRMRegression(zgcGraph, myMaster.PaneList[1]).Update(null, e);

        myMaster.SetLayout(g, PaneLayout.SingleRow);
        zgcGraph.AxisChange();
      }
      finally
      {
        ZedGraphicExtension.UpdateGraph(this.zgcGraph);
      }
    }
  }
}
