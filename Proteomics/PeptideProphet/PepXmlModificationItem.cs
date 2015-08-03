using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RCPA.Proteomics.PeptideProphet
{
  public class PepXmlModificationItem
  {
    public bool IsAminoacid { get; set; }
    public bool IsProteinTerminal { get; set; }
    public bool IsTerminalN { get; set; }

    public string Aminoacid { get; set; }
    public double Mass { get; set; }
    public double MassDiff { get; set; }
    public bool IsVariable { get; set; }
    public string Symbol { get; set; }

    public override string ToString()
    {
      if (IsAminoacid)
      {
        return MyConvert.Format("{0}({1:0.000000})", Aminoacid, MassDiff);
      }

      var ptype = IsProteinTerminal ? "Protein " : "Peptide ";
      var ttype = IsTerminalN ? "N-term" : "C-term";

      return MyConvert.Format("{0}{1}({2:0.000000})", ptype, ttype, MassDiff);
    }
  }
}
