using System.Collections.Generic;
using System.Linq;

namespace RCPA.Proteomics.Summary.Uniform
{
  public interface ITargetDecoyConflictType
  {
    string Name { get; }

    IFilter<IIdentifiedSpectrum> GetSpectrumFilter(string decoyPattern);

    IIdentifiedProteinGroupFilter GetGroupFilter(string decoyPattern);
  }

  public static class ResolveTargetDecoyConflictTypeFactory
  {
    /// <summary>
    /// 任何一个蛋白质名匹配上decoy pattern都算是decoy
    /// </summary>
    private class ConflictAsDecoy : ITargetDecoyConflictType
    {
      public string Name { get { return "As decoy"; } }

      public IFilter<IIdentifiedSpectrum> GetSpectrumFilter(string decoyPattern)
      {
        return new IdentifiedSpectrumProteinNameRegexFilter(decoyPattern, false);
      }

      public IIdentifiedProteinGroupFilter GetGroupFilter(string decoyPattern)
      {
        return new IdentifiedProteinGroupNameRegexFilter(decoyPattern, false);
      }

      public override string ToString()
      {
        return Name;
      }
    }

    /// <summary>
    /// 必须所有蛋白质名都匹配上decoy pattern才算是decoy
    /// </summary>
    private class ConflictAsTarget : ITargetDecoyConflictType
    {
      public string Name { get { return "As target"; } }

      public IFilter<IIdentifiedSpectrum> GetSpectrumFilter(string decoyPattern)
      {
        return new IdentifiedSpectrumProteinNameRegexFilter(decoyPattern, true);
      }

      public IIdentifiedProteinGroupFilter GetGroupFilter(string decoyPattern)
      {
        return new IdentifiedProteinGroupNameRegexFilter(decoyPattern, true);
      }

      public override string ToString()
      {
        return Name;
      }
    }

    private static ITargetDecoyConflictType[] conflictTypes;

    public static readonly ITargetDecoyConflictType Decoy = new ConflictAsDecoy();

    public static readonly ITargetDecoyConflictType Target = new ConflictAsTarget();

    public static ITargetDecoyConflictType[] GetTypes()
    {
      if (conflictTypes == null)
      {
        List<ITargetDecoyConflictType> result = new List<ITargetDecoyConflictType>();

        result.Add(Decoy);
        result.Add(Target);

        //var types = EnumUtils.EnumToArray<SearchEngineType>();
        //foreach (var seType in types)
        //{
        //  result.Add(new ResolveSearchEngineConflictPreferEngines(new SearchEngineType[] { seType }));
        //}

        conflictTypes = result.ToArray();
      }
      return conflictTypes;
    }

    public static void Add(ITargetDecoyConflictType aType)
    {
      var types = GetTypes().ToList();
      types.Add(aType);
      conflictTypes = types.ToArray();
    }

    public static ITargetDecoyConflictType Find(string name)
    {
      var types = GetTypes();
      foreach (var t in types)
      {
        if (t.Name == name)
        {
          return t;
        }
      }

      return types[0];
    }
  }
}
