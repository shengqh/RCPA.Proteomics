using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using RCPA;
using RCPA.Utils;

namespace RCPA.Proteomics.Image
{
  public class CombinedNeutralLossType : INeutralLossType
  {
    private List<INeutralLossType> combinedTypes;

    private double mass;

    private string name;

    private int count;

    public CombinedNeutralLossType(IEnumerable<INeutralLossType> combinedTypes)
    {
      this.combinedTypes = combinedTypes.ToList();

      Initialize();
    }

    public CombinedNeutralLossType(INeutralLossType aType)
      : this(new[] { aType })
    { }

    public bool InsertNeutralLossType(INeutralLossType aType)
    {
      if (!aType.CanMultipleLoss)
      {
        foreach (INeutralLossType oldType in combinedTypes)
        {
          if (oldType.Equals(aType))
          {
            return false;
          }
        }
      }

      combinedTypes.Insert(0, aType);

      Initialize();

      return true;
    }

    private void Initialize()
    {
      mass = (from nl in combinedTypes
              select nl.Mass).Sum();

      name = StringUtils.Merge((from nl in combinedTypes
                                select nl.Name), "-");

      count = (from nl in combinedTypes
               select nl.Count).Sum();
    }

    public int Count { get { return count; } }

    public bool CanMultipleLoss { get { return false; } }

    public double Mass { get { return mass; } }

    public string Name { get { return name; } }

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

      CombinedNeutralLossType other = (CombinedNeutralLossType)obj;

      return mass == other.mass && name == other.name;
    }
  }
}
