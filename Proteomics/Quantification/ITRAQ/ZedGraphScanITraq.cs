using System.Drawing;
using ZedGraph;

namespace RCPA.Proteomics.Quantification.ITraq
{
  public class ZedGraphScanITraq
  {
    private ZedGraphControl zgcGraph;
    private GraphPane panel;
    private bool updateGraph;

    public ZedGraphScanITraq(ZedGraphControl zgcGraph, GraphPane panel, bool updateGraph)
    {
      this.zgcGraph = zgcGraph;

      this.panel = panel;

      this.updateGraph = updateGraph;

      panel.InitGraphPane("Isobaric Tag Channels", "m/z", "Intensity", true, 0.0);

      ZedGraphicExtension.UpdateGraph(zgcGraph);
    }

    public void Update(object sender, UpdateQuantificationItemEventArgs e)
    {
      panel.ClearData();

      var summary = e.Item as IsobaricItem;
      if (null != summary)
      {
        var pplData = new PointPairList();
        if (summary.RawPeaks.Count > 0)
        {
          foreach (var peak in summary.RawPeaks)
          {
            pplData.Add(new PointPair(peak.Mz, peak.Intensity));
          }
        }
        else
        {
          var definitions = summary.Definition.Items;
          foreach (var def in definitions)
          {
            pplData.Add(new PointPair(def.Index, summary[def.Index]));
          }
        }
        panel.AddIndividualLine("", pplData, Color.Black);

        panel.XAxis.Scale.Min = summary.Definition.MinIndex - 1;
        panel.XAxis.Scale.Max = summary.Definition.MaxIndex + 1;

        panel.Title.Text = string.Format("{0},{1}", summary.Experimental, summary.Scan);
      }

      if (updateGraph)
      {
        ZedGraphicExtension.UpdateGraph(zgcGraph);
      }
    }
  }
}
