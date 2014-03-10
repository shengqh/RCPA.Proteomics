using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ZedGraph;
using RCPA.Gui.Image;
using System.Drawing;
using RCPA.Proteomics.Quantification.SILAC;
using System.Drawing.Drawing2D;

namespace RCPA.Proteomics.Quantification.Srm
{
  public class ZedGraphMRMProductIonScans
  {
    private ZedGraphControl zgcGraph;
    private GraphPane panel;

    public ZedGraphMRMProductIonScans(ZedGraphControl zgcGraph, GraphPane panel)
    {
      this.zgcGraph = zgcGraph;
      this.panel = panel;

      panel.InitGraphPane(null, "Retention Time", "Ion Count", true, 0.0);

      ZedGraphicExtension.UpdateGraph(zgcGraph);
    }

    public void Update(object sender, UpdateMRMPairedProductIonEventArgs e)
    {
      panel.ClearData();

      var summary = e.Item;
      if (summary == null)
      {
        return;
      }

      var option = e.ViewOption;

      panel.Tag = summary;

      panel.ClearData();
      try
      {
        var enabledCount = summary.EnabledScanCount;
        double maxLightIntensity, maxHeavyIntensity;
        if (enabledCount == 0)
        {
          maxLightIntensity = summary.LightMaxIntensity;
          maxHeavyIntensity = summary.HeavyMaxIntensity;
        }
        else
        {
          maxLightIntensity = summary.LightMaxEnabledIntensity;
          maxHeavyIntensity = summary.HeavyMaxEnabledIntensity;
        }
        var maxIntensity = Math.Max(maxLightIntensity, maxHeavyIntensity);

        if (summary.IsCurrent && option.ViewCurrentHighlight)
        {
          panel.Chart.Fill = new Fill(Color.Cornsilk);
        }
        else
        {
          panel.Chart.Fill = new Fill(Color.Transparent);
        }

        if (summary.Light != null && summary.Heavy != null && !option.ViewGreenLine)
        {
          AddAbsoluteCurve(summary.Light, "Light", Color.Blue, DashStyle.Solid);
          AddAbsoluteCurve(summary.Heavy, "Heavy", Color.Red, DashStyle.Dot);
        }
        else
        {
          if (summary.Light != null)
          {
            AddAbsoluteCurve(summary.Light, "Light", Color.Blue, DashStyle.Solid);
          }

          if (summary.Heavy != null)
          {
            AddAbsoluteCurve(summary.Heavy, "Heavy", Color.Red, DashStyle.Solid);
          }

          if (option.ViewGreenLine)
          {
            if ((maxLightIntensity > maxHeavyIntensity) && (summary.Heavy != null))
            {
              AddAbsoluteCurve2(summary.Heavy, maxLightIntensity * 0.9 / maxHeavyIntensity, Color.Green);
            }
            else if (summary.Light != null)
            {
              AddAbsoluteCurve2(summary.Light, maxHeavyIntensity * 0.9 / maxLightIntensity, Color.Green);
            }
          }
        }

        var filename = string.IsNullOrEmpty(summary.FileName) ? "" : summary.FileName + "; ";
        if (summary.IsPaired)
        {
          this.panel.Title.Text = filename + summary.GetFormula() + "\n" + summary.GetSignalToNoise();
        }
        else
        {
          this.panel.Title.Text = filename + summary.GetSignalToNoise();
        }

        this.panel.Title.IsVisible = true;
        this.panel.YAxis.Scale.Min = 0;

        if (option.ViewType == DisplayType.PerfectSize || option.ViewType == DisplayType.FullHeight)
        {
          var range = (from scan in summary.Light.Intensities
                       where scan.Enabled
                       orderby scan.RetentionTime
                       select scan.RetentionTime).ToList();

          if (range.Count > 0)
          {
            var minRt = range.First() - 1;
            var maxRt = range.Last() + 1;

            panel.XAxis.Scale.Min = minRt;
            panel.XAxis.Scale.Max = maxRt;
          }

          if (option.ViewType == DisplayType.PerfectSize)
          {
            panel.YAxis.Scale.Max = maxIntensity * 1.2;
          }
        }
      }
      finally
      {
        ZedGraphicExtension.UpdateGraph(this.zgcGraph);
      }
    }

    private void AddCurve(SrmTransition trans, double maxIntensity, string title, Color color)
    {
      if (trans == null || trans.Intensities == null || trans.Intensities.Count == 0)
      {
        return;
      }

      var item = trans.Intensities;

      var ppl = new PointPairList();
      item.ForEach(m => ppl.Add(m.RetentionTime, m.Intensity * 100 / maxIntensity));
      panel.AddCurve(MyConvert.Format("{0}, {1:0.00}-{2:0.00}", title, item[0].PrecursorMz, item[0].ProductMz), ppl, color, SymbolType.None);

      var pplCurve = new PointPairList();
      bool bChecked = false;

      item.ForEach(envelope =>
      {
        if (envelope.Enabled != bChecked)
        {
          if (pplCurve.Count > 0)
          {
            if (bChecked)
            {
              panel.AddPoly("", pplCurve, color, new[] { SilacQuantificationConstants.PLOY_COLOR });
            }
          }
          pplCurve = new PointPairList();
          bChecked = envelope.Enabled;
        }

        pplCurve.Add(envelope.RetentionTime, envelope.Intensity * 100 / maxIntensity);
      });

      if (pplCurve.Count > 0)
      {
        if (bChecked)
        {
          panel.AddPoly("", pplCurve, color, new[] { SilacQuantificationConstants.PLOY_COLOR });
        }
      }
    }

    private void AddAbsoluteCurve(SrmTransition trans, string title, Color color, DashStyle dStyle)
    {
      if (trans == null || trans.Intensities == null || trans.Intensities.Count == 0)
      {
        return;
      }

      var item = trans.Intensities;

      var ppl = new PointPairList();
      item.ForEach(m => ppl.Add(m.RetentionTime, m.Intensity));
      var line = panel.AddCurve(MyConvert.Format("{0}, {1:0.00}-{2:0.00}", title, item[0].PrecursorMz, item[0].ProductMz), ppl, color, SymbolType.None);
      line.Line.Style = dStyle;

      var pplCurve = new PointPairList();
      bool bChecked = false;

      item.ForEach(envelope =>
      {
        if (envelope.Enabled != bChecked)
        {
          if (pplCurve.Count > 0)
          {
            if (bChecked)
            {
              panel.AddPoly("", pplCurve, color, new[] { SilacQuantificationConstants.PLOY_COLOR });
            }
          }
          pplCurve = new PointPairList();
          bChecked = envelope.Enabled;
        }

        pplCurve.Add(envelope.RetentionTime, envelope.Intensity);
      });

      if (pplCurve.Count > 0)
      {
        if (bChecked)
        {
          panel.AddPoly("", pplCurve, color, new[] { SilacQuantificationConstants.PLOY_COLOR });
        }
      }
    }

    private void AddAbsoluteCurve2(SrmTransition trans, double factor, Color color)
    {
      if (trans == null || trans.Intensities == null || trans.Intensities.Count == 0)
      {
        return;
      }

      var item = trans.Intensities;

      var ppl = new PointPairList();
      item.ForEach(m => ppl.Add(m.RetentionTime, m.Intensity * factor));
      var line = panel.AddCurve("", ppl, color, SymbolType.None);
      line.Line.Style = System.Drawing.Drawing2D.DashStyle.DashDot;
    }
  }
}
