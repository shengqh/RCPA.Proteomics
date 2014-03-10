using System;
using System.Collections.Generic;
using System.Text;

namespace RCPA.Proteomics.Summary
{
  public interface ISpectrumQuery
  {
    SequestFilename FileScan { get; set; }

    double ObservedMz { get; set; }

    int Charge { get; set; }

    int QueryId { get; set; }

    int MatchCount { get; set; }

    string Title { get; set; }

    double RetentionTime { get; set; }

    double RetentionLength { get; set; }
  }
}
