using RCPA.Proteomics.Raw;
using RCPA.Proteomics.Spectrum;
using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.IO;
using System.Linq;
using ZedGraph;

namespace RCPA.Proteomics.Quantification.O18
{
  public class ZedGraphO18IndividualScan : AbstractZedGraph
  {
    private IRawFile rawFile;

    public ZedGraphO18IndividualScan(ZedGraphControl zgcGraph)
      : base(zgcGraph, "Experimental Envelope")
    { }

    public ZedGraphO18IndividualScan(ZedGraphControl zgcGraph, GraphPane panel)
      : base(zgcGraph, panel, "Experimental Envelope")
    { }

    public override void Update(object sender, UpdateQuantificationItemEventArgs e)
    {
      var summary = e.Item as O18QuantificationSummaryItem;
      if (summary == null)
      {
        throw new ArgumentNullException("UpdateQuantificationItemEventArgs.Item cannot be null");
      }

      panel.InitGraphPane(this.title, "m/z", "Intensity", true, 0.0);

      ZedGraphicExtension.ClearData(zgcGraph, false);
      try
      {
        var envelope = summary.ObservedEnvelopes.Find(m => m.IsSelected);
        if (envelope == null)
        {
          return;
        }

        panel.InitGraphPane(this.title + ", Scan=" + envelope.Scan.ToString(), "m/z", "Intensity", true, 0.0);

        double minMz = envelope[0].Mz - 1.0;
        double maxMz = envelope[envelope.Count - 1].Mz + 1.0;

        zgcGraph.GraphPane.XAxis.Scale.Min = minMz;
        zgcGraph.GraphPane.XAxis.Scale.Max = maxMz;

        var ppl = new PointPairList();
        envelope.ForEach(p => { if (p.Intensity > 0) { ppl.Add(p.Mz, p.Intensity); } });
        panel.AddIndividualLine("Isotopic Envelope", ppl, Color.Red);
        panel.AddTextUp(ppl, 5, (m => m.X.ToString("0.0000")));

        var halfMax = (from env in envelope
                       select env.Intensity).Max() / 2;
        ppl.Clear();
        envelope.ForEach(p => { if (p.Intensity == 0) { ppl.Add(p.Mz, halfMax); } });
        panel.AddIndividualLine("", ppl, defaultColor: Color.Red, dStyle: DashStyle.Dash);
        panel.AddTextUp(ppl, 5, (m => m.X.ToString("0.0000")));

        if (File.Exists(summary.RawFilename))
        {
          if (rawFile == null)
          {
            rawFile = RawFileFactory.GetRawFileReader(summary.RawFilename);
          }
          else if (!rawFile.FileName.Equals(summary.RawFilename))
          {
            rawFile.Close();
            rawFile = RawFileFactory.GetRawFileReader(summary.RawFilename);
          }

          PeakList<Peak> pkl = rawFile.GetPeakList(envelope.ScanTimes[0].Scan);
          var pplRaw = new PointPairList();
          for (int i = 0; i < pkl.Count; i++)
          {
            if (pkl[i].Mz < minMz)
            {
              continue;
            }
            else if (pkl[i].Mz > maxMz)
            {
              break;
            }
            pplRaw.Add(pkl[i].Mz, -pkl[i].Intensity);
          }

          panel.AddIndividualLine("PeakList From RawFile", pplRaw, Color.Blue);
          panel.AddTextDown(pplRaw, 5, (m => m.X.ToString("0.0000")));

          /*
          foreach (var p in ppl)
          {
            if (p.Y == 0)
            {
              double ppm = double.MaxValue;
              double ppmAbs = Math.Abs(ppm);
              foreach (var pp in pplRaw)
              {
                double ppmCur = PrecursorUtils.mz2ppm(pp.X, pp.X - p.X);
                double ppmCurAbs = Math.Abs(ppmCur);
                if (Math.Abs(ppmCur) < ppmAbs)
                {
                  ppm = ppmCur;
                  ppmAbs = ppmCurAbs;
                }

                if (pp.X > p.X)
                {
                  break;
                }
              }

              p.Z = ppm;
            }
          }
           */
        }

      }
      finally
      {
        ZedGraphicExtension.UpdateGraph(zgcGraph);
      }
    }
  }
}
