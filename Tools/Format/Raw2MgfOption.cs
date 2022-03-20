using RCPA.Proteomics;
using RCPA.Proteomics.IO;
using RCPA.Proteomics.Processor;
using RCPA.Proteomics.Spectrum;
using System.IO;

namespace RCPA.Tools.Format
{
  public class Raw2MgfOption
  {
    public double MinimumMass { get; set; }

    public double MaximumMass { get; set; }

    public double MinimumIonIntensity { get; set; }

    public int MinIonCount { get; set; }

    public double MinimumTotalIonCount { get; set; }

    public bool MergeScans { get; set; }

    public bool GroupByScanMode { get; set; }

    public double RetentionTimeTolerance { get; set; }

    public double PrecursorTolerance { get; set; }

    public double PeakTolerance { get; set; }

    public ChargeClass DefaultCharges { get; set; }

    public string TargetDirectory { get; set; }

    public string Converter { get; set; }

    public IPeakListWriter<Peak> Writer { get; set; }

    public Raw2MgfProcessor GetRaw2MgfProcessor()
    {
      PeakListMassRangeProcessor<Peak> p1 = new PeakListMassRangeProcessor<Peak>(MinimumMass, MaximumMass, new int[] { 2, 3 });
      PeakListMinIonIntensityProcessor<Peak> p2 = new PeakListMinIonIntensityProcessor<Peak>(MinimumIonIntensity);
      PeakListMinIonCountProcessor<Peak> p3 = new PeakListMinIonCountProcessor<Peak>(MinIonCount);
      PeakListMinTotalIonIntensityProcessor<Peak> p4 = new PeakListMinTotalIonIntensityProcessor<Peak>(MinimumTotalIonCount);

      CompositeProcessor<PeakList<Peak>> p = new CompositeProcessor<PeakList<Peak>>();
      p.Add(p1);
      p.Add(p2);
      p.Add(p3);
      p.Add(p4);

      return new Raw2MgfProcessor(null, GetMascotGenericFormatWriter(), GetRetentionTime(), PrecursorTolerance, PeakTolerance, p, new DirectoryInfo(TargetDirectory), GroupByScanMode);
    }

    private double GetRetentionTime()
    {
      if (MergeScans)
      {
        return RetentionTimeTolerance;
      }
      else
      {
        return 0.0;
      }
    }

    private IPeakListWriter<Peak> GetMascotGenericFormatWriter()
    {
      return Writer.Clone() as IPeakListWriter<Peak>;
    }

  }
}
