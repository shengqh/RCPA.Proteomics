using RCPA.Proteomics.Summary;
using RCPA.Utils;
using System;
using System.Diagnostics;
using System.Linq;
using ZedGraph;

namespace RCPA.Proteomics.Quantification
{
  public class ZedGraphProteinsLSPAD : AbstractZedGraph
  {
    public ZedGraphProteinsLSPAD(ZedGraphControl zgcGraph, GraphPane panel, string title)
      : base(zgcGraph, panel, title)
    { }

    public ZedGraphProteinsLSPAD(ZedGraphControl zgcGraph)
      : base(zgcGraph, "Protein Ratios")
    { }

    public override void Update(object sender, UpdateQuantificationItemEventArgs e)
    {
      IIdentifiedResult mr = e.Item as IIdentifiedResult;
      IQuantificationSummaryOption option = e.Option as IQuantificationSummaryOption;

      string xTitle = MyConvert.Format("(Log({0}) + Log({1})) / 2", option.Func.ReferenceKey, option.Func.SampleKey);
      string yTitle = MyConvert.Format("Log(Ratio)");

      panel.InitGraphPane(title, xTitle, yTitle, true, 0.0);

      var groups = from g in mr
                   where option.IsProteinRatioValid(g[0])
                   select g;

      try
      {
        var pplNormal = new PointPairList();
        var pplSelected = new PointPairList();
        var pplOutlier = new PointPairList();

        double maxX = 0.0;
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

          double int1 = Math.Log(option.Func.GetReferenceIntensity(group[0]));
          double int2 = Math.Log(option.Func.GetSampleIntensity(group[0]));

          double A = (int1 + int2) / 2;
          double ratio = Math.Log(option.GetProteinRatio(group[0]));
          ppl.Add(A, ratio);
          ppl[ppl.Count - 1].Tag = group;

          Debug.Assert(ppl[ppl.Count - 1].Tag == group);

          maxX = Math.Max(A, maxX);
        }

        this.panel.ClearData();

        var ratios = (from mpg in groups
                      let ratio = Math.Log(option.GetProteinRatio(mpg[0]))
                      orderby ratio
                      select ratio).ToList();

        panel.DrawProbabilityRange(maxX, ratios);

        this.panel.AddPoints(pplOutlier, OutlierColor, "Outlier");

        this.panel.AddPoints(pplSelected, GroupColor, "Current Protein");

        this.panel.AddPoints(pplNormal, NormalColor, "Other");
      }
      finally
      {
        ZedGraphicExtension.UpdateGraph(this.zgcGraph);
      }
    }
  }
}
