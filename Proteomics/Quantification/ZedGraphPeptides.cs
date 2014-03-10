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
using MathNet.Numerics.Distributions;
using MathNet.Numerics.Statistics;

namespace RCPA.Proteomics.Quantification
{
  public class ZedGraphPeptides : AbstractZedGraph
  {
    public ZedGraphPeptides(ZedGraphControl zgcGraph, GraphPane panel, string title)
      : base(zgcGraph, panel, title)
    { }

    public ZedGraphPeptides(ZedGraphControl zgcGraph)
      : base(zgcGraph, "All Peptide Ratios")
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
        var pplGroup = new PointPairList();

        var groups = from g in mr
                     where option.IsProteinRatioValid(g[0])
                     select g;

        HashSet<IIdentifiedSpectrum> spectra = new HashSet<IIdentifiedSpectrum>();

        double maxX = 0.0;
        PointPairList ppl;
        foreach (var mpg in groups)
        {
          var peptides = from p in mpg.GetPeptides()
                         where option.IsPeptideRatioValid(p)
                         select p;

          spectra.UnionWith(peptides);

          foreach (var pep in peptides)
          {
            if (pep.Selected)
            {
              ppl = pplSelected;
            }
            else if (mpg.Selected)
            {
              ppl = pplGroup;
            }
            else if (option.IsPeptideOutlier(pep))
            {
              ppl = pplOutlier;
            }
            else
            {
              ppl = pplNormal;
            }

            double refIntensity = option.Func.GetReferenceIntensity(pep);
            double samIntensity = option.Func.GetSampleIntensity(pep);

            ppl.Add(refIntensity, samIntensity);
            ppl[ppl.Count - 1].Tag = new Pair<IIdentifiedProteinGroup, IIdentifiedSpectrum>(mpg, pep);

            maxX = Math.Max(refIntensity, maxX);
          }
        }

        this.panel.ClearData();

        this.panel.AddPoints(pplSelected, SelectedColor, "Current Peptide");
        this.panel.AddPoints(pplGroup, GroupColor, "Current Protein");
        this.panel.AddPoints(pplOutlier, OutlierColor, "Outlier");
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

          var lineItem = this.panel.AddCurve(MyConvert.Format("Ratio={0:0.0000}, R2={1:0.0000}", lrrr.Ratio, lrrr.RSquare), line, Color.Red, SymbolType.None);
        }
      }
      finally
      {
        ZedGraphicExtension.UpdateGraph(this.zgcGraph);
      }
    }
  }
}
