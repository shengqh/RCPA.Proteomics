using System.Collections.Generic;
using System.Linq;

namespace RCPA.Proteomics.Summary.Uniform
{
  public interface IResolveSearchEngineConflictType
  {
    string Name { get; }

    IConflictProcessor GetProcessor();
  }

  public static class ResolveSearchEngineConflictTypeFactory
  {
    private class ResolveSearchEngineConflictDiscardAll : IResolveSearchEngineConflictType
    {
      public string Name { get { return "Discard all"; } }

      public IConflictProcessor GetProcessor()
      {
        return new ConflictDiscardAllProcessor();
      }

      public override string ToString()
      {
        return Name;
      }
    }

    private class ResolveSearchEngineConflictQValue : IResolveSearchEngineConflictType
    {
      public string Name { get { return "Lowest Qvalue"; } }

      public IConflictProcessor GetProcessor()
      {
        return new ConflictQvalueProcessor();
      }

      public override string ToString()
      {
        return Name;
      }
    }

    private class ResolveSearchEngineConflictPreferEngines : IResolveSearchEngineConflictType
    {
      private SearchEngineType[] perferEngines;
      public ResolveSearchEngineConflictPreferEngines(SearchEngineType[] perferEngines)
      {
        this.perferEngines = perferEngines.ToArray();
      }

      public string Name { get { return "Perfer Engines"; } }

      public IConflictProcessor GetProcessor()
      {
        return new ConflictPreferEngineProcessor(this.perferEngines);
      }

      public override string ToString()
      {
        return perferEngines[0].ToString();
      }
    }

    private static IResolveSearchEngineConflictType[] conflictTypes;

    public static readonly IResolveSearchEngineConflictType DiscardAll = new ResolveSearchEngineConflictDiscardAll();

    public static readonly IResolveSearchEngineConflictType QValue = new ResolveSearchEngineConflictQValue();

    public static IResolveSearchEngineConflictType[] GetTypes()
    {
      if (conflictTypes == null)
      {
        List<IResolveSearchEngineConflictType> result = new List<IResolveSearchEngineConflictType>();

        result.Add(DiscardAll);
        result.Add(QValue);

        //var types = EnumUtils.EnumToArray<SearchEngineType>();
        //foreach (var seType in types)
        //{
        //  result.Add(new ResolveSearchEngineConflictPreferEngines(new SearchEngineType[] { seType }));
        //}

        conflictTypes = result.ToArray();
      }
      return conflictTypes;
    }

    public static void Add(IResolveSearchEngineConflictType aType)
    {
      var types = GetTypes().ToList();
      types.Add(aType);
      conflictTypes = types.ToArray();
    }

    public static IResolveSearchEngineConflictType Find(string name)
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
