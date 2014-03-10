using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RCPA.Proteomics.Raw
{
  public interface IRawFormat
  {
    string Description { get; }

    string GetRawFile(string rawDir, string experimental);

    string GetRawFile(string rawDir, string experimental, bool existOnly);

    IRawFile2 GetRawFile();
  }
}
