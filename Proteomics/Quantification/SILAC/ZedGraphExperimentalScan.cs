using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ZedGraph;
using RCPA.Gui.Image;
using System.Drawing;

namespace RCPA.Proteomics.Quantification.SILAC
{
  public class ZedGraphExperimentalScan
  {
    private ZedGraphControl zgcGraph;
    private GraphPane panel;

    public ZedGraphExperimentalScan(ZedGraphControl zgcGraph, GraphPane panel, string title)
    {
      this.zgcGraph = zgcGraph;
      this.panel = panel;

      panel.InitGraphPane(title, "Scan", "Intensity", true, 0.0);

      ZedGraphicExtension.UpdateGraph(zgcGraph);
    }

    public ZedGraphExperimentalScan(ZedGraphControl zgcGraph)
      : this(zgcGraph, zgcGraph.GraphPane, "Experimental Envelopes")
    { }

    public void Update(object sender, UpdateQuantificationItemEventArgs e)
    {
      var summary = e.Item as SilacQuantificationSummaryItem;
      if (summary == null)
      {
        throw new ArgumentNullException("UpdateQuantificationItemEventArgs.Item cannot be null");
      }

      double sampleMz;
      double referenceMz;
      if (summary.SampleIsLight)
      {
        sampleMz = summary.ObservedEnvelopes[0].Light[0].Mz;
        referenceMz = summary.ObservedEnvelopes[0].Heavy[0].Mz;
      }
      else
      {
        sampleMz = summary.ObservedEnvelopes[0].Heavy[0].Mz;
        referenceMz = summary.ObservedEnvelopes[0].Light[0].Mz;
      }

      panel.ClearData();

      var pplSample = new PointPairList();
      var pplReference = new PointPairList();

      var pplSampleCurve = new PointPairList();
      var pplReferenceCurve = new PointPairList();
      bool bChecked = false;

      for (int i = 0; i < summary.ObservedEnvelopes.Count; i++)
      {
        var envelope = summary.ObservedEnvelopes[i];
        if (envelope.Enabled != bChecked)
        {
          if (pplSampleCurve.Count > 0)
          {
            if (bChecked)
            {
              panel.AddPoly("", pplSampleCurve, SilacQuantificationConstants.SAMPLE_COLOR, new[] { SilacQuantificationConstants.PLOY_COLOR });
              panel.AddPoly("", pplReferenceCurve, SilacQuantificationConstants.REFERENCE_COLOR, new[] { SilacQuantificationConstants.PLOY_COLOR });
            }
          }
          pplSampleCurve = new PointPairList();
          pplReferenceCurve = new PointPairList();
          bChecked = envelope.Enabled;
        }

        if (summary.SampleIsLight)
        {
          pplSampleCurve.Add(envelope.Scan, envelope.LightIntensity);
          pplReferenceCurve.Add(envelope.Scan, envelope.HeavyIntensity);
          pplSample.Add(envelope.Scan, envelope.LightIntensity);
          pplReference.Add(envelope.Scan, envelope.HeavyIntensity);
        }
        else
        {
          pplSampleCurve.Add(envelope.Scan, envelope.LightIntensity);
          pplReferenceCurve.Add(envelope.Scan, envelope.HeavyIntensity);
          pplSample.Add(envelope.Scan, envelope.HeavyIntensity);
          pplReference.Add(envelope.Scan, envelope.LightIntensity);
        }
      }

      if (pplSampleCurve.Count > 0)
      {
        if (bChecked)
        {
          panel.AddPoly("", pplSampleCurve, SilacQuantificationConstants.SAMPLE_COLOR, new[] { SilacQuantificationConstants.PLOY_COLOR });
          panel.AddPoly("", pplReferenceCurve, SilacQuantificationConstants.REFERENCE_COLOR, new[] { SilacQuantificationConstants.PLOY_COLOR });
        }
      }

      panel.AddCurve("Sample", pplSample, SilacQuantificationConstants.SAMPLE_COLOR, SymbolType.None);
      panel.AddCurve("Reference", pplReference, SilacQuantificationConstants.REFERENCE_COLOR, SymbolType.None);

      var identified = new PointPairList();
      summary.ObservedEnvelopes.FindAll(m => m.IsIdentified).ForEach(m =>
      {
        identified.Add(new PointPair(m.Scan, m.LightIntensity));
        identified.Add(new PointPair(m.Scan, m.HeavyIntensity));
      });
      panel.AddIndividualLine("Identified", identified, SilacQuantificationConstants.IDENTIFIED_COLOR);

      var currentScan = new PointPairList();
      summary.ObservedEnvelopes.FindAll(m => m.IsSelected).ForEach(m =>
      {
        currentScan.Add(new PointPair(m.Scan, Math.Max(m.LightIntensity, m.HeavyIntensity)));
      });
      if (currentScan.Count > 0)
      {
        panel.AddIndividualLine("CurrentScan", currentScan, Color.Black);
      }

      ZedGraphicExtension.UpdateGraph(zgcGraph);
    }
  }
}
