using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RCPA.Proteomics.Summary.Uniform
{
  public interface ITitleDatasetOptions : IDatasetOptions
  {
    string TitleParserName { get; set; }

    ITitleParser GetTitleParser();
  }
}
