using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using RCPA.Proteomics.Quantification.SmallMolecule;
using ZedGraph;
using System.Drawing;
using System.Drawing.Imaging;

namespace RCPA.Tools.Quantification.SmallMolecule
{
  /// <summary>
  /// 根据给定的Peak，画出该Peak在各个不同文件中的Intensity/RetentionTime图。
  /// </summary>
  public class SmallMoleculeSignificantPeakImageBuilder : AbstractThreadFileProcessor
  {
    private List<FileData2> datas;

    private string peakName;

    private Color color;

    private double maxIntensity;

    public SmallMoleculeSignificantPeakImageBuilder(List<FileData2> datas, string peakName, Color color, double maxIntensity)
    {
      this.datas = datas;
      this.peakName = peakName;
      this.color = color;
      this.maxIntensity = maxIntensity;
    }

    public override IEnumerable<string> Process(string saveFileName)
    {
      ZedGraphControl zgcScan = new ZedGraphControl();
      zgcScan.MasterPane.PaneList.Clear();
      zgcScan.MasterPane.Margin.All = 1;
      zgcScan.MasterPane.InnerPaneGap = 1;

      var count = (int)Math.Sqrt(datas.Count) + 1;
      zgcScan.Width = 300 * count;
      zgcScan.Height = 200 * count;

      foreach (var data in datas)
      {
        GraphPane myPane = new GraphPane();

        var items = data[peakName];
        var points = new PointPairList();
        items.ConvertAll(m => new PointPair(m.Scan, m.Intensity)).ForEach(m => points.Add(m));

        myPane.AddCurve(data.FileName, points, color, SymbolType.None);

        zgcScan.MasterPane.PaneList.Add(myPane);
      }

      zgcScan.MasterPane.PaneList.ForEach(m => m.YAxis.Scale.Max = maxIntensity);

      Bitmap bm = new Bitmap(1, 1);
      using (Graphics g = Graphics.FromImage(bm))
      {
        zgcScan.MasterPane.SetLayout(g, PaneLayout.SquareRowPreferred);

        zgcScan.AxisChange();
      }

      zgcScan.GetImage().Save(saveFileName, ImageFormat.Png);

      return new string[] { saveFileName };
    }
  }
}
