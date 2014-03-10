using System;
using System.Collections.Generic;
using System.Text;

namespace RCPA.Proteomics.Summary
{
  public class IdentifiedProteinGroupUniquePeptideCountFilter : IIdentifiedProteinGroupFilter
  {
    private int uniqueCount;

    public IdentifiedProteinGroupUniquePeptideCountFilter(int uniqueCount)
    {
      this.uniqueCount = uniqueCount;
    }

    #region IFilter<IIdentifiedProteinGroup> Members

    public bool Accept(IIdentifiedProteinGroup t)
    {
      return t[0].UniquePeptideCount >= uniqueCount;
    }

    #endregion

    #region IIdentifiedProteinGroupFilter Members

    public string FilterCondition
    {
      get { return "UniquePeptideCount >= " + uniqueCount; }
    }

    #endregion
  }
}
