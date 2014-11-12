using RCPA.Proteomics.Summary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RCPA.Proteomics.Percolator
{
  public class MultiplePercolatorPeptideDistillerOptions
  {
    public MultiplePercolatorPeptideDistillerOptions()
    {
      PercolatorOutputFiles = new List<string>();
      ThreadCount = 1;
    }

    /// <summary>
    /// The percolator output xml file, the corresponding input xml file should with same prefix as output xml file
    /// XXX.output.xml
    /// XXX.input.xml
    /// Should be at same folder
    /// </summary>
    public IList<string> PercolatorOutputFiles { get; set; }

    public int ThreadCount { get; set; }
  }
}
