using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using ZedGraph;

namespace RCPA.Proteomics.Quantification.Labelfree
{
  public class SimplePeakChroPngWriter : IFileWriter<SimplePeakChro>
  {
    ZedGraphControl zgc = new ZedGraphControl();

    public SimplePeakChroPngWriter()
    {
      zgc.Width = 800;
      zgc.Height = 600;
      zgc.GraphPane.Border.IsVisible = false;
    }

    #region IFileWriter<SimplePeakChro> Members

    public void WriteToFile(string fileName, SimplePeakChro t)
    {
      zgc.GraphPane.CurveList.Clear();

      PointPairList ppl = new PointPairList();
      PointPairList identified = new PointPairList();
      t.Peaks.ForEach(m =>
      {
        ppl.Add(m.RetentionTime, m.Intensity / m.IonInjectionTime);
        if (m.Identified)
          identified.Add(m.RetentionTime, m.Intensity / m.IonInjectionTime);
      });

      zgc.GraphPane.AddCurve("", ppl, Color.Blue, SymbolType.None);
      zgc.GraphPane.AddIndividualLine("", identified, Color.Red);

      zgc.GraphPane.XAxis.Title.Text = "Retention Time (min)";
      zgc.GraphPane.YAxis.Title.Text = "Abundance";
      zgc.GraphPane.Title.Text = string.Format("{0}\n{1} : m/z={2:0.0000}, charge={3}, scan={4}", Path.GetFileName(fileName), t.Sequence, t.Mz, t.Charge, t.Peaks.Count);
      zgc.AxisChange();

      zgc.GetImage().Save(fileName, ImageFormat.Png);
    }

    #endregion
  }
}
