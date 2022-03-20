using System;
using System.Drawing;
using System.Linq;
using ZedGraph;

namespace RCPA.Proteomics.Quantification.O18
{
  public class ZedGraphO18ExperimentalScan : IQuantificationItemUpdate
  {
    private ZedGraphControl zgcGraph;
    private GraphPane panel;

    public ZedGraphO18ExperimentalScan(ZedGraphControl zgcGraph, GraphPane panel, string title)
    {
      this.zgcGraph = zgcGraph;
      this.panel = panel;

      panel.InitGraphPane(title, "Scan", "Intensity", true, 0.0);

      ZedGraphicExtension.UpdateGraph(zgcGraph);
    }

    public ZedGraphO18ExperimentalScan(ZedGraphControl zgcGraph)
      : this(zgcGraph, zgcGraph.GraphPane, "Experimental Envelopes")
    { }

    public void Update(object sender, UpdateQuantificationItemEventArgs e)
    {
      var summary = e.Item as O18QuantificationSummaryItem;
      if (summary == null)
      {
        throw new ArgumentNullException("UpdateQuantificationItemEventArgs.Item cannot be null");
      }

      panel.ClearData();

      var pplO16 = new PointPairList();
      var pplO18_1 = new PointPairList();
      var pplO18_2 = new PointPairList();

      var pplO16Curve = new PointPairList();
      var pplO18_1Curve = new PointPairList();
      var pplO18_2Curve = new PointPairList();

      var identified = new PointPairList();
      var currentScan = new PointPairList();

      bool bChecked = false;
      for (int i = 0; i < summary.ObservedEnvelopes.Count; i++)
      {
        var envelope = summary.ObservedEnvelopes[i];
        if (envelope.Enabled != bChecked)
        {
          if (pplO16Curve.Count > 0)
          {
            if (bChecked)
            {
              panel.AddPoly("", pplO16Curve, O18QuantificationConstants.COLOR_O16, new[] { O18QuantificationConstants.COLOR_PLOY });
              panel.AddPoly("", pplO18_1Curve, O18QuantificationConstants.COLOR_O18_1, new[] { O18QuantificationConstants.COLOR_PLOY });
              panel.AddPoly("", pplO18_2Curve, O18QuantificationConstants.COLOR_O18_2, new[] { O18QuantificationConstants.COLOR_PLOY });
            }
          }
          pplO16Curve = new PointPairList();
          pplO18_1Curve = new PointPairList();
          pplO18_2Curve = new PointPairList();
          bChecked = envelope.Enabled;
        }

        pplO16.Add(envelope.Scan, envelope[0].Intensity);
        pplO18_1.Add(envelope.Scan, envelope[2].Intensity);
        pplO18_2.Add(envelope.Scan, envelope[4].Intensity);

        pplO16Curve.Add(envelope.Scan, envelope[0].Intensity);
        pplO18_1Curve.Add(envelope.Scan, envelope[2].Intensity);
        pplO18_2Curve.Add(envelope.Scan, envelope[4].Intensity);

        if (envelope.IsIdentified)
        {
          identified.Add(envelope.Scan, new double[] { envelope[0].Intensity, envelope[2].Intensity, envelope[4].Intensity }.Max());
        }

        if (envelope.IsSelected)
        {
          currentScan.Add(envelope.Scan, new double[] { envelope[0].Intensity, envelope[2].Intensity, envelope[4].Intensity }.Max());
        }
      }

      if (pplO16Curve.Count > 0)
      {
        if (bChecked)
        {
          panel.AddPoly("", pplO16Curve, O18QuantificationConstants.COLOR_O16, new[] { O18QuantificationConstants.COLOR_PLOY });
          panel.AddPoly("", pplO18_1Curve, O18QuantificationConstants.COLOR_O18_1, new[] { O18QuantificationConstants.COLOR_PLOY });
          panel.AddPoly("", pplO18_2Curve, O18QuantificationConstants.COLOR_O18_2, new[] { O18QuantificationConstants.COLOR_PLOY });
        }
      }

      panel.AddCurve("O16", pplO16, O18QuantificationConstants.COLOR_O16, SymbolType.None);
      panel.AddCurve("O18(1)", pplO18_1, O18QuantificationConstants.COLOR_O18_1, SymbolType.None);
      panel.AddCurve("O18(2)", pplO18_2, O18QuantificationConstants.COLOR_O18_2, SymbolType.None);

      panel.AddIndividualLine("Identified", identified, O18QuantificationConstants.COLOR_IDENTIFIED);
      panel.AddIndividualLine("CurrentScan", currentScan, Color.Black);

      ZedGraphicExtension.UpdateGraph(zgcGraph);
    }
  }
}
