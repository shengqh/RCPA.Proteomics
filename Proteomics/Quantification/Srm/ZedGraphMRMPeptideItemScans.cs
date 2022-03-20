using System.Drawing;
using ZedGraph;

namespace RCPA.Proteomics.Quantification.Srm
{
  public class ZedGraphMRMPeptideItemScans
  {
    private ZedGraphControl zgcGraph;
    private Graphics g;
    public string FileName { get; set; }

    public ZedGraphMRMPeptideItemScans(ZedGraphControl zgcGraph, Graphics g, string fileName)
    {
      this.zgcGraph = zgcGraph;
      this.g = g;
      this.FileName = fileName;
    }

    public ZedGraphMRMPeptideItemScans(ZedGraphControl zgcGraph, Graphics g)
      : this(zgcGraph, g, string.Empty)
    { }

    public void Update(object sender, UpdateMRMPairedPeptideItemEventArgs e)
    {
      MasterPane myMaster = zgcGraph.MasterPane;
      myMaster.Border.IsVisible = false;

      myMaster.PaneList.Clear();
      try
      {
        myMaster.Margin.All = 1;
        myMaster.InnerPaneGap = 1;
        myMaster.Title.IsVisible = false;
        myMaster.Legend.IsVisible = false;

        var summary = e.Item;
        if (summary == null)
        {
          return;
        }

        for (int i = 0; i < summary.ProductIonPairs.Count; i++)
        {
          if (summary.ProductIonPairs[i] == null)
          {
            continue;
          }

          if (string.IsNullOrEmpty(FileName) || FileName.Equals(summary.ProductIonPairs[i].FileName))
          {
            GraphPane panel = new GraphPane();
            myMaster.Add(panel);
            ZedGraphMRMProductIonScans scan = new ZedGraphMRMProductIonScans(zgcGraph, panel);
            scan.Update(null, new UpdateMRMPairedProductIonEventArgs(summary.ProductIonPairs[i], e.ViewOption));
          }
        }

        myMaster.SetLayout(g, PaneLayout.SquareColPreferred);
        zgcGraph.AxisChange();
      }
      finally
      {
        ZedGraphicExtension.UpdateGraph(this.zgcGraph);
      }
    }
  }
}
