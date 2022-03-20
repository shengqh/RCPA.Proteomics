using RCPA.Proteomics.Summary;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using ZedGraph;

namespace RCPA.Proteomics.Quantification.ITraq
{
  public class ZedGraphITraqProtein
  {
    private ZedGraphControl zgcGraph;
    private Graphics g;
    private string title;
    private PaneLayout pl;

    public ZedGraphITraqProtein(ZedGraphControl zgcGraph, Graphics g, string title, PaneLayout pl = PaneLayout.SquareRowPreferred)
    {
      this.zgcGraph = zgcGraph;
      this.g = g;
      this.title = title;
      this.pl = pl;
    }

    private List<string> xlabels = new List<string>();

    public void Update(object sender, UpdateQuantificationItemEventArgs e)
    {
      ITraqProteinStatisticOption option = e.Option as ITraqProteinStatisticOption;

      IIdentifiedProteinGroup protein = null;
      if (e.Item is IEnumerable<IIdentifiedSpectrum>)
      {
        var spectra = e.Item as IEnumerable<IIdentifiedSpectrum>;
        protein = new IdentifiedProteinGroup();
        protein.Add(new IdentifiedProtein());
        protein[0].Peptides.AddRange(from s in spectra select s.Peptide);
      }
      else if (e.Item is IIdentifiedProteinGroup)
      {
        protein = e.Item as IIdentifiedProteinGroup;
      }

      if (protein == null)
      {
        throw new ArgumentException("e.Item should be IIdentifiedProteinGroup or IEnumerable<IIdentifiedSpectrum>");
      }

      var validItem = protein[0].Peptides.FirstOrDefault(m =>
      {
        var item = m.Spectrum.FindIsobaricItem();
        return null != item && item.Valid;
      });

      if (null == validItem)
      {
        zgcGraph.ClearData(true);
        return;
      }

      var masterPane = zgcGraph.InitMasterPanel(g, 1, title, this.pl);

      var panel = masterPane[0];

      var samples = option.GetSamples(validItem.Spectrum.FindIsobaricItem().PlexType);

      var dsNames = option.DatasetMap.Keys.OrderBy(m => m).ToList();

      var ratioCalc = option.GetRatioCalculator();

      xlabels.Clear();

      double index = 0.0;
      string outlierStr = "Outlier";
      string proteinStr = "Protein Ratio";
      foreach (var dsName in dsNames)
      {
        var expNames = new HashSet<string>(option.DatasetMap[dsName]);
        foreach (var sample in samples)
        {
          index += 1.0;

          ratioCalc.GetSample = sample.GetValue;
          ratioCalc.DatasetName = dsName;
          ratioCalc.ChannelName = sample.ChannelRatioName;
          ratioCalc.Filter = m => expNames.Contains(m.Query.FileScan.Experimental);
          var ratios = ratioCalc.Calculate(protein);

          xlabels.Add(dsName + ":" + sample.Name);

          if (ratios.Count > 0)
          {
            var ratio = protein[0].FindITraqChannelItem(dsName, sample.ChannelRatioName).Ratio;

            PointPairList pplNormal = new PointPairList();
            PointPairList pplOutlier = new PointPairList();
            PointPairList pplProteinRatio = new PointPairList();
            foreach (var r in ratios)
            {
              if (r.IsOutlier)
              {
                pplOutlier.Add(new PointPair(index, Math.Log(r.Ratio)));
              }
              else
              {
                pplNormal.Add(new PointPair(index, Math.Log(r.Ratio)));
              }
            }
            pplProteinRatio.Add(new PointPair(index, Math.Log(ratio)));

            panel.AddPoints(pplProteinRatio, Color.Red, proteinStr);
            if (pplOutlier.Count > 0)
            {
              panel.AddPoints(pplOutlier, Color.Green, outlierStr);
              outlierStr = string.Empty;
            }

            panel.AddPoints(pplNormal, Color.Black);
            proteinStr = string.Empty;
          }
        }
      }

      panel.XAxis.ScaleFormatEvent += new Axis.ScaleFormatHandler(XAxis_ScaleFormatEvent);
      panel.XAxis.Scale.Min = 0.0;
      panel.XAxis.Scale.Max = index + 1.0;
      panel.XAxis.Scale.FontSpec.Angle = 90;
      panel.YAxis.Title.Text = "log(Ratio)";

      ZedGraphicExtension.UpdateGraph(zgcGraph);
    }

    string XAxis_ScaleFormatEvent(GraphPane pane, Axis axis, double val, int index)
    {
      var ind = val - 1.0;
      if (ind - (int)ind > 0)
      {
        return string.Empty;
      }

      if (ind < 0 || ind >= xlabels.Count)
      {
        return string.Empty;
      }

      return xlabels[(int)ind];
    }
  }
}
