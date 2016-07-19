using RCPA.Proteomics.Spectrum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RCPA.Proteomics.Quantification.Labelfree
{
  public interface IObservedProfileFinder
  {
    bool Find(PeakList<Peak> ms1, ChromatographProfile chro, double mzTolerancePPM, int minimumProfileLength, ref ChromatographProfileScan envelope);
  }
}
