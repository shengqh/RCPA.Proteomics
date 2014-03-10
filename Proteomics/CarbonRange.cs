namespace RCPA.Proteomics
{
  public class CarbonRange
  {
    public CarbonRange(int min, int max)
    {
      Min = min;
      Max = max;
    }

    public int Min { get; set; }

    public int Max { get; set; }
  }

  public interface ICarbonRangePredictor
  {
    CarbonRange GetCarbonRange(double precursorMass);
  }

  public class ProteinCarbonRangePredictor : ICarbonRangePredictor
  {
    private readonly Aminoacid maxCarbonPercentAminoacid;
    private readonly Aminoacid minCarbonPercentAminoacid;

    public ProteinCarbonRangePredictor()
    {
      var aas = new Aminoacids();
      this.maxCarbonPercentAminoacid = aas.GetMaxCarbonPercentAminoacid();
      this.minCarbonPercentAminoacid = aas.GetMinCarbonPercentAminoacid();
    }

    #region ICarbonRangePredictor Members

    public CarbonRange GetCarbonRange(double precursorMass)
    {
      var minCarbon =
        (int) (precursorMass/this.minCarbonPercentAminoacid.MonoMass*this.minCarbonPercentAminoacid.Composition[Atom.C]);
      var maxCarbon =
        (int) (precursorMass/this.maxCarbonPercentAminoacid.MonoMass*this.maxCarbonPercentAminoacid.Composition[Atom.C]);
      return new CarbonRange(minCarbon, maxCarbon);
    }

    #endregion
  }

  public class FixCarbonRangePredictor : ICarbonRangePredictor
  {
    private readonly int max;
    private readonly int min;

    public FixCarbonRangePredictor(int min, int max)
    {
      this.min = min;
      this.max = max;
    }

    #region ICarbonRangePredictor Members

    public CarbonRange GetCarbonRange(double precursorMass)
    {
      return new CarbonRange(this.min, this.max);
    }

    #endregion
  }
}