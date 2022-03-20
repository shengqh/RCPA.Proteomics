using RCPA.Proteomics.Summary;

namespace RCPA.Proteomics.Quantification
{
  public interface IScanWriter
  {
    IIdentifiedSpectrumWriter ScanWriter { get; set; }
  }
}
