using RCPA.Proteomics.Summary;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using ZedGraph;

namespace RCPA.Proteomics.Quantification.ITraq
{
  public class ZedGraphITraqUniquePeptide
  {
    private ZedGraphControl zgcGraph;
    private Graphics g;
    private string title;
    private PaneLayout pl;

    public ZedGraphITraqUniquePeptide(ZedGraphControl zgcGraph, Graphics g, string title, PaneLayout pl = PaneLayout.SquareRowPreferred)
    {
      this.zgcGraph = zgcGraph;
      this.g = g;
      this.title = title;
      this.pl = pl;
    }

    public void Update(object sender, UpdateQuantificationItemEventArgs e)
    {
      var spectra = e.Item as IEnumerable<IIdentifiedSpectrum>;
      if (spectra == null)
      {
        throw new ArgumentException("e.Item should be IEnumerable<IIdentifiedSpectrum>");
      }

      var validItems = (from spectrum in spectra
                        let item = spectrum.FindIsobaricItem()
                        where null != item
                        select item).ToList();

      zgcGraph.InitMasterPanel(g, validItems.Count, title, this.pl);
      for (int i = 0; i < validItems.Count; i++)
      {
        new ZedGraphScanITraq(zgcGraph, zgcGraph.MasterPane[i], false).Update(sender, new UpdateQuantificationItemEventArgs(null, validItems[i]));
      }

      ZedGraphicExtension.UpdateGraph(zgcGraph);
    }
  }
}
