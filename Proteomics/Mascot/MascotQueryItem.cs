namespace RCPA.Proteomics.Mascot
{
  public class MascotQueryItem
  {
    public int QueryId { get; set; }

    public double ExperimentalMass { get; set; }

    public double Intensity { get; set; }

    public double Observed { get; set; }

    public int Charge { get; set; }

    public int MatchCount { get; set; }

    public double HomologyScore { get; set; }
  }
}
