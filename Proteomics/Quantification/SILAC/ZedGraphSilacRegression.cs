using System;
using System.Drawing;
using ZedGraph;

namespace RCPA.Proteomics.Quantification.SILAC
{
  public class ZedGraphSilacRegression
  {
    private ZedGraphControl zgcGraph;

    public ZedGraphSilacRegression(ZedGraphControl zgcGraph, string title, string xtitle, string ytitle)
    {
      this.zgcGraph = zgcGraph;

      ZedGraphicExtension.InitGraph(zgcGraph, title, xtitle, ytitle, false, 0.05);
    }

    public void Update(object sender, UpdateQuantificationItemEventArgs e)
    {
      var summary = e.Item as SilacQuantificationSummaryItem;
      if (summary == null)
      {
        throw new ArgumentNullException("UpdateQuantificationItemEventArgs.Item cannot be null");
      }

      var pplRed = new PointPairList();
      var pplGreen = new PointPairList();

      var enabled = summary.ObservedEnvelopes.FindAll(m => m.Enabled);
      enabled.ForEach(m =>
      {
        PointPairList ppl = m.IsSelected ? pplGreen : pplRed;
        if (summary.SampleIsLight)
        {
          ppl.Add(new PointPair(m.HeavyIntensity, m.LightIntensity, 0.0, m));
        }
        else
        {
          ppl.Add(new PointPair(m.LightIntensity, m.HeavyIntensity, 0.0, m));
        }
      });

      ZedGraphicExtension.ClearData(this.zgcGraph, false);

      AddCurve(pplGreen, SilacQuantificationConstants.REFERENCE_COLOR);
      AddCurve(pplRed, SilacQuantificationConstants.SAMPLE_COLOR);

      var pplTotal = new PointPairList();
      pplTotal.AddRange(pplRed);
      pplTotal.AddRange(pplGreen);

      if (pplTotal.Count > 0)
      {
        PointPairList line = ZedGraphicExtension.GetRegressionLine(pplTotal, summary.Ratio);

        ZedGraphicExtension.AddDataToLine(this.zgcGraph, MyConvert.Format("Ratio = {0:0.0000}, Correlation = {1:0.0000}", summary.Ratio,
                                                    summary.RegressionCorrelation), line, SilacQuantificationConstants.IDENTIFIED_COLOR, false);
      }

      ZedGraphicExtension.UpdateGraph(this.zgcGraph);
    }

    private void AddCurve(PointPairList ppl, Color color)
    {
      if (ppl.Count > 0)
      {
        LineItem curve = this.zgcGraph.GraphPane.AddCurve("", ppl, color, SymbolType.Diamond);
        curve.Symbol.Size = 8;
        curve.Symbol.Fill = new Fill(color);
        curve.Symbol.Border.IsVisible = false;
        curve.Line.IsVisible = false;
      }
    }
  }
}
