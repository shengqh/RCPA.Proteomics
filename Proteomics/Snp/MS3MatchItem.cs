namespace RCPA.Proteomics.Snp
{
  public class MS3MatchItem
  {
    public string Category { get; set; }
    public string Sequence1 { get; set; }
    public int Scan1 { get; set; }
    public string Sequence2 { get; set; }
    public int Scan2 { get; set; }
    public string MinMz { get; set; }
    public int PrecursorMatched { get; set; }
    public int MS3Matched { get; set; }
  }
}
