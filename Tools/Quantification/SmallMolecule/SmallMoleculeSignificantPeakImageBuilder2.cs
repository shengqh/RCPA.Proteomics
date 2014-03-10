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
  public class SmallMoleculeSignificantPeakImageBuilder2 : AbstractThreadFileProcessor
  {
    private List<FileData2> sampleDatas;
    private List<FileData2> refDatas;

    private string peakName;

    private Color sampleColor;
    private Color refColor;

    public SmallMoleculeSignificantPeakImageBuilder2(string peakName, List<FileData2> sampleDatas, List<FileData2> refDatas, Color sampleColor, Color refColor)
    {
      this.peakName = peakName;
      this.sampleDatas = sampleDatas;
      this.refDatas = refDatas;
      this.sampleColor = sampleColor;
      this.refColor = refColor;
    }

    public override IEnumerable<string> Process(string saveFileName)
    {
      using (ZedGraphControl zgcScan = new ZedGraphControl())
      {
        zgcScan.MasterPane.PaneList.Clear();
        zgcScan.MasterPane.Margin.All = 1;
        zgcScan.MasterPane.InnerPaneGap = 1;
        zgcScan.MasterPane.Title.Text = "Red - Sample; Blue - Reference";
        zgcScan.MasterPane.Title.IsVisible = true;

        var count = (int)Math.Sqrt(sampleDatas.Count + refDatas.Count) + 1;
        zgcScan.Width = 160 * count;
        zgcScan.Height = 120 * count;

        foreach (var data in sampleDatas)
        {
          AddGraphPane(zgcScan, data, sampleColor);
        }

        foreach (var data in refDatas)
        {
          AddGraphPane(zgcScan, data, refColor);
        }

        var maxIntensity = (from d in sampleDatas.Union(refDatas)
                            from p in d[peakName]
                            select p.Intensity).Max();

        zgcScan.MasterPane.PaneList.ForEach(m => m.YAxis.Scale.Max = maxIntensity);

        using (Bitmap bm = new Bitmap(1, 1))
        {
          using (Graphics g = Graphics.FromImage(bm))
          {
            zgcScan.MasterPane.SetLayout(g, PaneLayout.SquareRowPreferred);

            zgcScan.AxisChange();
          }

          using (var bmp = zgcScan.GetImage())
          {
            bmp.Save(saveFileName, ImageFormat.Png);
          }
        }
      }

      GC.Collect();
      GC.WaitForFullGCComplete();

      return new string[] { saveFileName };
    }

    private void AddGraphPane(ZedGraphControl zgcScan, FileData2 data, Color color)
    {
      var items = data[peakName];

      GraphPane myPane = new GraphPane();

      var points = new PointPairList();

      items.ConvertAll(m => new PointPair(m.Scan, m.Intensity)).ForEach(m => points.Add(m));

      myPane.AddCurve(data.FileName, points, color, SymbolType.None);

      zgcScan.MasterPane.PaneList.Add(myPane);
    }
  }
}
