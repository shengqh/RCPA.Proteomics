using RCPA.Proteomics.Spectrum;

namespace RCPA.Proteomics.Quantification
{
  public class QuanEnvelope : PeakList<Peak>
  {
    public bool Enabled { get; set; }

    public bool IsSelected { get; set; }

    public int Scan
    {
      get
      {
        if (ScanTimes.Count > 0)
        {
          return ScanTimes[0].Scan;
        }
        return 0;
      }
    }

    public QuanEnvelope()
    {
      this.Enabled = true;
      this.IsSelected = false;
    }

    public QuanEnvelope(PeakList<Peak> source)
    {
      this.Enabled = true;
      this.IsSelected = false;
      this.AddRange(source);
      this.ScanTimes.AddRange(source.ScanTimes);
    }
  }
}
