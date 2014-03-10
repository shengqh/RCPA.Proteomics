using System;
using System.Collections.Generic;
using System.Text;

namespace RCPA.Proteomics.Summary
{
  public interface IIdentifiedProteinGroupFilter : IFilter<IIdentifiedProteinGroup>
  {
    string FilterCondition { get; }
  }

  public class AndIdentifiedProteinGroupFilter : IIdentifiedProteinGroupFilter
  {
    private List<IIdentifiedProteinGroupFilter> filters = new List<IIdentifiedProteinGroupFilter>();

    public AndIdentifiedProteinGroupFilter(IEnumerable<IIdentifiedProteinGroupFilter> filters)
    {
      this.filters.AddRange(filters);
    }

    #region IIdentifiedProteinGroupFilter Members

    public bool Accept(IIdentifiedProteinGroup t)
    {
      foreach (IIdentifiedProteinGroupFilter filter in filters)
      {
        if (!filter.Accept(t))
        {
          return false;
        }
      }

      return true;
    }

    public string FilterCondition
    {
      get
      {
        StringBuilder sb = new StringBuilder();

        foreach (IIdentifiedProteinGroupFilter filter in filters)
        {
          if (sb.Length == 0)
          {
            sb.Append(filter.FilterCondition);
          }
          else
          {
            sb.Append(" ; ").Append(filter.FilterCondition);
          }
        }

        return sb.ToString();
      }
    }

    #endregion
  }
}
