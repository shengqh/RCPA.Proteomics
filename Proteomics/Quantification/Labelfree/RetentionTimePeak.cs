using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using RCPA.Proteomics.Spectrum;
using RCPA.Proteomics.Summary;

namespace RCPA.Proteomics.Quantification.Labelfree
{
  public class RetentionTimePeak
  {
    public RetentionTimePeak()
    { }

    public double Mz { get; set; }

    public int Charge { get; set; }

    public double Intensity { get; set; }

    public double RententionTime { get; set; }

    public Envelope PeakEnvelope { get; set; }

    public string Sequence { get; set; }

    public List<double> Profile { get; set; }

    public double MzTolerance { get; set; }

    public List<PeakList<Peak>> Chromotographs { get; set; }

    public void Initialize()
    {
      PeakEnvelope = new Envelope(Mz, Charge, 4);
      Chromotographs = new List<PeakList<Peak>>();
    }

    private void TrimPeaksFromTop()
    {
      while (Chromotographs.Count > 0)
      {
        if (Chromotographs[0][0].Intensity == 0)
        {
          Chromotographs.RemoveAt(0);
          continue;
        }

        int count = 1;
        for (int i = 1; i < Chromotographs.Count; i++)
        {
          if (Chromotographs[i][0].Intensity == 0)
          {
            break;
          }
          count++;
          if (count > 3)
          {
            break;
          }
        }

        if (count == Chromotographs.Count || count > 3)
        {
          return;
        }

        Chromotographs.RemoveAt(0);
      }
    }

    public void TrimPeaks()
    {
      TrimPeaksFromTop();

      Chromotographs.Reverse();

      TrimPeaksFromTop();

      Chromotographs.Reverse();
    }
  }
}
