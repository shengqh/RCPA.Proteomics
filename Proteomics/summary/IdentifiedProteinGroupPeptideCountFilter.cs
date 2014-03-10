using System;
using System.Collections.Generic;
using System.Text;

namespace RCPA.Proteomics.Summary
{
  public class IdentifiedProteinGroupPeptideCountFilter : IIdentifiedProteinGroupFilter
  {
    private int peptideCount;

    public IdentifiedProteinGroupPeptideCountFilter(int peptideCount)
    {
      this.peptideCount = peptideCount;
    }

    #region IFilter<IIdentifiedProteinGroup> Members

    public bool Accept(IIdentifiedProteinGroup t)
    {
      return t[0].Peptides.Count >= peptideCount;
    }

    #endregion

    #region IIdentifiedProteinGroupFilter Members

    public string FilterCondition
    {
      get { return "PeptideCount >= " + peptideCount; }
    }

    #endregion
  }
}
