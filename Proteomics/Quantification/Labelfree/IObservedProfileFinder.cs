using RCPA.Proteomics.Spectrum;

namespace RCPA.Proteomics.Quantification.Labelfree
{
  public interface IObservedProfileFinder
  {
    bool Find(PeakList<Peak> ms1, ChromatographProfile chro, double mzTolerancePPM, int minimumProfileLength, ref ChromatographProfileScan envelope);
  }
}
