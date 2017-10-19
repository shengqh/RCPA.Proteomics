using RCPA.Proteomics.Raw;

namespace RCPA.Proteomics.Quantification.SILAC
{
  public class SilacQuantificationOption
  {
    public IRawFormat RawFormat { get; set; }
    public string RawDir { get; set; }
    public string SilacParamFile { get; set; }
    public double PPMTolerance { get; set; }
    public string IgnoreModifications { get; set; }
    public int ProfileLength { get; set; }
    public bool KeepPeptideWithMostScan { get; set; }
  }
}
