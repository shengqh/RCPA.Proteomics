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
  public class ZedGraphPeptidesLSPAD2 : AbstractZedGraph
  {
    public ZedGraphPeptidesLSPAD2(ZedGraphControl zgcGraph, GraphPane panel, string title)
      : base(zgcGraph, panel, title)
    { }

    public ZedGraphPeptidesLSPAD2(ZedGraphControl zgcGraph)
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
        HashSet<IIdentifiedSpectrum> spectra = new HashSet<IIdentifiedSpectrum>();

        var groups = from g in mr
                     where g[0].IsEnabled(true) && option.IsProteinRatioValid(g[0])
                     select g;

        foreach (var mpg in groups)
        {
          var peptides = from p in mpg.GetPeptides()
                         where p.IsEnabled(true) && option.IsPeptideRatioValid(p)
                         select p;

          spectra.UnionWith(peptides);
        }

        List<LSPADItem> items = new List<LSPADItem>();
        foreach (var pep in spectra)
        {
          double refIntensity = Math.Log(option.Func.GetReferenceIntensity(pep));
          double sampleIntensity = Math.Log(option.Func.GetSampleIntensity(pep));

          double A = (refIntensity + sampleIntensity) / 2;
          double ratio = Math.Log(option.GetPeptideRatio(pep));

          items.Add(new LSPADItem()
          {
            LogRatio = ratio,
            Intensity = A,
            Tag = pep
          });
        }

        LSPADItem.CalculatePValue(items);



        //this.panel.DrawProbabilityRange(maxX, ratios);

        //this.panel.AddPoints(pplSelected, SelectedColor, "Current Peptide");
        //this.panel.AddPoints(pplGroup, GroupColor, "Current Protein");
        //this.panel.AddPoints(pplOutlier, OutlierColor, "Outlier");
        //this.panel.AddPoints(pplNormal, NormalColor, "Other");
      }
      finally
      {
        ZedGraphicExtension.UpdateGraph(this.zgcGraph);
      }
    }
  }
}
