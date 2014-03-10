using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using RCPA.Proteomics.Summary;

namespace RCPA.Proteomics.Classification
{
  public class IdentifiedPeptideTagClassification : AbstractDiscreteClassification<IIdentifiedPeptide, string>
  {
    string principle;

    public IdentifiedPeptideTagClassification(string principle)
    {
      this.principle = principle;
    }

    protected override string DoGetClassification(IIdentifiedPeptide obj)
    {
      return obj.Spectrum.Tag;
    }

    public override string GetPrinciple()
    {
      return this.principle;
    }
  }
}
