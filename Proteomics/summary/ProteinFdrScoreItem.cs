namespace RCPA.Proteomics.Summary
{
  public class ProteinFdrScoreItem
  {
    public ProteinFdrScoreItem(double score, int acceptedPeptideCount)
    {
      Score = score;
      PeptideCount = acceptedPeptideCount;
    }

    public double Score { get; set; }

    public int PeptideCount { get; set; }
  }
}
