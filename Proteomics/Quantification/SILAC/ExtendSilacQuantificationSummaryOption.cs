using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using RCPA.Proteomics.Summary;
using System.IO;

namespace RCPA.Proteomics.Quantification.SILAC
{
  public class ExtendSilacQuantificationSummaryOption : SilacQuantificationSummaryOption, IExtendQuantificationSummaryOption
  {
    public ExtendSilacQuantificationSummaryOption()
    { }

    #region IExtendQuantificationSummaryOption Members

    public string GetPeptideClassification(IIdentifiedSpectrum ann)
    {
      throw new NotImplementedException();
    }

    public double GetProteinRatio(IIdentifiedProtein ann, string key)
    {
      throw new NotImplementedException();
    }

    public string GetProteinRatioDescription(IIdentifiedProtein ann, string key)
    {
      throw new NotImplementedException();
    }

    #endregion
  }
}
