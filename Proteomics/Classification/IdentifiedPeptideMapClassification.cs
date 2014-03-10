using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using RCPA.Proteomics.Summary;

namespace RCPA.Proteomics.Classification
{
  public class IdentifiedPeptideMapClassification : IdentifiedPeptideExperimentClassification
  {
    private Dictionary<string, string> identityMap;

    public IdentifiedPeptideMapClassification(string principle, Dictionary<string, string> identityMap)
      : base(principle)
    {
      if (identityMap == null)
      {
        throw new ArgumentNullException("identifiedMap");
      }

      if (identityMap.Count == 0)
      {
        throw new ArgumentException("Parameter identityMap cannot be empty.");
      }

      this.identityMap = identityMap;
    }

    protected override string DoGetClassification(IIdentifiedPeptide obj)
    {
      string experimentalName = base.DoGetClassification(obj);

      if (!identityMap.ContainsKey(experimentalName))
      {
        throw new ArgumentException(experimentalName + " does not match to any classified name defined in map, so classification failed!");
      }

      return identityMap[experimentalName];
    }
  }
}
