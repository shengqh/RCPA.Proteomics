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
  public class ZedGraphProtein : AbstractZedGraph
  {
    public Color RegressionLineColor { get; set; }

    public ZedGraphProtein(ZedGraphControl zgcGraph, GraphPane panel, string title)
      : base(zgcGraph, panel, title)
    {
      this.RegressionLineColor = Color.Red;
    }

    public ZedGraphProtein(ZedGraphControl zgcGraph)
      : base(zgcGraph, "Protein Regression")
    {
      this.RegressionLineColor = Color.Red;
    }

    public override void Update(object sender, UpdateQuantificationItemEventArgs e)
    {
      IQuantificationSummaryOption option = e.Option as IQuantificationSummaryOption;
      panel.InitGraphPane(title, option.Func.ReferenceKey, option.Func.SampleKey, true, 0.0);

      IIdentifiedProteinGroup group = e.Item as IIdentifiedProteinGroup;
      try
      {
        var pplNormal = new PointPairList();
        var pplSelected = new PointPairList();
        var pplOutlier = new PointPairList();

        var spectra = group.GetPeptides();

        foreach (var pep in spectra)
        {
          if (option.IsPeptideRatioValid(pep))
          {
            PointPairList ppl;
            if (pep.Selected)
            {
              ppl = pplSelected;
            }
            else if (option.IsPeptideOutlier(pep))
            {
              ppl = pplOutlier;
            }
            else
            {
              ppl = pplNormal;
            }

            double sampleIntensity = option.Func.GetSampleIntensity(pep);
            double refIntensity = option.Func.GetReferenceIntensity(pep);
            ppl.Add(refIntensity, sampleIntensity);
            ppl[ppl.Count - 1].Tag = pep;

            Debug.Assert(ppl[ppl.Count - 1].Tag == pep);
          }
        }

        this.panel.ClearData();

        this.panel.AddPoints(pplSelected, SelectedColor);

        this.panel.AddPoints(pplNormal, NormalColor);

        this.panel.AddPoints(pplOutlier, OutlierColor);

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

          var ratio = option.GetProteinRatio(group[0]);

          PointPairList line = pplTotal.GetRegressionLine(ratio);

          var lineItem = this.panel.AddCurve(option.GetProteinRatioDescription(group[0]), line, RegressionLineColor, SymbolType.None);
          lineItem.Label.FontSpec = new FontSpec() { Size = 15, Border = new Border() { IsVisible = false } };
        }
      }
      finally
      {
        ZedGraphicExtension.UpdateGraph(this.zgcGraph);
      }
    }
  }
}
