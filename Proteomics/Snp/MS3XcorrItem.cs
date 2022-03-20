namespace RCPA.Proteomics.Snp
{
  public class MS3XcorrItem
  {
    public string Category { get; set; }
    public string Sequence1 { get; set; }
    public int MS2Scan1 { get; set; }
    public double MS2Precursor1 { get; set; }
    public int MS3Scan1 { get; set; }
    public double MS3Precursor1 { get; set; }
    public string Sequence2 { get; set; }
    public int MS2Scan2 { get; set; }
    public double MS2Precursor2 { get; set; }
    public int MS3Scan2 { get; set; }
    public double MS3Precursor2 { get; set; }
    public double Xcorr { get; set; }
  }
}
