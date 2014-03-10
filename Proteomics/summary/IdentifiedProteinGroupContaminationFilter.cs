using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace RCPA.Proteomics.Summary
{
  public class IdentifiedProteinGroupNameRegexFilter : IIdentifiedProteinGroupFilter
  {
    private Regex reg;

    private Func<IIdentifiedProteinGroup, bool> acceptFunc;

    public IdentifiedProteinGroupNameRegexFilter(string pattern, bool forAll)
    {
      this.reg = new Regex(pattern);
      if (forAll)
        acceptFunc = t => t.All(m => reg.Match(m.Name).Success);
      else
        acceptFunc = t => t.Any(m => reg.Match(m.Name).Success);
    }

    #region IIdentifiedProteinGroupFilter Members

    public bool Accept(IIdentifiedProteinGroup t)
    {
      return acceptFunc(t);
    }

    public string FilterCondition
    {
      get { return ToString(); }
    }

    #endregion

    public override string ToString()
    {
      return MyConvert.Format("Protein name filter {0}", reg);
    }
  }


  public class IdentifiedProteinGroupContaminationMapFilter : IIdentifiedProteinGroupFilter
  {
    private IStringParser<string> acParser;

    private HashSet<string> conMap;

    public IdentifiedProteinGroupContaminationMapFilter(IStringParser<string> acParser, HashSet<string> contaminationNameMap)
    {
      this.acParser = acParser;
      this.conMap = contaminationNameMap;
    }

    #region IIdentifiedProteinGroupFilter Members

    public bool Accept(IIdentifiedProteinGroup t)
    {
      foreach (var p in t)
      {
        var ac = acParser.GetValue(p.Name);
        if (conMap.Contains(ac))
        {
          return true;
        }
      }

      return false;
    }

    public string FilterCondition
    {
      get { return "Contamination Protein Filter"; }
    }

    #endregion
  }

  public class IdentifiedProteinGroupContaminationDescriptionFilter : IIdentifiedProteinGroupFilter
  {
    private Regex conRegex;

    public IdentifiedProteinGroupContaminationDescriptionFilter(string contaminationProteinDescriptionPattern)
    {
      this.conRegex = new Regex(contaminationProteinDescriptionPattern, RegexOptions.IgnoreCase);
    }

    #region IIdentifiedProteinGroupFilter Members

    public bool Accept(IIdentifiedProteinGroup t)
    {
      return t.Any(m => conRegex.Match(m.Reference).Success);
    }

    public string FilterCondition
    {
      get { return "Contamination Protein (" + conRegex.ToString() + ")"; }
    }

    #endregion
  }

  public class IdentifiedProteinGroupNotFilter : IIdentifiedProteinGroupFilter
  {
    private IIdentifiedProteinGroupFilter filter;

    public IdentifiedProteinGroupNotFilter(IIdentifiedProteinGroupFilter filter)
    {
      this.filter = filter;
    }

    #region IIdentifiedProteinGroupFilter Members

    public string FilterCondition
    {
      get { return "Not " + filter.FilterCondition; }
    }

    #endregion

    #region IFilter<IIdentifiedProteinGroup> Members

    public bool Accept(IIdentifiedProteinGroup t)
    {
      return !filter.Accept(t);
    }

    #endregion
  }


}
