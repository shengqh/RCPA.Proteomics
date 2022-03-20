using RCPA.Proteomics.Summary;
using System;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using ZedGraph;

namespace RCPA.Proteomics.Quantification.O18
{
  public class ZedGraphO18ProteinScan : AbstractZedGraph
  {
    public Color RegressionLineColor { get; set; }

    public string SummaryFilename { get; set; }

    public string DefaultDetailDirectory { get; set; }

    public ZedGraphO18ProteinScan(ZedGraphControl zgcGraph, GraphPane panel, string title)
      : base(zgcGraph, panel, title)
    {
      this.RegressionLineColor = Color.Red;
    }

    public ZedGraphO18ProteinScan(ZedGraphControl zgcGraph)
      : base(zgcGraph, "Protein Scan Regression")
    {
      this.RegressionLineColor = Color.Red;
    }

    private bool IsRelativeDir(string ratioFile)
    {
      return ratioFile.Contains("\\") || ratioFile.Contains("/");
    }

    protected string GetRatioFile(IQuantificationSummaryOption option, IIdentifiedSpectrum mph)
    {
      var o18option = option as O18QuantificationSummaryViewerOptions;
      string ratioFile = (string)mph.Annotations[o18option.RatioFileKey];
      if (ratioFile.Equals("-"))
      {
        return null;
      }

      FileInfo fi = new FileInfo(SummaryFilename);
      string result;
      if (IsRelativeDir(ratioFile))
      {
        result = fi.DirectoryName + "/" + ratioFile;
      }
      else
      {
        result = fi.DirectoryName + "/" + DefaultDetailDirectory + "/" + ratioFile;
      }

      result = new FileInfo(result).FullName;

      if (!File.Exists(result))
      {
        return null;
      }

      return result;
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

        var spectra = group.GetPeptides();

        var format = new O18QuantificationSummaryItemXmlFormat();

        foreach (var pep in spectra)
        {
          if (pep.IsEnabled(true) && option.IsPeptideRatioValid(pep))
          {
            string ratioFile = GetRatioFile(option, pep);
            if (ratioFile == null)
            {
              continue;
            }

            O18QuantificationSummaryItem item = new O18QuantificationSummaryItemXmlFormat().ReadFromFile(ratioFile);
            item.CalculateIndividualAbundance();

            PointPairList ppl;
            if (pep.Selected)
            {
              ppl = pplSelected;
            }
            else
            {
              ppl = pplNormal;
            }

            foreach (var envelope in item.ObservedEnvelopes)
            {
              if (!envelope.Enabled)
              {
                continue;
              }

              double refIntensity = envelope.SampleAbundance.O16;
              double sampleIntensity = envelope.SampleAbundance.O18;

              if (refIntensity == 0.0 || sampleIntensity == 0.0)
              {
                continue;
              }

              ppl.Add(refIntensity, sampleIntensity);
              ppl[ppl.Count - 1].Tag = pep;

              Debug.Assert(ppl[ppl.Count - 1].Tag == pep);
            }
          }
        }
        this.panel.ClearData();

        this.panel.AddPoints(pplSelected, SelectedColor);

        this.panel.AddPoints(pplNormal, NormalColor);

        var pplTotal = new PointPairList();
        pplTotal.AddRange(pplSelected);
        pplTotal.AddRange(pplNormal);

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
