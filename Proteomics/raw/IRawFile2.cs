using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using RCPA.Proteomics.Spectrum;

namespace RCPA.Proteomics.Raw
{
  public interface IRawFile2 : IRawFile
  {
    IMasterScanFinder MasterScanFinder { get; set; }

    int GetMasterScan(int scan);

    PeakList<Peak> GetMasterScanPeakList(int childScan);

    PrecursorPeak GetPrecursorPeakWithMasterScan(int scan);
  }
}
