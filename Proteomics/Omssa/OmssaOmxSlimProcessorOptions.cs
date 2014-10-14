using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RCPA.Proteomics.Omssa
{
  public class OmssaOmxSlimProcessorOptions
  {
    public string[] SourceFiles { get; set; }

    public string TargetDirectory { get; set; }

    public bool Overwrite { get; set; }
  }
}
