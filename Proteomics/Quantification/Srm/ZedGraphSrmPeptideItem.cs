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
  public class ZedGraphSrmPeptideItem
  {
    private ZedGraphControl zgcGraph;
    private GraphPane panel;

    public ZedGraphSrmPeptideItem(ZedGraphControl zgcGraph, GraphPane panel)
    {
      this.zgcGraph = zgcGraph;
      this.panel = panel;

      panel.InitGraphPane(null, "Retention Time", "Ion Count", true, 0.0);

      ZedGraphicExtension.UpdateGraph(zgcGraph);
    }

    public ZedGraphSrmPeptideItem(ZedGraphControl zgcGraph)
      : this(zgcGraph, zgcGraph.GraphPane)
    { }

    public void Update(object sender, UpdateMRMPairedPeptideItemEventArgs e)
    {
      panel.ClearData();

      var peptide = e.Item;
      if (peptide == null || peptide.ProductIonPairs.Count == 0)
      {
        return;
      }

      var option = e.ViewOption;

      panel.Tag = peptide;

      panel.ClearData();
      try
      {
        foreach (var summary in peptide.ProductIonPairs)
        {
          if (summary.Light != null && summary.Heavy != null && !option.ViewGreenLine)
          {
            AddAbsoluteCurve(summary.Light, "Light", Color.Blue, DashStyle.Solid);
            AddAbsoluteCurve(summary.Heavy, "Heavy", Color.Red, DashStyle.Dot);
          }
        }

        this.panel.Title.IsVisible = false;
        this.panel.YAxis.Scale.Min = 0;

        if (option.ViewType == DisplayType.PerfectSize || option.ViewType == DisplayType.FullHeight)
        {
          var range = (from summary in peptide.ProductIonPairs
                       from scan in summary.Light.Intensities
                       where scan.Enabled
                       orderby scan.RetentionTime
                       select scan.RetentionTime).ToList();

          if (range.Count > 0)
          {
            var minRt = range.First() - 1;
            var maxRt = range.Last() + 1;

            panel.XAxis.Scale.Min = minRt;
            panel.XAxis.Scale.Max = maxRt;

            if (option.ViewType == DisplayType.PerfectSize)
            {
              var maxIntensity = Math.Max((from summary in peptide.ProductIonPairs
                                           from scan in summary.Light.Intensities
                                           where scan.Enabled
                                           select scan.Intensity).Max(),
                                           (from summary in peptide.ProductIonPairs
                                            from scan in summary.Heavy.Intensities
                                            where scan.Enabled
                                            select scan.Intensity).Max());
              panel.YAxis.Scale.Max = maxIntensity * 1.2;
            }
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
