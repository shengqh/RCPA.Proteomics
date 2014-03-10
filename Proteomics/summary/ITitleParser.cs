using System;
using System.Collections.Generic;
using System.Text;

namespace RCPA.Proteomics.Summary
{
  public interface ITitleParser : IParser<string, SequestFilename>
  {
    string FormatName { get; }

    string Example { get; }

    bool IsMatch(string title);
  }
}
