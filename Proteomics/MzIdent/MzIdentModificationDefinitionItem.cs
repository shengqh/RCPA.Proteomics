using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RCPA.Proteomics.MzIdent
{
  public class MzIdentModificationDefinitionItem
  {
    public bool IsFixed { get; set; }
    public double MassDelta { get; set; }
    public string Residues { get; set; }
    public string CvRef { get; set; }
    public string Accession { get; set; }
    public string Name { get; set; }
    public string ModificationChar { get; set; }

    public override string ToString()
    {
      return Name;
    }
  }
}
