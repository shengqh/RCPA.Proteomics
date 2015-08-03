using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RCPA.Proteomics.MzIdent
{
  public class MzIdentPeptideEvidenceItem
  {
    public string Id { get; set; }
    public string PeptideRef { get; set; }
    public string DbRef { get; set; }
    public string Pre { get; set; }
    public string Post { get; set; }
  }
}
