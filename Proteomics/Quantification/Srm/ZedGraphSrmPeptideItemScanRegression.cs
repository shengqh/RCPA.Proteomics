using System.Drawing;
using System.Linq;
using ZedGraph;

namespace RCPA.Proteomics.Quantification.Srm
{
  public class ZedGraphSrmPeptideItemScanRegression
  {
    private ZedGraphControl zgcGraph;
    private Graphics g;
    public string FileName { get; set; }

    public ZedGraphSrmPeptideItemScanRegression(ZedGraphControl zgcGraph, Graphics g, string fileName)
    {
      this.zgcGraph = zgcGraph;
      this.g = g;
      this.FileName = fileName;
    }

    public void Update(object sender, UpdateMRMPairedPeptideItemEventArgs e)
    {
      MasterPane myMaster = zgcGraph.MasterPane;
      myMaster.Border.IsVisible = false;
      myMaster.PaneList.Clear();

      bool updated = false;
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

        var ion = summary.ProductIonPairs.FirstOrDefault(m => m != null && m.FileName.Equals(FileName));
        if (ion == null)
        {
          return;
        }

        ZedGraphMRMProductIonScansAndRegression scan = new ZedGraphMRMProductIonScansAndRegression(zgcGraph, g);
        scan.Update(null, new UpdateMRMPairedProductIonEventArgs(ion, e.ViewOption));
        updated = true;
      }
      finally
      {
        if (!updated)
        {
          ZedGraphicExtension.UpdateGraph(this.zgcGraph);
        }
      }
    }
  }
}
