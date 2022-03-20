using RCPA.Proteomics.Summary;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using ZedGraph;

namespace RCPA.Proteomics.Quantification
{
  public class ZedGraphExtendProtein : AbstractZedGraph
  {
    public Color RegressionLineColor { get; set; }

    public ZedGraphExtendProtein(ZedGraphControl zgcGraph, GraphPane panel, string title)
      : base(zgcGraph, panel, title)
    {
      this.RegressionLineColor = Color.Red;
    }

    public ZedGraphExtendProtein(ZedGraphControl zgcGraph)
      : base(zgcGraph, "Protein Regression")
    {
      this.RegressionLineColor = Color.Red;
    }

    public override void Update(object sender, UpdateQuantificationItemEventArgs e)
    {
      IExtendQuantificationSummaryOption option = e.Option as IExtendQuantificationSummaryOption;
      string xTitle = string.Format("Log({0})", option.Func.ReferenceKey);
      string yTitle = string.Format("Log({0})", option.Func.SampleKey);
      panel.InitGraphPane(title, xTitle, yTitle, true, 0.0);

      IIdentifiedProteinGroup group = e.Item as IIdentifiedProteinGroup;
      try
      {
        Dictionary<string, List<PointPairList>> pplMap = new Dictionary<string, List<PointPairList>>();

        var pplNormal = new PointPairList();
        var pplSelected = new PointPairList();
        var pplOutlier = new PointPairList();

        var spectra = group.GetPeptides();

        foreach (var pep in spectra)
        {
          if (pep.IsEnabled(true) && option.IsPeptideRatioValid(pep))
          {
            var key = option.GetPeptideClassification(pep);

            if (!pplMap.ContainsKey(key))
            {
              var lst = new List<PointPairList>();
              pplMap[key] = lst;
              lst.Add(new PointPairList());//selected
              lst.Add(new PointPairList());//outlier
              lst.Add(new PointPairList());//normal
            }

            var ppls = pplMap[key];

            PointPairList ppl;
            if (pep.Selected)
            {
              ppl = ppls[0];
            }
            else if (option.IsPeptideOutlier(pep))
            {
              ppl = ppls[1];
            }
            else
            {
              ppl = ppls[2];
            }

            double refIntensity = option.Func.GetReferenceIntensity(pep);
            double sampleIntensity = option.Func.GetSampleIntensity(pep);
            ppl.Add(refIntensity, sampleIntensity);
            ppl[ppl.Count - 1].Tag = pep;

            Debug.Assert(ppl[ppl.Count - 1].Tag == pep);
          }
        }

        this.panel.ClearData();

        foreach (var key in pplMap.Keys)
        {
          var ppls = pplMap[key];
          this.panel.AddPoints(ppls[0], SelectedColor);

          this.panel.AddPoints(ppls[1], NormalColor);

          this.panel.AddPoints(ppls[2], OutlierColor);

          var pplTotal = new PointPairList();
          pplTotal.AddRange(ppls[0]);
          pplTotal.AddRange(ppls[1]);
          pplTotal.AddRange(ppls[2]);

          if (pplTotal.Count > 0)
          {
            PointPairList line = ZedGraphicExtension.GetRegressionLine(pplTotal, option.GetProteinRatio(group[0], key));

            var lineItem = this.panel.AddCurve(option.GetProteinRatioDescription(group[0], key), line, RegressionLineColor, SymbolType.None);
            lineItem.Label.FontSpec = new FontSpec() { Size = 15, Border = new Border() { IsVisible = false } };
          }
        }
      }
      finally
      {
        ZedGraphicExtension.UpdateGraph(this.zgcGraph);
      }
    }
  }
}
