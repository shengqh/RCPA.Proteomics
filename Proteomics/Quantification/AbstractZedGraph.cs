using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ZedGraph;
using System.Drawing;
using MathNet.Numerics.Statistics;
using MathNet.Numerics.Distributions;
using RCPA.Utils;

namespace RCPA.Proteomics.Quantification
{
  public abstract class AbstractZedGraph : IQuantificationItemUpdate
  {
    protected ZedGraphControl zgcGraph;
    protected GraphPane panel;
    protected string title;

    public Color SelectedColor { get; set; }
    public Color OutlierColor { get; set; }
    public Color NormalColor { get; set; }
    public Color GroupColor { get; set; }

    public AbstractZedGraph(ZedGraphControl zgcGraph, GraphPane panel, string title)
    {
      this.zgcGraph = zgcGraph;
      this.panel = panel;
      this.title = title;
      this.NormalColor = Color.YellowGreen;
      this.SelectedColor = Color.Blue;
      this.OutlierColor = Color.SkyBlue;
      this.GroupColor = Color.Red;
    }

    public AbstractZedGraph(ZedGraphControl zgcGraph, string title)
      : this(zgcGraph, zgcGraph.GraphPane, title)
    { }

    public abstract void Update(object sender, UpdateQuantificationItemEventArgs e);
  }
}
