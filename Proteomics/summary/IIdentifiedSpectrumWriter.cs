using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace RCPA.Proteomics.Summary
{
  public interface IIdentifiedSpectrumWriter
  {
    string Header { get; }

    void WriteToStream(StreamWriter sw, IIdentifiedSpectrum peptide);
  }
}
