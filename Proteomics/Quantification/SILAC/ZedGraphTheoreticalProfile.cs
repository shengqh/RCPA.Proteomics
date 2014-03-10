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
using RCPA.Proteomics.Isotopic;

namespace RCPA.Proteomics.Quantification.SILAC
{
  public class ZedGraphTheoreticalProfiles
  {
    private ZedGraphProfiles zgcProfiles;

    public ZedGraphTheoreticalProfiles(ZedGraphControl zgcGraph)
    {
      zgcProfiles = new ZedGraphProfiles(zgcGraph);
    }

    public void Update(object sender, UpdateQuantificationItemEventArgs e)
    {

      zgcProfiles.Update(e.Item as SilacQuantificationSummaryItem);
    }
  }
}
