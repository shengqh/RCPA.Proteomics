namespace RCPA.Proteomics
{
  public interface IPeptideInfo
  {
    string Sequence { get; }
    double MassH { get; }
    int Charge { get; }
  }

  public class IdentifiedPeptideInfo : IPeptideInfo
  {
    private readonly int charge;
    private readonly double massH;
    private readonly string sequence;

    public IdentifiedPeptideInfo(string sequence, double massH, int charge)
    {
      this.sequence = sequence;
      this.massH = massH;
      this.charge = charge;
    }

    #region IPeptideInfo Members

    public string Sequence
    {
      get { return this.sequence; }
    }

    public double MassH
    {
      get { return this.massH; }
    }

    public int Charge
    {
      get { return this.charge; }
    }

    #endregion
  }
}