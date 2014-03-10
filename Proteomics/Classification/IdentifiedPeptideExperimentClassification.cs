using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using RCPA.Proteomics.Summary;

namespace RCPA.Proteomics.Classification
{
  public class IdentifiedPeptideExperimentClassification : AbstractDiscreteClassification<IIdentifiedPeptide, string>
  {
    string principle;

    public IdentifiedPeptideExperimentClassification(string principle)
    {
      this.principle = principle;
    }

    protected override string DoGetClassification(IIdentifiedPeptide obj)
    {
      return obj.Spectrum.Query.FileScan.Experimental;
    }

    public override string GetPrinciple()
    {
      return principle;
    }
  }
}
