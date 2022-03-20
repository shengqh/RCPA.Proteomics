namespace RCPA.Proteomics.MaxQuant
{
  public class MaxQuant2MascotPeptideProcessorOption
  {
    public string SiteFile { get; set; }
    public double MinProbability { get; set; }
    public double MinDeltaScore { get; set; }
    public string MSMSFile { get; set; }
    public bool IsSILAC { get; set; }
    public string SILACAminoacids { get; set; }
  }
}
