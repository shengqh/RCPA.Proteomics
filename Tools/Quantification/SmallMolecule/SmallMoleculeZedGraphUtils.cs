using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ZedGraph;
using RCPA.Proteomics.Quantification.SmallMolecule;
using System.Drawing;
using System.IO;

namespace RCPA.Tools.Quantification.SmallMolecule
{
  public static class SmallMoleculeZedGraphUtils
  {
    public static void LoadPeaks(ZedGraphControl zgcScan, List<ZedGraphSmallMoleculeFilePeak> zgcPeaks, List<FileData2> datas, Color color)
    {
      zgcScan.MasterPane.PaneList.Clear();
      zgcScan.MasterPane.Margin.All = 1;
      zgcScan.MasterPane.InnerPaneGap = 1;

      zgcPeaks.Clear();

      foreach (var data in datas)
      {
        GraphPane panel = new GraphPane();

        zgcScan.MasterPane.PaneList.Add(panel);

        zgcPeaks.Add(new ZedGraphSmallMoleculeFilePeak(zgcScan, panel, data.FileName, color) { Data = data });
      }
    }

  }
}
