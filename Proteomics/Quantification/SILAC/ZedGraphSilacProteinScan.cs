using RCPA.Proteomics.Summary;
using System;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using ZedGraph;

namespace RCPA.Proteomics.Quantification.SILAC
{
  public class ZedGraphSilacProteinScan : AbstractZedGraph
  {
    public Color RegressionLineColor { get; set; }

    public string SummaryFilename { get; set; }

    public string DefaultDetailDirectory { get; set; }

    public ZedGraphSilacProteinScan(ZedGraphControl zgcGraph, GraphPane panel, string title)
      : base(zgcGraph, panel, title)
    {
      this.RegressionLineColor = Color.Red;
    }

    public ZedGraphSilacProteinScan(ZedGraphControl zgcGraph)
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
      string ratioFile = mph.GetQuantificationItem().Filename;
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
      panel.InitGraphPane(this.title, option.Func.ReferenceKey, option.Func.SampleKey, true, 0.0);

      IIdentifiedProteinGroup group = e.Item as IIdentifiedProteinGroup;
      try
      {
        var pplNormal = new PointPairList();
        var pplSelected = new PointPairList();

        var spectra = group.GetPeptides();

        var format = new SilacQuantificationSummaryItemXmlFormat();

        foreach (var pep in spectra)
        {
          if (option.IsPeptideRatioValid(pep))
          {
            string ratioFile = GetRatioFile(option, pep);
            if (ratioFile == null)
            {
              continue;
            }

            var item = format.ReadFromFile(ratioFile);

            Func<SilacPeakListPair, double> getSamIntensity;
            Func<SilacPeakListPair, double> getRefIntensity;

            if (item.SampleIsLight)
            {
              getSamIntensity = m => m.LightIntensity;
              getRefIntensity = m => m.HeavyIntensity;
            }
            else
            {
              getSamIntensity = m => m.HeavyIntensity;
              getRefIntensity = m => m.LightIntensity;
            }

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

              double refIntensity = getRefIntensity(envelope);
              double sampleIntensity = getSamIntensity(envelope);

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
          var lr = pplTotal.GetRegression();
          var lr_text = MyConvert.Format("Ratio={0:0.00}, Correl={1:0.00}, FValue={2:0.00}, FProb={3:E4}", lr.Ratio, lr.RSquare, lr.TValue, lr.PValue);
          PointPairList line = pplTotal.GetRegressionLine();

          var lineItem = this.panel.AddCurve(lr_text, line, RegressionLineColor, SymbolType.None);
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
