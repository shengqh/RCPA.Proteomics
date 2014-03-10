using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using RCPA.Gui.Image;
using RCPA.Proteomics.Summary;
using RCPA.Utils;
using ZedGraph;
using System.Diagnostics;

namespace RCPA.Proteomics.Quantification
{
  public class ZedGraphProteins : AbstractZedGraph
  {
    public ZedGraphProteins(ZedGraphControl zgcGraph, GraphPane panel, string title)
      : base(zgcGraph, panel, title)
    { }

    public ZedGraphProteins(ZedGraphControl zgcGraph)
      : base(zgcGraph, "Protein Ratios")
    { }

    public override void Update(object sender, UpdateQuantificationItemEventArgs e)
    {
      IQuantificationSummaryOption option = e.Option as IQuantificationSummaryOption;
      IIdentifiedResult mr = e.Item as IIdentifiedResult;

      panel.InitGraphPane(title, option.Func.ReferenceKey, option.Func.SampleKey, true, 0.0);
      try
      {
        var pplNormal = new PointPairList();
        var pplSelected = new PointPairList();
        var pplOutlier = new PointPairList();

        var groups = (from g in mr
                     where option.IsProteinRatioValid(g[0])
                     select g).ToList();

        foreach (var group in groups)
        {
          PointPairList ppl;
          if (group.Selected)
          {
            ppl = pplSelected;
          }
          else if (option.IsProteinOutlier(group[0]))
          {
            ppl = pplOutlier;
          }
          else
          {
            ppl = pplNormal;
          }

          double sampleIntensity = option.Func.GetSampleIntensity(group[0]);
          double refIntensity = option.Func.GetReferenceIntensity(group[0]);
          ppl.Add(refIntensity, sampleIntensity);
          ppl[ppl.Count - 1].Tag = group;

          Debug.Assert(ppl[ppl.Count - 1].Tag == group);
        }

        this.panel.ClearData();

        this.panel.AddPoints(pplOutlier, OutlierColor, "Outlier");

        this.panel.AddPoints(pplSelected, GroupColor, "Current Protein");

        this.panel.AddPoints(pplNormal, NormalColor, "Other");

        var pplTotal = new PointPairList();
        pplTotal.AddRange(pplSelected);
        pplTotal.AddRange(pplNormal);
        pplTotal.AddRange(pplOutlier);

        if (pplTotal.Count > 0)
        {
          var maxValue = (from p in pplTotal
                          select Math.Max(p.X, p.Y)).Max() * 1.1;

          this.panel.XAxis.Scale.Max = maxValue;
          this.panel.YAxis.Scale.Max = maxValue;
          
          var lrrr = pplTotal.GetRegression();

          PointPairList line = pplTotal.GetRegressionLine(lrrr.Ratio);

          var lineItem = this.panel.AddCurve(MyConvert.Format("Ratio={0:0.0000}", lrrr.Ratio), line, Color.Red, SymbolType.None);
        }
      }
      finally
      {
        ZedGraphicExtension.UpdateGraph(this.zgcGraph);
      }
    }
  }
}
