using RCPA.Proteomics.Summary;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using ZedGraph;

namespace RCPA.Proteomics.Quantification.ITraq
{
  public class ZedGraphITraqProtein2
  {
    private ZedGraphControl zgcGraph;
    private Graphics g;
    private string title;
    private PaneLayout pl;

    public ZedGraphITraqProtein2(ZedGraphControl zgcGraph, Graphics g, string title, PaneLayout pl = PaneLayout.SquareRowPreferred)
    {
      this.zgcGraph = zgcGraph;
      this.g = g;
      this.title = title;
      this.pl = pl;
    }

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

      List<string> xlabels = new List<string>();

      List<PointPairList> outliers = new List<PointPairList>();
      List<PointPairList> normals = new List<PointPairList>();
      PointPairList proteins = new PointPairList();

      //按照数据集循环
      foreach (var dsName in dsNames)
      {
        var expNames = new HashSet<string>(option.DatasetMap[dsName]);
        //按照样品循环
        foreach (var sample in samples)
        {
          ratioCalc.GetSample = sample.GetValue;
          ratioCalc.DatasetName = dsName;
          ratioCalc.ChannelName = sample.ChannelRatioName;
          ratioCalc.Filter = m => expNames.Contains(m.Query.FileScan.Experimental);
          var ratios = ratioCalc.Calculate(protein);

          //添加相应的分类名
          xlabels.Add(dsName + ":" + sample.Name);

          //每个分类有三种数据：outlier,normal和proteinratio
          var outlier = new PointPairList();
          outliers.Add(outlier);
          var normal = new PointPairList();
          normals.Add(normal);

          if (ratios.Count > 0)
          {
            var ratio = protein[0].FindITraqChannelItem(dsName, sample.ChannelRatioName).Ratio;
            proteins.Add(new PointPair() { Y = Math.Log(ratio) });

            ratios.ForEach(m =>
            {
              if (m.IsOutlier)
              {
                outlier.Add(new PointPair() { Y = Math.Log(m.Ratio) });
              }
              else
              {
                normal.Add(new PointPair() { Y = Math.Log(m.Ratio) });
              }
            });
          }
          else
          {
            //缺失值用missing表示。
            proteins.Add(new PointPair() { Y = PointPair.Missing });
          }
        }
      }
      panel.AddPoints(proteins, Color.Red, "Ratio");
      AddOrdinalPoints(outliers, panel, Color.Green, "Outlier");
      AddOrdinalPoints(normals, panel, Color.Black, "");

      panel.XAxis.Type = AxisType.Text;
      panel.XAxis.Scale.FontSpec.Angle = 90;
      panel.XAxis.Scale.TextLabels = xlabels.ToArray();
      panel.YAxis.Title.Text = "log(Ratio)";

      ZedGraphicExtension.UpdateGraph(zgcGraph);
    }

    private void AddOrdinalPoints(List<PointPairList> pplList, GraphPane panel, Color color, string pplName)
    {
      //去除所有类中最大数据数。
      var maxCount = pplList.Max(m => m.Count);

      //将其他类用missing补满。
      pplList.ForEach(m =>
      {
        while (m.Count < maxCount)
        {
          m.Add(new PointPair() { Y = PointPair.Missing });
        };
      });

      //分批添加数据。
      for (int i = 0; i < maxCount; i++)
      {
        PointPairList ppl = new PointPairList();
        pplList.ForEach(m => ppl.Add(m[i]));
        if (i == 0)
        {
          panel.AddPoints(ppl, color, pplName);
        }
        else
        {
          panel.AddPoints(ppl, color);
        }
      }
    }
  }
}
