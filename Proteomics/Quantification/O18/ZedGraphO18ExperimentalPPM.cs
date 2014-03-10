using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ZedGraph;
using RCPA.Gui.Image;
using System.Drawing;

namespace RCPA.Proteomics.Quantification.O18
{
  public class ZedGraphO18ExperimentalPPM : IQuantificationItemUpdate
  {
    private ZedGraphControl zgcGraph;
    private GraphPane panel;

    public ZedGraphO18ExperimentalPPM(ZedGraphControl zgcGraph, GraphPane panel, string title)
    {
      this.zgcGraph = zgcGraph;
      this.panel = panel;

      panel.InitGraphPane(title, "Scan", "Monoisotopic PPM", true, 0.0);

      ZedGraphicExtension.UpdateGraph(zgcGraph);
    }

    public ZedGraphO18ExperimentalPPM(ZedGraphControl zgcGraph)
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
      try
      {
        int firstScan = summary.ObservedEnvelopes.First().Scan;
        int lastScan = summary.ObservedEnvelopes.Last().Scan;

        var ppms = summary.GetPPMList();

        var O16PPMs = GetPoints(firstScan, lastScan, ppms, m => m.O16);
        var O181PPMs = GetPoints(firstScan, lastScan, ppms, m => m.O181);
        var O182PPMs = GetPoints(firstScan, lastScan, ppms, m => m.O182);

        panel.AddCurve("O16", O16PPMs, O18QuantificationConstants.COLOR_O16, SymbolType.None);
        panel.AddCurve("O18(1)", O181PPMs, O18QuantificationConstants.COLOR_O18_1, SymbolType.None);
        panel.AddCurve("O18(2)", O182PPMs, O18QuantificationConstants.COLOR_O18_2, SymbolType.None);
      }
      finally
      {
        ZedGraphicExtension.UpdateGraph(this.zgcGraph);
      }
    }

    private static PointPairList GetPoints(int firstScan, int lastScan, List<O18PPMEntry> PPMList, Func<O18PPMEntry, double> ppmFunc)
    {
      var result = new PointPairList();
      result.Add(new PointPair(firstScan, 0));

      PPMList.ForEach(m =>
      {
        var ppm = ppmFunc(m);

        if (ppm != double.NaN)
        {
          result.Add(new PointPair(m.Scan, ppm));
        }
      });

      result.Add(new PointPair(lastScan, 0));
      return result;
    }
  }
}
