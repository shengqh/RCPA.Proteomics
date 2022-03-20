﻿using System.IO;

namespace RCPA.Proteomics.Summary
{
  public interface IIdentifiedProteinGroupWriter
  {
    LineFormat<IIdentifiedProtein> ProteinFormat { get; set; }

    void WriteToStream(StreamWriter sw, IIdentifiedProteinGroup mpg);
  }
}
