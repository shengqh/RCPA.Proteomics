using System;

namespace RCPA.Proteomics.Image
{
  public class NeutralLossType : INeutralLossType
  {
    public NeutralLossType(double mass, String name, bool allowMultipleLoss)
    {
      this.mass = mass;
      this.name = name;
      this.allowMultipleLoss = allowMultipleLoss;
    }

    private double mass;

    private string name;

    private bool allowMultipleLoss;

    public double Mass { get { return mass; } }

    public string Name { get { return name; } }

    public bool CanMultipleLoss { get { return allowMultipleLoss; } }

    public int Count { get { return 1; } }

    public override int GetHashCode()
    {
      return this.GetHashCodeFromFields(mass, name);
    }

    public override bool Equals(object obj)
    {
      if (this == obj)
        return true;

      if (obj == null)
        return false;

      if (this.GetType() != obj.GetType())
        return false;

      NeutralLossType other = (NeutralLossType)obj;

      return mass == other.mass && name == other.name;
    }
  }
}
