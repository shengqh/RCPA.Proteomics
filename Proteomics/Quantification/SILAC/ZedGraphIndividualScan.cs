using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ZedGraph;
using RCPA.Gui.Image;
using System.Drawing;
using System.IO;
using RCPA.Proteomics.Raw;
using RCPA.Proteomics.Spectrum;

namespace RCPA.Proteomics.Quantification.SILAC
{
  public class ZedGraphIndividualScan
  {
    private ZedGraphControl zgcGraph;

    private IRawFile rawFile;

    public ZedGraphIndividualScan(ZedGraphControl zgcGraph)
    {
      this.zgcGraph = zgcGraph;

      ZedGraphicExtension.InitGraph(zgcGraph, "Experimental Envelope", "m/z", "Intensity", false, 0.05);
    }

    public void Update(object sender, UpdateQuantificationItemEventArgs e)
    {
      var summary = e.Item as SilacQuantificationSummaryItem;
      if (summary == null)
      {
        throw new ArgumentNullException("UpdateQuantificationItemEventArgs.Item cannot be null");
      }

      ZedGraphicExtension.ClearData(zgcGraph, false);
      try
      {
        var envelope = summary.ObservedEnvelopes.Find(m => m.IsSelected);
        if (envelope == null)
        {
          return;
        }

        double minMz = envelope.Light[0].Mz - 1.0;
        double maxMz = envelope.Heavy[envelope.Heavy.Count - 1].Mz + 1.0;

        zgcGraph.GraphPane.XAxis.Scale.Min = minMz;
        zgcGraph.GraphPane.XAxis.Scale.Max = maxMz;

        var pplLight = new PointPairList();
        envelope.Light.ForEach(p => pplLight.Add(p.Mz, p.Intensity));

        var pplHeavy = new PointPairList();
        envelope.Heavy.ForEach(p => pplHeavy.Add(p.Mz, p.Intensity));

        if (summary.SampleIsLight)
        {
          ZedGraphicExtension.AddIndividualLine(zgcGraph, "Sample", pplLight, SilacQuantificationConstants.SAMPLE_COLOR, false);
          ZedGraphicExtension.AddIndividualLine(zgcGraph, "Reference", pplHeavy, SilacQuantificationConstants.REFERENCE_COLOR, false);
        }
        else
        {
          ZedGraphicExtension.AddIndividualLine(zgcGraph, "Sample", pplHeavy, SilacQuantificationConstants.SAMPLE_COLOR, false);
          ZedGraphicExtension.AddIndividualLine(zgcGraph, "Reference", pplLight, SilacQuantificationConstants.REFERENCE_COLOR, false);
        }

        // Shift the text items up by 5 user scale units above the bars
        const float shift = 5;

        var ppl = new PointPairList();
        ppl.AddRange(pplLight);
        ppl.AddRange(pplHeavy);
        for (int i = 0; i < ppl.Count; i++)
        {
          // format the label string to have 1 decimal place
          string lab = ppl[i].X.ToString("0.00");
          // create the text item (assumes the x axis is ordinal or text)
          // for negative bars, the label appears just above the zero value
          var text = new TextObj(lab, ppl[i].X, ppl[i].Y + shift);
          // tell Zedgraph to use user scale units for locating the TextItem
          text.Location.CoordinateFrame = CoordType.AxisXYScale;
          // AlignH the left-center of the text to the specified point
          text.Location.AlignH = AlignH.Left;
          text.Location.AlignV = AlignV.Center;
          text.FontSpec.Border.IsVisible = false;
          text.FontSpec.Fill.IsVisible = false;
          // rotate the text 90 degrees
          text.FontSpec.Angle = 90;
          // add the TextItem to the list
          zgcGraph.GraphPane.GraphObjList.Add(text);
        }

        if (File.Exists(summary.RawFilename))
        {
          if (rawFile == null)
          {
            rawFile = new RawFileImpl(summary.RawFilename);
          }
          else if (!rawFile.FileName.Equals(summary.RawFilename))
          {
            rawFile.Open(summary.RawFilename);
          }

          PeakList<Peak> pkl = rawFile.GetPeakList(envelope.Light.ScanTimes[0].Scan);
          var pplRaw = new PointPairList();
          pplRaw.Add(minMz, 0.0);
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
          pplRaw.Add(maxMz, 0.0);

          ZedGraphicExtension.AddIndividualLine(zgcGraph, "PeakList From RawFile", pplRaw, SilacQuantificationConstants.IDENTIFIED_COLOR, true);
        }
      }
      finally
      {
        ZedGraphicExtension.UpdateGraph(zgcGraph);
      }
    }
  }
}
