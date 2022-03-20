using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using ZedGraph;

namespace RCPA.Proteomics.Quantification.Srm
{
  public class ZedGraphMRMPeptideItemRetentionTimes
  {
    private ZedGraphControl zgcGraph;

    public ZedGraphMRMPeptideItemRetentionTimes(ZedGraphControl zgcGraph)
    {
      this.zgcGraph = zgcGraph;
    }

    public void Update(object sender, UpdateMRMPairedPeptideItemEventArgs e)
    {
      var summary = e.Item;
      if (summary == null)
      {
        throw new ArgumentNullException("UpdateMRMPairedPeptideItemEventArgs.Item cannot be null");
      }

      var myMaster = zgcGraph.GraphPane;
      myMaster.Title.Text = "Retention time ranges";
      myMaster.XAxis.Title.Text = "Files";
      myMaster.YAxis.Title.Text = "Retention time";
      myMaster.CurveList.Clear();
      try
      {
        var splRange = new PointPairList();
        var splEnabled = new PointPairList();
        var splAll = new StockPointList();
        List<string> filenames = new List<string>();
        int index = 0;

        List<double> median = new List<double>();
        for (int i = 0; i < summary.ProductIonPairs.Count; i++)
        {
          if (summary.ProductIonPairs[i] == null)
          {
            continue;
          }

          index++;
          filenames.Add(summary.ProductIonPairs[i].FileName);

          var ints = summary.ProductIonPairs[i].Light.Intensities;
          var low = ints.First().RetentionTime;
          var high = ints.Last().RetentionTime;

          var firstEnabled = ints.Find(m => m.Enabled);
          var open = firstEnabled == null ? (low + high) / 2 : firstEnabled.RetentionTime;
          var lastEnabled = ints.FindLast(m => m.Enabled);
          var close = lastEnabled == null ? (low + high) / 2 : lastEnabled.RetentionTime;

          median.Add((open + close) / 2);

          splRange.Add(new PointPair(index, low, high));
          splEnabled.Add(new PointPair(index, open, close));
          splAll.Add(index, high, low, open, close, 1000);
        }
        myMaster.XAxis.Type = AxisType.Text;
        myMaster.XAxis.Scale.TextLabels = filenames.ToArray();

        var item = myMaster.AddJapaneseCandleStick("", splAll);
        item.Stick.RisingFill = new Fill(Color.Red);
        item.Stick.FallingFill = new Fill(Color.Blue);

        zgcGraph.AxisChange();
      }
      finally
      {
        ZedGraphicExtension.UpdateGraph(this.zgcGraph);
      }
    }
  }
}
