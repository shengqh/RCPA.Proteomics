using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ZedGraph;
using RCPA.Gui.Image;
using System.Drawing;

namespace RCPA.Proteomics.Quantification.SILAC
{
  public class ZedGraphExperimentalPPM
  {
    private ZedGraphControl zgcGraph;
    private GraphPane panel;

    public ZedGraphExperimentalPPM(ZedGraphControl zgcGraph, GraphPane panel, string title)
    {
      this.zgcGraph = zgcGraph;
      this.panel = panel;

      panel.InitGraphPane(title, "Scan", "Monoisotopic PPM", true, 0.0);

      ZedGraphicExtension.UpdateGraph(zgcGraph);
    }

    public ZedGraphExperimentalPPM(ZedGraphControl zgcGraph)
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
        int firstScan = summary.ObservedEnvelopes.First().Scan;
        int lastScan = summary.ObservedEnvelopes.Last().Scan;

        var samplePPMList = summary.GetSamplePPMList();
        var refPPMList = summary.GetReferencePPMList();

        var samplePPMs = GetPoints(firstScan, lastScan, samplePPMList);
        var referencePPMs = GetPoints(firstScan, lastScan, refPPMList);

        panel.AddCurve("Sample", samplePPMs, SilacQuantificationConstants.SAMPLE_COLOR, SymbolType.None);
        panel.AddCurve("Reference", referencePPMs, SilacQuantificationConstants.REFERENCE_COLOR, SymbolType.None);

        var identified = new PointPairList();
        summary.ObservedEnvelopes.FindAll(m => m.IsIdentified).ForEach(m =>
        {
          if (samplePPMList.ContainsKey(m))
          {
            identified.Add(new PointPair(m.Scan, samplePPMList[m]));
          }
          if (refPPMList.ContainsKey(m))
          {
            identified.Add(new PointPair(m.Scan, refPPMList[m]));
          }
        });
        panel.AddIndividualLine("Identified", identified, SilacQuantificationConstants.IDENTIFIED_COLOR);

        var selected = new PointPairList();
        summary.ObservedEnvelopes.FindAll(m => m.IsSelected).ForEach(m =>
        {
          if (samplePPMList.ContainsKey(m))
          {
            selected.Add(new PointPair(m.Scan, samplePPMList[m]));
          }
          if (refPPMList.ContainsKey(m))
          {
            selected.Add(new PointPair(m.Scan, refPPMList[m]));
          }
        });

        if (selected.Count > 0)
        {
          panel.AddIndividualLine("CurrentScan", selected, Color.Black);
        }

        panel.YAxis.Scale.Min = -10;
        panel.YAxis.Scale.Max = 10;
      }
      finally
      {
        ZedGraphicExtension.UpdateGraph(this.zgcGraph);
      }
    }

    private static PointPairList GetPoints(int firstScan, int lastScan, Dictionary<SilacPeakListPair, double> samplePPMList)
    {
      var result = new PointPairList();
      result.Add(new PointPair(firstScan, 0));
      foreach (var entry in samplePPMList)
      {
        result.Add(new PointPair(entry.Key.Scan, entry.Value));
      }
      result.Add(new PointPair(lastScan, 0));
      return result;
    }
  }
}
