using RCPA.Proteomics.Mascot;
using RCPA.Proteomics.Summary;
using System.IO;

namespace RCPA.Proteomics.Quantification
{
  public class QuantificationResultSingleFileFormat : AbstractIdentifiedResultSingleFileTextFormat, IScanWriter
  {

    public QuantificationResultSingleFileFormat()
    {
    }

    protected override IIdentifiedResult Allocate()
    {
      return new IdentifiedResult();
    }

    protected override string GetDefaultProteinHeader()
    {
      return MascotHeader.MASCOT_PROTEIN_HEADER;
    }

    protected override string GetDefaultPeptideHeader()
    {
      return MascotHeader.MASCOT_PEPTIDE_HEADER;
    }

    protected override string GetEngineName()
    {
      return "mascot";
    }

    #region IScanWriter Members

    public IIdentifiedSpectrumWriter ScanWriter { get; set; }

    #endregion

    protected override void WritePeptide(StreamWriter sw, IIdentifiedSpectrum mph)
    {
      base.WritePeptide(sw, mph);
      if (ScanWriter != null)
      {
        ScanWriter.WriteToStream(sw, mph);
      }
    }

    protected override void WritePeptideHeader(StreamWriter sw)
    {
      base.WritePeptideHeader(sw);
      if (ScanWriter != null)
      {
        sw.WriteLine(ScanWriter.Header);
      }
    }
  }
}