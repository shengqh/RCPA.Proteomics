using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RCPA.Proteomics.MzIdent
{
  public class MzIdentPeptideItem
  {
    public string Id { get; set; }
    public string PureSequence { get; set; }
    public MzIdentModificationItem[] Modifications { get; set; }
    public string Sequence { get; set; }
    public int NumMissCleavage { get; set; }
  }
}
