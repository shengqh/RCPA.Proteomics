using RCPA.Proteomics.Summary;
using RCPA.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using ZedGraph;

namespace RCPA.Proteomics.Quantification
{
  public class ZedGraphPeptidesLSPAD : AbstractZedGraph
  {
    public ZedGraphPeptidesLSPAD(ZedGraphControl zgcGraph, GraphPane panel, string title)
      : base(zgcGraph, panel, title)
    { }

    public ZedGraphPeptidesLSPAD(ZedGraphControl zgcGraph)
      : base(zgcGraph, "All Peptide Ratios")
    { }

    public override void Update(object sender, UpdateQuantificationItemEventArgs e)
    {
      IQuantificationSummaryOption option = e.Option as IQuantificationSummaryOption;
      IIdentifiedResult mr = e.Item as IIdentifiedResult;

      string xTitle = MyConvert.Format("(Log({0}) + Log({1})) / 2", option.Func.ReferenceKey, option.Func.SampleKey);
      string yTitle = MyConvert.Format("Log(Ratio)");
      panel.InitGraphPane(title, xTitle, yTitle, true, 0.0);

      try
      {
        var pplNormal = new PointPairList();
        var pplSelected = new PointPairList();
        var pplOutlier = new PointPairList();
        var pplGroup = new PointPairList();

        var groups = from g in mr
                     where g[0].IsEnabled(true) && option.IsProteinRatioValid(g[0])
                     select g;

        HashSet<IIdentifiedSpectrum> spectra = new HashSet<IIdentifiedSpectrum>();

        double maxX = 0.0;
        PointPairList ppl;
        foreach (var mpg in groups)
        {
          var peptides = from p in mpg.GetPeptides()
                         where p.IsEnabled(true) && option.IsPeptideRatioValid(p)
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

            double refIntensity = Math.Log(option.Func.GetReferenceIntensity(pep));
            double sampleIntensity = Math.Log(option.Func.GetSampleIntensity(pep));

            double A = (refIntensity + sampleIntensity) / 2;
            double ratio = Math.Log(option.GetPeptideRatio(pep));
            ppl.Add(A, ratio);
            ppl[ppl.Count - 1].Tag = new Pair<IIdentifiedProteinGroup, IIdentifiedSpectrum>(mpg, pep);

            maxX = Math.Max(A, maxX);
          }
        }

        this.panel.ClearData();

        var ratios = (from pep in spectra
                      let ratio = Math.Log(option.GetPeptideRatio(pep))
                      orderby ratio
                      select ratio).ToList();

        this.panel.DrawProbabilityRange(maxX, ratios);

        this.panel.AddPoints(pplSelected, SelectedColor, "Current Peptide");
        this.panel.AddPoints(pplGroup, GroupColor, "Current Protein");
        this.panel.AddPoints(pplOutlier, OutlierColor, "Outlier");
        this.panel.AddPoints(pplNormal, NormalColor, "Other");
      }
      finally
      {
        ZedGraphicExtension.UpdateGraph(this.zgcGraph);
      }
    }
  }
}
