using RCPA.Proteomics.Summary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RCPA.Proteomics.Format
{
  public class MultipleMgf2Ms2ProcessorOptions
  {
    public MultipleMgf2Ms2ProcessorOptions()
    {
      InputFiles = new List<string>();
      Parser = new DefaultTitleParser();
      TargetDir = string.Empty;
      ThreadCount = 1;
    }

    public IList<string> InputFiles { get; set; }

    public ITitleParser Parser { get; set; }

    public string TargetDir { get; set; }

    public string DefaultCharge { get; set; }

    public int ThreadCount { get; set; }
  }
}
