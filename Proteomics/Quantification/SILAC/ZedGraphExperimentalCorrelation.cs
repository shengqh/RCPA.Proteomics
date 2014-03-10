using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ZedGraph;
using RCPA.Gui.Image;
using System.Drawing;

namespace RCPA.Proteomics.Quantification.SILAC
{
  public class ZedGraphExperimentalCorrelation
  {
    private ZedGraphControl zgcGraph;
    private GraphPane panel;

    public ZedGraphExperimentalCorrelation(ZedGraphControl zgcGraph, GraphPane panel, string title)
    {
      this.zgcGraph = zgcGraph;
      this.panel = panel;

      panel.InitGraphPane(title, "Scan", "Cosine similarity", true, 0.0);

      ZedGraphicExtension.UpdateGraph(zgcGraph);
    }

    public ZedGraphExperimentalCorrelation(ZedGraphControl zgcGraph)
      : this(zgcGraph, zgcGraph.GraphPane, "Experimental Envelopes")
    { }

    public void Update(object sender, UpdateQuantificationItemEventArgs e)
    {
      var summary = e.Item as SilacQuantificationSummaryItem;

      if (summary == null)
      {
        throw new ArgumentNullException("UpdateQuantificationItemEventArgs.Item cannot be null");
      }

      panel.ClearData();
      try
      {
        var lightCorrel = new PointPairList();
        foreach (var entry in summary.ObservedEnvelopes)
        {
          lightCorrel.Add(new PointPair(entry.Scan, entry.LightCorrelation));
        }

        var heavyCorrel = new PointPairList();
        foreach (var entry in summary.ObservedEnvelopes)
        {
          heavyCorrel.Add(new PointPair(entry.Scan, entry.HeavyCorrelation));
        }

        var sampleCorrel = summary.SampleIsLight ? lightCorrel : heavyCorrel;
        var refCorrel = summary.SampleIsLight ? heavyCorrel : lightCorrel;
        panel.AddCurve("Sample", sampleCorrel, SilacQuantificationConstants.SAMPLE_COLOR, SymbolType.None);
        panel.AddCurve("Reference", refCorrel, SilacQuantificationConstants.REFERENCE_COLOR, SymbolType.None);
        panel.YAxis.Scale.Min = 0.0;
        panel.YAxis.Scale.Max = 1.2;
      }
      finally
      {
        ZedGraphicExtension.UpdateGraph(this.zgcGraph);
      }
    }
  }
}
