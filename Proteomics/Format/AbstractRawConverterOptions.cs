using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RCPA.Proteomics.Format
{
  public abstract class AbstractRawConverterOptions
  {
    public AbstractRawConverterOptions()
    {
      this.ExtractRawMS3 = false;
    }

    public string TargetDirectory { get; set; }

    public bool ExtractRawMS3 { get; set; }

    public bool GroupByMode { get; set; }

    public bool GroupByMsLevel { get; set; }

    public abstract string Extension { get; }
  }
}
