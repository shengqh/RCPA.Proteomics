namespace RCPA.Proteomics
{
  public class MassCalculator
  {
    private readonly GetAtomMassFunction getMass;

    public MassCalculator(GetAtomMassFunction mf)
    {
      this.getMass = mf;
    }

    public double GetMass(AtomComposition ac)
    {
      double result = 0.0;
      foreach (Atom atom in ac.Keys)
      {
        result += this.getMass(atom) * ac[atom];
      }
      return result;
    }
  }
}