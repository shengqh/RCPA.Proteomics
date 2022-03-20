using System.Drawing;
using ZedGraph;

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
