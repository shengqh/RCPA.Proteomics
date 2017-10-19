using RCPA.Proteomics.Mascot;
using RCPA.Proteomics.Modification;
using RCPA.Proteomics.Summary;
using RCPA.Seq;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

namespace RCPA.Proteomics.MaxQuant
{
  public class MaxQuantSiteProteinK8R10Builder : AbstractMaxQuantSiteProteinBuilder
  {
    public MaxQuantSiteProteinK8R10Builder(MaxQuantSiteProteinBuilderOption option) : base(option)
    { }

    protected override void InitializeAminoacids(out Aminoacids samAminoacids, out Aminoacids refAminoacids)
    {
      samAminoacids = new Aminoacids();
      refAminoacids = new Aminoacids();
      refAminoacids['R'].CompositionStr = "Cx6H12ONx4";
      refAminoacids['K'].CompositionStr = "Cx6H12ONx2";
    }
  }
}
