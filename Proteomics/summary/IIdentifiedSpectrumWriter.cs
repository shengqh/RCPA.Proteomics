using System.IO;

namespace RCPA.Proteomics.Summary
{
  public interface IIdentifiedSpectrumWriter
  {
    string Header { get; }

    void WriteToStream(StreamWriter sw, IIdentifiedSpectrum peptide);
  }
}
