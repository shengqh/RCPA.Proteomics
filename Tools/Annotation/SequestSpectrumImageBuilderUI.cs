using RCPA.Gui;
using RCPA.Gui.FileArgument;
using RCPA.Proteomics.Image;
using RCPA.Proteomics.IO;
using RCPA.Proteomics.Spectrum;
using System.Drawing;
using System.IO;
using System.Linq;
using ZedGraph;

namespace RCPA.Tools.Annotation
{
  public partial class SequestSpectrumImageBuilderUI : AbstractUI
  {
    RcpaFileField dtaFile;
    public SequestSpectrumImageBuilderUI()
    {
      InitializeComponent();

      dtaFile = new RcpaFileField(btnDtaFile, txtDtaFile, "DtaFile", new OpenFileArgument("sequest dta", "dta"), true);
      AddComponent(dtaFile);
    }

    protected override void DoRealGo()
    {
      PeakList<MatchedPeak> pkl = GetAnnotatedPeakList();

      var points = new PointPairList();
      pkl.ForEach(p =>
      {
        if (p.PeakType == IonType.UNKNOWN)
        {
          points.Add(new PointPair(p.Mz, p.Intensity));
        }
        else
        {
          points.Add(new PointPair(p.Mz, p.Intensity, p.PeakType.ToString()));
        }
      });

      zgcPeaks.InitGraph(new FileInfo(dtaFile.FullName).Name, "MZ", "Intensity", false, 1.0);
      zgcPeaks.AddIndividualLine("", points, Color.Black, true);
    }

    private PeakList<MatchedPeak> GetAnnotatedPeakList()
    {
      PeakList<MatchedPeak> pkl = new DtaFormat<MatchedPeak>().ReadFromFile(dtaFile.FullName);

      var b =
        (from p in pkl
         orderby p.Intensity descending
         select p).ToList().Take(10);

      foreach (var p in b)
      {
        p.PeakType = IonType.B;
      }

      return pkl;
    }
  }
}
